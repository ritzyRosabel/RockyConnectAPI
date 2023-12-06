using System;
using System.Net.NetworkInformation;
using Microsoft.Extensions.Options;
using RockyConnectBackend.Controllers;
using RockyConnectBackend.Data;
using RockyConnectBackend.Model;

namespace RockyConnectBackend.Services
{
	public  class TripService
	{
       
        public static Response CreateTrip(CreateTripRequest trip)
        {
            Trip tripD = new Trip()
            {
                CustomerEmail = trip.CustomerEmail,
                SourceLatitude = trip.SourceLatitude,
                SourceLocation = trip.SourceLocation,
                Destination = trip.Destination,
                DriverEmail = trip.DriverEmail,
                DestinationLat = trip.DestinationLat,
                DestinationLong = trip.DestinationLong,
                TripStatus = "Created",
                ID = UtilityService.UniqueIDGenerator(),
                TripCost = trip.TripDistance * 10,
                TripInitiator=trip.TripInitiator,
                Date_Created=DateTime.Now,
                Date_Updated = DateTime.Now,
                TripDate=trip.TripDate,
                TripDistance = trip.TripDistance,
                SourceLongitude=trip.SourceLongitude,
                PaymentID=null,
                TotalTime = trip.TotalTime,
                DestinationState = trip.DestinationState,
                IsRated=1,
                CancelReason = null
              
            };

            if (tripD.TripInitiator == "Driver")
            {
                tripD.CustomerEmail = null;
            }else
            {
                tripD.DriverEmail = null;
            }
            var status = new Response();

            string result = TripData.CreateTripData(tripD);
            SuperTrip user = TripData.SelectSuperTripData(tripD.ID);

            if (result == "00")
            {

                status.statusCode = "00";
                status.status = "Successfully Created";
                status.data = user;
            }
            else
            {

                status.statusCode = "01";
                status.status = "Failed to Create";
            }
            return status;
        }
        internal static Response GetDriverTrips(TripSearch trip)
        {
            var status = new Response();
            List<SuperTrip> result = TripData.SelectDriverTripList(trip);

            if (result.Count >= 1)
            {
               
                status.statusCode = "00";
                status.status = "Successfull";
                status.data = result;
            }
            else
            {

                status.statusCode = "00";
                status.status = "No trip available for your chosen field. Modify trip details or create a new trip";
            }
            return status;
        }
        internal static Response GetRiderTrips(TripSearch trip)
        {
            var status = new Response();
            List<SuperTrip> result = TripData.SelectRiderTripList(trip);

            if (result.Count >= 1)
            {
                status.statusCode = "00";
                status.status = "Successfull";
                status.data = result;
            }
            else
            {

                status.statusCode = "01";
                status.status = "No trip available for your chosen field. Modify trip details or create a new trip";
            }
            return status;
        }
        //internal static Response GetTripHistory(TripsRequest trip)
        //{
        //    var status = new Response();
        //    List<Trip> result = TripData.SelectEmailTrips(trip.Email);

        //    if (result.Count >= 1)
        //    {
        //        status.statusCode = "00";
        //        status.status = "Successfull";
        //        status.data = result;
        //    }
        //    else
        //    {

        //        status.statusCode = "01";
        //        status.status = "Record not found";
        //    }
        //    return status;
        //}
        internal static Response GetTrip(string id)
        {
            var status = new Response();
            Trip result = TripData.SelectTripData(id);
            if (result.ID is not null)
            {
                status.statusCode = "00";
                status.status = "Successfull";
                status.data = result;
            }
            else
            {

                status.statusCode = "01";
                status.status = "Record not found for this trip ID, try creating a new trip";
            }
            return status;
        }


