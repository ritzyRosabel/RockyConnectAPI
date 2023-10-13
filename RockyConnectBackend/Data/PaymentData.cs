using System;
using Microsoft.Data.SqlClient;
using RockyConnectBackend.Model;
using System.Data;

namespace RockyConnectBackend.Data
{
	public class PaymentData
	{
        internal static string CreateCardData(PaymentCard card)
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
                SqlCommand cmd = new SqlCommand($"INSERT INTO [dbo].[CRockyconnect] ([CardAlias],[CardType],[Code],[ExpiryDate],[Email],[DateCreated],[DateUpdated],[FullName]) VALUES (@CardAlias, @CardType,@Code,@ExpiryDate,@Email,@DateCreated,@DateUpdated,@FullName)", connection);

                cmd.Parameters.AddWithValue("@CardAlias", card.CardAlias);
                cmd.Parameters.AddWithValue("@CardType", card.CardType);
                cmd.Parameters.AddWithValue("@Code", card.Code);
                cmd.Parameters.AddWithValue("@Email", card.Email);
                cmd.Parameters.AddWithValue("@ExpiryDate", card.ExpiryDate);
                cmd.Parameters.AddWithValue("@FullName", card.FullName);
                cmd.Parameters.AddWithValue("@DateCreated", card.Date_Created).Value = date;
                cmd.Parameters.AddWithValue("@DateUpdated", card.Date_Updated).Value = date;

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
        internal static string UpdateCardData(CardUpdate card)
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
                SqlCommand cmd = new SqlCommand($"Update [dbo].[CRockyconnect] set [CardAlias]=@CardAlias,[CardType] = @CardType,[Code]=@Code,[ExpiryDate]= @ExpiryDate,[DateCreated]=@DateCreated,[DateUpdated]=@DateUpdated,[FullName]=@FullName where Email ={card.Email} and CardAlias={card.OldCardAlias}", connection);

                cmd.Parameters.AddWithValue("@CardAlias", card.CardAlias);
                cmd.Parameters.AddWithValue("@CardType", card.CardType);
                cmd.Parameters.AddWithValue("@Code", card.Code);
                cmd.Parameters.AddWithValue("@ExpiryDate", card.ExpiryDate);
                cmd.Parameters.AddWithValue("@FullName", card.FullName);
                cmd.Parameters.AddWithValue("@DateCreated", card.Date_Created).Value = date;
                cmd.Parameters.AddWithValue("@DateUpdated", card.Date_Updated).Value = date;

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
        internal static string DeleteCardData(CardUpdate card)
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
                    SqlCommand cmd = new SqlCommand($"Delete * from [dbo].[CRockyconnect] Where  Email ={card.Email} and CardAlias ={card.OldCardAlias}", connection);


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
            internal static string SelectCardData(CardUpdate card)
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
                    SqlCommand cmd = new SqlCommand($"Select * from [dbo].[CRockyconnect] Where  Email ={card.Email} and CardAlias ={card.OldCardAlias}", connection);


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
        internal static string SelectEmailCards(CardUpdate card)
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
                SqlCommand cmd = new SqlCommand($"Select * from [dbo].[CRockyconnect] Where  Email ={card.Email}", connection);


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

    }
}

