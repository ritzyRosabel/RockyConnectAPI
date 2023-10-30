﻿using System;
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
    public class CancelRequest
    {
        public string? ID { get; set; }
        public string? ReasonForCancel { get; set; }

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
        public string? Destination { get; set; }
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
        public string? CancelReason { get; set; }

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
        public int TripDistance { get; set; }
        public double TotalTime { get; set; }
        public string? TripInitiator { get; set; }
        public DateTime TripDate { get; set; }

    }
}

