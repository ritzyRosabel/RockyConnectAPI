using System;
namespace RockyConnectBackend.Model
{
	public class Trip
	{
        public required string ID { get; set; }

    }
    public class TrioRequest {

    }
    public class TripDataInfo
	{
        public string? ID { get; set; }
        public  string? Email { get; set; }
        public  string? Destination { get; set; }
        public  string? SourceLocation { get; set; }
        public  string? SourceLongitude { get; set; }
        public  string? SourceLatitude { get; set; }
        public  string? DestinationLong { get; set; }
        public  string? DestinationLat { get; set; }
        public  int TripDistance { get; set; }
        public  int TripType { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }
    }

}

