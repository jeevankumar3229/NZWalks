using Microsoft.EntityFrameworkCore;
using NZWalks_Api.Data;
using NZWalks_Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NZWalksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks"));
});
//Now we will inject this interface and the implementation into these services again using dependencyinjection.
builder.Services.AddScoped<IRegionRepository, RegionRepository>();//we will tell that Whenever I ask for the Iregionrepository interface, give me the implementation for the
                                                                  //region repository.
                                                                  //because we have injected this now into the services, we can use this region repository, implement implement
                                                                  //ation inside the controller.

//register IWalkRepository to services
builder.Services.AddScoped<IWalkRepository, WalkRepository>();

//We have to inject these profiles into our services.
builder.Services.AddAutoMapper(typeof(Program).Assembly);//And this takes an assembly so that it uses that assembly name to scan all the profiles using this profile class.
                                                         //so we specify the assembly there.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
