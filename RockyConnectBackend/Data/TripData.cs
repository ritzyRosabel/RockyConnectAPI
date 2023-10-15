//using System;
//using Microsoft.Data.SqlClient;
//using RockyConnectBackend.Model;
//using System.Data;

//namespace RockyConnectBackend.Data
//{
//    public class TripData
//    {
//        internal static string CreateTripData(TripDataInfo trip)
//        {
//            //  var result = new User   ();
//            string result = "01";

//            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

//            builder.DataSource = "rosabeldbserver.database.windows.net";
//            builder.UserID = "rosabelDB";
//            builder.Password = "Mololuwa@14";
//            builder.InitialCatalog = "RockyConnectDB";

//            SqlConnection connection = new SqlConnection(builder.ConnectionString);

//            Console.WriteLine("\nQuery data example:");
//            Console.WriteLine("=========================================\n");
//            int ret = 6;
//            connection.Open();
//            try
//            {
//                DateTime date = DateTime.Now;
//                DateTime defaultVerified = new DateTime(1900, 01, 01);
//                SqlCommand cmd = new SqlCommand($"INSERT INTO [dbo].[CRockyconnect] ([TripDistance],[TripType],[SourceLocation],[SourceLatitude],[Email],[DateCreated],[DateUpdated],[SourceLongitude],[DestinationLat],[DestinationLong],[Destination]) VALUES (@TripDistance, @TripType,@SourceLocation,@SourceLatitude,@Email,@DateCreated,@DateUpdated,@SourceLongitude,@DestinationLat,@DestinationLong,@Destination)", connection);

//                cmd.Parameters.AddWithValue("@TripDistance", trip.TripDistance); ;
//                cmd.Parameters.AddWithValue("@TripType", trip.TripType);
//                cmd.Parameters.AddWithValue("@SourceLocation", trip.SourceLocation);
//                cmd.Parameters.AddWithValue("@Email", trip.Email);
//                cmd.Parameters.AddWithValue("@SourceLatitude", trip.SourceLatitude);
//                cmd.Parameters.AddWithValue("@SourceLongitude", trip.SourceLongitude);
//                cmd.Parameters.AddWithValue("@DestinationLat", trip.DestinationLat);
//                cmd.Parameters.AddWithValue("@DestinationLong", trip.DestinationLong);
//                cmd.Parameters.AddWithValue("@SourceLongitude", trip.Destination);
//                cmd.Parameters.AddWithValue("@DateCreated", trip.Date_Created).Value = date;
//                cmd.Parameters.AddWithValue("@DateUpdated", trip.Date_Updated).Value = date;

//                ret = cmd.ExecuteNonQuery();
//                if (ret == 1)
//                {
//                    result = "00";
//                }



//            }
//            catch (SqlException e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//            finally
//            {
//                if (connection.State != ConnectionState.Closed)
//                {
//                    connection.Close();

//                }
//            }

//            //Console.WriteLine("\nDone. Press enter.");
//            //Console.ReadLine();



//            return result;
//        }
//        internal static string UpdateCardData(TripDataInfo trip)
//        {
//            //  var result = new User   ();
//            string result = "01";

//            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

//            builder.DataSource = "rosabeldbserver.database.windows.net";
//            builder.UserID = "rosabelDB";
//            builder.Password = "Mololuwa@14";
//            builder.InitialCatalog = "RockyConnectDB";

//            SqlConnection connection = new SqlConnection(builder.ConnectionString);

//            Console.WriteLine("\nQuery data example:");
//            Console.WriteLine("=========================================\n");
//            int ret = 6;
//            connection.Open();
//            try
//            {
//                DateTime date = DateTime.Now;
//                DateTime defaultVerified = new DateTime(1900, 01, 01);
//                SqlCommand cmd = new SqlCommand($"Update [dbo].[CRockyconnect] set [TripDistance]=@TripDistance,[TripType] = @TripType,[SourceLatitude]=@SourceLatitude,[SourceLocation]= @SourceLocation,[DateCreated]=@DateCreated,[DateUpdated]=@DateUpdated,[SourceLongitude]=@SourceLongitude,[DestinationLat]=@DestinationLat,[DestinationLong]=@DestinationLong, where Email ={card.Email} and CardAlias={card.OldCardAlias}", connection);

//                cmd.Parameters.AddWithValue("@TripDistance", trip.TripDistance); ;
//                cmd.Parameters.AddWithValue("@TripType", trip.TripType);
//                cmd.Parameters.AddWithValue("@SourceLocation", trip.SourceLocation);
//                cmd.Parameters.AddWithValue("@Email", trip.Email);
//                cmd.Parameters.AddWithValue("@SourceLatitude", trip.SourceLatitude);
//                cmd.Parameters.AddWithValue("@SourceLongitude", trip.SourceLongitude);
//                cmd.Parameters.AddWithValue("@DestinationLat", trip.DestinationLat);
//                cmd.Parameters.AddWithValue("@DestinationLong", trip.DestinationLong);
//                cmd.Parameters.AddWithValue("@SourceLongitude", trip.Destination);
//                cmd.Parameters.AddWithValue("@DateCreated", trip.Date_Created).Value = date;
//                cmd.Parameters.AddWithValue("@DateUpdated", trip.Date_Updated).Value = date;

//                ret = cmd.ExecuteNonQuery();

//                ret = cmd.ExecuteNonQuery();
//                if (ret == 1)
//                {
//                    result = "00";
//                }



