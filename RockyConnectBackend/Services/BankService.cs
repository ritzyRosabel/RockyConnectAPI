using System;
using RockyConnectBackend.Data;
using RockyConnectBackend.Model;

namespace RockyConnectBackend.Services
{
	public class BankService
    {
        internal static Response CreateBank(Bank bank)
        {
            var status = new Response();
           
            string result = BankData.CreateBankData(bank);
            if (result == "00")
            {
                status.statusCode = "00";
                status.status = "Successfully Added";
            }
            else
            {

                status.statusCode = "01";
                status.status = "Failed to add Bank Detail";
            }
            return status;
        }

        internal static Response GetBank(string? email)
        {
            var status = new Response();
            Bank result = BankData.SelectBankData(email);
            if (result.Email is not null)
            {
                status.statusCode = "00";
                status.status = "Successfull";
                status.data = result;
            }
            else
            {

                status.statusCode = "01";
                status.status = "No Bank Detail is tied to this account";
            }
            return status;
        }

        internal static Response UpdateBank(Bank bank)
        {
            var cars = new Car();
            var status = new Response();
            Bank result = BankData.SelectBankData(bank.Email);
            if (result.Email is not null)
            {
                result.BankName = bank.BankName;
                result.AccountNumber = bank.AccountNumber;
                result.RoutingNumber = bank.RoutingNumber;


                string result2 = BankData.UpdateBankData(result);
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
                status.status = "No Bank Detail is tied to this account";
            }
            return status;
        }



    }

}

