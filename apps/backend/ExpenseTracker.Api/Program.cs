
using ExpenseTracker.Application.MappingProfile;
using ExpenseTracker.Application.Service;
using ExpenseTracker.Application.Validator;
using ExpenseTracker.Infrastructure.Context;
using ExpenseTracker.Infrastructure.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(MappingProfile));

//  Add Fulent validator
builder.Services.AddValidatorsFromAssemblyContaining<ExpenseValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidator>();
builder.Services.AddFluentValidationAutoValidation();


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Expense Tracker API", Version = "v1" });
});

// Configure EF Core with SQL Server
builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Repositories and Services for Dependency Injection
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped(typeof(IShareRepository<>), typeof(ShareRepository<>));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Expense Tracker API V1");
  });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Apply migrations on startup (optional, but convenient)
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  var context = services.GetRequiredService<ExpenseTrackerDbContext>();
  context.Database.Migrate();
}

app.Run();