        internal static Response ApproveRiderTrip(TripDataInfo trip, FcmNotificationSetting setting)
        {
            Trip trip1 = TripData.SelectTripData(trip.ID);
            var status = new Response();
            
            if (trip1.TripStatus== "Approved")
            {
                status.statusCode = "01";
                status.status = "trip already approved";
                return status;
            }
            if(trip1.TripInitiator == "Rider")
            {
                trip1.DriverEmail = trip.DriverEmail;

            }
            else
            {
                trip1.CustomerEmail = trip.CustomerEmail;
            }
            trip1.TripStatus = "Approved";

            trip1.Date_Updated = DateTime.Now;
           
            string result = TripData.UpdateTripData(trip1.PaymentID,trip1);
            if (result == "00")
            {
                SuperTrip user2 = TripData.SelectSuperTripData(trip.ID);
                string message2 = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\"><head>\n  <meta charset=\"UTF-8\">\n  <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\">\n  <meta name=\"x-apple-disable-message-reformatting\">\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n  <meta content=\"telephone=no\" name=\"format-detection\">\n  <title></title>\n  <!--[if (mso 16)]>\n    <style type=\"text/css\">\n    a {{text-decoration: none;}}\n    </style>\n    <![endif]-->\n  <!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]-->\n  <!--[if gte mso 9]>\n<xml>\n    <o:OfficeDocumentSettings>\n    <o:AllowPNG></o:AllowPNG>\n    <o:PixelsPerInch>96</o:PixelsPerInch>\n    </o:OfficeDocumentSettings>\n</xml>\n<![endif]-->\n <!--[if mso]>\n <style type=\"text/css\">\n     ul {{\n  margin: 0 !important;\n  }}\n  ol {{\n  margin: 0 !important;\n  }}\n  li {{\n  margin-left: 47px !important;\n  }}\n\n </style><![endif]\n--></head>\n <body class=\"body\">\n  <div dir=\"ltr\" class=\"es-wrapper-color\">\n   <!--[if gte mso 9]>\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\n\t\t\t\t<v:fill type=\"tile\" color=\"#fafafa\"></v:fill>\n\t\t\t</v:background>\n\t\t<![endif]-->\n   <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\n    <tbody>\n     <tr>\n      <td class=\"esd-email-paddings\" valign=\"top\">\n       \n       \n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe\" align=\"center\">\n           <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p30t es-p30b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-image es-p10t es-p10b\" style=\"font-size: 0px;\"><a target=\"_blank\"><img src=\"https://fblkeki.stripocdn.email/content/guids/CABINET_67e080d830d87c17802bd9b4fe1c0912/images/55191618237638326.png\" alt=\"\" style=\"display: block;\" width=\"100\"></a></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10b es-m-txt-c\"><h1 style=\"font-size: 46px; line-height: 100%;\">Your trip is approved</h1></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Hi {user2.RiderFirstName}, You’ve received this message because your trip to {user2.Destination} was approved. Login to view drivers detail.&nbsp;</p></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10t es-p5b\"><p>If you did not register with us, please disregard this email.</p></td>\n                     </tr>\n                     \n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Rocky Connect... Nowhere is beyond reach</p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-footer\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-footer-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"640\" style=\"background-color: transparent;\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20t es-p20b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"600\" class=\"esd-container-frame\" align=\"left\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-social es-p15t es-p15b\" style=\"font-size:0\">\n                       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-table-not-adapt es-social\">\n                        <tbody>\n                         <tr>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Facebook\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png\" alt=\"Fb\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"X.com\" src=\"https://localfiles.stripocdn.email/content/assets/img/social-icons/logo-black/x-logo-black.png\" alt=\"X\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Instagram\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png\" alt=\"Inst\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\"><a target=\"_blank\" href=\"\"><img title=\"Youtube\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png\" alt=\"Yt\" width=\"32\"></a></td>\n                         </tr>\n                        </tbody>\n                       </table></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p35b\"><p>&nbsp;© 2021 Rocky Connect, Inc. All Rights Reserved.</p><p>1543 West Jackson st, Macomb, Illinois, US, 61455</p></td>\n                     </tr>\n                     \n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content esd-footer-popover\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"background-color: transparent;\" bgcolor=\"rgba(0, 0, 0, 0)\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-infoblock\"><p><a target=\"_blank\"></a>No longer want to receive these emails?&nbsp;<a href=\"\" target=\"_blank\">Unsubscribe</a>.<a target=\"_blank\"></a></p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table></td>\n     </tr>\n    </tbody>\n   </table>\n  </div>\n \n</body></html>";
                UtilityService.SendEmail(message2, trip1.CustomerEmail, "RockyConnect Trip Update");
                string notify = $"Your Trip Request to {user2.Destination} has been approved";
                Notification notification = new Notification() {
                    Body = notify,
                    Title = "RockyConnect",
                    Email=user2.CustomerEmail,
                    DateSent=DateTime.Now,
                    NotificationID=UtilityService.UniqueIDGenerator()

                };
                NotificationModel model = new NotificationModel()
                {
                    Body = notify,
                    Title = "RockyConnect",
                    DeviceId=user2.RiderDeviceID
                };
              //  NotificationService service;
                NotificationService.SendNotification(model);
                NotificationData.CreateNotificationData(notification);
                status.statusCode = "00";
                status.status = "Successfully saved";
                status.data = user2;
            }
            else
            {

                status.statusCode = "01";
                status.status = "Record not found for this trip ID, try creating a new trip";
            }
            return status;
        }
        internal static Response DeclineRiderTrip(TripDataInfo trip, FcmNotificationSetting setting)
        {
            var status = new Response();
            User customer = UserData.GetUserUsingEmail(trip.CustomerEmail);
            Trip trip1 = TripData.SelectTripData(trip.ID);
            if (trip1.TripStatus == "Approved")
            {
                status.statusCode = "01";
                status.status = "trip already approved";
                return status;
            }
            if (trip1.TripInitiator == "Driver")
            {
                trip1.CustomerEmail = null;
                trip1.TripStatus = "Created";

            }
            if (trip1.TripInitiator == "Rider")
            {
                status.statusCode = "01";
                status.status = "Rider initiated trip cannot decline request";
                return status;

            }

            trip1.Date_Updated = DateTime.Now;

            string result = TripData.UpdateTripData(trip1.PaymentID, trip1);
            if (result == "00")
            {
                SuperTrip user2 = TripData.SelectSuperTripData(trip.ID);
                string message2 = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\"><head>\n  <meta charset=\"UTF-8\">\n  <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\">\n  <meta name=\"x-apple-disable-message-reformatting\">\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n  <meta content=\"telephone=no\" name=\"format-detection\">\n  <title></title>\n  <!--[if (mso 16)]>\n    <style type=\"text/css\">\n    a {{text-decoration: none;}}\n    </style>\n    <![endif]-->\n  <!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]-->\n  <!--[if gte mso 9]>\n<xml>\n    <o:OfficeDocumentSettings>\n    <o:AllowPNG></o:AllowPNG>\n    <o:PixelsPerInch>96</o:PixelsPerInch>\n    </o:OfficeDocumentSettings>\n</xml>\n<![endif]-->\n <!--[if mso]>\n <style type=\"text/css\">\n     ul {{\n  margin: 0 !important;\n  }}\n  ol {{\n  margin: 0 !important;\n  }}\n  li {{\n  margin-left: 47px !important;\n  }}\n\n </style><![endif]\n--></head>\n <body class=\"body\">\n  <div dir=\"ltr\" class=\"es-wrapper-color\">\n   <!--[if gte mso 9]>\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\n\t\t\t\t<v:fill type=\"tile\" color=\"#fafafa\"></v:fill>\n\t\t\t</v:background>\n\t\t<![endif]-->\n   <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\n    <tbody>\n     <tr>\n      <td class=\"esd-email-paddings\" valign=\"top\">\n       \n       \n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe\" align=\"center\">\n           <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p30t es-p30b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-image es-p10t es-p10b\" style=\"font-size: 0px;\"><a target=\"_blank\"><img src=\"https://fblkeki.stripocdn.email/content/guids/CABINET_67e080d830d87c17802bd9b4fe1c0912/images/55191618237638326.png\" alt=\"\" style=\"display: block;\" width=\"100\"></a></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10b es-m-txt-c\"><h1 style=\"font-size: 46px; line-height: 100%;\">Your trip is Declined</h1></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Hi {user2.RiderFirstName}You’ve received this message because your trip to {user2.Destination} was Declined. Login to request a driver.&nbsp;</p></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10t es-p5b\"><p>If you did not register with us, please disregard this email.</p></td>\n                     </tr>\n                     \n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Rocky Connect... Nowhere is beyond reach</p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-footer\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-footer-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"640\" style=\"background-color: transparent;\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20t es-p20b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"600\" class=\"esd-container-frame\" align=\"left\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-social es-p15t es-p15b\" style=\"font-size:0\">\n                       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-table-not-adapt es-social\">\n                        <tbody>\n                         <tr>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Facebook\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png\" alt=\"Fb\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"X.com\" src=\"https://localfiles.stripocdn.email/content/assets/img/social-icons/logo-black/x-logo-black.png\" alt=\"X\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Instagram\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png\" alt=\"Inst\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\"><a target=\"_blank\" href=\"\"><img title=\"Youtube\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png\" alt=\"Yt\" width=\"32\"></a></td>\n                         </tr>\n                        </tbody>\n                       </table></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p35b\"><p>&nbsp;© 2021 Rocky Connect, Inc. All Rights Reserved.</p><p>1543 West Jackson st, Macomb, Illinois, US, 61455</p></td>\n                     </tr>\n                     \n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content esd-footer-popover\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"background-color: transparent;\" bgcolor=\"rgba(0, 0, 0, 0)\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-infoblock\"><p><a target=\"_blank\"></a>No longer want to receive these emails?&nbsp;<a href=\"\" target=\"_blank\">Unsubscribe</a>.<a target=\"_blank\"></a></p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table></td>\n     </tr>\n    </tbody>\n   </table>\n  </div>\n \n</body></html>";
                UtilityService.SendEmail(message2, trip.CustomerEmail, "RockyConnect Trip Update");
                string notify = $"Your Trip Request to {user2.Destination} has been declined";
                Notification notification = new Notification()
                {
                    Body = notify,
                    Title = "RockyConnect",
                    Email = customer.Email,
                    DateSent = DateTime.Now,
                    NotificationID = UtilityService.UniqueIDGenerator()


                };

                NotificationModel model = new NotificationModel()
                {
                    Body = notify,
                    Title = "RockyConnect",
                    DeviceId = customer.DeviceID
                };
               NotificationService.SendNotification(model);
                NotificationData.CreateNotificationData(notification);
                status.statusCode = "00";
                status.status = "Successfully saved";
                status.data = user2;

            }
            else
            {

                status.statusCode = "01";
                status.status = "unsuccessful update on trip, try again";
            }
            return status;
        }
        internal static Response DeleteTrip(CancelRequest usertrip)
        {
            var status = new Response();
            Trip trip = TripData.SelectTripData(usertrip.ID);
            if(trip.ID is not null)
            {
                if (trip.TripStatus == "Cancelled")
                {
                    status.status = "Trip already Cancelled";
                    status.statusCode = "00";
                    return status;
                }
                if (trip.TripStatus == "Created")
                {
                    trip.TripStatus = "Cancelled";

                }
                if (trip.TripStatus == "Requested")
                {
                    if (usertrip.Role == Role.driver)
                    {
                        trip.TripStatus = "Cancelled";

                    }
                    else {
                        trip.TripStatus = "Created";
                        trip.CustomerEmail = null;
                    }
                }
                else if (trip.TripStatus == "Approved" && trip.PaymentID is not null)
                {
                    PaymentService.Refund(trip.PaymentID);
                    if (usertrip.Role == Role.driver && trip.TripInitiator=="Driver")
                    {
                        trip.TripStatus = "Cancelled";

                    }
                    else if (usertrip.Role==Role.rider && trip.TripInitiator=="Rider")
                    {
                        trip.TripStatus = "Cancelled";
                    }
                    else
                    {
                        trip.TripStatus = "Created";
                        if (trip.TripInitiator == "Driver")
                        {
                            trip.CustomerEmail = null;
                        }
                        else
                        {
                            trip.DriverEmail = null;
                        }


                    }

                }else if (trip.TripStatus == "Approved" && trip.PaymentID is null)
                {
                    if (usertrip.Role == Role.driver && trip.TripInitiator == "Driver")
                    {
                        trip.TripStatus = "Cancelled";

                    }
                    else if (usertrip.Role == Role.rider && trip.TripInitiator == "Rider")
                    {
                        trip.TripStatus = "Cancelled";
                    }
                    else
                    {
                        trip.TripStatus = "Created";
                        if (trip.TripInitiator == "Driver")
                        {
                            trip.CustomerEmail = null;
                        }
                        else
                        {
                            trip.DriverEmail = null;
                        }


                    }
                }
                else if (trip.TripStatus == "Completed" || trip.TripStatus == "Enroute")
                {
                    status.status = "Trip Cannot be Cancelled";
                    status.statusCode = "01";                               
                    return status;
                }

            }
            else {
                status.statusCode = "01";
                status.status = "Trip does't exist";
                return status;
            }

            trip.CancelReason = usertrip.ReasonForCancel;
            trip.Date_Updated = DateTime.Now;
            string result =   TripData.UpdateTripData(trip.PaymentID,trip);
            if (result =="00")
            {
               
                    status.statusCode = "00";
                status.status = "Successfully Cancelled";
            }
            else
            {

                status.statusCode = "01";
                status.status = "Trip does't exist";
            }
            return status;
        }

