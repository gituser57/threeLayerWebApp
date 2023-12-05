// See https://aka.ms/new-console-template for more information
using DAL;
using DAL.Entities;
using DAL.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

Console.WriteLine("Hello, World!");

PizzaDbContext db = new PizzaDbContext();

UnitOfWork uof = new UnitOfWork(db, new ManufacturerRepository(db), new PizzaRepository(db));

var a = uof.ManufacturerRepository.FindAll();
var b = uof.PizzaRepository.FindAll().ToList<Pizza>();

var c = uof.ManufacturerRepository.GetAllWithDetails;
var d = uof.PizzaRepository.GetAllWithDetails();

//var a1 = uof.ManufacturerRepository.FindAll().Select(x => x.Pizzas.Count() == 2).Count();


int f = a.Count() + b.Count();
Console.WriteLine(d);


