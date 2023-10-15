using System;
namespace RockyConnectBackend.Model
{
	public class Trip
	{
		public Trip()
		{
		}
	}
	public class TrioRequest { }
	public class TripDataInfo
	{

        public required string Email { get; set; }
        public required string Destination { get; set; }
        public required string SourceLocation { get; set; }
        public required string SourceLongitude { get; set; }
        public required string SourceLatitude { get; set; }
        public required string DestinationLong { get; set; }
        public required string DestinationLat { get; set; }
        public required int TripDistance { get; set; }
        public required int TripType { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }
    }

}