//            }
//            catch (SqlException e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//            finally
//            {
//                if (connection.State != ConnectionState.Closed)
//                {
//                    connection.Close();

//                }
//            }

//            return result;
//        }
//        internal static string DeleteCardData(SavedCardRequest card)
//        {
//            //  var result = new User   ();
//            string result = "01";

//            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

//            builder.DataSource = "rosabeldbserver.database.windows.net";
//            builder.UserID = "rosabelDB";
//            builder.Password = "Mololuwa@14";
//            builder.InitialCatalog = "RockyConnectDB";

//            SqlConnection connection = new SqlConnection(builder.ConnectionString);

//            Console.WriteLine("\nQuery data example:");
//            Console.WriteLine("=========================================\n");
//            int ret = 6;
//            connection.Open();
//            try
//            {
//                DateTime date = DateTime.Now;
//                DateTime defaultVerified = new DateTime(1900, 01, 01);
//                SqlCommand cmd = new SqlCommand($"Delete * from [dbo].[CRockyconnect] Where  Email ={card.Email} and CardAlias ={card.CardAlias}", connection);


//                ret = cmd.ExecuteNonQuery();
//                if (ret == 1)
//                {
//                    result = "00";
//                }



//            }
//            catch (SqlException e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//            finally
//            {
//                if (connection.State != ConnectionState.Closed)
//                {
//                    connection.Close();

//                }
//            }


//            //Console.WriteLine("\nDone. Press enter.");
//            //Console.ReadLine();



//            return result;
//        }
//        internal static PaymentCard SelectCardData(SavedCardRequest card)
//        {
//            //  var result = new User   ();
//            var result = new PaymentCard();

//            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

//            builder.DataSource = "rosabeldbserver.database.windows.net";
//            builder.UserID = "rosabelDB";
//            builder.Password = "Mololuwa@14";
//            builder.InitialCatalog = "RockyConnectDB";

//            SqlConnection connection = new SqlConnection(builder.ConnectionString);

//            Console.WriteLine("\nQuery data example:");
//            Console.WriteLine("=========================================\n");
//            connection.Open();
//            try
//            {


//                using (SqlCommand cmd = new SqlCommand($"Select * from [dbo].[CRockyconnect] Where  Email ={card.Email} and CardAlias ={card.CardAlias}", connection))
//                {
//                    using (SqlDataReader reader = cmd.ExecuteReader())
//                    {

//                        while (reader.Read())
//                        {

//                            //  result.UserID = int.Parse(reader["UserID"]);
//                            result.Email = reader["Email"].ToString().Trim();
//                            result.CardAlias = reader["CardAlias"].ToString().Trim();
//                            result.CardType = reader["CardType"].ToString().Trim();
//                            result.Code = reader["Code"].ToString().Trim();
//                            result.FullName = reader["FullName"].ToString().Trim();
//                            result.ExpiryDate = Convert.ToDateTime(reader.GetDateTime("ExpiryDate"));
//                            result.Date_Created = Convert.ToDateTime(reader.GetDateTime("DateCreated"));
//                            result.Date_Updated = Convert.ToDateTime(reader.GetDateTime("DateUpdated"));


//                        }

//                    }
//                }


//            }
//            catch (SqlException e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//            finally
//            {
//                if (connection.State != ConnectionState.Closed)
//                {
//                    connection.Close();

//                }
//            }
//            return result;


//        }

//        internal static List<PaymentCard> SelectEmailCards(string email)
//        {
//            var result = new PaymentCard();
//            var res = new List<PaymentCard>();

//            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

//            builder.DataSource = "rosabeldbserver.database.windows.net";
//            builder.UserID = "rosabelDB";
//            builder.Password = "Mololuwa@14";
//            builder.InitialCatalog = "RockyConnectDB";

//            SqlConnection connection = new SqlConnection(builder.ConnectionString);

//            Console.WriteLine("\nQuery data example:");
//            Console.WriteLine("=========================================\n");
//            connection.Open();
//            try
//            {
//                DateTime date = DateTime.Now;
//                DateTime defaultVerified = new DateTime(1900, 01, 01);
//                using (SqlCommand cmd = new SqlCommand($"Select * from [dbo].[CRockyconnect] Where  Email ={email}", connection))
//                {
//                    using (SqlDataReader reader = cmd.ExecuteReader())
//                    {

//                        while (reader.Read())
//                        {

//                            //  result.UserID = int.Parse(reader["UserID"]);
//                            result.Email = reader["Email"].ToString().Trim();
//                            result.CardAlias = reader["CardAlias"].ToString().Trim();
//                            result.CardType = reader["CardType"].ToString().Trim();
//                            result.Code = reader["Code"].ToString().Trim();
//                            result.FullName = reader["FullName"].ToString().Trim();
//                            result.ExpiryDate = Convert.ToDateTime(reader.GetDateTime("ExpiryDate"));
//                            result.Date_Created = Convert.ToDateTime(reader.GetDateTime("DateCreated"));
//                            result.Date_Updated = Convert.ToDateTime(reader.GetDateTime("DateUpdated"));
//                            res.Add(result);

//                        }

//                    }
//                }


//            }
//            catch (SqlException e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//            finally
//            {
//                if (connection.State != ConnectionState.Closed)
//                {
//                    connection.Close();

//                }
//            }
//            return res;

//        }

//    }
//}

