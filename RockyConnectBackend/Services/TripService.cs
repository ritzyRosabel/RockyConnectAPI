using System;
using RockyConnectBackend.Data;
using RockyConnectBackend.Model;

namespace RockyConnectBackend.Services
{
	public class TripService
	{
        internal static Response CreateTrip(TripDataInfo trip)
        {
            var status = new Response();
            string result = TripData.CreateTripData(trip);
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

        internal static Response GetTripHistory(TripRequest trip)
        {
            var status = new Response();
            List<TripDataInfo> result = TripData.SelectEmailTrips(trip.Email);

            if (result.Count >= 1)
            {
                status.statusCode = "00";
                status.status = "Successfull";
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
            TripDataInfo result = TripData.SelectTripData(trip);
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


