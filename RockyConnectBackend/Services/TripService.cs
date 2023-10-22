using System;
using RockyConnectBackend.Data;
using RockyConnectBackend.Model;

namespace RockyConnectBackend.Services
{
	public class TripService
	{
        internal static Response CreateTrip(CreateTripRequest trip)
        {
            Trip tripD = new Trip()
            {
                CustomerEmail = trip.CustomerEmail,
                SourceLatitude = trip.SourceLatitude,
                SourceLocation = trip.SourceLocation,
                Destination = trip.Destination,
                DriverEmail = trip.DriverEmail,
                DestinationLat = trip.DestinationLat,
                DestinationLong = trip.DestinationLong,
                TripStatus = "Created",
                ID = UtilityService.UniqueIDGenerator(),
                TripCost = trip.TripDistance * 10,
                TripInitiator=trip.TripInitiator,
                Date_Created=DateTime.Now,
                Date_Updated = DateTime.Now,
                TripDate=trip.TripDate,
                TripDistance = trip.TripDistance,
                SourceLongitude=trip.SourceLongitude,
                PaymentID=string.Empty
            };
            var status = new Response();

            string result = TripData.CreateTripData(tripD);
            if (result == "00")
            {
                status.statusCode = "00";
                status.status = "Successfull";
                status.data = tripD;
            }
            else
            {

                status.statusCode = "01";
                status.status = "UnSuccessfull";
            }
            return status;
        }

        internal static Response GetTripHistory(TripsRequest trip)
        {
            var status = new Response();
            List<Trip> result = TripData.SelectEmailTrips(trip.Email);

            if (result.Count >= 1)
            {
                status.statusCode = "00";
                status.status = "Successfull";
                status.data = result;
            }
            else
            {

                status.statusCode = "01";
                status.status = "Record not found";
            }
            return status;
        }
        internal static Response GetTrip(TripRequest trip)
        {
            var status = new Response();
            Trip result = TripData.SelectTripData(trip.ID);
            if (result.ID is not null)
            {
                status.statusCode = "00";
                status.status = "Successfull";
                status.data = result;
            }
            else
            {

                status.statusCode = "01";
                status.status = "Record not found";
            }
            return status;
        }


        internal static Response UpdateTrip(TripDataInfo trip)
        {
            var card = new TripDataInfo();
            Trip trip1 = TripData.SelectTripData(trip.ID);

            if (trip.CustomerEmail is not null)
            {
                trip1.CustomerEmail = trip.CustomerEmail;
                trip1.TripStatus = "Requested";

            }
            if(trip.DriverEmail is not null)
            {
                trip1.DriverEmail = trip.DriverEmail;
                trip1.TripStatus = "Approved";

            }
       
            trip1.Date_Updated = DateTime.Now;
           
            var status = new Response();
            string result = TripData.UpdateTripData(trip1.PaymentID,trip1);
            if (result == "00")
            {
               
                    status.statusCode = "00";
                    status.status = "Successfully saved";
              
            }
            else
            {

                status.statusCode = "01";
                status.status = "Record not found";
            }
            return status;
        }
        internal static Response DeleteTrip(TripRequest trip)
        {
            var status = new Response();
            string result =   TripData.DeleteTripData(trip.ID);
            if (result =="00")
            {
               
                    status.statusCode = "00";
                status.status = "Successfully deleted";
            }
            else
            {

                status.statusCode = "01";
                status.status = "Record not found";
            }
            return status;
        }

        internal static Response StartTrip(TripRequest trip)
        {

            Trip trip1 = TripData.SelectTripData(trip.ID);

                trip1.TripStatus = "Enroute";

            
            trip1.Date_Updated = DateTime.Now;

            var status = new Response();
            string result = TripData.UpdateTripData(trip1.PaymentID, trip1);
            if (result == "00")
            {

                status.statusCode = "00";
                status.status = "Successfully saved";

            }
            else
            {

                status.statusCode = "01";
                status.status = "Record not found";
            }
            return status;
        }

        internal static Response EndTrip(TripRequest trip)
        {
            Trip trip1 = TripData.SelectTripData(trip.ID);

            trip1.TripStatus = "Completed";


            trip1.Date_Updated = DateTime.Now;

            var status = new Response();
            string result = TripData.UpdateTripData(trip1.PaymentID, trip1);
            if (result == "00")
            {

                status.statusCode = "00";
                status.status = "Successfully saved";

            }
            else
            {

                status.statusCode = "01";
                status.status = "Record not found";
            }
            return status;
        }
    }
}


