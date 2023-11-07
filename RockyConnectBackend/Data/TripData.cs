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
                cmd.Parameters.AddWithValue("@CustomerEmail", trip.CustomerEmail?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DriverEmail", trip.DriverEmail?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PaymentID", (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CancelReason", (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SourceLatitude", trip.SourceLatitude);
                cmd.Parameters.AddWithValue("@SourceLongitude", trip.SourceLongitude);
                cmd.Parameters.AddWithValue("@DestinationLat", trip.DestinationLat);
                cmd.Parameters.AddWithValue("@DestinationLong", trip.DestinationLong);
                cmd.Parameters.AddWithValue("@Destination", trip.Destination);
                cmd.Parameters.AddWithValue("@DestinationState", trip.DestinationState);
                cmd.Parameters.AddWithValue("@TripDate", trip.TripDate);
                cmd.Parameters.AddWithValue("@TotalTime", trip.TotalTime);
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
                cmd.Parameters.AddWithValue("@CustomerEmail", trip.CustomerEmail ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TripStatus", trip.TripStatus);
                cmd.Parameters.AddWithValue("@PaymentID", pay ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CancelReason", trip.CancelReason ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DateUpdated", trip.Date_Updated);
                cmd.Parameters.AddWithValue("@TripDistance", trip.TripDistance); ;
                cmd.Parameters.AddWithValue("@TripInitiator", trip.TripInitiator);
                cmd.Parameters.AddWithValue("@SourceLocation", trip.SourceLocation);
                cmd.Parameters.AddWithValue("@TripCost", trip.TripCost);
                cmd.Parameters.AddWithValue("@DriverEmail", trip.DriverEmail ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SourceLatitude", trip.SourceLatitude);
                cmd.Parameters.AddWithValue("@SourceLongitude", trip.SourceLongitude);
                cmd.Parameters.AddWithValue("@DestinationLat", trip.DestinationLat);
                cmd.Parameters.AddWithValue("@DestinationLong", trip.DestinationLong);
                cmd.Parameters.AddWithValue("@Destination", trip.Destination);
                cmd.Parameters.AddWithValue("@DestinationState", trip.DestinationState ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TripDate", trip.TripDate);
                cmd.Parameters.AddWithValue("@TotalTime", trip.TotalTime);

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
        internal static SuperTrip SelectSuperTripData(string ID )
        {
            //  var result = new User   ();
            var result = new SuperTrip();

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


                using (SqlCommand cmd = new SqlCommand("SelectASuperTrip", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            result.ID = reader["ID"].ToString().Trim();
                            result.TripInitiator = reader["TripInitiator"].ToString().Trim();
                            result.TripStatus = reader["TripStatus"].ToString().Trim();
                            result.TripDistance = reader.IsDBNull("TripDistance") ? null : (int)reader["TripDistance"];
                            result.TripCost = reader.IsDBNull("TripCost") ? null : (int)reader["TripCost"];
                            result.SourceLocation = reader["SourceLocation"].ToString().Trim();
                            result.SourceLatitude = reader["SourceLatitude"].ToString().Trim();
                            result.SourceLongitude = reader["SourceLongitude"].ToString().Trim();
                            result.DestinationLat = reader["DestinationLat"].ToString().Trim();
                            result.DestinationLong = reader["DestinationLong"].ToString().Trim();
                            result.Destination = reader["Destination"].ToString().Trim();
                            result.TripDate = Convert.ToDateTime(reader.GetDateTime("TripDate"));
                            result.CustomerEmail = reader.IsDBNull("CustomerEmail") ? null : reader["CustomerEmail"].ToString().Trim();
                            result.DriverEmail = reader.IsDBNull("DriverEmail") ? null : reader["DriverEmail"].ToString().Trim();
                            result.PaymentID = reader.IsDBNull("PaymentID") ? null : reader["PaymentID"].ToString().Trim(); 
                            result.TotalTime = (double)reader["TotalTime"];
                            result.CancelReason = reader["CancelReason"].ToString().Trim();
                            result.DriverFirstName = reader.IsDBNull("DriverFirstName") ? null : reader["DriverFirstName"].ToString().Trim();
                            result.DriverLastName = reader.IsDBNull("DriverLastName") ? null : reader["DriverLastName"].ToString().Trim();
                            result.DriverPhoneNumber = reader.IsDBNull("DriverPhoneNumber") ? null : reader["DriverPhoneNumber"].ToString().Trim();
                            result.RiderFirstName = reader.IsDBNull("RiderFirstName") ? null : reader["RiderFirstName"].ToString().Trim();
                            result.RiderLastName = reader.IsDBNull("RiderLastName") ? null : reader["RiderLastName"].ToString().Trim();
                            result.RiderPhoneNumber = reader.IsDBNull("RiderPhoneNumber") ? null : reader["RiderPhoneNumber"].ToString().Trim();
                            result.Rating = reader.IsDBNull("Rating") ? null : (int)reader["Rating"];
                            result.NoOfRides = reader.IsDBNull("NoOfRides") ? null : (int)reader["NoOfRides"];
                            result.CarMake = reader.IsDBNull("CarMake") ? null : reader["CarMake"].ToString().Trim();
                            result.CarModel = reader.IsDBNull("CarModel") ? null : reader["CarModel"].ToString().Trim();
                            result.CarColor = reader.IsDBNull("CarColor") ? null : reader["CarColor"].ToString().Trim();
                            result.TypeOfVehicle = reader.IsDBNull("TypeOfVehicle") ? null : reader["DriverFirstName"].ToString().Trim();
                            result.DriverLiscense = reader.IsDBNull("DriverLiscense") ? null : reader["DriverLiscense"].ToString().Trim();
                            result.CarPreferences = reader.IsDBNull("CarPreferences") ? null : reader["CarPreferences"].ToString().Trim();
                            result.DestinationState = reader.IsDBNull("DestinationState") ? null : reader["DestinationState"].ToString().Trim();
                            result.PlateNumber = reader.IsDBNull("PlateNumber") ? null : reader["PlateNumber"].ToString().Trim();


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


        } internal static Trip SelectTripData(string ID )
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
                            result.CustomerEmail = reader.IsDBNull("CustomerEmail") ? null: reader["CustomerEmail"].ToString().Trim();
                            result.DriverEmail = reader.IsDBNull("DriverEmail") ? null: reader["DriverEmail"].ToString().Trim();
                            result.PaymentID = reader.IsDBNull("PaymentID") ? null : reader["PaymentID"].ToString().Trim();
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
                            result.TotalTime = (double) reader["TotalTime"];
                            result.CancelReason = reader["CancelReason"].ToString().Trim();
                            result.TripDate = Convert.ToDateTime(reader.GetDateTime("TripDate"));
                            result.Date_Created = Convert.ToDateTime(reader.GetDateTime("DateCreated"));
                            result.Date_Updated = Convert.ToDateTime(reader.GetDateTime("DateUpdated"));

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
                            var result = new Trip();

                            result.ID = reader["ID"].ToString().Trim();
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
                            result.CustomerEmail = reader.IsDBNull("CustomerEmail") ? null : reader["CustomerEmail"].ToString().Trim();
                            result.DriverEmail = reader.IsDBNull("DriverEmail") ? null : reader["DriverEmail"].ToString().Trim();
                            result.PaymentID = reader.IsDBNull("PaymentID") ? null : reader["PaymentID"].ToString().Trim(); res.Add(result);
                            result.TotalTime = (double)reader["TotalTime"];
                            result.CancelReason = reader["CancelReason"].ToString().Trim();

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

        internal static List<SuperTrip> SelectDriverTripList(TripSearch trip)
        {
            var res = new List<SuperTrip>();

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
                    cmd.Parameters.AddWithValue("@DestinationState", trip.DestinationState);
                    cmd.Parameters.AddWithValue("@TripDate", trip.TripDate);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var result = new SuperTrip();


                            result.ID = reader["ID"].ToString().Trim();
                            result.TripInitiator = reader["TripInitiator"].ToString().Trim();
                            result.TripStatus = reader["TripStatus"].ToString().Trim();
                            result.TripDistance = reader.IsDBNull("TripDistance") ? null : (int)reader["TripDistance"];
                            result.TripCost = reader.IsDBNull("TripCost") ? null : (int)reader["TripCost"];
                            result.SourceLocation = reader["SourceLocation"].ToString().Trim();
                            result.SourceLatitude = reader["SourceLatitude"].ToString().Trim();
                            result.SourceLongitude = reader["SourceLongitude"].ToString().Trim();
                            result.DestinationLat = reader["DestinationLat"].ToString().Trim();
                            result.DestinationLong = reader["DestinationLong"].ToString().Trim();
                            result.Destination = reader["Destination"].ToString().Trim();
                            result.TripDate = Convert.ToDateTime(reader.GetDateTime("TripDate"));
                            result.CustomerEmail = reader.IsDBNull("CustomerEmail") ? null : reader["CustomerEmail"].ToString().Trim();
                            result.DriverEmail = reader.IsDBNull("DriverEmail") ? null : reader["DriverEmail"].ToString().Trim();
                            result.PaymentID = reader.IsDBNull("PaymentID") ? null : reader["PaymentID"].ToString().Trim(); 
                            result.TotalTime = (double)reader["TotalTime"];
                            result.CancelReason = reader["CancelReason"].ToString().Trim();
                            result.DriverFirstName = reader.IsDBNull("DriverFirstName") ? null : reader["DriverFirstName"].ToString().Trim();
                            result.DriverLastName = reader.IsDBNull("DriverLastName") ? null : reader["DriverLastName"].ToString().Trim();
                            result.DriverPhoneNumber = reader.IsDBNull("DriverPhoneNumber") ? null : reader["DriverPhoneNumber"].ToString().Trim();
                            result.RiderFirstName = reader.IsDBNull("RiderFirstName") ? null : reader["RiderFirstName"].ToString().Trim();
                            result.RiderLastName = reader.IsDBNull("RiderLastName") ? null : reader["RiderLastName"].ToString().Trim();
                            result.RiderPhoneNumber = reader.IsDBNull("RiderPhoneNumber") ? null : reader["RiderPhoneNumber"].ToString().Trim();
                            result.Rating = reader.IsDBNull("Rating") ? null : (int)reader["Rating"];
                            result.NoOfRides = reader.IsDBNull("NoOfRides") ? null : (int)reader["NoOfRides"];
                            result.CarMake = reader.IsDBNull("CarMake") ? null : reader["CarMake"].ToString().Trim();
                            result.CarModel = reader.IsDBNull("CarModel") ? null : reader["CarModel"].ToString().Trim();
                            result.CarColor = reader.IsDBNull("CarColor") ? null : reader["CarColor"].ToString().Trim();
                            result.TypeOfVehicle = reader.IsDBNull("TypeOfVehicle") ? null : reader["DriverFirstName"].ToString().Trim();
                            result.DriverLiscense = reader.IsDBNull("DriverLiscense") ? null : reader["DriverLiscense"].ToString().Trim();
                            result.CarPreferences = reader.IsDBNull("CarPreferences") ? null : reader["CarPreferences"].ToString().Trim();
                            result.DestinationState = reader.IsDBNull("DestinationState") ? null : reader["DestinationState"].ToString().Trim();
                            result.PlateNumber = reader.IsDBNull("PlateNumber") ? null : reader["PlateNumber"].ToString().Trim();
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

        internal static List<SuperTrip> SelectRiderTripList(TripSearch trip)
        {

            var res = new List<SuperTrip>();

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
                    cmd.Parameters.AddWithValue("@DestinationState", trip.DestinationState);
                    cmd.Parameters.AddWithValue("@TripDate", trip.TripDate);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var result = new SuperTrip();


                            result.ID = reader["ID"].ToString().Trim();
                            result.TripInitiator = reader["TripInitiator"].ToString().Trim();
                            result.TripStatus = reader["TripStatus"].ToString().Trim();
                            result.TripDistance = reader.IsDBNull("TripDistance") ? null : (int)reader["TripDistance"];
                            result.TripCost = reader.IsDBNull("TripCost") ? null : (int)reader["TripCost"];
                            result.SourceLocation = reader["SourceLocation"].ToString().Trim();
                            result.SourceLatitude = reader["SourceLatitude"].ToString().Trim();
                            result.SourceLongitude = reader["SourceLongitude"].ToString().Trim();
                            result.DestinationLat = reader["DestinationLat"].ToString().Trim();
                            result.DestinationLong = reader["DestinationLong"].ToString().Trim();
                            result.Destination = reader["Destination"].ToString().Trim();
                            result.TripDate = Convert.ToDateTime(reader.GetDateTime("TripDate"));
                            result.CustomerEmail = reader.IsDBNull("CustomerEmail") ? null : reader["CustomerEmail"].ToString().Trim();
                            result.DriverEmail = reader.IsDBNull("DriverEmail") ? null : reader["DriverEmail"].ToString().Trim();
                            result.PaymentID = reader.IsDBNull("PaymentID") ? null : reader["PaymentID"].ToString().Trim();
                            result.TotalTime = (double)reader["TotalTime"];
                            result.CancelReason = reader["CancelReason"].ToString().Trim();
                            result.DriverFirstName = reader.IsDBNull("DriverFirstName") ? null : reader["DriverFirstName"].ToString().Trim();
                            result.DriverLastName = reader.IsDBNull("DriverLastName") ? null : reader["DriverLastName"].ToString().Trim();
                            result.DriverPhoneNumber = reader.IsDBNull("DriverPhoneNumber") ? null : reader["DriverPhoneNumber"].ToString().Trim();
                            result.RiderFirstName = reader.IsDBNull("RiderFirstName") ? null : reader["RiderFirstName"].ToString().Trim();
                            result.RiderLastName = reader.IsDBNull("RiderLastName") ? null : reader["RiderLastName"].ToString().Trim();
                            result.RiderPhoneNumber = reader.IsDBNull("RiderPhoneNumber") ? null : reader["RiderPhoneNumber"].ToString().Trim();
                            result.Rating = reader.IsDBNull("Rating") ? null : (int)reader["Rating"];
                            result.NoOfRides = reader.IsDBNull("NoOfRides") ? null : (int)reader["NoOfRides"];
                            result.CarMake = reader.IsDBNull("CarMake") ? null : reader["CarMake"].ToString().Trim();
                            result.CarModel = reader.IsDBNull("CarModel") ? null : reader["CarModel"].ToString().Trim();
                            result.CarColor = reader.IsDBNull("CarColor") ? null : reader["CarColor"].ToString().Trim();
                            result.TypeOfVehicle = reader.IsDBNull("TypeOfVehicle") ? null : reader["DriverFirstName"].ToString().Trim();
                            result.DriverLiscense = reader.IsDBNull("DriverLiscense") ? null : reader["DriverLiscense"].ToString().Trim();
                            result.CarPreferences = reader.IsDBNull("CarPreferences") ? null : reader["CarPreferences"].ToString().Trim();
                            result.DestinationState = reader.IsDBNull("DestinationState") ? null : reader["DestinationState"].ToString().Trim();
                            result.PlateNumber = reader.IsDBNull("PlateNumber") ? null : reader["PlateNumber"].ToString().Trim();
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

        internal static List<SuperTrip> CompletedTrip(string email)
        {


            var res = new List<SuperTrip>();

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


                using (SqlCommand cmd = new SqlCommand("CompletedTrips", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            
                                var result = new SuperTrip();


                                result.ID = reader["ID"].ToString().Trim();
                                result.TripInitiator = reader["TripInitiator"].ToString().Trim();
                                result.TripStatus = reader["TripStatus"].ToString().Trim();
                                result.TripDistance = reader.IsDBNull("TripDistance") ? null : (int)reader["TripDistance"];
                                result.TripCost = reader.IsDBNull("TripCost") ? null : (int)reader["TripCost"];
                                result.SourceLocation = reader["SourceLocation"].ToString().Trim();
                                result.SourceLatitude = reader["SourceLatitude"].ToString().Trim();
                                result.SourceLongitude = reader["SourceLongitude"].ToString().Trim();
                                result.DestinationLat = reader["DestinationLat"].ToString().Trim();
                                result.DestinationLong = reader["DestinationLong"].ToString().Trim();
                                result.Destination = reader["Destination"].ToString().Trim();
                                result.TripDate = Convert.ToDateTime(reader.GetDateTime("TripDate"));
                                result.CustomerEmail = reader.IsDBNull("CustomerEmail") ? null : reader["CustomerEmail"].ToString().Trim();
                                result.DriverEmail = reader.IsDBNull("DriverEmail") ? null : reader["DriverEmail"].ToString().Trim();
                                result.PaymentID = reader.IsDBNull("PaymentID") ? null : reader["PaymentID"].ToString().Trim(); 
                                result.TotalTime = (double)reader["TotalTime"];
                                result.CancelReason = reader["CancelReason"].ToString().Trim();
                                result.DriverFirstName = reader.IsDBNull("DriverFirstName") ? null : reader["DriverFirstName"].ToString().Trim();
                                result.DriverLastName = reader.IsDBNull("DriverLastName") ? null : reader["DriverLastName"].ToString().Trim();
                                result.DriverPhoneNumber = reader.IsDBNull("DriverPhoneNumber") ? null : reader["DriverPhoneNumber"].ToString().Trim();
                                result.RiderFirstName = reader.IsDBNull("RiderFirstName") ? null : reader["RiderFirstName"].ToString().Trim();
                                result.RiderLastName = reader.IsDBNull("RiderLastName") ? null : reader["RiderLastName"].ToString().Trim();
                                result.RiderPhoneNumber = reader.IsDBNull("RiderPhoneNumber") ? null : reader["RiderPhoneNumber"].ToString().Trim();
                                result.Rating = reader.IsDBNull("Rating") ? null : (int)reader["Rating"];
                                result.NoOfRides = reader.IsDBNull("NoOfRides") ? null : (int)reader["NoOfRides"];
                                result.CarMake = reader.IsDBNull("CarMake") ? null : reader["CarMake"].ToString().Trim();
                                result.CarModel = reader.IsDBNull("CarModel") ? null : reader["CarModel"].ToString().Trim();
                                result.CarColor = reader.IsDBNull("CarColor") ? null : reader["CarColor"].ToString().Trim();
                                result.TypeOfVehicle = reader.IsDBNull("TypeOfVehicle") ? null : reader["DriverFirstName"].ToString().Trim();
                                result.DriverLiscense = reader.IsDBNull("DriverLiscense") ? null : reader["DriverLiscense"].ToString().Trim();
                                result.CarPreferences = reader.IsDBNull("CarPreferences") ? null : reader["CarPreferences"].ToString().Trim();
                                result.DestinationState = reader.IsDBNull("DestinationState") ? null : reader["DestinationState"].ToString().Trim();
                                result.PlateNumber = reader.IsDBNull("PlateNumber") ? null : reader["PlateNumber"].ToString().Trim();
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

        internal static List<SuperTrip> UpcomingTrips(string email)
        {


            var res = new List<SuperTrip>();

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


                using (SqlCommand cmd = new SqlCommand("UpcomingTrips", connection))
                {
                    DateTime date = DateTime.Now;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            
                              var result = new SuperTrip();


                                result.ID = reader["ID"].ToString().Trim();
                                result.TripInitiator = reader["TripInitiator"].ToString().Trim();
                                result.TripStatus = reader["TripStatus"].ToString().Trim();
                                result.TripDistance = reader.IsDBNull("TripDistance") ? null : (int)reader["TripDistance"];
                                result.TripCost = reader.IsDBNull("TripCost") ? null : (int)reader["TripCost"];
                                result.SourceLocation = reader["SourceLocation"].ToString().Trim();
                                result.SourceLatitude = reader["SourceLatitude"].ToString().Trim();
                                result.SourceLongitude = reader["SourceLongitude"].ToString().Trim();
                                result.DestinationLat = reader["DestinationLat"].ToString().Trim();
                                result.DestinationLong = reader["DestinationLong"].ToString().Trim();
                                result.Destination = reader["Destination"].ToString().Trim();
                                result.TripDate = Convert.ToDateTime(reader.GetDateTime("TripDate"));
                                result.CustomerEmail = reader.IsDBNull("CustomerEmail") ? null : reader["CustomerEmail"].ToString().Trim();
                                result.DriverEmail = reader.IsDBNull("DriverEmail") ? null : reader["DriverEmail"].ToString().Trim();
                                result.PaymentID = reader.IsDBNull("PaymentID") ? null : reader["PaymentID"].ToString().Trim(); 
                                result.TotalTime = (double)reader["TotalTime"];
                                result.CancelReason = reader["CancelReason"].ToString().Trim();
                                result.DriverFirstName = reader.IsDBNull("DriverFirstName") ? null : reader["DriverFirstName"].ToString().Trim();
                                result.DriverLastName = reader.IsDBNull("DriverLastName") ? null : reader["DriverLastName"].ToString().Trim();
                                result.DriverPhoneNumber = reader.IsDBNull("DriverPhoneNumber") ? null : reader["DriverPhoneNumber"].ToString().Trim();
                                result.RiderFirstName = reader.IsDBNull("RiderFirstName") ? null : reader["RiderFirstName"].ToString().Trim();
                                result.RiderLastName = reader.IsDBNull("RiderLastName") ? null : reader["RiderLastName"].ToString().Trim();
                                result.RiderPhoneNumber = reader.IsDBNull("RiderPhoneNumber") ? null : reader["RiderPhoneNumber"].ToString().Trim();
                                result.Rating = reader.IsDBNull("Rating") ? null : (int)reader["Rating"];
                                result.NoOfRides = reader.IsDBNull("NoOfRides") ? null : (int)reader["NoOfRides"];
                                result.CarMake = reader.IsDBNull("CarMake") ? null : reader["CarMake"].ToString().Trim();
                                result.CarModel = reader.IsDBNull("CarModel") ? null : reader["CarModel"].ToString().Trim();
                                result.CarColor = reader.IsDBNull("CarColor") ? null : reader["CarColor"].ToString().Trim();
                                result.TypeOfVehicle = reader.IsDBNull("TypeOfVehicle") ? null : reader["DriverFirstName"].ToString().Trim();
                                result.DriverLiscense = reader.IsDBNull("DriverLiscense") ? null : reader["DriverLiscense"].ToString().Trim();
                                result.CarPreferences = reader.IsDBNull("CarPreferences") ? null : reader["CarPreferences"].ToString().Trim();
                                result.DestinationState = reader.IsDBNull("DestinationState") ? null : reader["DestinationState"].ToString().Trim();
                                result.PlateNumber = reader.IsDBNull("PlateNumber") ? null : reader["PlateNumber"].ToString().Trim();
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

        internal static List<SuperTrip> AwaitingApproval(string email)
        {

            var res = new List<SuperTrip>();

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


                using (SqlCommand cmd = new SqlCommand("AwaitingApproval", connection))
                {
                    DateTime date = DateTime.Now;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var result = new SuperTrip();


                            result.ID = reader["ID"].ToString().Trim();
                            result.TripInitiator = reader["TripInitiator"].ToString().Trim();
                            result.TripStatus = reader["TripStatus"].ToString().Trim();
                            result.TripDistance = reader.IsDBNull("TripDistance") ? null : (int)reader["TripDistance"];
                            result.TripCost = reader.IsDBNull("TripCost") ? null : (int)reader["TripCost"];
                            result.SourceLocation = reader["SourceLocation"].ToString().Trim();
                            result.SourceLatitude = reader["SourceLatitude"].ToString().Trim();
                            result.SourceLongitude = reader["SourceLongitude"].ToString().Trim();
                            result.DestinationLat = reader["DestinationLat"].ToString().Trim();
                            result.DestinationLong = reader["DestinationLong"].ToString().Trim();
                            result.Destination = reader["Destination"].ToString().Trim();
                            result.TripDate = Convert.ToDateTime(reader.GetDateTime("TripDate"));
                            result.CustomerEmail = reader.IsDBNull("CustomerEmail") ? null : reader["CustomerEmail"].ToString().Trim();
                            result.DriverEmail = reader.IsDBNull("DriverEmail") ? null : reader["DriverEmail"].ToString().Trim();
                            result.PaymentID = reader.IsDBNull("PaymentID") ? null : reader["PaymentID"].ToString().Trim(); 
                            result.TotalTime = (double)reader["TotalTime"];
                            result.CancelReason = reader["CancelReason"].ToString().Trim();
                            result.DriverFirstName = reader.IsDBNull("DriverFirstName") ? null : reader["DriverFirstName"].ToString().Trim();
                            result.DriverLastName = reader.IsDBNull("DriverLastName") ? null : reader["DriverLastName"].ToString().Trim();
                            result.DriverPhoneNumber = reader.IsDBNull("DriverPhoneNumber") ? null : reader["DriverPhoneNumber"].ToString().Trim();
                            result.RiderFirstName = reader.IsDBNull("RiderFirstName") ? null : reader["RiderFirstName"].ToString().Trim();
                            result.RiderLastName = reader.IsDBNull("RiderLastName") ? null : reader["RiderLastName"].ToString().Trim();
                            result.RiderPhoneNumber = reader.IsDBNull("RiderPhoneNumber") ? null : reader["RiderPhoneNumber"].ToString().Trim();
                            result.Rating = reader.IsDBNull("Rating") ? null : (int)reader["Rating"];
                            result.NoOfRides = reader.IsDBNull("NoOfRides") ? null : (int)reader["NoOfRides"];
                            result.CarMake = reader.IsDBNull("CarMake") ? null : reader["CarMake"].ToString().Trim();
                            result.CarModel = reader.IsDBNull("CarModel") ? null : reader["CarModel"].ToString().Trim();
                            result.CarColor = reader.IsDBNull("CarColor") ? null : reader["CarColor"].ToString().Trim();
                            result.TypeOfVehicle = reader.IsDBNull("TypeOfVehicle") ? null : reader["DriverFirstName"].ToString().Trim();
                            result.DriverLiscense = reader.IsDBNull("DriverLiscense") ? null : reader["DriverLiscense"].ToString().Trim();
                            result.CarPreferences = reader.IsDBNull("CarPreferences") ? null : reader["CarPreferences"].ToString().Trim();
                            result.DestinationState = reader.IsDBNull("DestinationState") ? null : reader["DestinationState"].ToString().Trim();
                            result.PlateNumber = reader.IsDBNull("PlateNumber") ? null : reader["PlateNumber"].ToString().Trim();
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

        internal static List<SuperTrip> ApprovalList(string email)
        {

            var res = new List<SuperTrip>();

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


                using (SqlCommand cmd = new SqlCommand("ApprovalList", connection))
                {
                    DateTime date = DateTime.Now;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var result = new SuperTrip();

                            result.ID = reader["ID"].ToString().Trim();
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
                            result.CustomerEmail = reader.IsDBNull("CustomerEmail") ? null : reader["CustomerEmail"].ToString().Trim();
                            result.DriverEmail = reader.IsDBNull("DriverEmail") ? null : reader["DriverEmail"].ToString().Trim();
                            result.PaymentID = reader.IsDBNull("PaymentID") ? null : reader["PaymentID"].ToString().Trim(); 
                            result.TotalTime = (double)reader["TotalTime"];
                            result.CancelReason = reader["CancelReason"].ToString().Trim();
                            result.DriverFirstName = reader.IsDBNull("DriverFirstName") ? null : reader["DriverFirstName"].ToString().Trim();
                            result.DriverLastName = reader.IsDBNull("DriverLastName") ? null : reader["DriverLastName"].ToString().Trim();
                            result.DriverPhoneNumber = reader.IsDBNull("DriverPhoneNumber") ? null : reader["DriverPhoneNumber"].ToString().Trim();
                            result.RiderFirstName = reader.IsDBNull("RiderFirstName") ? null : reader["RiderFirstName"].ToString().Trim();
                            result.RiderLastName = reader.IsDBNull("RiderLastName") ? null : reader["RiderLastName"].ToString().Trim();
                            result.RiderPhoneNumber = reader.IsDBNull("RiderPhoneNumber") ? null : reader["RiderPhoneNumber"].ToString().Trim();
                            result.Rating = reader.IsDBNull("Rating") ? null : (int)reader["Rating"];
                            result.NoOfRides = reader.IsDBNull("NoOfRides") ? null : (int)reader["NoOfRides"];
                            result.CarMake = reader.IsDBNull("CarMake") ? null : reader["CarMake"].ToString().Trim();
                            result.CarModel = reader.IsDBNull("CarModel") ? null : reader["CarModel"].ToString().Trim();
                            result.CarColor = reader.IsDBNull("CarColor") ? null : reader["CarColor"].ToString().Trim();
                            result.TypeOfVehicle = reader.IsDBNull("TypeOfVehicle") ? null : reader["DriverFirstName"].ToString().Trim();
                            result.DriverLiscense = reader.IsDBNull("DriverLiscense") ? null : reader["DriverLiscense"].ToString().Trim();
                            result.CarPreferences = reader.IsDBNull("CarPreferences") ? null : reader["CarPreferences"].ToString().Trim();
                            result.DestinationState = reader.IsDBNull("DestinationState") ? null : reader["DestinationState"].ToString().Trim();
                            result.PlateNumber = reader.IsDBNull("PlateNumber") ? null : reader["PlateNumber"].ToString().Trim();
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

