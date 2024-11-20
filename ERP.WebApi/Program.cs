using Microsoft.EntityFrameworkCore;
using Products.Core;
using Products.DB;
using Projects.Core;
using Projects.DB;  // Ensure this matches the namespace in your class library

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
    settings.Title = "Products & Projects";
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("ProductsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddDbContext<ProjectDbContext>();
builder.Services.AddTransient<IProjectsServices, ProjectsServices>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ProjectsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ProductsPolicy");
app.UseCors("ProjectsPolicy");
app.UseAuthorization();
app.UseSwaggerUi();
app.MapControllers();

app.Run();
