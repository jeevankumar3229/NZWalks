using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NZWalks_Api.Data;
using NZWalks_Api.Profiles;
using NZWalks_Api.Repositories;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//to add authorization to swaggerGen
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "NZ Walks API", Version = "v1" });
    //adding authorization header
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,//pass authorization as a header
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                },
                Scheme="Oauth2",
                Name=JwtBearerDefaults.AuthenticationScheme,
                In=ParameterLocation.Header
            },
            new List<string>()
        }

    }) ;
   
});

//when using two dbcontext class we get error ,change in each constructor dbcontextoptions to dbcontextoptions<dbcontextclassname>
//when running migrations we need to specift dbcontext class by ....... -Context "dbcontextclassname"
builder.Services.AddDbContext<NZWalksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks"));
});

builder.Services.AddDbContext<NZWalksAuthdbcontext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksAuth"));
});


//we also need to inject identity into services so add we can use it inside controllers
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
    .AddEntityFrameworkStores<NZWalksAuthdbcontext>()//store that identity uses
    .AddDefaultTokenProviders();


//set up identityoptions
//here we specify password length....
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 9;
    options.Password.RequiredUniqueChars = 1;


});

//Now we will inject this interface and the implementation into these services again using dependencyinjection.
builder.Services.AddScoped<IRegionRepository, RegionRepository>();//we will tell that Whenever I ask for the Iregionrepository interface, give me the implementation for the
                                                                  //region repository.
                                                                  //because we have injected this now into the services, we can use this region repository, implement implement
                                                                  //ation inside the controller.

//register IWalkRepository to services
builder.Services.AddScoped<IWalkRepository, WalkRepository>();

builder.Services.AddScoped<ITokenRepository, TokenRepository>();

builder.Services.AddScoped<IwalkDifficultyRepository, WalkDifficultyRepository>();

//We have to inject these profiles into our services.
builder.Services.AddAutoMapper(typeof(Program).Assembly);//And this takes an assembly so that it uses that assembly name to scan all the profiles using this profile class.
//or  builder.Services.AddAutoMapper(typeof(RegionProfiles));  add for each profile file          //so we specify the assembly there.



//Ihis way we are creating the, you know, adding authentication to our services and also adding the JWT bearer token along with the parameters that we want the token to be
//validated against.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new Microsoft.IdentityModel.
Tokens.TokenValidationParameters
{
    ValidateIssuer=true,
    ValidateAudience=true,
    ValidateLifetime=true,
    ValidateIssuerSigningKey=true,
    ValidIssuer = builder.Configuration["JWT:issuer"],
    ValidAudience= builder.Configuration["JWT:Audience"],
    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])) 

});

var app = builder.Build();

// Configure the HTTP request pipeline.
//Another important function of the Program.cs file is to configure the HTTP request pipeline.By using the request pipeline, we add middleware, which is a software
//that assemble build into an application pipeline to handle requests and responses.
//These are the middlewares that have been configured in our application by default.These are the https redirection,useauthorization and mapping controllers as well.
//Finally, when we have configured the middleware, we then run our application.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//And there's one final thing left in here, which is to add authentication into the middleware pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
