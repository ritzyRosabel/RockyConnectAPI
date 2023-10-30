using System;
using Microsoft.Data.SqlClient;
using System.Data;
using RockyConnectBackend.Model;

namespace RockyConnectBackend.Data
{
	public class CarData
	{
		public CarData()
		{
		}

        

        internal static Car SelectCarData(string? email)
        {

            var result = new Car();

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
                using (SqlCommand cmd = new SqlCommand($"SelectCar", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email",email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {


                            //  result.UserID = int.Parse(reader["UserID"]);
                            result.Email = reader["Email"].ToString().Trim();
                            result.ID = reader["ID"].ToString().Trim();
                            result.CarPreferences = reader["CarPreferences"].ToString().Trim();
                            result.CarColor = reader["CarColor"].ToString().Trim();
                            result.CarMake = reader["CarMake"].ToString().Trim();
                            result.CarModel = reader["CarModel"].ToString().Trim();
                            result.PlateNumber = reader["PlateNumber"].ToString().Trim();
                            result.TypeOfVehicle = reader["TypeOfVehicle"].ToString().Trim();
                            result.DriverLiscense = reader["DriverLiscense"].ToString().Trim();
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

        internal static string CreateCarData(Car card)
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
                SqlCommand cmd = new SqlCommand($"CreateCar", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CarMake", card.CarMake);
                cmd.Parameters.AddWithValue("@CarColor", card.CarColor);
                cmd.Parameters.AddWithValue("@CarModel", card.CarModel);
                cmd.Parameters.AddWithValue("@ID", card.ID);
                cmd.Parameters.AddWithValue("@Email", card.Email);
                cmd.Parameters.AddWithValue("@DriverLiscense", card.DriverLiscense);
                cmd.Parameters.AddWithValue("@CarPreferences", card.CarPreferences);
                cmd.Parameters.AddWithValue("@TypeOfVehicle", card.TypeOfVehicle);
                cmd.Parameters.AddWithValue("@PlateNumber", card.PlateNumber);
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
        internal static string UpdateCarData(Car card)
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
                SqlCommand cmd = new SqlCommand("UpdateCar", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CarMake", card.CarMake);
                cmd.Parameters.AddWithValue("@CarColor", card.CarColor);
                cmd.Parameters.AddWithValue("@CarModel", card.CarModel);
                cmd.Parameters.AddWithValue("@Email", card.Email);
                cmd.Parameters.AddWithValue("@CarPreferences", card.CarPreferences);
                cmd.Parameters.AddWithValue("@TypeOfVehicle", card.TypeOfVehicle);
                cmd.Parameters.AddWithValue("@PlateNumber", card.PlateNumber);
                cmd.Parameters.AddWithValue("@DriverLiscense", card.DriverLiscense);
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

            return result;
        }
    }
}

