using ProjectTest.Business;
using ProjectTest.Business.Implementations;
using ProjectTest.Repository;
using ProjectTest.Repository.Implementations;
using Microsoft.EntityFrameworkCore;
using ProjectTest.Model.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Connect to a database
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SqlContext>(options =>
    options.UseSqlServer(connection));

//Version Api
builder.Services.AddApiVersioning();

//Dependecy Injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
