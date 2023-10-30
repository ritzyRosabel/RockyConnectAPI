using System;
namespace RockyConnectBackend.Model
{
    public class Car
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string CarColor { get; set; }
        public string PlateNumber { get; set; }
        public string TypeOfVehicle { get; set; }
        public string DriverLiscense { get; set; }
        public string CarPreferences { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }

    }
    public class CarRequest
    {
        public string Email { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string CarColor { get; set; }
        public string PlateNumber { get; set; }
        public string TypeOfVehicle { get; set; }
        public string DriverLiscense { get; set; }
        public string CarPreferences { get; set; }

    }
}

