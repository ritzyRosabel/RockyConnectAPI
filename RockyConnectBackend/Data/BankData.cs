using System;
using Microsoft.Data.SqlClient;
using RockyConnectBackend.Model;
using System.Data;

namespace RockyConnectBackend.Data
{
	public class BankData
	{
	
            internal static Bank SelectBankData(string? email)
            {

                var result = new Bank();

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
                    using (SqlCommand cmd = new SqlCommand($"SelectBank", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email", email);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {


                                //  result.UserID = int.Parse(reader["UserID"]);
                                result.Email = reader["Email"].ToString().Trim();
                                result.BankName = reader["BankName"].ToString().Trim();
                                result.RoutingNumber = reader["RoutingNumber"].ToString().Trim();
                                result.AccountNumber = reader["AccountNumber"].ToString().Trim();
                               
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

            internal static string CreateBankData(Bank card)
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

                    SqlCommand cmd = new SqlCommand($"CreateBank", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BankName", card.BankName);
                    cmd.Parameters.AddWithValue("@AccountNumber", card.AccountNumber);
                    cmd.Parameters.AddWithValue("@RoutingNumber", card.RoutingNumber);
                    cmd.Parameters.AddWithValue("@Email", card.Email);
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
            internal static string UpdateBankData(Bank card)
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
                    SqlCommand cmd = new SqlCommand("UpdateBank", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BankName", card.BankName);
                    cmd.Parameters.AddWithValue("@AccountNumber", card.AccountNumber);
                    cmd.Parameters.AddWithValue("@RoutingNumber", card.RoutingNumber);
                    cmd.Parameters.AddWithValue("@Email", card.Email);
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
        }
    }

