using System;
using RockyConnectBackend.Model;
using System.Data;
using Microsoft.Data.SqlClient;

namespace RockyConnectBackend.Data
{
	public class UserData
	{
		public UserData()
		{
		}
        public static LoginUser LoginData(string value)
        {
            var result = new LoginUser();


            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "rosabeldbserver.database.windows.net";
            builder.UserID = "rosabelDB";
            builder.Password = "Mololuwa@14";
            builder.InitialCatalog = "RockyConnectDB";

            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");
            int role = 0;
            connection.Open();
            try
            {

                using (SqlCommand command = new SqlCommand($"SELECT FirstName,Password,Email,Role FROM[dbo].[LoginInfo] where Email = '{value.ToLower()}'", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            //  result.UserID = int.Parse(reader["UserID"]);
                            result.Email = reader["Email"].ToString().Trim();
                            result.FirstName = reader["FirstName"].ToString().Trim();
                            result.Password = reader["Password"].ToString().Trim();
                            role= Convert.ToInt32(reader.GetOrdinal("Role"));
                            result.Role = (Role)role;


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



            return result;
        }
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

                cmd.Parameters.AddWithValue("@UserID", customer.UserID);
                cmd.Parameters.AddWithValue("@Password", customer.Password.ToLower());
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

                using (SqlCommand command = new SqlCommand($"SELECT * FROM [dbo].[Customer] where Email = '{email}'", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                              //  res.Year = reader["Year"].ToString().Trim();
                               // res.UserID = Convert.ToInt32(reader.GetOrdinal("UserID"));
                                res.FirstName = reader["FirstName"].ToString().Trim();
                                res.LastName = reader["LastName"].ToString().Trim();
                                res.Email = reader["Email"].ToString().Trim();
                                res.PhoneNumber = reader["PhoneNumber"].ToString().Trim();
                                res.Password = reader["Password"].ToString().Trim();
                                // res.CompanyCertificate = reader["CompanyCertificate"].ToString().Trim();
                                res.AccountVerified = Convert.ToInt32(reader.GetOrdinal("AccountVerified"));
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
                SqlCommand cmd = new SqlCommand($"Update [dbo].[Customer] set [AccountVerified]=@AccountVerified, [DateVerified] =@DateVerify where  [Email]='{user.Email}'", connection);
                cmd.Parameters.AddWithValue("@AccountVerified", user.AccountVerified);
                cmd.Parameters.AddWithValue("@DateVerify", user.Date_Verified);


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

                SqlCommand cmd = new SqlCommand($"Update [dbo].[Customer] set [FirstName]=@FirstName, [LastName] = @LastName,[PhoneNumber]=@PhoneNumber,[Password]=@Password,[DateUpdated] = @DateUpdated where  Email='{customer.Email}'", connection);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@Password", customer.Password);
                cmd.Parameters.AddWithValue("@DateUpdated", customer.Date_Updated).Value = date;

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

                using (SqlCommand command = new SqlCommand($"  SELECT  ID, Code, Email, Status, DateCreated FROM OTPVerify  where Email = '{email}' and Code='{code}'", connection))
                {
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
                                res.DateCreated = (DateTime)reader.GetDateTime("DateCreated");

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

                SqlCommand cmd = new SqlCommand($"INSERT INTO [dbo].[OTPVerify] ([Email],[Code],[DateCreated],[Status]) VALUES (@Email,@Code,@DateCreated,@Status)", connection);

                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Code", otp);
                cmd.Parameters.AddWithValue("@DateCreated", date);
                cmd.Parameters.AddWithValue("@Status", "Pending");
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
                SqlCommand cmd = new SqlCommand($"Update [dbo].[Customer] set [IsAccountActive]=@IsAccountActive, [DateUpdated] =@Date_Updated where  [Email]='{user.Email}'", connection);
                cmd.Parameters.AddWithValue("@IsAccountActive", user.IsAccountActive);
                cmd.Parameters.AddWithValue("@Date_Updated", user.Date_Updated);


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
