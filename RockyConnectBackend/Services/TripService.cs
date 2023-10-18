using System;
using RockyConnectBackend.Data;
using RockyConnectBackend.Model;

namespace RockyConnectBackend.Services
{
	public class TripService
	{
        internal static Response CreateTrip(CreateTripRequest trip)
        {
            var status = new Response();
           string id = UtilityService.UniqueIDGenerator();

            string result = TripData.CreateTripData(id,trip);
            if (result == "00")
            {
                status.statusCode = "00";
                status.status = "Successfull";
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
            Trip result = TripData.SelectTripData(trip);
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

            var status = new Response();
            string result = TripData.UpdateTripData(trip);
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
    }
}


