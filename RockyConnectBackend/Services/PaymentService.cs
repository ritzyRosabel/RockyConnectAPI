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

        internal static Response PaymentReminder(string email, string id )
        {
            var status = new Response();

             
            SuperTrip result = TripData.SelectSuperTripData(id);
            string message2 = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\"><head>\n  <meta charset=\"UTF-8\">\n  <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\">\n  <meta name=\"x-apple-disable-message-reformatting\">\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n  <meta content=\"telephone=no\" name=\"format-detection\">\n  <title></title>\n  <!--[if (mso 16)]>\n    <style type=\"text/css\">\n    a {{text-decoration: none;}}\n    </style>\n    <![endif]-->\n  <!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]-->\n  <!--[if gte mso 9]>\n<xml>\n    <o:OfficeDocumentSettings>\n    <o:AllowPNG></o:AllowPNG>\n    <o:PixelsPerInch>96</o:PixelsPerInch>\n    </o:OfficeDocumentSettings>\n</xml>\n<![endif]-->\n <!--[if mso]>\n <style type=\"text/css\">\n     ul {{\n  margin: 0 !important;\n  }}\n  ol {{\n  margin: 0 !important;\n  }}\n  li {{\n  margin-left: 47px !important;\n  }}\n\n </style><![endif]\n--></head>\n <body class=\"body\">\n  <div dir=\"ltr\" class=\"es-wrapper-color\">\n   <!--[if gte mso 9]>\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\n\t\t\t\t<v:fill type=\"tile\" color=\"#fafafa\"></v:fill>\n\t\t\t</v:background>\n\t\t<![endif]-->\n   <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\n    <tbody>\n     <tr>\n      <td class=\"esd-email-paddings\" valign=\"top\">\n       \n       \n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe\" align=\"center\">\n           <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p30t es-p30b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-image es-p10t es-p10b\" style=\"font-size: 0px;\"><a target=\"_blank\"><img src=\"https://fblkeki.stripocdn.email/content/guids/CABINET_67e080d830d87c17802bd9b4fe1c0912/images/55191618237638326.png\" alt=\"\" style=\"display: block;\" width=\"100\"></a></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10b es-m-txt-c\"><h1 style=\"font-size: 46px; line-height: 100%;\">Trip Payment Reminder</h1></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Hi {result.RiderFirstName}, You’ve received this message as a reminder to make payment for your upcoming trip to {result.Destination}. Please Login to pay or ignore message if payments have already been made.&nbsp;</p></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10t es-p5b\"><p>If you did not register with us, please disregard this email.</p></td>\n                     </tr>\n                     \n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Rocky Connect... Nowhere is beyond reach</p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-footer\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-footer-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"640\" style=\"background-color: transparent;\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20t es-p20b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"600\" class=\"esd-container-frame\" align=\"left\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-social es-p15t es-p15b\" style=\"font-size:0\">\n                       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-table-not-adapt es-social\">\n                        <tbody>\n                         <tr>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Facebook\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png\" alt=\"Fb\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"X.com\" src=\"https://localfiles.stripocdn.email/content/assets/img/social-icons/logo-black/x-logo-black.png\" alt=\"X\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Instagram\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png\" alt=\"Inst\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\"><a target=\"_blank\" href=\"\"><img title=\"Youtube\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png\" alt=\"Yt\" width=\"32\"></a></td>\n                         </tr>\n                        </tbody>\n                       </table></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p35b\"><p>&nbsp;© 2021 Rocky Connect, Inc. All Rights Reserved.</p><p>1543 West Jackson st, Macomb, Illinois, US, 61455</p></td>\n                     </tr>\n                     \n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content esd-footer-popover\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"background-color: transparent;\" bgcolor=\"rgba(0, 0, 0, 0)\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-infoblock\"><p><a target=\"_blank\"></a>No longer want to receive these emails?&nbsp;<a href=\"\" target=\"_blank\">Unsubscribe</a>.<a target=\"_blank\"></a></p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table></td>\n     </tr>\n    </tbody>\n   </table>\n  </div>\n \n</body></html>";
           string res =  UtilityService.SendEmail(message2, result.CustomerEmail, "RockyConnect Trip Update");
            if (res == "00")
            {
                status.statusCode = "00";
                status.status = "Email reminder successfully sent";
            }
            else {
                status.statusCode = "01";
                status.status = "Failed to send Email reminder";

            }
            return status;


        }
    }

}