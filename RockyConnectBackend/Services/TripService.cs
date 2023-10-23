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

            if (tripD.TripInitiator == "Driver")
            {
                tripD.CustomerEmail = null;
            }else
            {
                tripD.DriverEmail = null;
            }
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
        internal static Response GetDriverTrips(TripSearch trip)
        {
            var status = new Response();
            List<Trip> result = TripData.SelectDriverTripList(trip);

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
        internal static Response GetRiderTrips(TripSearch trip)
        {
            var status = new Response();
            List<Trip> result = TripData.SelectRiderTripList(trip);

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
        internal static Response GetTrip(string id)
        {
            var status = new Response();
            Trip result = TripData.SelectTripData(id);
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


        internal static Response ApproveRiderTrip(TripDataInfo trip)
        {
            Trip trip1 = TripData.SelectTripData(trip.ID);
            var status = new Response();
            
            if (trip1.TripStatus!= "Approved")
            {

                trip1.CustomerEmail = trip.CustomerEmail;
                trip1.TripStatus = "Approved";

            }
            else
            {

                status.statusCode = "01";
                status.status = "trip already approved";
                return status;
            }
            if(trip1.TripInitiator == "Rider")
            {
                trip1.DriverEmail = trip.DriverEmail;

            }
            trip1.TripStatus = "Approved";

            trip1.Date_Updated = DateTime.Now;
           
            string result = TripData.UpdateTripData(trip1.PaymentID,trip1);
            if (result == "00")
            {
               
                    status.statusCode = "00";
                    status.status = "Successfully saved";
                status.data = trip1;
            }
            else
            {

                status.statusCode = "01";
                status.status = "Record not found";
            }
            return status;
        }
        internal static Response DeclineRiderTrip(TripDataInfo trip)
        {
            var status = new Response();

            Trip trip1 = TripData.SelectTripData(trip.ID);
            if (trip1.TripInitiator == "Driver")
            {
                trip1.CustomerEmail = null;
                trip1.TripStatus = "Created";

            }
            if (trip1.TripInitiator == "Rider")
            {
                status.statusCode = "01";
                status.status = "Rider initiated trip cannot decline request";
                status.data = trip;
                return status;

            }

            trip1.Date_Updated = DateTime.Now;

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

        internal static Response DriverRequestTrip(TripDataInfo trip)
        {
            Trip trip1 = TripData.SelectTripData(trip.ID);
            var status = new Response();

            if (trip1.TripInitiator == "Driver")
            {
                trip1.CustomerEmail = trip.CustomerEmail;
                trip1.TripStatus = "Requested";

            }
            else
            {
                status.statusCode = "01";
                status.status = "Rider initiated trip cannot request driver";
                status.data = trip;
                return status;
            }
           

            trip1.Date_Updated = DateTime.Now;

            string result = TripData.UpdateTripData(trip1.PaymentID, trip1);
            if (result == "00")
            {

                status.statusCode = "00";
                status.status = "Successfully saved";
                status.data = trip1;
            }
            else
            {

                status.statusCode = "01";
                status.status = "Record not found";
            }
            return status;
        }

        internal static Response CompletedTrip(string email)
        {

            var status = new Response();

            List<Trip> result = TripData.CompletedTrip(email);
            if (result.Count >0)
            {

                status.statusCode = "00";
                status.status = "Successfully saved";
                status.data = result;
            }
            else
            {

                status.statusCode = "01";
                status.status = "Record not found";
            }
            return status;
        }

        internal static Response UpcomingTrips(string email)
        {

            var status = new Response();

            List<Trip> result = TripData.UpcomingTrips(email);
            if (result.Count > 0)
            {

                status.statusCode = "00";
                status.status = "Successfully saved";
                status.data = result;
            }
            else
            {

                status.statusCode = "01";
                status.status = "Record not found";
            }
            return status;
        }

        internal static Response AwaitingApproval(string email)
        {
            var status = new Response();

            List<Trip> result = TripData.AwaitingApproval(email);
            if (result.Count > 0)
            {

                status.statusCode = "00";
                status.status = "Successfully saved";
                status.data = result;
            }
            else
            {

                status.statusCode = "01";
                status.status = "Record not found";
            }
            return status;
        }

        internal static Response ApprovedTrips(string email)
        {
            var status = new Response();

            List<Trip> result = TripData.ApprovalList(email);
            if (result.Count > 0)
            {

                status.statusCode = "00";
                status.status = "Successfully saved";
                status.data = result;
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