        internal static Response StartTrip(TripRequest trip, FcmNotificationSetting setting)
        {   var status = new Response();

            Trip trip1 = TripData.SelectTripData(trip.ID);
            if (trip1.TripStatus == "Approved" && trip1.PaymentID is not null)
            {

                trip1.TripStatus = "Enroute";


                trip1.Date_Updated = DateTime.Now;

             
                string result = TripData.UpdateTripData(trip1.PaymentID, trip1);
                if (result == "00")
                {

                    SuperTrip user = TripData.SelectSuperTripData(trip.ID);
                    string message = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\"><head>\n  <meta charset=\"UTF-8\">\n  <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\">\n  <meta name=\"x-apple-disable-message-reformatting\">\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n  <meta content=\"telephone=no\" name=\"format-detection\">\n  <title></title>\n  <!--[if (mso 16)]>\n    <style type=\"text/css\">\n    a {{text-decoration: none;}}\n    </style>\n    <![endif]-->\n  <!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]-->\n  <!--[if gte mso 9]>\n<xml>\n    <o:OfficeDocumentSettings>\n    <o:AllowPNG></o:AllowPNG>\n    <o:PixelsPerInch>96</o:PixelsPerInch>\n    </o:OfficeDocumentSettings>\n</xml>\n<![endif]-->\n <!--[if mso]>\n <style type=\"text/css\">\n     ul {{\n  margin: 0 !important;\n  }}\n  ol {{\n  margin: 0 !important;\n  }}\n  li {{\n  margin-left: 47px !important;\n  }}\n\n </style><![endif]\n--></head>\n <body class=\"body\">\n  <div dir=\"ltr\" class=\"es-wrapper-color\">\n   <!--[if gte mso 9]>\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\n\t\t\t\t<v:fill type=\"tile\" color=\"#fafafa\"></v:fill>\n\t\t\t</v:background>\n\t\t<![endif]-->\n   <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\n    <tbody>\n     <tr>\n      <td class=\"esd-email-paddings\" valign=\"top\">\n       \n       \n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe\" align=\"center\">\n           <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p30t es-p30b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-image es-p10t es-p10b\" style=\"font-size: 0px;\"><a target=\"_blank\"><img src=\"https://fblkeki.stripocdn.email/content/guids/CABINET_67e080d830d87c17802bd9b4fe1c0912/images/55191618237638326.png\" alt=\"\" style=\"display: block;\" width=\"100\"></a></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10b es-m-txt-c\"><h1 style=\"font-size: 46px; line-height: 100%;\">Your trip is Enroute</h1></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Hi {user.RiderFirstName}You’ve received this message because your trip to {user.Destination} is Enroute. We wish you a safe journey and we hope you have a lovely trip.&nbsp;</p></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10t es-p5b\"><p>If you did not register with us, please disregard this email.</p></td>\n                     </tr>\n                     \n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Rocky Connect... Nowhere is beyond reach</p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-footer\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-footer-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"640\" style=\"background-color: transparent;\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20t es-p20b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"600\" class=\"esd-container-frame\" align=\"left\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-social es-p15t es-p15b\" style=\"font-size:0\">\n                       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-table-not-adapt es-social\">\n                        <tbody>\n                         <tr>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Facebook\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png\" alt=\"Fb\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"X.com\" src=\"https://localfiles.stripocdn.email/content/assets/img/social-icons/logo-black/x-logo-black.png\" alt=\"X\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Instagram\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png\" alt=\"Inst\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\"><a target=\"_blank\" href=\"\"><img title=\"Youtube\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png\" alt=\"Yt\" width=\"32\"></a></td>\n                         </tr>\n                        </tbody>\n                       </table></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p35b\"><p>&nbsp;© 2021 Rocky Connect, Inc. All Rights Reserved.</p><p>1543 West Jackson st, Macomb, Illinois, US, 61455</p></td>\n                     </tr>\n                     \n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content esd-footer-popover\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"background-color: transparent;\" bgcolor=\"rgba(0, 0, 0, 0)\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-infoblock\"><p><a target=\"_blank\"></a>No longer want to receive these emails?&nbsp;<a href=\"\" target=\"_blank\">Unsubscribe</a>.<a target=\"_blank\"></a></p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table></td>\n     </tr>\n    </tbody>\n   </table>\n  </div>\n \n</body></html>";
                    UtilityService.SendEmail(message, trip1.CustomerEmail, "RockyConnect Trip Update");
                    string notify = $"Your Trip to {user.Destination} has started";
                    Notification notification = new Notification()
                    {
                        Body = notify,
                        Title = "RockyConnect",
                        Email = user.CustomerEmail,
                        DateSent = DateTime.Now,
                        NotificationID = UtilityService.UniqueIDGenerator()


                    };
                    NotificationModel model = new NotificationModel()
                    {
                        Body = notify,
                        Title = "RockyConnect",
                        DeviceId = user.RiderDeviceID
                    };
                    NotificationService.SendNotification(model);
                    NotificationData.CreateNotificationData(notification);
                    notification.Body = $"You just started a Trip to {user.Destination}.";
                    notification.Email = user.DriverEmail;
                    NotificationData.CreateNotificationData(notification);
                    model.Body = $"You just started a Trip to {user.Destination}.";
                    model.DeviceId = user.DriverDeviceID;
                    NotificationService.SendNotification(model);

                    status.statusCode = "00";
                    status.status = "You started a Trip";
                    status.data = user;
                }
                else
                {

                    status.statusCode = "01";
                    status.status = " Failed to start the trip";
                }
            }
            else
            {
                status.statusCode = "01";
                status.status = "Trip has to be approved and paid for before started";
            }
            return status;
        }

