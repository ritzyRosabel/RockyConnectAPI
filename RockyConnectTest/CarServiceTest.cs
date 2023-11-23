using RockyConnectBackend.Services;
using RockyConnectBackend.Model;
namespace RockyConnectTest;

public class CarServiceTest
{
    [Fact]
    public void Test1()
    {
        //arrange
        string expected = "00";
        //Act
        CarController cars = new CarController();
        //CarService car = new CarService();
        CarRequest carRequest = new CarRequest() { CarColor = "White", DriverLiscense = "O028456478fyfh8388", CarMake = "Kia", CarModel = "Soul", Email = "or-olugbenga@wiu.edu", PlateNumber = "TX128345", TypeOfVehicle = "SUV", CarPreferences = "No Pet, Music, 4Person" };
        Response status = new Response();
        status = CarService.CreateCard(carRequest);
        //Assert
        Assert.Equal(expected, status.statusCode);

    }
    [Fact]
    public void Test2()
    {
        //arrange
        string expected = "00";
        //Act
        CarController cars = new CarController();
        //CarService car = new CarService();
        CarRequest carRequest = new CarRequest() { CarColor = "White", DriverLiscense = "O028456478fyfh8388", CarMake = "Kia", CarModel = "Soul", Email = "or-olugbenga@wiu.edu", PlateNumber = "TX128345", TypeOfVehicle = "SUV", CarPreferences = "No Pet, Music, 4Person" };
        Response status = new Response();
        status = CarService.CreateCard(carRequest);
        //Assert
        Assert.Equal(expected, status.statusCode);

    }
}
