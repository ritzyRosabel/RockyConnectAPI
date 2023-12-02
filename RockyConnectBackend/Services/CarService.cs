using System;
using RockyConnectBackend.Data;
using RockyConnectBackend.Model;

namespace RockyConnectBackend.Services
{
	public  static class CarService
	{
		
        public static Response CreateCard(CarRequest carR)
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
                Driver res = UserData.GetDriver(carR.Email);
                if (res.Email is not null)
                {
                    res.CarID = car.ID;
                    UserData.UpdateDriverRating(res);

                }

                status.statusCode = "00";
                status.status = "Successfully Added";
            }
            else
            {

                status.statusCode = "01";
                status.status = "Failed to add Car";
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
                status.status = "No car is tied to this account";
            }
            return status;
        }

        internal static Response UpdateCar(CarRequest car)
        {
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
                Car cars = new Car()
                {
                    CarColor = car.CarColor,
                    CarMake = car.CarMake,
                    Email = car.Email,
                    ID = UtilityService.UniqueIDGenerator(),
                    CarModel = car.CarModel,
                    CarPreferences = car.CarPreferences,
                    DriverLiscense = car.DriverLiscense,
                    PlateNumber = car.PlateNumber,
                    TypeOfVehicle = car.TypeOfVehicle
                };
                string results = CarData.CreateCarData(cars);
                if (results == "00")
                {
                    Driver res = UserData.GetDriver(car.Email);
                    if (res.Email is not null)
                    {
                        res.CarID = cars.ID;
                        UserData.UpdateDriverRating(res);

                    }

                    status.statusCode = "00";
                    status.status = "Successfully Added";
                }
                else
                {

                    status.statusCode = "01";
                    status.status = "Failed to add Car";
                }

                status.statusCode = "01";
                status.status = "No Car is tied to this account";
            }
            return status;
        }
        
     

    }

}