        internal static Response EndTrip(TripRequest trip, FcmNotificationSetting setting)
        {
                           var status = new Response();
            Trip trip1 = TripData.SelectTripData(trip.ID);
            if (trip1.TripStatus == "Enroute" && trip1.PaymentID is  not null)
            {

                trip1.TripStatus = "Completed";


                trip1.Date_Updated = DateTime.Now;

                string result = TripData.UpdateTripData(trip1.PaymentID, trip1);
                if (result == "00")
                {
                    Driver driver = UserData.GetDriver(trip1.DriverEmail);
                    driver.NoOfRides = driver.NoOfRides + 1;
                    UserData.UpdateDriverRating(driver);
                    SuperTrip user= TripData.SelectSuperTripData(trip.ID);
                    string message = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\"><head>\n  <meta charset=\"UTF-8\">\n  <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\">\n  <meta name=\"x-apple-disable-message-reformatting\">\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n  <meta content=\"telephone=no\" name=\"format-detection\">\n  <title></title>\n  <!--[if (mso 16)]>\n    <style type=\"text/css\">\n    a {{text-decoration: none;}}\n    </style>\n    <![endif]-->\n  <!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]-->\n  <!--[if gte mso 9]>\n<xml>\n    <o:OfficeDocumentSettings>\n    <o:AllowPNG></o:AllowPNG>\n    <o:PixelsPerInch>96</o:PixelsPerInch>\n    </o:OfficeDocumentSettings>\n</xml>\n<![endif]-->\n <!--[if mso]>\n <style type=\"text/css\">\n     ul {{\n  margin: 0 !important;\n  }}\n  ol {{\n  margin: 0 !important;\n  }}\n  li {{\n  margin-left: 47px !important;\n  }}\n\n </style><![endif]\n--></head>\n <body class=\"body\">\n  <div dir=\"ltr\" class=\"es-wrapper-color\">\n   <!--[if gte mso 9]>\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\n\t\t\t\t<v:fill type=\"tile\" color=\"#fafafa\"></v:fill>\n\t\t\t</v:background>\n\t\t<![endif]-->\n   <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\n    <tbody>\n     <tr>\n      <td class=\"esd-email-paddings\" valign=\"top\">\n       \n       \n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe\" align=\"center\">\n           <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p30t es-p30b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-image es-p10t es-p10b\" style=\"font-size: 0px;\"><a target=\"_blank\"><img src=\"https://fblkeki.stripocdn.email/content/guids/CABINET_67e080d830d87c17802bd9b4fe1c0912/images/55191618237638326.png\" alt=\"\" style=\"display: block;\" width=\"100\"></a></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10b es-m-txt-c\"><h1 style=\"font-size: 46px; line-height: 100%;\">Your trip is completed</h1></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Hi {user.RiderFirstName}You’ve received this message because your trip to {user.Destination} just ended. Login to give your driver a rating.&nbsp;</p></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10t es-p5b\"><p>If you did not register with us, please disregard this email.</p></td>\n                     </tr>\n                     \n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Rocky Connect... Nowhere is beyond reach</p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-footer\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-footer-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"640\" style=\"background-color: transparent;\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20t es-p20b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"600\" class=\"esd-container-frame\" align=\"left\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-social es-p15t es-p15b\" style=\"font-size:0\">\n                       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-table-not-adapt es-social\">\n                        <tbody>\n                         <tr>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Facebook\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png\" alt=\"Fb\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"X.com\" src=\"https://localfiles.stripocdn.email/content/assets/img/social-icons/logo-black/x-logo-black.png\" alt=\"X\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Instagram\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png\" alt=\"Inst\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\"><a target=\"_blank\" href=\"\"><img title=\"Youtube\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png\" alt=\"Yt\" width=\"32\"></a></td>\n                         </tr>\n                        </tbody>\n                       </table></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p35b\"><p>&nbsp;© 2021 Rocky Connect, Inc. All Rights Reserved.</p><p>1543 West Jackson st, Macomb, Illinois, US, 61455</p></td>\n                     </tr>\n                     \n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content esd-footer-popover\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"background-color: transparent;\" bgcolor=\"rgba(0, 0, 0, 0)\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-infoblock\"><p><a target=\"_blank\"></a>No longer want to receive these emails?&nbsp;<a href=\"\" target=\"_blank\">Unsubscribe</a>.<a target=\"_blank\"></a></p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table></td>\n     </tr>\n    </tbody>\n   </table>\n  </div>\n \n</body></html>";
                    UtilityService.SendEmail(message, trip1.CustomerEmail, "RockyConnect Trip Update");
                    string notify = $"Your Trip to {user.Destination} has Ended";
                    Notification notification = new Notification()
                    {
                        Body = notify,
                        Title = "RockyConnect",
                        Email = trip1.CustomerEmail,
                        DateSent = DateTime.Now,
                        NotificationID = UtilityService.UniqueIDGenerator()


                    };
                    NotificationModel model = new NotificationModel()
                    {
                        Body = notify,
                        Title = "RockyConnect",
                        DeviceId = user.RiderDeviceID
                    };
                    NotificationService.SendNotification(model);
                    NotificationData.CreateNotificationData(notification);
                    notification.Body = $"You just ended a Trip to {user.Destination}.";
                    notification.Email = user.DriverEmail;
                    NotificationData.CreateNotificationData(notification);
                    model.Body = $"You just ended a Trip to {user.Destination}.";
                    model.DeviceId = user.DriverDeviceID;
                    NotificationService.SendNotification(model);
                    status.statusCode = "00";
                    status.status = "You ended a trip";

                }
                else
                {

                    status.statusCode = "01";
                    status.status = " Failed to stop the trip";
                }
            }
            else {
                status.statusCode = "01";
                status.status = "Only Trip started can be stopped";
            }

            return status;
        }

