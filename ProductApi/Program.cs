/*using Microsoft.EntityFrameworkCore;
using ProductApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductDbContext>(opt =>
    opt.UseInMemoryDatabase("ProductDb"));

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

app.Run();*/


// to use in-memory data base
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductDbContext>(opt =>
    opt.UseInMemoryDatabase("ProductDb"));

var app = builder.Build();

// Seed the in-memory database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ProductDbContext>();
    SeedData(context);
}

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

void SeedData(ProductDbContext context)
{
    // Check if the database already has data
    if (!context.Products.Any())
    {
        // Add some initial products
        context.Products.AddRange(
            new Product { Id = 1, Name = "Laptop", Description = "Dell 2025 Model", Inventory = 10, Price = 250000 },
            new Product { Id = 2, Name = "Mouse", Description = "HP 225", Inventory = 20, Price = 60000 },
            new Product { Id = 3, Name = "Keyboard", Description = "HP Model", Inventory = 30, Price = 2500 }
        );

        context.SaveChanges();
    }
}