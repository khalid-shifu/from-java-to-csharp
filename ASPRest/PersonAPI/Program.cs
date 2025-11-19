// This is like your Spring Boot @SpringBootApplication main class
// It configures and runs the web application

using Microsoft.EntityFrameworkCore;
using PersonAPI.Data;
using PersonAPI.Services;
using PersonAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ========== Add services to the container (Like @Configuration in Spring) ==========

// Add Controller support (Like @EnableWebMvc in Spring)
builder.Services.AddControllers();

// Add Database Context (Like Spring Boot auto-configuration for JPA)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));

// Register Service Layer (Like @Service in Spring Boot)
builder.Services.AddScoped<IPersonService, PersonService>();

// ========== Build the application ==========
var app = builder.Build();

// ========== Auto-create database and tables (Like spring.jpa.hibernate.ddl-auto=update) ==========
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}

// ========== Configure the HTTP request pipeline (Middleware) ==========

// Global Exception Handler (Like @ControllerAdvice in Spring Boot)
app.UseMiddleware<GlobalExceptionMiddleware>();

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Map controllers to routes (enables your @ApiController classes)
app.MapControllers();

// ========== Run the application ==========
app.Run();
