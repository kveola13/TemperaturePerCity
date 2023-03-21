public class CityValidatorTests
{
    [Fact]
    public void ValidateCity_ShouldReturnTrue_WhenCityIsValid()
    {
        // Arrange
        var city = new CityDTO
        {
            CityName = "Paris",
            Continent = "Europe",
            Country = "France"
        };
        var validator = new CityValidator();

        // Act
        var result = validator.ValidateCity(city);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateCity_ShouldReturnFalse_WhenCityNameIsNull()
    {
        // Arrange
        var city = new CityDTO
        {
            CityName = null,
            Continent = "Europe",
            Country = "France"
        };
        var validator = new CityValidator();

        // Act
        var result = validator.ValidateCity(city);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateCity_ShouldReturnFalse_WhenContinentIsNull()
    {
        // Arrange
        var city = new CityDTO
        {
            CityName = "Paris",
            Continent = null,
            Country = "France"
        };
        var validator = new CityValidator();

        // Act
        var result = validator.ValidateCity(city);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateCity_ShouldReturnFalse_WhenCountryIsNull()
    {
        // Arrange
        var city = new CityDTO
        {
            CityName = "Paris",
            Continent = "Europe",
            Country = null
        };
        var validator = new CityValidator();

        // Act
        var result = validator.ValidateCity(city);

        // Assert
        Assert.False(result);
    }
}
