﻿using System;
using Microsoft.Data.SqlClient;
using RockyConnectBackend.Model;
using System.Data;
using RockyConnectBackend.Data;
using System.Collections.Generic;

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
                SqlCommand cmd = new SqlCommand($"CreateCard", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CardAlias", card.CardAlias);
                cmd.Parameters.AddWithValue("@CardType", card.CardType);
                cmd.Parameters.AddWithValue("@Code", card.Code);
                cmd.Parameters.AddWithValue("@Pan", card.Pan);
                cmd.Parameters.AddWithValue("@Email", card.Email);
                cmd.Parameters.AddWithValue("@ExpiryDate", card.ExpiryDate);
                cmd.Parameters.AddWithValue("@FullName", card.FullName);
                cmd.Parameters.AddWithValue("@DateCreated", card.Date_Created).Value = date;
                cmd.Parameters.AddWithValue("@DateUpdated", card.Date_Updated).Value = date;

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
                SqlCommand cmd = new SqlCommand("UpdateCard", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", card.Email);
                cmd.Parameters.AddWithValue("@OldCardAlias", card.OldCardAlias);
                cmd.Parameters.AddWithValue("@NewCardAlias", card.CardAlias);
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

            return result;
        }
        internal static string DeleteCardData(SavedCardRequest card)
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
                    SqlCommand cmd = new SqlCommand("DeleteCard", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CardAlias", card.CardAlias);
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
            internal static PaymentCard SelectCardData(SavedCardRequest card)
            {
                //  var result = new User   ();
                var result = new PaymentCard();

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
                    

                    using (SqlCommand cmd = new SqlCommand($"Select * from [dbo].[CRockyconnect] Where  Email ='{card.Email}' and CardAlias ='{card.CardAlias}'", connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {

                                //  result.UserID = int.Parse(reader["UserID"]);
                                result.Email = reader["Email"].ToString().Trim();
                                result.CardAlias = reader["CardAlias"].ToString().Trim();
                                result.CardType = reader["CardType"].ToString().Trim();
                                result.Code = reader["ICV"].ToString().Trim();
                            result.Pan = reader["Pan"].ToString().Trim();
                            result.FullName = reader["FullName"].ToString().Trim();
                                result.ExpiryDate = Convert.ToDateTime(reader.GetDateTime("ExpiryDate"));
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
       
        internal static List<PaymentCard> SelectEmailCards(string  email)
        {
            var res = new List<PaymentCard>();

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
                using (SqlCommand cmd = new SqlCommand($"Select * from [dbo].[CRockyconnect] Where  Email ='{email}'", connection)) 
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var result = new PaymentCard();


                            //  result.UserID = int.Parse(reader["UserID"]);
                            result.Email = reader["Email"].ToString().Trim();
                            result.CardAlias = reader["CardAlias"].ToString().Trim();
                            result.CardType = reader["CardType"].ToString().Trim();
                            result.Code = reader["ICV"].ToString().Trim();
                            result.Pan = reader["Pan"].ToString().Trim();
                            result.FullName = reader["FullName"].ToString().Trim();
                            result.ExpiryDate = Convert.ToDateTime(reader.GetDateTime("ExpiryDate"));
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
        internal static string MakePayment(Payment pay)
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
                SqlCommand cmd = new SqlCommand($"insert into[dbo].[Payment] ( [ID],[TripID],[Bill],[RidRentEmail],[DriOwnEmail],[PaymentStatus],[PaymentType],[PaymentDate])\n  values(@ID, @TripID, @Bill, @RidRentEmail, @DriOwnEmail, @PaymentStatus, @PaymentType, @PaymentDate)", connection);

                cmd.Parameters.AddWithValue("@ID", pay.ID);
                cmd.Parameters.AddWithValue("@TripID", pay.TripID);
                cmd.Parameters.AddWithValue("@Bill", pay.Bill);
                cmd.Parameters.AddWithValue("@RidRentEmail", pay.RidRentEmail);
                cmd.Parameters.AddWithValue("@DriOwnEmail", pay.DriOwnEmail);
                cmd.Parameters.AddWithValue("@PaymentStatus", pay.PaymentStatus);
                cmd.Parameters.AddWithValue("@PaymentType", pay.PaymentType);
                cmd.Parameters.AddWithValue("@PaymentDate", pay.PaymentDate).Value = date;

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

        internal static Payment GetPayment(string pay)
        {

            var result = new Payment();

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


                using (SqlCommand cmd = new SqlCommand($"SelectPayment", connection))
                {
                    DateTime date = DateTime.Now;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", pay);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            result.ID = reader["ID"].ToString().Trim();
                            result.RidRentEmail = reader["RidRentEmail"].ToString().Trim();
                            result.Bill = (int)reader["Bill"];
                            result.DriOwnEmail = reader["DriOwnEmail"].ToString().Trim();
                            result.PaymentType = reader["PaymentType"].ToString().Trim();
                            result.PaymentStatus = reader["PaymentStatus"].ToString().Trim();
                            result.PaymentDate = Convert.ToDateTime(reader.GetDateTime("PaymentDate"));
                            result.TripID = reader["TripID"].ToString().Trim();


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

        internal static string UpdatePayment(Payment pay)
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
                SqlCommand cmd = new SqlCommand($"update [dbo].[Payment] set [PaymentStatus]=@PaymentStatus, [RefundID] = @RefundID where ID=@ID", connection);
                cmd.Parameters.AddWithValue("@ID", pay.ID);
                cmd.Parameters.AddWithValue("@RefundID", pay.TripID);
                cmd.Parameters.AddWithValue("@PaymentStatus", pay.PaymentStatus);

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
        internal static string MakeRefund(Refund refund)
        {
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
                SqlCommand cmd = new SqlCommand("CreateRefund", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", refund.ID);
                cmd.Parameters.AddWithValue("@Bill", refund.Bill);
                cmd.Parameters.AddWithValue("@PaymentMethod", refund.PaymentMethod);
                cmd.Parameters.AddWithValue("@RefundStatus", refund.RefundStatus);
                cmd.Parameters.AddWithValue("@PaymentID", refund.PaymentID);
                cmd.Parameters.AddWithValue("@RefundDate", refund.RefundDate).Value = date;

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
    }
    }

