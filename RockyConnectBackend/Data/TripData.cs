using System;
using Microsoft.Data.SqlClient;
using RockyConnectBackend.Model;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RockyConnectBackend.Data
{
    public class TripData
    {
        internal static string CreateTripData(TripDataInfo trip)
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
                SqlCommand cmd = new SqlCommand($"INSERT INTO [dbo].[TripRequest] ([ID],[TripDistance],[TripType],[SourceLocation],[SourceLatitude],[Email],[DateCreated],[DateUpdated],[SourceLongitude],[DestinationLat],[DestinationLong],[Destination]) VALUES (@ID,@TripDistance, @TripType,@SourceLocation,@SourceLatitude,@Email,@DateCreated,@DateUpdated,@SourceLongitude,@DestinationLat,@DestinationLong,@Destination)", connection);
                cmd.Parameters.AddWithValue("@ID", trip.ID); ;
                cmd.Parameters.AddWithValue("@TripDistance", trip.TripDistance); ;
                cmd.Parameters.AddWithValue("@TripType", trip.TripType);
                cmd.Parameters.AddWithValue("@SourceLocation", trip.SourceLocation);
                cmd.Parameters.AddWithValue("@Email", trip.Email);
                cmd.Parameters.AddWithValue("@SourceLatitude", trip.SourceLatitude);
                cmd.Parameters.AddWithValue("@SourceLongitude", trip.SourceLongitude);
                cmd.Parameters.AddWithValue("@DestinationLat", trip.DestinationLat);
                cmd.Parameters.AddWithValue("@DestinationLong", trip.DestinationLong);
                cmd.Parameters.AddWithValue("@SourceLongitude", trip.Destination);
                cmd.Parameters.AddWithValue("@TripDate", trip.TripDate);
                cmd.Parameters.AddWithValue("@DateCreated", trip.Date_Created).Value = date;
                cmd.Parameters.AddWithValue("@DateUpdated", trip.Date_Updated).Value = date;

                ret = cmd.ExecuteNonQuery();
                if (ret == 1)
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
        internal static string UpdateTripData(TripDataInfo trip)
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
                SqlCommand cmd = new SqlCommand($"Update [dbo].[TripRequest] set [TripDistance]=@TripDistance,[TripType] = @TripType,[SourceLatitude]=@SourceLatitude,[SourceLocation]= @SourceLocation,[DateCreated]=@DateCreated,[DateUpdated]=@DateUpdated,[SourceLongitude]=@SourceLongitude,[DestinationLat]=@DestinationLat,[DestinationLong]=@DestinationLong,[TripDate]=@TripDate where ID ={trip.ID}", connection);

                cmd.Parameters.AddWithValue("@TripDistance", trip.TripDistance); ;
                cmd.Parameters.AddWithValue("@TripType", trip.TripType);
                cmd.Parameters.AddWithValue("@SourceLocation", trip.SourceLocation);
                cmd.Parameters.AddWithValue("@Email", trip.Email);
                cmd.Parameters.AddWithValue("@SourceLatitude", trip.SourceLatitude);
                cmd.Parameters.AddWithValue("@SourceLongitude", trip.SourceLongitude);
                cmd.Parameters.AddWithValue("@DestinationLat", trip.DestinationLat);
                cmd.Parameters.AddWithValue("@DestinationLong", trip.DestinationLong);
                cmd.Parameters.AddWithValue("@SourceLongitude", trip.Destination);
                cmd.Parameters.AddWithValue("@TripDate", trip.TripDate).Value = date;
                cmd.Parameters.AddWithValue("@DateCreated", trip.Date_Created).Value = date;
                cmd.Parameters.AddWithValue("@DateUpdated", trip.Date_Updated).Value = date;

                ret = cmd.ExecuteNonQuery();

                ret = cmd.ExecuteNonQuery();
                if (ret == 1)
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
                SqlCommand cmd = new SqlCommand($"Delete * from [dbo].[TripRequest] Where  ID ={trip}", connection);


                ret = cmd.ExecuteNonQuery();
                if (ret == 1)
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
        internal static TripDataInfo SelectTripData(TripRequest trip)
        {
            //  var result = new User   ();
            var result = new TripDataInfo();

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


                using (SqlCommand cmd = new SqlCommand($"Select * from [dbo].[CRockyconnect] Where  ID ={trip.ID}", connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            //  result.UserID = int.Parse(reader["UserID"]);
                            result.Email = reader["Email"].ToString().Trim();
                            result.TripDistance = Convert.ToInt32(reader["TripDistance"]);
                            result.TripType = Convert.ToInt32(reader["TripType"]);
                            result.SourceLocation = reader["SourceLocation"].ToString().Trim();
                            result.SourceLatitude = reader["SourceLatitude"].ToString().Trim();
                            result.SourceLongitude = reader["SourceLongitude"].ToString().Trim();
                            result.SourceLocation = reader["DestinationLat"].ToString().Trim();
                            result.SourceLatitude = reader["DestinationLong"].ToString().Trim();
                            result.SourceLongitude = reader["Destination"].ToString().Trim();
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

        internal static List<TripDataInfo> SelectEmailTrips(string email)
        {
            var result = new TripDataInfo();
            var res = new List<TripDataInfo>();

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


                            result.Email = reader["Email"].ToString().Trim();
                            result.TripDistance = Convert.ToInt32(reader["TripDistance"]);
                            result.TripType = Convert.ToInt32(reader["TripType"]);
                            result.SourceLocation = reader["SourceLocation"].ToString().Trim();
                            result.SourceLatitude = reader["SourceLatitude"].ToString().Trim();
                            result.SourceLongitude = reader["SourceLongitude"].ToString().Trim();
                            result.SourceLocation = reader["DestinationLat"].ToString().Trim();
                            result.SourceLatitude = reader["DestinationLong"].ToString().Trim();
                            result.SourceLongitude = reader["Destination"].ToString().Trim();
                            result.TripDate = Convert.ToDateTime(reader.GetDateTime("TripDate"));
                            result.Date_Created = Convert.ToDateTime(reader.GetDateTime("DateCreated"));
                            result.Date_Updated = Convert.ToDateTime(reader.GetDateTime("DateUpdated"));
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

