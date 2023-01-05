using Microsoft.EntityFrameworkCore;
using TemperaturePerCity.Controllers;
using TemperaturePerCity.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddDbContext<CityDb>(opt => opt.UseInMemoryDatabase("CityList"));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
