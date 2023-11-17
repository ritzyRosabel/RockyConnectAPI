using System;
namespace RockyConnectBackend.Model
{
    public class Trip
    {
        public string? ID { get; set; }
        public string? CustomerEmail { get; set; }
        public string? DriverEmail { get; set; }
        public string? Destination { get; set; }
        public string? SourceLocation { get; set; }
        public string? SourceLongitude { get; set; }
        public string? SourceLatitude { get; set; }
        public string? DestinationLong { get; set; }
        public string? DestinationLat { get; set; }
        public string? DestinationState { get; set; }
        public int TripDistance { get; set; }
        public int TripCost { get; set; }
        public string? TripInitiator { get; set; }
        public string? PaymentID { get; set; }
        public string? TripStatus { get; set; }
        public string? CancelReason { get; set; }
        public double TotalTime { get; set; }
        public DateTime TripDate { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }
    }
    public class SuperTrip
    {
        public string? ID { get; set; }
        public string? CustomerEmail { get; set; }
        public string? DriverEmail { get; set; }
        public string? Destination { get; set; }
        public string? SourceLocation { get; set; }
        public string? SourceLongitude { get; set; }
        public string? SourceLatitude { get; set; }
        public string? DestinationLong { get; set; }
        public string? DestinationLat { get; set; }
        public string? DestinationState { get; set; }
        public int? TripDistance { get; set; }
        public int? TripCost { get; set; }
        public string? TripInitiator { get; set; }
        public string? PaymentID { get; set; }
        public string? TripStatus { get; set; }
        public string? CancelReason { get; set; }
        public double TotalTime { get; set; }
        public DateTime TripDate { get; set; }
        public string? DriverFirstName { get; set; }
        public string? DriverLastName { get; set; }
        public string? DriverPhoneNumber { get; set; }
        public string? RiderFirstName { get; set; }
        public string? RiderLastName { get; set; }
        public string? RiderPhoneNumber { get; set; }
        public int? Rating{ get; set; }
        public int? NoOfRides { get; set; }
        public string? CarMake { get; set; }
        public string? CarModel { get; set; }
        public string? CarColor { get; set; }
        public string? PlateNumber { get; set; }
        public string? TypeOfVehicle { get; set; }
        public string? DriverLiscense { get; set; }
        public string? CarPreferences { get; set; }
        public string? DeviceID { get; set; }

    }
    public class CancelRequest
    {
        public string? ID { get; set; }
        public string? ReasonForCancel { get; set; }

    }

    public class UpdateTripRequest
    {
        public string? ID { get; set; }
        public string? CustomerEmail { get; set; }
        public string? DriverEmail { get; set; }
        public string? SourceLocation { get; set; }
        public string? SourceLongitude { get; set; }
        public string? SourceLatitude { get; set; }
        public string? Destination { get; set; }
        public string? TripInitiator { get; set; }
        public string? DestinationLong { get; set; }
        public string? DestinationLat { get; set; }
        public string? DestinationState { get; set; }
        public int TripDistance { get; set; }
        public double TotalTime { get; set; }
        public DateTime TripDate { get; set; }
    }
    public class TripRequest
    {
        public string? ID { get; set; }

    }
    public class RateRequest
    {
        public string? Email { get; set; }
        public int Rate { get; set; }

    }
    public class TripSearch
    { 
        public string? DestinationState { get; set; }
        public string? DestinationLong { get; set; }
        public string? DestinationLat { get; set; }
        public string? TripInitiator { get; set; }
        public DateTime TripDate { get; set; }
    }
    public class TripDataInfo
    {

        public string? ID { get; set; }
        public string? CustomerEmail { get; set; }
        public string? DriverEmail { get; set; }

    }
    public class CreateTripRequest
    {
        public string? CustomerEmail { get; set; }
        public string? DriverEmail { get; set; }
        public string? SourceLocation { get; set; }
        public string? SourceLongitude { get; set; }
        public string? SourceLatitude { get; set; }
        public string? Destination { get; set; }
        public string? DestinationLong { get; set; }
        public string? DestinationLat { get; set; }
        public string? DestinationState { get; set; }
        public int TripDistance { get; set; }
        public double TotalTime { get; set; }
        public string? TripInitiator { get; set; }
        public DateTime TripDate { get; set; }


    }
}

