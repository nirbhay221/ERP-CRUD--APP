using Microsoft.EntityFrameworkCore;
using Products.Core;
using Products.DB;  // Ensure this matches the namespace in your class library

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the AppDbContext with SQL Server configuration
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddTransient<IProductsServices, ProductsServices>();
builder.Services.AddSwaggerDocument(settings =>
{
    settings.Title = "Products";
});
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseSwaggerUi();
app.MapControllers();

app.Run();