        internal static Response DriverRequestTrip(TripDataInfo trip, FcmNotificationSetting setting)
        {
            Trip trip1 = TripData.SelectTripData(trip.ID);
            var status = new Response();

            if (trip1.TripInitiator == "Driver")
            {
                trip1.CustomerEmail = trip.CustomerEmail;
                trip1.TripStatus = "Requested";

            }
            else if (trip1.TripInitiator=="Rider")
            {
                status.statusCode = "01";
                status.status = "Rider initiated trip cannot request driver";
                return status;
            }
            else
            {
                status.statusCode = "01";
                status.status = "No available trip exist with this record, try requesting another";

            }


            trip1.Date_Updated = DateTime.Now;

            string result = TripData.UpdateTripData(trip1.PaymentID, trip1);
            SuperTrip user = TripData.SelectSuperTripData(trip.ID);
            if (result == "00")
            {
                try {
                    string message = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\"><head>\n  <meta charset=\"UTF-8\">\n  <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\">\n  <meta name=\"x-apple-disable-message-reformatting\">\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n  <meta content=\"telephone=no\" name=\"format-detection\">\n  <title></title>\n  <!--[if (mso 16)]>\n    <style type=\"text/css\">\n    a {{text-decoration: none;}}\n    </style>\n    <![endif]-->\n  <!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]-->\n  <!--[if gte mso 9]>\n<xml>\n    <o:OfficeDocumentSettings>\n    <o:AllowPNG></o:AllowPNG>\n    <o:PixelsPerInch>96</o:PixelsPerInch>\n    </o:OfficeDocumentSettings>\n</xml>\n<![endif]-->\n <!--[if mso]>\n <style type=\"text/css\">\n     ul {{\n  margin: 0 !important;\n  }}\n  ol {{\n  margin: 0 !important;\n  }}\n  li {{\n  margin-left: 47px !important;\n  }}\n\n </style><![endif]\n--></head>\n <body class=\"body\">\n  <div dir=\"ltr\" class=\"es-wrapper-color\">\n   <!--[if gte mso 9]>\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\n\t\t\t\t<v:fill type=\"tile\" color=\"#fafafa\"></v:fill>\n\t\t\t</v:background>\n\t\t<![endif]-->\n   <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\n    <tbody>\n     <tr>\n      <td class=\"esd-email-paddings\" valign=\"top\">\n       \n       \n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe\" align=\"center\">\n           <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p30t es-p30b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-image es-p10t es-p10b\" style=\"font-size: 0px;\"><a target=\"_blank\"><img src=\"https://fblkeki.stripocdn.email/content/guids/CABINET_67e080d830d87c17802bd9b4fe1c0912/images/55191618237638326.png\" alt=\"\" style=\"display: block;\" width=\"100\"></a></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10b es-m-txt-c\"><h1 style=\"font-size: 46px; line-height: 100%;\">Trip Requested</h1></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Hi {user.DriverFirstName}You’ve received this message because your trip to {user.Destination} has a request. Login to approve or decline request.&nbsp;</p></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10t es-p5b\"><p>If you did not register with us, please disregard this email.</p></td>\n                     </tr>\n                     \n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Rocky Connect... Nowhere is beyond reach</p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-footer\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-footer-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"640\" style=\"background-color: transparent;\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20t es-p20b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"600\" class=\"esd-container-frame\" align=\"left\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-social es-p15t es-p15b\" style=\"font-size:0\">\n                       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-table-not-adapt es-social\">\n                        <tbody>\n                         <tr>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Facebook\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png\" alt=\"Fb\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"X.com\" src=\"https://localfiles.stripocdn.email/content/assets/img/social-icons/logo-black/x-logo-black.png\" alt=\"X\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Instagram\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png\" alt=\"Inst\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\"><a target=\"_blank\" href=\"\"><img title=\"Youtube\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png\" alt=\"Yt\" width=\"32\"></a></td>\n                         </tr>\n                        </tbody>\n                       </table></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p35b\"><p>&nbsp;© 2021 Rocky Connect, Inc. All Rights Reserved.</p><p>1543 West Jackson st, Macomb, Illinois, US, 61455</p></td>\n                     </tr>\n                     \n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content esd-footer-popover\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"background-color: transparent;\" bgcolor=\"rgba(0, 0, 0, 0)\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-infoblock\"><p><a target=\"_blank\"></a>No longer want to receive these emails?&nbsp;<a href=\"\" target=\"_blank\">Unsubscribe</a>.<a target=\"_blank\"></a></p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table></td>\n     </tr>\n    </tbody>\n   </table>\n  </div>\n \n</body></html>";
                    UtilityService.SendEmail(message, trip1.DriverEmail, "Trip Request From RockyConnect");
                    string message2 = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\"><head>\n  <meta charset=\"UTF-8\">\n  <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\">\n  <meta name=\"x-apple-disable-message-reformatting\">\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n  <meta content=\"telephone=no\" name=\"format-detection\">\n  <title></title>\n  <!--[if (mso 16)]>\n    <style type=\"text/css\">\n    a {{text-decoration: none;}}\n    </style>\n    <![endif]-->\n  <!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]-->\n  <!--[if gte mso 9]>\n<xml>\n    <o:OfficeDocumentSettings>\n    <o:AllowPNG></o:AllowPNG>\n    <o:PixelsPerInch>96</o:PixelsPerInch>\n    </o:OfficeDocumentSettings>\n</xml>\n<![endif]-->\n <!--[if mso]>\n <style type=\"text/css\">\n     ul {{\n  margin: 0 !important;\n  }}\n  ol {{\n  margin: 0 !important;\n  }}\n  li {{\n  margin-left: 47px !important;\n  }}\n\n </style><![endif]\n--></head>\n <body class=\"body\">\n  <div dir=\"ltr\" class=\"es-wrapper-color\">\n   <!--[if gte mso 9]>\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\n\t\t\t\t<v:fill type=\"tile\" color=\"#fafafa\"></v:fill>\n\t\t\t</v:background>\n\t\t<![endif]-->\n   <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\n    <tbody>\n     <tr>\n      <td class=\"esd-email-paddings\" valign=\"top\">\n       \n       \n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe\" align=\"center\">\n           <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p30t es-p30b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-image es-p10t es-p10b\" style=\"font-size: 0px;\"><a target=\"_blank\"><img src=\"https://fblkeki.stripocdn.email/content/guids/CABINET_67e080d830d87c17802bd9b4fe1c0912/images/55191618237638326.png\" alt=\"\" style=\"display: block;\" width=\"100\"></a></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10b es-m-txt-c\"><h1 style=\"font-size: 46px; line-height: 100%;\">Trip Request Sent</h1></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Hi {user.RiderFirstName}You’ve received this message because your request to join a trip to {user.Destination} was successfully sent.&nbsp;</p></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10t es-p5b\"><p>If you did not register with us, please disregard this email.</p></td>\n                     </tr>\n                     \n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Rocky Connect... Nowhere is beyond reach</p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-footer\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-footer-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"640\" style=\"background-color: transparent;\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20t es-p20b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"600\" class=\"esd-container-frame\" align=\"left\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-social es-p15t es-p15b\" style=\"font-size:0\">\n                       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-table-not-adapt es-social\">\n                        <tbody>\n                         <tr>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Facebook\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png\" alt=\"Fb\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"X.com\" src=\"https://localfiles.stripocdn.email/content/assets/img/social-icons/logo-black/x-logo-black.png\" alt=\"X\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Instagram\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png\" alt=\"Inst\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\"><a target=\"_blank\" href=\"\"><img title=\"Youtube\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png\" alt=\"Yt\" width=\"32\"></a></td>\n                         </tr>\n                        </tbody>\n                       </table></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p35b\"><p>&nbsp;© 2021 Rocky Connect, Inc. All Rights Reserved.</p><p>1543 West Jackson st, Macomb, Illinois, US, 61455</p></td>\n                     </tr>\n                     \n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content esd-footer-popover\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"background-color: transparent;\" bgcolor=\"rgba(0, 0, 0, 0)\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-infoblock\"><p><a target=\"_blank\"></a>No longer want to receive these emails?&nbsp;<a href=\"\" target=\"_blank\">Unsubscribe</a>.<a target=\"_blank\"></a></p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table></td>\n     </tr>\n    </tbody>\n   </table>\n  </div>\n \n</body></html>";
                    UtilityService.SendEmail(message2, trip1.CustomerEmail, "RockyConnect Trip Update");
                    string notify = $"You have a Trip Request to {user.Destination} login to approve or decline";

                    Notification notification = new Notification()
                    {
                        Body = notify,
                        Title = "RockyConnect",
                        Email = trip1.DriverEmail,
                        DateSent = DateTime.Now,
                        NotificationID = UtilityService.UniqueIDGenerator()


                    };
                    NotificationModel model = new NotificationModel()
                    {
                        Body = notify,
                        Title = "RockyConnect",
                        DeviceId = user.DriverDeviceID
                    };
                    NotificationService.SendNotification(model);
                    NotificationData.CreateNotificationData(notification);
                }
                catch (Exception e)
                {
                    status.statusCode = "00";
                    status.status = "Successfully saved";
                    status.data = user;
                }
                status.statusCode = "00";
                status.status = "Successfully saved";
                status.data = user;
            }
            else
            {
                status.statusCode = "01";
                status.status = "No available trip exist with this record, try to request another";
            }
            return status;
        }

