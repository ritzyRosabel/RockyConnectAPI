using RockyConnectBackend.Controllers;
using RockyConnectBackend.Services;
using RockyConnectBackend.Model;
namespace RockyConnectTest;

public class TripServiceTest
{
    [Fact]
    public void Test1()
    {
        //arrange
        string expected = "00";
        //Act
        //CarService car = new CarService();
        CreateTripRequest carRequest = new CreateTripRequest() { CustomerEmail = "mipoolugbenga@gmail.com", DriverEmail = null, SourceLatitude = "4", SourceLocation = "Sherman Hall", Destination = "Morgan Hall", DestinationLat = "2", SourceLongitude = "3", DestinationLong = "5", DestinationState = "7", TotalTime = 4.03, TripDate = DateTime.Now, TripDistance = 9, TripInitiator = "Driver" };
        Response status = new Response();
        status = TripService.CreateTrip(carRequest);
        //Assert
        Assert.Equal(expected, status.statusCode);

    }
    [Fact]
    public void Test2()
    {
        //arrange
        string expected = "00";
        //Act
        //CarService car = new CarService();
        CreateTripRequest carRequest = new CreateTripRequest() { CustomerEmail = "mipoolugbenga@gmail.com", DriverEmail = null, SourceLatitude = "4", SourceLocation = "Sherman Hall", Destination = "Morgan Hall", DestinationLat = "2", SourceLongitude = "3", DestinationLong = "5", DestinationState = "7", TotalTime = 4.03, TripDate = DateTime.Now, TripDistance = 9, TripInitiator = "Driver" };
        Response status = new Response();
        status = TripService.CreateTrip(carRequest);
        //Assert
        Assert.Equal(expected, status.statusCode);

    }
}
