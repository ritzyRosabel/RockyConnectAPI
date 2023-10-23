using System;
using Microsoft.Data.SqlClient;
using RockyConnectBackend.Model;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RockyConnectBackend.Data
{
    public class TripData
    {
        internal static string CreateTripData( Trip trip)
        {
            //  var result = new User   ();
            string result = "01";

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "rosabeldbserver.database.windows.net";
            builder.UserID = "rosabelDB";
            builder.Password = "Mololuwa@14";
            builder.InitialCatalog = "RockyConnectDB";

            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");
            int ret = 6;
            connection.Open();
            try
            {
                DateTime date = DateTime.Now;
                DateTime defaultVerified = new DateTime(1900, 01, 01);
                SqlCommand cmd = new SqlCommand("ScheduleTrip", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", trip.ID); ;
                cmd.Parameters.AddWithValue("@TripDistance", trip.TripDistance); ;
                cmd.Parameters.AddWithValue("@TripInitiator", trip.TripInitiator);
                cmd.Parameters.AddWithValue("@SourceLocation", trip.SourceLocation);
                cmd.Parameters.AddWithValue("@TripCost", trip.TripCost);
                cmd.Parameters.AddWithValue("@TripStatus", trip.TripStatus);
                cmd.Parameters.AddWithValue("@CustomerEmail", trip.CustomerEmail);
                cmd.Parameters.AddWithValue("@DriverEmail", trip.DriverEmail);
                cmd.Parameters.AddWithValue("@PaymentID", trip.PaymentID);
                cmd.Parameters.AddWithValue("@SourceLatitude", trip.SourceLatitude);
                cmd.Parameters.AddWithValue("@SourceLongitude", trip.SourceLongitude);
                cmd.Parameters.AddWithValue("@DestinationLat", trip.DestinationLat);
                cmd.Parameters.AddWithValue("@DestinationLong", trip.DestinationLong);
                cmd.Parameters.AddWithValue("@Destination", trip.Destination);
                cmd.Parameters.AddWithValue("@TripDate", trip.TripDate);
                cmd.Parameters.AddWithValue("@DateCreated", date);
                cmd.Parameters.AddWithValue("@DateUpdated", date);

                ret = cmd.ExecuteNonQuery();
                if (ret == -1)
                {
                    result = "00";
                }



            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();

                }
            }

            //Console.WriteLine("\nDone. Press enter.");
            //Console.ReadLine();



            return result;
        }
        internal static string UpdateTripData(string pay,Trip trip)
        {
            //  var result = new User   ();
            string result = "01";

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "rosabeldbserver.database.windows.net";
            builder.UserID = "rosabelDB";
            builder.Password = "Mololuwa@14";
            builder.InitialCatalog = "RockyConnectDB";

            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");
            int ret = 6;
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("UpdateASchedule", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", trip.ID); 
                cmd.Parameters.AddWithValue("@DriverEmail", trip.DriverEmail);
                cmd.Parameters.AddWithValue("@CustomerEmail", trip.CustomerEmail);
                cmd.Parameters.AddWithValue("@TripStatus", trip.TripStatus);
                cmd.Parameters.AddWithValue("@PaymentID", pay);
                cmd.Parameters.AddWithValue("@DateUpdated", trip.Date_Updated);

                ret = cmd.ExecuteNonQuery();

                if (ret == -1)
                {
                    result = "00";
                }



            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();

                }
            }

            return result;
        }
        internal static string DeleteTripData(string trip)
        {
            //  var result = new User   ();
            string result = "01";

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "rosabeldbserver.database.windows.net";
            builder.UserID = "rosabelDB";
            builder.Password = "Mololuwa@14";
            builder.InitialCatalog = "RockyConnectDB";

            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");
            int ret = 6;
            connection.Open();
            try
            {
                DateTime date = DateTime.Now;
                DateTime defaultVerified = new DateTime(1900, 01, 01);
                SqlCommand cmd = new SqlCommand($"DeleteATrip", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", trip);


                ret = cmd.ExecuteNonQuery();
                if (ret == -1)
                {
                    result = "00";
                }



            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();

                }
            }


            //Console.WriteLine("\nDone. Press enter.");
            //Console.ReadLine();



            return result;
        }
        internal static Trip SelectTripData(string ID )
        {
            //  var result = new User   ();
            var result = new Trip();

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "rosabeldbserver.database.windows.net";
            builder.UserID = "rosabelDB";
            builder.Password = "Mololuwa@14";
            builder.InitialCatalog = "RockyConnectDB";

            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");
            connection.Open();
            try
            {


                using (SqlCommand cmd = new SqlCommand("SelectATrip", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            //  result.UserID = int.Parse(reader["UserID"]);
                            result.ID = reader["ID"].ToString().Trim();
                            result.CustomerEmail = reader["CustomerEmail"].ToString().Trim();
                            result.DriverEmail = reader["DriverEmail"].ToString().Trim();
                            result.TripInitiator = reader["TripInitiator"].ToString().Trim();
                            result.TripStatus = reader["TripStatus"].ToString().Trim();
                            result.TripDistance = Convert.ToInt32(reader["TripDistance"]);
                            result.TripCost = Convert.ToInt32(reader["TripCost"]);
                            result.SourceLocation = reader["SourceLocation"].ToString().Trim();
                            result.SourceLatitude = reader["SourceLatitude"].ToString().Trim();
                            result.SourceLongitude = reader["SourceLongitude"].ToString().Trim();
                            result.DestinationLat = reader["DestinationLat"].ToString().Trim();
                            result.DestinationLong = reader["DestinationLong"].ToString().Trim();
                            result.Destination = reader["Destination"].ToString().Trim();
                            result.TripDate = Convert.ToDateTime(reader.GetDateTime("TripDate"));
                            result.Date_Created = Convert.ToDateTime(reader.GetDateTime("DateCreated"));
                            result.Date_Updated = Convert.ToDateTime(reader.GetDateTime("DateUpdated"));
                            result.PaymentID = reader["PaymentID"].ToString().Trim();

                        }

                    }
                }


            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();

                }
            }
            return result;


        }

        internal static List<Trip> SelectEmailTrips(string email)
        {
            var result = new Trip();
            var res = new List<Trip>();

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "rosabeldbserver.database.windows.net";
            builder.UserID = "rosabelDB";
            builder.Password = "Mololuwa@14";
            builder.InitialCatalog = "RockyConnectDB";

            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");
            connection.Open();
            try
            {
                DateTime date = DateTime.Now;
                DateTime defaultVerified = new DateTime(1900, 01, 01);
                using (SqlCommand cmd = new SqlCommand($"Select * from [dbo].[TripRequest] inner join [dbo].[AcceptedTrip] using(ID) Where  Email ={email} and Status='Completed'", connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            result.ID = reader["ID"].ToString().Trim();
                            result.CustomerEmail = reader["CustomerEmail"].ToString().Trim();
                            result.DriverEmail = reader["DriverEmail"].ToString().Trim();
                            result.TripInitiator = reader["TripInitiator"].ToString().Trim();
                            result.TripStatus = reader["TripStatus"].ToString().Trim();
                            result.TripDistance = Convert.ToInt32(reader["TripDistance"]);
                            result.TripCost = Convert.ToInt32(reader["TripType"]);
                            result.SourceLocation = reader["SourceLocation"].ToString().Trim();
                            result.SourceLatitude = reader["SourceLatitude"].ToString().Trim();
                            result.SourceLongitude = reader["SourceLongitude"].ToString().Trim();
                            result.DestinationLat = reader["DestinationLat"].ToString().Trim();
                            result.DestinationLong = reader["DestinationLong"].ToString().Trim();
                            result.Destination = reader["Destination"].ToString().Trim();
                            result.TripDate = Convert.ToDateTime(reader.GetDateTime("TripDate"));
                            result.Date_Created = Convert.ToDateTime(reader.GetDateTime("DateCreated"));
                            result.Date_Updated = Convert.ToDateTime(reader.GetDateTime("DateUpdated"));
                            result.PaymentID = reader["PaymentID"].ToString().Trim();
                            res.Add(result);

                        }

                    }
                }


            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();

                }
            }
            return res;

        }

        internal static List<Trip> SelectDriverTripList(TripSearch trip)
        {
            var result = new Trip();
            var res = new List<Trip>();

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "rosabeldbserver.database.windows.net";
            builder.UserID = "rosabelDB";
            builder.Password = "Mololuwa@14";
            builder.InitialCatalog = "RockyConnectDB";

            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");
            connection.Open();
            try
            {


                using (SqlCommand cmd = new SqlCommand("SearchDriverTripList", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DestinationLat", trip.DestinationLat);
                    cmd.Parameters.AddWithValue("@DestinationLong", trip.DestinationLong);
                    cmd.Parameters.AddWithValue("@Destination", trip.Destination);
                    cmd.Parameters.AddWithValue("@TripDate", trip.TripDate);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            //  result.UserID = int.Parse(reader["UserID"]);
                            result.ID = reader["ID"].ToString().Trim();
                            result.CustomerEmail = reader["CustomerEmail"].ToString().Trim();
                            result.DriverEmail = reader["DriverEmail"].ToString().Trim();
                            result.TripInitiator = reader["TripInitiator"].ToString().Trim();
                            result.TripStatus = reader["TripStatus"].ToString().Trim();
                            result.TripDistance = Convert.ToInt32(reader["TripDistance"]);
                            result.TripCost = Convert.ToInt32(reader["TripCost"]);
                            result.SourceLocation = reader["SourceLocation"].ToString().Trim();
                            result.SourceLatitude = reader["SourceLatitude"].ToString().Trim();
                            result.SourceLongitude = reader["SourceLongitude"].ToString().Trim();
                            result.DestinationLat = reader["DestinationLat"].ToString().Trim();
                            result.DestinationLong = reader["DestinationLong"].ToString().Trim();
                            result.Destination = reader["Destination"].ToString().Trim();
                            result.TripDate = Convert.ToDateTime(reader.GetDateTime("TripDate"));
                            result.Date_Created = Convert.ToDateTime(reader.GetDateTime("DateCreated"));
                            result.Date_Updated = Convert.ToDateTime(reader.GetDateTime("DateUpdated"));
                            result.PaymentID = reader["PaymentID"].ToString().Trim();
                            res.Add(result);

                        }

                    }
                }


            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();

                }
            }
            return res;
        }

        internal static List<Trip> SelectRiderTripList(TripSearch trip)
        {

            var result = new Trip();
            var res = new List<Trip>();

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "rosabeldbserver.database.windows.net";
            builder.UserID = "rosabelDB";
            builder.Password = "Mololuwa@14";
            builder.InitialCatalog = "RockyConnectDB";

            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");
            connection.Open();
            try
            {


                using (SqlCommand cmd = new SqlCommand("SearchRiderTripList", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DestinationLat", trip.DestinationLat);
                    cmd.Parameters.AddWithValue("@DestinationLong", trip.DestinationLong);
                    cmd.Parameters.AddWithValue("@Destination", trip.Destination);
                    cmd.Parameters.AddWithValue("@TripDate", trip.TripDate);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            //  result.UserID = int.Parse(reader["UserID"]);
                            result.ID = reader["ID"].ToString().Trim();
                            result.CustomerEmail = reader["CustomerEmail"].ToString().Trim();
                            result.DriverEmail = reader["DriverEmail"].ToString().Trim();
                            result.TripInitiator = reader["TripInitiator"].ToString().Trim();
                            result.TripStatus = reader["TripStatus"].ToString().Trim();
                            result.TripDistance = Convert.ToInt32(reader["TripDistance"]);
                            result.TripCost = Convert.ToInt32(reader["TripCost"]);
                            result.SourceLocation = reader["SourceLocation"].ToString().Trim();
                            result.SourceLatitude = reader["SourceLatitude"].ToString().Trim();
                            result.SourceLongitude = reader["SourceLongitude"].ToString().Trim();
                            result.DestinationLat = reader["DestinationLat"].ToString().Trim();
                            result.DestinationLong = reader["DestinationLong"].ToString().Trim();
                            result.Destination = reader["Destination"].ToString().Trim();
                            result.TripDate = Convert.ToDateTime(reader.GetDateTime("TripDate"));
                            result.Date_Created = Convert.ToDateTime(reader.GetDateTime("DateCreated"));
                            result.Date_Updated = Convert.ToDateTime(reader.GetDateTime("DateUpdated"));
                            result.PaymentID = reader["PaymentID"].ToString().Trim();
                            res.Add(result);

                        }

                    }
                }


            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();

                }
            }
            return res;
        }
    }
}

