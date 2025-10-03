using EvolveDb;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using ProjectTest.Business;
using ProjectTest.Business.Implementations;
using ProjectTest.Model.Context;
using ProjectTest.Repository;
using ProjectTest.Repository.Generic;
using ProjectTest.Repository.Implementations;
using Serilog;

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

builder.Services.AddMvc(options => {
    options.RespectBrowserAcceptHeader = true;


    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
})
.AddXmlSerializerFormatters();

//Version Api
builder.Services.AddApiVersioning();

//Dependecy Injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();

//builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));


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