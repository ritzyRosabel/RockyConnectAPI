using System;
using RockyConnectBackend.Model;
using System.Data;
using Microsoft.Data.SqlClient;

namespace RockyConnectBackend.Data
{
	public class UserData
	{

        internal static string CreateCustomerData(User customer)
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

                SqlCommand cmd = new SqlCommand($"dbo.CreateCustomer", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Password", customer.Password);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@Role", (int)customer.Role);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@IsAccountActive", customer.IsAccountActive);
                cmd.Parameters.AddWithValue("@Email", customer.Email.ToLower());
                cmd.Parameters.AddWithValue("@DateCreated", customer.Date_Created).Value = date;
                cmd.Parameters.AddWithValue("@DateUpdated", customer.Date_Updated).Value = date;
                cmd.Parameters.AddWithValue("@DateVerified", customer.Date_Verified).Value = defaultVerified;       

        ret = cmd.ExecuteNonQuery();
                if (ret == -1)
                {
                    result = "00";
                }



            }
            catch (SqlException e)
            {
                int data = e.ErrorCode;
                result = data.ToString();
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

        
        internal static User GetUserUsingEmail(string email)
        {
            var res = new User();


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

                using (SqlCommand command = new SqlCommand($"SelectUserUsingEmail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@email", email);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                //  res.Year = reader["Year"].ToString().Trim();(int)reader["ID"];
                                res.Role = (Role)reader["Role"];
                                res.FirstName = reader["FirstName"].ToString().Trim();
                                res.LastName = reader["LastName"].ToString().Trim();
                                res.Email = reader["Email"].ToString().Trim();
                                res.PhoneNumber = reader["PhoneNumber"].ToString().Trim();
                                res.Password = reader["Password"].ToString().Trim();
                                // res.CompanyCertificate = reader["CompanyCertificate"].ToString().Trim();
                                res.AccountVerified = (int)reader["AccountVerified"];
                                res.Date_Created = Convert.ToDateTime(reader.GetDateTime("DateCreated"));
                               res.Date_Updated = Convert.ToDateTime(reader.GetDateTime("DateUpdated"));
                                res.Date_Verified = Convert.ToDateTime(reader.GetDateTime("DateVerified"));


                            }
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

            //Console.WriteLine("\nDone. Press enter.");
            //Console.ReadLine();



            return res;
        }
        internal static string VerifyAccount(User user)
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
                SqlCommand cmd = new SqlCommand($"VerifyAccount", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccountVerified", user.AccountVerified);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@DateVerify", user.Date_Verified);


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


        internal static string UpdateData(User customer)
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

                SqlCommand cmd = new SqlCommand("dbo.UpdateCustomer", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", customer.Email);

                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@Password", customer.Password);
                cmd.Parameters.AddWithValue("@DateUpdated", customer.Date_Updated).Value = date;

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


        internal static OTP GetUserOtp(string code,string email)
        {
            var res = new OTP();


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

                using (SqlCommand command = new SqlCommand($"GetUserOtp", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Code", code);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader is not null)
                        {
                            while (reader.Read())
                            {
                                 res.ID= (int)reader["ID"];
                                res.Code = reader["Code"].ToString().Trim();
                                res.Email = reader["Email"].ToString().Trim();
                                res.Status = reader["Status"].ToString().Trim();
                                res.DateCreated = (DateTime)reader.GetDateTime("DateCreate");

                           }
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

            //Console.WriteLine("\nDone. Press enter.");
            //Console.ReadLine();



            return res;
        }


        internal static AppUser GetAppUser(string appID,string secret)
        {
            var res = new AppUser();


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

                using (SqlCommand command = new SqlCommand($"AppUSer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AppID", appID);
                    command.Parameters.AddWithValue("@AppSecret", secret);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader is not null)
                        {
                            while (reader.Read())
                            {
                                 res.AppID= reader["AppID"].ToString().Trim();
                                res.AppSecret = reader["AppSecret"].ToString().Trim();
                               
                           }
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

            //Console.WriteLine("\nDone. Press enter.");
            //Console.ReadLine();



            return res;
        }

        internal static string SaveOTP(object email, object otp)
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

                SqlCommand cmd = new SqlCommand("SaveOtp", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Code", otp);
                cmd.Parameters.AddWithValue("@DateCreate", date);
                cmd.Parameters.AddWithValue("@Status", "Pending");
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

        internal static string UpdateOTP(string email, string code,string status)
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

                SqlCommand cmd = new SqlCommand($"Update [dbo].[OTPVerify] set [Status]=@Status where  Email='{email}' and Code={code}", connection);

                cmd.Parameters.AddWithValue("@Status", status);
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

        internal static string DeleteAccount(User user)
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
                SqlCommand cmd = new SqlCommand("DeleteAccount", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", user.Email);


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

        internal static string RateDriver(Driver driver)
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

                SqlCommand cmd = new SqlCommand($"dbo.RateDriver", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", driver.Email.ToLower());
                cmd.Parameters.AddWithValue("@Rating", driver.Rating);
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
        internal static Driver GetDriver(string email)
        {
            var res = new Driver();


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

                using (SqlCommand command = new SqlCommand($"GetDriver", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader is not null)
                        {
                            while (reader.Read())
                            {
                                res.Rating = (int)reader["Rating"];
                                res.Email = reader["Email"].ToString().Trim();
                                res.CarID = reader.IsDBNull("CarID") ? null : reader["CarID"].ToString().Trim();

                            }
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

            //Console.WriteLine("\nDone. Press enter.");
            //Console.ReadLine();



            return res;
        }

        internal static string SaveRider(string email)
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

                SqlCommand cmd = new SqlCommand($"dbo.SaveRider", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", email);
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
        internal static string UpdateDriverRating(Driver driver)
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

                SqlCommand cmd = new SqlCommand($"dbo.UpdateDriver", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", driver.Email.ToLower());
                cmd.Parameters.AddWithValue("@Rating", driver.Rating);
                cmd.Parameters.AddWithValue("@CarID", driver.CarID ?? (object)DBNull.Value);

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
        //internal static object UpdateLogin(User user)
        //{
        //    string result = "01";

        //    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        //    builder.DataSource = "rosabeldbserver.database.windows.net";
        //    builder.UserID = "rosabelDB";
        //    builder.Password = "Mololuwa@14";
        //    builder.InitialCatalog = "RockyConnectDB";

        //    SqlConnection connection = new SqlConnection(builder.ConnectionString);

        //    Console.WriteLine("\nQuery data example:");
        //    Console.WriteLine("=========================================\n");
        //    int ret = 6;
        //    connection.Open();
        //    try
        //    {
        //        DateTime date = DateTime.Now;

        //        SqlCommand cmd = new SqlCommand($"Update [dbo].[LoginInfo] set [FirstName]=@FirstName, [LastName] = @LastName,[PhoneNumber]=@PhoneNumber,[Password]=@Password,[DateUpdated] = @DateUpdated where  Email='{customer.Email}'", connection);
        //        cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
        //        cmd.Parameters.AddWithValue("@LastName", customer.LastName);
        //        cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
        //        cmd.Parameters.AddWithValue("@Password", customer.Password);
        //        cmd.Parameters.AddWithValue("@DateUpdated", customer.Date_Updated).Value = date;

        //        ret = cmd.ExecuteNonQuery();
        //        if (ret == 1)
        //        {
        //            result = "00";
        //        }



        //    }
        //    catch (SqlException e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //    finally
        //    {
        //        if (connection.State != ConnectionState.Closed)
        //        {
        //            connection.Close();

        //        }
        //    }

        //    //Console.WriteLine("\nDone. Press enter.");
        //    //Console.ReadLine();



        //    return result;
        //}
    }
}
