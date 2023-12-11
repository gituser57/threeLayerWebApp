using AutoMapper;
using DAL.Interfaces;
using DAL.Repositories;
using DAL;
using Business;
using Business.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Business.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<PizzaDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var mapper = new MapperConfiguration(c => c.AddProfile(new AutomapperProfile())).CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddTransient<IManufacturerService, ManufacturerService>();
builder.Services.AddTransient<IPizzaService, PizzaService>();

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