        internal static Response CompletedTrip(string email)
        {

            var status = new Response();

            List<SuperTrip> result = TripData.CompletedTrip(email);
            if (result.Count >0)
            {

                status.statusCode = "00";
                status.status = "Successfully fetched";
                status.data = result;
            }
            else
            {

                status.statusCode = "00";
                status.status = "No completed trip for this user";
            }
            return status;
        }

        internal static Response UpcomingTrips(string email)
        {

            var status = new Response();

            List<SuperTrip> result = TripData.UpcomingTrips(email);
            if (result.Count > 0)
            {

                status.statusCode = "00";
                status.status = "Successfully fetched";
                status.data = result;
            }
            else
            {

                status.statusCode = "00";
                status.status = "No upcoming trip for this user";
            }
            return status;
        }

        internal static Response AwaitingApproval(string email)
        {
            var status = new Response();

            List<SuperTrip> result = TripData.AwaitingApproval(email);
            if (result.Count > 0)
            {

                status.statusCode = "00";
                status.status = "Successfully fetched";
                status.data = result;
            }
            else
            {

                status.statusCode = "00";
                status.status = "No trip awaiting approval for this user";
            }
            return status;
        }

        internal static Response ApprovedTrips(string email)
        {
            var status = new Response();

            List<SuperTrip> result = TripData.ApprovalList(email);
            if (result.Count > 0)
            {

                status.statusCode = "00";
                status.status = "Successfully fetched";
                status.data = result;
            }
            else
            {

                status.statusCode = "01";
                status.status = "No approved trip for this user";
            }
            return status;
        }

       
        internal static Response RateTrip(RateRequest customer)
        {
            var status = new Response();
            Trip trip = TripData.SelectTripData(customer.TripID);
            User user = UserData.GetUserUsingEmail(customer.Email);
            if (user.Email is null)
            {

                status.statusCode = "01";
                status.status = "Failed to rate, Driver not found";
                return status;
            }
            if (user.Role != Role.driver)
            {

                status.statusCode = "01";
                status.status = "Failed to rate, User is not a driver";
                return status;
            }
            trip.IsRated = 0;
            string res = TripData.UpdateTripData(trip.PaymentID, trip);
            Driver drive = UserData.GetDriver(customer.Email);
                if (drive.Email is not null) {

                int sum = (drive.Rating + customer.Rate)/2;
                drive.Rating = sum;
                string driver = UserData.UpdateDriverRating(drive);
                if (driver == "00")
                {

                    status.statusCode = "00";
                    status.status = "Successfully saved";
                }
                else
                {

                    status.statusCode = "01";
                    status.status = "Failed to rate ";
                }

            }
            else
            {
                Driver driver1 = new Driver {Email =customer.Email,Rating=customer.Rate,NoOfRides=1 };
            
                string driver = UserData.CreateDriver(driver1);
                if (driver == "00")
                {

                    status.statusCode = "00";
                    status.status = "Thank you for your Rating";
                }
                else
                {

                    status.statusCode = "01";
                    status.status = "Failed to rate";
                }
            }
            return status;
        }

