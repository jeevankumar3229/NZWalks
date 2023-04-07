using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NZAPI.Data;
using NZAPI.Repository;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File("Logging/log.txt").CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NZWalksDbsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks"));
});

builder.Services.AddScoped<IRegionRepository, RegionRepository>();

builder.Services.AddScoped<IWalkRepository, WalkRepository>();

builder.Services.AddScoped<IwalkDifficultyRepository, WalkDifficultyRepository>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

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
