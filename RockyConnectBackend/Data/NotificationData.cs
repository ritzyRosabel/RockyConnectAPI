using System;
using Microsoft.Data.SqlClient;
using RockyConnectBackend.Model;
using System.Data;

namespace RockyConnectBackend.Data
{
	public class NotificationData
	{
        internal static List<Notification> SelectNotificationData(string email)
        {

            var res = new List<Notification>();

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
                using (SqlCommand cmd = new SqlCommand($"SelectNotification", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var result = new Notification();

                            //  result.UserID = int.Parse(reader["UserID"]);
                            result.Email = reader["Email"].ToString().Trim();
                            result.Title = reader["Title"].ToString().Trim();
                            result.Body = reader["Body"].ToString().Trim();
                            result.NotificationID = reader["NotificationID"].ToString().Trim();
                            result.DateSent = Convert.ToDateTime(reader.GetDateTime("DateSent"));
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

        internal static string CreateNotificationData(Notification notify)
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

                SqlCommand cmd = new SqlCommand($"CreateNotification", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NotificationID", notify.NotificationID);
                cmd.Parameters.AddWithValue("@Body", notify.Body);
                cmd.Parameters.AddWithValue("@Title", notify.Title);
                cmd.Parameters.AddWithValue("@Email", notify.Email);
                cmd.Parameters.AddWithValue("@DateSent", notify.DateSent);
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

