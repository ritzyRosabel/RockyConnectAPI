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

        internal static Response CreateCard(PaymentCardRequest customer)
        {
            var status = new Response();
            PaymentCard card = new PaymentCard()
            {
                CardAlias = customer.CardAlias,
                Pan = customer.Pan,
                CardType = customer.CardType,
                Code = UtilityService.RandomOTPGenerator(),
                Email = customer.Email,
                ExpiryDate = customer.ExpiryDate,
                FullName = customer.FullName
            };
            string result = PaymentData.CreateCardData(card);
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

        internal static Response GetPaymentCardList(SavedCardsRequest customer)
        {
            var status = new Response();
            List<PaymentCard> result = PaymentData.SelectEmailCards(customer.Email);
            if (result.Count >= 1)
            {
                status.statusCode = "00";
                status.status = "Successfull";
                status.data = result;
            }

            else
            {

                status.statusCode = "01";
                status.status = "No card saved in this account";
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
                status.status = "No card saved with this name on this account";
            }
            return status;
        }


        internal static Response UpdatePaymentCard(CardUpdate customer)
        {
            var card = new SavedCardRequest();
            card.Email = customer.Email;
            card.CardAlias = customer.OldCardAlias;
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
                status.status = "No Card exist with that Alias on this account";
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
                    status.status = "Failed to delete Card, Try again";
                }
            }
            else
            {

                status.statusCode = "01";
                status.status = "No Card exist with that Alias on this account";
            }
            return status;
        }

        internal static Response MakePayment(PaymentRequest card)
        {    var status = new Response();

            string result;
            Trip resultTrip =TripData.SelectTripData(card.TripID);
              if (resultTrip.PaymentID is null)
                {
                if (resultTrip.TripCost != card.Bill)
                {
                    status.statusCode = "01";
                    status.status = "Payment Failure, Please pay exact amount on your bill";
                    return status;
                }
                    Payment pay = new Payment()
                    {
                        ID = UtilityService.UniqueIDGenerator(),
                        DriOwnEmail = card.DrivOwnEmail,
                        RidRentEmail = card.RidRentEmail,
                        Bill = card.Bill,
                        TripID = card.TripID,

                    };

          
                if (!card.SavedCard)
                {
                    pay.PaymentType = card.Card.CardType;
                    string pan = card.Card.Pan;
                    string f = pan[0].ToString();
                    string l = pan[15].ToString();
                    //string[] panC = pan.Split().Split();
                    if (f == "3" && l== "1")
                    {
                        pay.PaymentStatus = "Completed";
                        result = PaymentData.MakePayment(pay);
                        resultTrip.PaymentID = pay.ID;
                        TripData.UpdateTripData(pay.ID, resultTrip);
                        status.statusCode = "00";
                        status.status = "Payment Successfull";


                    }
                    else
                    {

                        status.statusCode = "01";
                        status.status = "Invalid Card Details";
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
                    if (res.data != null)
                    {

                        PaymentCard card1;

                        card1 = (PaymentCard)res.data;
                        string pan = card1.Pan;
                        string f = pan[0].ToString();
                        string l = pan[15].ToString();
                        //string[] panC = pan.Split().Split();
                        if (f == "3" && l == "1")
                        {
                            pay.PaymentStatus = "Completed";
                            result = PaymentData.MakePayment(pay);
                            resultTrip.PaymentID = pay.ID;
                            TripData.UpdateTripData(pay.ID, resultTrip);
                            status.statusCode = "00";
                            status.status = "Payment Successful";
                        }
                        else
                        {

                            status.statusCode = "01";
                            status.status = "Invalid Card Details";
                        }
                    }


                }
            }
            else
            {
                status.statusCode = "00";
                status.status = "Trip is already Paid.";
            }
            return status;
        }

        internal static Response Refund(string ID)
        {

            var status = new Response();
          
            Payment result = PaymentData.GetPayment(ID);
            if (result.ID is not null){
                Refund refund = new Refund()
                {
                    ID=UtilityService.RandomOTPGenerator(),
                    RefundStatus = "Successful",
                    Bill = result.Bill,
                    PaymentMethod = result.PaymentType,
                    PaymentID = result.ID


                };
                string result2 = PaymentData.MakeRefund(refund);
                if (result2 == "00")
                {
                    result.RefundID = refund.ID;
                    string result3 = PaymentData.UpdatePayment(result);
                    if (result3 == "00")
                    {
                        status.status = "Successful Refund";
                        status.statusCode = "00";
                    }
                    else
                    {
                        status.status = "Successful Refund";
                        status.statusCode = "00";
                    }
                }
                else
                {

                    status.status = "Refund Failed";
                    status.statusCode = "01";
                }

            }
            else
            {
                status.status = "Refund No record Exist";
                status.statusCode = "01";
            }
            return status;
        }
    }

}