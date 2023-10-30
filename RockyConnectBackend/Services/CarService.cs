using System;
using RockyConnectBackend.Data;
using RockyConnectBackend.Model;

namespace RockyConnectBackend.Services
{
	public class CarService
	{
		public CarService()
		{
		}

        internal static Response CreateCard(CarRequest carR)
        {
            var status = new Response();
            Car car = new Car()
            {
                CarColor = carR.CarColor,
                CarMake = carR.CarMake,
                Email = carR.Email,
                ID = UtilityService.UniqueIDGenerator(),
                CarModel = carR.CarModel,
                CarPreferences = carR.CarPreferences,
                DriverLiscense = carR.DriverLiscense,
                PlateNumber = carR.PlateNumber,
                TypeOfVehicle=carR.TypeOfVehicle
            };
            string result = CarData.CreateCarData(car);
            if (result == "00")
            {
                status.statusCode = "00";
                status.status = "Successfully Added";
            }
            else
            {

                status.statusCode = "01";
                status.status = "Failed to add Card";
            }
            return status;
        }

        internal static Response GetCar(string? email)
        {
            var status = new Response();
            Car result = CarData.SelectCarData(email);
            if (result.Email is not null)
            {
                status.statusCode = "00";
                status.status = "Successfull";
                status.data = result;
            }
            else
            {

                status.statusCode = "01";
                status.status = "No card saved with this name on this account";
            }
            return status;
        }

        internal static Response UpdateCar(CarRequest car)
        {
            var cars = new Car();
            var status = new Response();
            Car result = CarData.SelectCarData(car.Email);
            if (result.Email is not null)
            {

                result.CarColor = car.CarColor;
                result.CarMake = car.CarMake;
                result.CarModel = car.CarModel;
                result.CarPreferences = car.CarPreferences;
                result.DriverLiscense = car.DriverLiscense;
                result.PlateNumber = car.PlateNumber;
                result.TypeOfVehicle = car.TypeOfVehicle;


                string result2 = CarData.UpdateCarData(result);
                if (result2 == "00")
                {
                    status.statusCode = "00";
                    status.status = "Successfully saved";
                }
                else
                {

                    status.statusCode = "01";
                    status.status = "UnSuccessfully saved";
                }
            }
            else
            {

                status.statusCode = "01";
                status.status = "No Card exist with that Alias on this account";
            }
            return status;
        }
        
     

    }

}

