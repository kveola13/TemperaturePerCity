using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.Client.AccountManagement;
using System.Xml.Linq;
using TemperaturePerCity.Controllers;
using TemperaturePerCity.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
//builder.Services.AddDbContext<CityDb>(opt => opt.UseInMemoryDatabase("CityList"));
var connectionString = builder.Configuration["ConnectionStrings:ConnectionString"];
var databaseName = "ToDoList";
var CosmosClient = new CosmosClient(connectionString,
    new CosmosClientOptions() { });
builder.Services.AddDbContext<CityDb>(optionsAction => optionsAction.UseCosmos(connectionString!, databaseName));


builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
