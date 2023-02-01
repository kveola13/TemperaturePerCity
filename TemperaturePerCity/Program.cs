using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using TemperaturePerCity.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
//builder.Services.AddDbContext<CityDb>(opt => opt.UseInMemoryDatabase("CityList"));
//var test = "AccountEndpoint=https://west-test-account.documents.azure.com:443/;AccountKey=NIBQJWnO6FCXlNp7Eidku2cZABQmBJAZNm32OIRwHJRcTmWujKWuAUyr4gzzJYVq94bIGW3u0wQeACDbn0z0rg==";
var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
var databaseName = "ToDoList";
builder.Services.AddDbContext<CityDb>(optionsAction => optionsAction.UseCosmos(connectionString: connectionString!, databaseName));
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
