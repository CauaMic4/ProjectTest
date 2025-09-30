using ProjectTest.Business;
using ProjectTest.Business.Implementations;
using ProjectTest.Repository;
using ProjectTest.Repository.Implementations;
using Microsoft.EntityFrameworkCore;
using ProjectTest.Model.Context;
using Serilog;
using EvolveDb;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

// Connect to a database
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SqlContext>(options =>
    options.UseSqlServer(connection));

if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connection);
}

//Version Api
builder.Services.AddApiVersioning();

//Dependecy Injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();

builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();
builder.Services.AddScoped<IBookRepository, BookRepositoryImplementation>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void MigrateDatabase(string connection)
{
    try
    {
        var evolveConnection = new SqlConnection(connection);
        var envolve = new Evolve(evolveConnection, Log.Information)
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true,
        };

        envolve.Migrate();
    }
    catch (Exception e)
    {
        Log.Error("An error occurred while migrating the database.", e);
        throw;
    }
}