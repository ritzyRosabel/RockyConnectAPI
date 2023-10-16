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

        internal static Response GetPaymentCardList(SavedCardsRequest customer)
        {
            var status = new Response();
            List<PaymentCard> result = PaymentData.SelectEmailCards(customer.Email);
            if (result.Count >= 1)
            {
                status.statusCode = "00";
                status.status = "Successfull";
            }
            else
            {

                status.statusCode = "01";
                status.status = "Record not found";
            }
            return status;
        }
        internal static Response GetPaymentCard(SavedCardRequest customer)
        {
            var status = new Response();
            PaymentCard result = PaymentData.SelectCardData(customer);
            if (result.CardAlias is not null)
            {
                status.statusCode = "00";
                status.status = "Successfull";
                status.data = result;
            }
            else
            {

                status.statusCode = "01";
                status.status = "Record not found";
            }
            return status;
        }


        internal static Response UpdatePaymentCard(CardUpdate customer)
        {
            var card = new SavedCardRequest();
            card.Email = customer.Email;
            card.CardAlias = customer.CardAlias;
            var status = new Response();
            PaymentCard result = PaymentData.SelectCardData(card);
            if (result.CardAlias is not null)
            {
                string result2 = PaymentData.UpdateCardData(customer);
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
                status.status = "Record not found";
            }
            return status;
        }
        internal static Response DeletePaymentCard(SavedCardRequest customer)
        {
            var card = new SavedCardRequest();
            card.Email = customer.Email;
            card.CardAlias = customer.CardAlias;
            var status = new Response();
            PaymentCard result = PaymentData.SelectCardData(customer);
            if (result.CardAlias is not null)
            {
                string result2 = PaymentData.DeleteCardData(customer);
                if (result2 == "00")
                {
                    status.statusCode = "00";
                    status.status = "Successfully deleted";
                }
                else
                {

                    status.statusCode = "01";
                    status.status = "UnSuccessfully deleted";
                }
            }
            else
            {

                status.statusCode = "01";
                status.status = "Record not found";
            }
            return status;
        }

        internal static Response MakePayment(PaymentRequest card)
        {                string result;

            var status = new Response();
            Payment pay = new Payment()
            {
                ID = UtilityService.UniqueIDGenerator(),
                DriOwnEmail = card.DrivOwnEmail,
                RidRentEmail = card.RidRentEmail,
                Bill = card.Bill,
                TripID = card.TripID,

            };

            if (card.CardAlias == string.Empty)
            {
                pay.PaymentType = "Card";
                string pan = card.Card.Pan;
                char[] panC = pan.ToCharArray();
               
                if (panC[0]==3 & panC[15] == 1)
                {
                    pay.PaymentStatus = "Completed";
                     result = PaymentData.MakePayment(pay);
                
                        status.statusCode = "00";
                        status.status = "Payment Successfull";
                    
                     
                }
                else
                {

                    status.statusCode = "01";
                    status.status = "Payment Failure";
                }
            }
            else
            {
                SavedCardRequest savedCard = new SavedCardRequest()
                {
                    Email = card.RidRentEmail,
                    CardAlias = card.CardAlias
                };
                var res = PaymentService.GetPaymentCard(savedCard);
                if (res.data != null) {

                    PaymentCard card1 = new PaymentCard();

                    card1 = (PaymentCard)res.data;
                    string pan = card1.Pan;
                    char[] panC = pan.ToCharArray();

                    if (panC[0] == 3 & panC[15] == 1)
                    {
                        pay.PaymentStatus = "Completed";
                        result = PaymentData.MakePayment(pay);
                        status.statusCode = "00";
                        status.status = "Payment Successfull";
                    }
                    else
                    {

                        status.statusCode = "01";
                        status.status = "Payment Failure";
                    }
                }


            }
            return status;
        }
    }

}