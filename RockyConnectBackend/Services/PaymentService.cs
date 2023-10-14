using System;
using System.Net.NetworkInformation;
using RockyConnectBackend.Data;
using RockyConnectBackend.Model;

namespace RockyConnectBackend.Services
{
	public class PaymentService
	{
		public PaymentService()
		{
		}

        internal static Response CreateCard(PaymentCard customer)
        {
            var status = new Response();
            string result = PaymentData.CreateCardData(customer);
            if (result == "00")
            {
                status.statusCode = "00";
                status.status = "Successfull";
            }
            else
            {

                status.statusCode = "01";
                status.status = "UnSuccessfull";
            }
            return status;
        }

}

