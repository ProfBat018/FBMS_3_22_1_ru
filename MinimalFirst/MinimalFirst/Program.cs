using Microsoft.EntityFrameworkCore;
using MinimalFirst.Data.Contexts;
using MinimalFirst.Data.Models;

var builder = WebApplication.CreateBuilder(args);

foreach (var item in args)
{
    Console.WriteLine(item);
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShowroomDbContext>(ops => ops.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/getcars", async (ShowroomDbContext db) =>
{
    var cars = await db.Cars.ToListAsync();
    return Results.Ok(cars);
})
.WithOpenApi()
.WithName("GetCars")
.WithDescription("Get all cars from database");


app.MapGet("/getcar/{id}", async (ShowroomDbContext db, int id) =>
{
    var car = await db.Cars.FindAsync(id);
    return car is not null ? Results.Ok(car) : Results.NotFound();
})
.WithOpenApi()
.WithName("GetCar")
.WithDescription("Get car from database by id");


app.MapPost("/addcar", async (ShowroomDbContext db, Car car) =>
{
    await db.Cars.AddAsync(car);
    await db.SaveChangesAsync();
    return Results.Created($"/getcar/{car.CarId}", car);
})
.WithOpenApi()
.WithName("AddCar")
.WithDescription("Add a new car to database");

app.Run();

