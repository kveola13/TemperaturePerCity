public class CityDTOControllerTests
{
    private readonly DbContextOptions<AppDbContext> _dbContextOptions;

    public CityDTOControllerTests()
    {
        // Set up an in-memory database for testing.
        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
    }

    [Fact]
    public async Task GetCityDTO_ReturnsCityDTO_WhenCityDTOExists()
    {
        // Arrange
        using (var context = new AppDbContext(_dbContextOptions))
        {
            var cityDTO = new CityDTO { Id = 1, Name = "New York City" };
            context.CityDTO.Add(cityDTO);
            await context.SaveChangesAsync();
        }

        using (var context = new AppDbContext(_dbContextOptions))
        {
            var controller = new CityDTOController(context);

            // Act
            var result = await controller.GetCityDTO(1);

            // Assert
            var objectResult = Assert.IsType<ActionResult<CityDTO>>(result);
            var cityDTO = Assert.IsType<CityDTO>(objectResult.Value);
            Assert.Equal("New York City", cityDTO.Name);
        }
    }

    [Fact]
    public async Task GetCityDTO_ReturnsNotFound_WhenCityDTONotFound()
    {
        // Arrange
        using (var context = new AppDbContext(_dbContextOptions))
        {
            var controller = new CityDTOController(context);

            // Act
            var result = await controller.GetCityDTO(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }

    [Fact]
    public async Task PostCityDTO_AddsCityDTOToDatabase()
    {
        // Arrange
        var cityDTO = new CityDTO { Name = "London" };
        using (var context = new AppDbContext(_dbContextOptions))
        {
            var controller = new CityDTOController(context);

            // Act
            var result = await controller.PostCityDTO(cityDTO);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<CityDTO>(createdAtActionResult.Value);
            Assert.Equal("London", returnValue.Name);
        }

        // Verify that the CityDTO was added to the database.
        using (var context = new AppDbContext(_dbContextOptions))
        {
            Assert.Equal(1, context.CityDTO.Count());
            Assert.Equal("London", context.CityDTO.Single().Name);
        }
    }

    [Fact]
    public async Task DeleteCityDTO_RemovesCityDTOFromDatabase()
    {
        // Arrange
        using (var context = new AppDbContext(_dbContextOptions))
        {
            var cityDTO = new CityDTO { Id = 1, Name = "Paris" };
            context.CityDTO.Add(cityDTO);
            await context.SaveChangesAsync();

            var controller = new CityDTOController(context);

            // Act
            var result = await controller.DeleteCityDTO(1);

            // Assert
            Assert.IsType<NoContentResult>(result);

            // Verify that the CityDTO was removed from the database.
            Assert.Equal(0, context.CityDTO.Count());
        }
    }
}