        internal static Response UpdateATrip(UpdateTripRequest trip)
        {
            Trip trip1 = TripData.SelectTripData(trip.ID);

            trip1.SourceLatitude = trip.SourceLatitude;
            trip1.SourceLocation = trip.SourceLocation;
            trip1.Destination = trip.Destination;
         
            trip1.DestinationLat = trip.DestinationLat;
            trip1.DestinationLong = trip.DestinationLong;
            trip1.TripStatus = "Created";
            trip1.TripCost = trip.TripDistance * 10;
            trip1.Date_Updated = DateTime.Now;
            trip1.TripDate = trip.TripDate;
            trip1.TripDistance = trip.TripDistance;
            trip1.SourceLongitude = trip.SourceLongitude;
            trip1.PaymentID = null;
            trip1.TotalTime = trip.TotalTime;
            trip1.DestinationState = trip.DestinationState;

            if (trip.TripInitiator == "Driver")
            {
                trip1.CustomerEmail = null;
                trip1.DriverEmail = trip.DriverEmail;
            }
            else
            {
                trip1.DriverEmail = null;
                trip1.CustomerEmail = trip.CustomerEmail;
            }
            var status = new Response();

            string result = TripData.UpdateTripData(trip1.PaymentID,trip1);
            SuperTrip user = TripData.SelectSuperTripData(trip1.ID);

            if (result == "00")
            {

                status.statusCode = "00";
                status.status = "Successfully Updated";
                status.data = user;
            }
            else
            {

                status.statusCode = "01";
                status.status = "Failed to Update";
            }
            return status;
         }

        internal static Response UnrequestedTrips(string email)
        {

            var status = new Response();

            List<SuperTrip> result = TripData.UnrequestedTrips(email);
            if (result.Count > 0)
            {

                status.statusCode = "00";
                status.status = "Successfully fetched";
                status.data = result;
            }
            else
            {

                status.statusCode = "00";
                status.status = "No unrequested trip for this user";
            }
            return status;
        }
    }
}


