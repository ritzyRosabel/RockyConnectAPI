using RockyConnectBackend.Data;
using RockyConnectBackend.Model;
using RockyConnectBackend.Services;

namespace RockyConnectBackend.Controllers
{
    internal class UserService
    {
        public static Response Login(LoginUserRequest cred)
        {
            var status = new Response();
            if (cred.Email != null)
            {
                User result = UserData.GetUserUsingEmail(cred.Email);

                if (result.Email == null)
                {
                    status.statusCode = "01";
                    status.status = "Invalid Username and Password Match";
                    status.data = null;

                }
                else
                {
                    if(cred.app ==2 && result.Role == Role.rider)
                    {
                        status.statusCode = "01";
                        status.status = "Invalid Username and Password Match";
                        status.data = null;
                        return status;
                    }else if(cred.app == 1 && result.Role == Role.driver)
                    {
                        status.statusCode = "01";
                        status.status = "Invalid Username and Password Match";
                        status.data = null;
                        return status;
                    }
                    // Regex.Replace(result.UserName, @"\s+", "");
                    if (result.Password is not null)
                        if (UtilityService.CompareHashKeys(cred.Password.ToLower(),result.Password))
                        {
                            result.DeviceID = cred.DeviceID;
                            UserData.UpdateData(result);
                            result.Password = "";
                            status.statusCode = "00";
                            status.status = "Successfull";
                            status.data = result;

                        }
                        else
                        {
                            status.statusCode = "01";
                            status.status = "Invalid Username and Password Match";
                            status.data = null;

                        }

                }
            }
            return status;
        }

        public static Response Create(UserRequest customer)
        {
            var response = new Response();
            string result; 
            User user = new User
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Password = customer.Password,
                Role = customer.Role,
                IsAccountActive = true
            };
            
            string pass = UtilityService.HashKeys(user.Password.ToLower());
            user.Password = pass;
            result = UserData.CreateCustomerData(user);
            
            if (result == "00")
            {
                if (user.Role == Role.driver)
                {
                    Driver driver = new Driver() { Email=user.Email,Rating=5,NoOfRides=0} ;
                    UserData.CreateDriver(driver);

                }
                else
                {
                    UserData.SaveRider(user.Email);


                }
                string se = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\"><head><meta charset=\"UTF-8\"><meta content=\"width=device-width, initial-scale=1\" name=\"viewport\"><meta name=\"x-apple-disable-message-reformatting\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><meta content=\"telephone=no\" name=\"format-detection\"><title></title><!--[if (mso 16)]>\n    <style type=\"text/css\">\n    a {text-decoration: none;}\n    </style>\n    <![endif]--><!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]--><!--[if gte mso 9]>\n<xml>\n    <o:OfficeDocumentSettings>\n    <o:AllowPNG></o:AllowPNG>\n    <o:PixelsPerInch>96</o:PixelsPerInch>\n    </o:OfficeDocumentSettings>\n</xml>\n<![endif]--><!--[if mso]>\n <style type=\"text/css\">\n     ul {\n  margin: 0 !important;\n  }\n  ol {\n  margin: 0 !important;\n  }\n  li {\n  margin-left: 47px !important;\n  }\n\n </style><![endif]\n--></head><body class=\"body\"><div dir=\"ltr\" class=\"es-wrapper-color\"><!--[if gte mso 9]>\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\n\t\t\t\t<v:fill type=\"tile\" color=\"#f6f6f6\"></v:fill>\n\t\t\t</v:background>\n\t\t<![endif]--><table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"esd-email-paddings\" valign=\"top\"><table class=\"es-content\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\"><tbody><tr></tr><tr><td class=\"es-adaptive esd-stripe\" align=\"center\"><table class=\"es-content-body\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\"><tbody><tr><td class=\"esd-structure\" esd-general-paddings-checked=\"false\" style=\"background-color: #663399;\" bgcolor=\"#663399\" align=\"left\"><table cellspacing=\"0\" cellpadding=\"0\" align=\"right\" class=\"es-right\"><tbody><tr><td class=\"esd-container-frame\" width=\"600\" align=\"left\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"esd-block-banner\" style=\"position: relative;\" align=\"center\"><a target=\"_blank\" href=\"null\"><img src=\"https://fblkeki.stripocdn.email/content/guids/bannerImgGuid/images/image17001167725298672.png\"  class=\"adapt-img\" width=\"500\" height=\"260\"></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table class=\"es-content\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\"><tbody><tr><td class=\"esd-stripe\" align=\"center\"><table class=\"es-content-body\" style=\"background-color: #ffffff;\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#ffffff\" align=\"center\"><tbody><tr><td class=\"esd-structure es-p20t es-p15b es-p20r es-p20l\" align=\"left\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"esd-container-frame\" width=\"560\" valign=\"top\" align=\"center\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"esd-block-text\" align=\"center\"><h2 style=\"color: #333333; line-height: 150%;\">Welcome to RockyConnect</h2></td></tr><tr><td class=\"esd-block-spacer es-p5t es-p5b es-p20r es-p20l\" align=\"center\" style=\"font-size:0\"><table width=\"5%\" height=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td style=\"border-bottom: 3px solid #663399; background: none 0% 0% repeat scroll rgba(0, 0, 0, 0); height: 1px; width: 100%; margin: 0px;\"></td></tr></tbody></table></td></tr><tr><td class=\"esd-block-text es-p10t es-p10b\" align=\"center\" esd-links-color=\"#663399\"><p style=\"color: #333333; font-size: 16px; line-height: 150%;\">We are so glad to have you as part of Rocky connect family, your trip has never been better, with rocky connect you can travel anywhere and anytime in just a tap of your fingers</p></td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td class=\"esd-structure es-p10t es-p10b es-p20r es-p20l\" esd-general-paddings-checked=\"false\" align=\"left\"><!--[if mso]><table width=\"560\" cellpadding=\"0\" cellspacing=\"0\"><tr><td width=\"194\"><![endif]--><table class=\"es-left\" cellspacing=\"0\" cellpadding=\"0\" align=\"left\"><tbody><tr><td class=\"es-m-p0r es-m-p20b esd-container-frame\" width=\"174\" align=\"center\"><table style=\"border-collapse: separate;\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"esd-block-image es-p10b\" align=\"center\" style=\"font-size:0\"><a target=\"_blank\"><img src=\"https://fblkeki.stripocdn.email/content/guids/CABINET_5203f6b0a8aa91077cdb7b82d8443bb1/images/321507705439564.jpg\" alt=\"App icon\" title=\"App icon\" width=\"133\"></a></td></tr><tr><td class=\"esd-block-text\" align=\"center\"><h1 style=\"color: #663399; line-height: 120%;\"><strong>01.</strong></h1></td></tr><tr><td class=\"esd-block-text\" align=\"center\"><p style=\"color: #333333;\"><span style=\"font-size:18px;\"><span style=\"line-height: 150%;\">Download app</span></span></p></td></tr><tr><td class=\"esd-block-text es-p10t es-p10b\" align=\"center\"><p>The app is available as a free download from the Google Play.</p></td></tr></tbody></table></td><td class=\"es-hidden\" width=\"20\"></td></tr></tbody></table><!--[if mso]></td><td width=\"173\"><![endif]--><table class=\"es-left\" cellspacing=\"0\" cellpadding=\"0\" align=\"left\"><tbody><tr><td class=\"es-m-p20b esd-container-frame\" width=\"173\" align=\"center\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"esd-block-image es-p10b\" align=\"center\" style=\"font-size:0\"><a target=\"_blank\"><img src=\"https://fblkeki.stripocdn.email/content/guids/CABINET_5203f6b0a8aa91077cdb7b82d8443bb1/images/63221507705546695.jpg\" alt=\"Taxi icon\" title=\"Taxi icon\" width=\"133\"></a></td></tr><tr><td class=\"esd-block-text\" align=\"center\"><h1 style=\"color: #663399; line-height: 120%;\"><strong>02.</strong></h1></td></tr><tr><td class=\"esd-block-text\" align=\"center\"><p style=\"color: #333333; font-size: 18px; line-height: 150%;\">Create/search a trip</p></td></tr><tr><td class=\"esd-block-text es-p10t es-p10b\" align=\"center\"><p style=\"color: #333333; line-height: 150%;\">One way drives, taxi-services,&nbsp;airport&nbsp;shuttles, hotel&nbsp;pickup&nbsp;- our basic services.</p></td></tr></tbody></table></td></tr></tbody></table><!--[if mso]></td><td width=\"20\"></td><td width=\"173\"><![endif]--><table class=\"es-right\" cellspacing=\"0\" cellpadding=\"0\" align=\"right\"><tbody><tr><td class=\"esd-container-frame\" width=\"173\" align=\"center\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"esd-block-image es-p10b\" align=\"center\" style=\"font-size:0\"><a target=\"_blank\"><img src=\"https://fblkeki.stripocdn.email/content/guids/CABINET_5203f6b0a8aa91077cdb7b82d8443bb1/images/45491507705559743.jpg\" alt=\"Money icon\" title=\"Money icon\" width=\"133\"></a></td></tr><tr><td class=\"esd-block-text\" align=\"center\"><h1 style=\"color: #663399; line-height: 120%;\"><strong>03.</strong></h1></td></tr><tr><td class=\"esd-block-text\" align=\"center\"><p style=\"color: #333333; font-size: 18px; line-height: 150%;\">Enjoy your trip</p></td></tr><tr><td class=\"esd-block-text es-p10t es-p10b\" align=\"center\"><p style=\"color: #333333;\">Enjoy and have a nice time with your ride.</p></td></tr></tbody></table></td></tr></tbody></table><!--[if mso]></td></tr></table><![endif]--></td></tr><tr><td class=\"esd-structure\" esd-general-paddings-checked=\"false\" align=\"left\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr>\n              \n          <td class=\"esd-container-frame\" width=\"600\" valign=\"top\" align=\"center\">\n              <table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\n                  <tbody><tr>\n    \n</tr></tbody></table>\n          </td>\n      \n              \n      </tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table cellpadding=\"0\" cellspacing=\"0\" class=\"es-footer\" align=\"center\"><tbody><tr><td class=\"esd-stripe\" align=\"center\"><table class=\"es-footer-body\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" bgcolor=\"#663399\" style=\"background-color: #663399;\"><tbody><tr><td class=\"esd-structure es-p20\" esd-general-paddings-checked=\"false\" align=\"left\"><!--[if mso]><table width=\"560\" cellpadding=\"0\" cellspacing=\"0\"><tr><td width=\"178\" valign=\"top\"><![endif]--><table class=\"es-left\" cellspacing=\"0\" cellpadding=\"0\" align=\"left\"><tbody><tr><td class=\"es-m-p0r es-m-p20b esd-container-frame\" width=\"178\" valign=\"top\" align=\"center\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td align=\"center\" class=\"esd-block-image\" style=\"font-size: 0px;\"><a target=\"_blank\"><img class=\"adapt-img\" src=\"https://fblkeki.stripocdn.email/content/guids/CABINET_73df0a15fea33c44f103ebb750da24e33365c39dd95a6383eeb7e211c5bdc1cf/images/rc.png\" alt=\"\" style=\"display:block\" width=\"178\"></a></td></tr></tbody></table></td></tr></tbody></table><!--[if mso]></td><td width=\"20\"></td><td width=\"362\" valign=\"top\"><![endif]--><table cellspacing=\"0\" cellpadding=\"0\" align=\"right\"><tbody><tr><td class=\"esd-container-frame\" width=\"362\" align=\"left\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"esd-block-text es-p10b es-m-txt-l\" align=\"left\"><h3>Contact us</h3></td></tr><tr><td class=\"esd-block-text es-m-txt-c\" align=\"left\"><p>BOX 11705, Station Centre-Ville Montreal, QC</p><p><a target=\"_blank\" href=\"tel:123456789\">123456789</a></p><p><a target=\"_blank\" href=\"mailto:your@mail.com\">your@mail.com</a></p><p><a target=\"_blank\" href=\"https://viewstripo.email/\">viewstripo.email</a></p></td></tr><tr><td class=\"esd-block-social es-p10t es-m-txt-c\" align=\"left\" style=\"font-size:0\"><table class=\"es-social es-table-not-adapt\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"es-p10r\" valign=\"top\" align=\"center\"><a target=\"_blank\" href=\"\"><img title=\"X.com\" src=\"https://localfiles.stripocdn.email/content/assets/img/social-icons/logo-black/x-logo-black.png\" alt=\"X\" width=\"24\" height=\"24\"></a></td><td class=\"es-p10r\" valign=\"top\" align=\"center\"><a target=\"_blank\" href=\"\"><img title=\"Facebook\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png\" alt=\"Fb\" width=\"24\" height=\"24\"></a></td><td class=\"es-p10r\" valign=\"top\" align=\"center\"><a target=\"_blank\" href=\"\"><img title=\"Youtube\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png\" alt=\"Yt\" width=\"24\" height=\"24\"></a></td><td valign=\"top\" align=\"center\"><a target=\"_blank\" href=\"\"><img title=\"Vkontakte\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/vk-logo-black.png\" alt=\"Vk\" width=\"24\" height=\"24\"></a></td></tr></tbody></table></td></tr><tr><td align=\"left\" class=\"esd-block-text es-p10t es-m-txt-c\"><p style=\"font-size: 12px; line-height: 150%;\">You are receiving this email because you subscribed to our site. Please note that you can&nbsp;<a target=\"_blank\" style=\"font-size: 12px;\" class=\"unsubscribe\">unsubscribe</a>&nbsp;at any time.</p><p>Vector graphics designed by <a target=\"_blank\" href=\"http://www.freepik.com/\">Freepik</a>.</p></td></tr></tbody></table></td></tr></tbody></table><!--[if mso]></td></tr></table><![endif]--></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></div><div style=\"position:absolute;left:-9999px;top:-9999px;\"></div></body></html>";
                UtilityService.SendEmail(se, user.Email, "Welcome To RockyConnect");

                SendOTP(user, "Email Verification");

                    response.statusCode = "00";
                    response.status = "OTP sent to email";
               
            }else if(result == "-2146232060")
            {
                response.statusCode = "01";
                response.status = "Account with email already exist";
            }
            else
            {
                response.statusCode = "01";
                response.status = "request was  unsuccessful";
            }
            return response;
        }

        private static string SendOTP(User user, string subject)
        {
            string send = "01";
            string code = UtilityService.RandomOTPGenerator();
            string result = UserData.SaveOTP(user.Email, code);
            string messageBody;
            if (user.FirstName == string.Empty)
            {
                messageBody = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\"><head>\n  <meta charset=\"UTF-8\">\n  <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\">\n  <meta name=\"x-apple-disable-message-reformatting\">\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n  <meta content=\"telephone=no\" name=\"format-detection\">\n  <title></title>\n  <!--[if (mso 16)]>\n    <style type=\"text/css\">\n    a {{text-decoration: none;}}\n    </style>\n    <![endif]-->\n  <!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]-->\n  <!--[if gte mso 9]>\n<xml>\n    <o:OfficeDocumentSettings>\n    <o:AllowPNG></o:AllowPNG>\n    <o:PixelsPerInch>96</o:PixelsPerInch>\n    </o:OfficeDocumentSettings>\n</xml>\n<![endif]-->\n <!--[if mso]>\n <style type=\"text/css\">\n     ul {{\n  margin: 0 !important;\n  }}\n  ol {{\n  margin: 0 !important;\n  }}\n  li {{\n  margin-left: 47px !important;\n  }}\n\n </style><![endif]\n--></head>\n <body class=\"body\">\n  <div dir=\"ltr\" class=\"es-wrapper-color\">\n   <!--[if gte mso 9]>\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\n\t\t\t\t<v:fill type=\"tile\" color=\"#fafafa\"></v:fill>\n\t\t\t</v:background>\n\t\t<![endif]-->\n   <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\n    <tbody>\n     <tr>\n      <td class=\"esd-email-paddings\" valign=\"top\">\n       \n       \n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe\" align=\"center\">\n           <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p30t es-p30b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-image es-p10t es-p10b\" style=\"font-size: 0px;\"><a target=\"_blank\"><img src=\"https://fblkeki.stripocdn.email/content/guids/CABINET_67e080d830d87c17802bd9b4fe1c0912/images/55191618237638326.png\" alt=\"\" style=\"display: block;\" width=\"100\"></a></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10b es-m-txt-c\"><h1 style=\"font-size: 46px; line-height: 100%;\">Your Rocky Connect Code</h1></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Hi, Here is your Rocky connect OTP {code} . Its only valid for the next 30minutes.&nbsp;</p></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10t es-p5b\"><p>If you did not register with us, please disregard this email.</p></td>\n                     </tr>\n                     \n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Rocky Connect... Nowhere is beyond reach</p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-footer\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-footer-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"640\" style=\"background-color: transparent;\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20t es-p20b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"600\" class=\"esd-container-frame\" align=\"left\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-social es-p15t es-p15b\" style=\"font-size:0\">\n                       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-table-not-adapt es-social\">\n                        <tbody>\n                         <tr>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Facebook\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png\" alt=\"Fb\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"X.com\" src=\"https://localfiles.stripocdn.email/content/assets/img/social-icons/logo-black/x-logo-black.png\" alt=\"X\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Instagram\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png\" alt=\"Inst\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\"><a target=\"_blank\" href=\"\"><img title=\"Youtube\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png\" alt=\"Yt\" width=\"32\"></a></td>\n                         </tr>\n                        </tbody>\n                       </table></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p35b\"><p>&nbsp;© 2021 Rocky Connect, Inc. All Rights Reserved.</p><p>1543 West Jackson st, Macomb, Illinois, US, 61455</p></td>\n                     </tr>\n                     \n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content esd-footer-popover\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"background-color: transparent;\" bgcolor=\"rgba(0, 0, 0, 0)\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-infoblock\"><p><a target=\"_blank\"></a>No longer want to receive these emails?&nbsp;<a href=\"\" target=\"_blank\">Unsubscribe</a>.<a target=\"_blank\"></a></p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table></td>\n     </tr>\n    </tbody>\n   </table>\n  </div>\n \n</body></html>";


            }
            else
            {
                messageBody = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\"><head>\n  <meta charset=\"UTF-8\">\n  <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\">\n  <meta name=\"x-apple-disable-message-reformatting\">\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n  <meta content=\"telephone=no\" name=\"format-detection\">\n  <title></title>\n  <!--[if (mso 16)]>\n    <style type=\"text/css\">\n    a {{text-decoration: none;}}\n    </style>\n    <![endif]-->\n  <!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]-->\n  <!--[if gte mso 9]>\n<xml>\n    <o:OfficeDocumentSettings>\n    <o:AllowPNG></o:AllowPNG>\n    <o:PixelsPerInch>96</o:PixelsPerInch>\n    </o:OfficeDocumentSettings>\n</xml>\n<![endif]-->\n <!--[if mso]>\n <style type=\"text/css\">\n     ul {{\n  margin: 0 !important;\n  }}\n  ol {{\n  margin: 0 !important;\n  }}\n  li {{\n  margin-left: 47px !important;\n  }}\n\n </style><![endif]\n--></head>\n <body class=\"body\">\n  <div dir=\"ltr\" class=\"es-wrapper-color\">\n   <!--[if gte mso 9]>\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\n\t\t\t\t<v:fill type=\"tile\" color=\"#fafafa\"></v:fill>\n\t\t\t</v:background>\n\t\t<![endif]-->\n   <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\n    <tbody>\n     <tr>\n      <td class=\"esd-email-paddings\" valign=\"top\">\n       \n       \n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe\" align=\"center\">\n           <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p30t es-p30b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-image es-p10t es-p10b\" style=\"font-size: 0px;\"><a target=\"_blank\"><img src=\"https://fblkeki.stripocdn.email/content/guids/CABINET_67e080d830d87c17802bd9b4fe1c0912/images/55191618237638326.png\" alt=\"\" style=\"display: block;\" width=\"100\"></a></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10b es-m-txt-c\"><h1 style=\"font-size: 46px; line-height: 100%;\">Your Rocky Connect Code</h1></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Hi {user.FirstName}, Here is your Rocky connect OTP {code} . Its only valid for the next 30minutes.&nbsp;</p></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10t es-p5b\"><p>If you did not register with us, please disregard this email.</p></td>\n                     </tr>\n                     \n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Rocky Connect... Nowhere is beyond reach</p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-footer\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-footer-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"640\" style=\"background-color: transparent;\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20t es-p20b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"600\" class=\"esd-container-frame\" align=\"left\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-social es-p15t es-p15b\" style=\"font-size:0\">\n                       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-table-not-adapt es-social\">\n                        <tbody>\n                         <tr>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Facebook\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png\" alt=\"Fb\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"X.com\" src=\"https://localfiles.stripocdn.email/content/assets/img/social-icons/logo-black/x-logo-black.png\" alt=\"X\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Instagram\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png\" alt=\"Inst\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\"><a target=\"_blank\" href=\"\"><img title=\"Youtube\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png\" alt=\"Yt\" width=\"32\"></a></td>\n                         </tr>\n                        </tbody>\n                       </table></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p35b\"><p>&nbsp;© 2021 Rocky Connect, Inc. All Rights Reserved.</p><p>1543 West Jackson st, Macomb, Illinois, US, 61455</p></td>\n                     </tr>\n                     \n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content esd-footer-popover\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"background-color: transparent;\" bgcolor=\"rgba(0, 0, 0, 0)\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-infoblock\"><p><a target=\"_blank\"></a>No longer want to receive these emails?&nbsp;<a href=\"\" target=\"_blank\">Unsubscribe</a>.<a target=\"_blank\"></a></p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table></td>\n     </tr>\n    </tbody>\n   </table>\n  </div>\n \n</body></html>";



            }
            try
            {
                send = UtilityService.SendEmail(messageBody, user.Email, subject);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return send;
        }

        internal static Response ValidateAccount(string code, string email)
        {
            var response = new Response();
            string result = string.Empty;
            User user = UserData.GetUserUsingEmail(email);

            if (user.Email == string.Empty)
            {
                response.statusCode = "01";
                response.status = "request was unsuccessful. User not found";
                return response;
            }
            Response res = ValidateOTP(code, email);
            if (res.statusCode == "00")
            {
                user.Email = email;
                user.AccountVerified = 0;
                user.Date_Verified = DateTime.Now;

                result = UserData.VerifyAccount(user);
                if (result == "00")
                {
                  string  MessageBody = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\"><head>\n  <meta charset=\"UTF-8\">\n  <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\">\n  <meta name=\"x-apple-disable-message-reformatting\">\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n  <meta content=\"telephone=no\" name=\"format-detection\">\n  <title></title>\n  <!--[if (mso 16)]>\n    <style type=\"text/css\">\n    a {{text-decoration: none;}}\n    </style>\n    <![endif]-->\n  <!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]-->\n  <!--[if gte mso 9]>\n<xml>\n    <o:OfficeDocumentSettings>\n    <o:AllowPNG></o:AllowPNG>\n    <o:PixelsPerInch>96</o:PixelsPerInch>\n    </o:OfficeDocumentSettings>\n</xml>\n<![endif]-->\n <!--[if mso]>\n <style type=\"text/css\">\n     ul {{\n  margin: 0 !important;\n  }}\n  ol {{\n  margin: 0 !important;\n  }}\n  li {{\n  margin-left: 47px !important;\n  }}\n\n </style><![endif]\n--></head>\n <body class=\"body\">\n  <div dir=\"ltr\" class=\"es-wrapper-color\">\n   <!--[if gte mso 9]>\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\n\t\t\t\t<v:fill type=\"tile\" color=\"#fafafa\"></v:fill>\n\t\t\t</v:background>\n\t\t<![endif]-->\n   <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\n    <tbody>\n     <tr>\n      <td class=\"esd-email-paddings\" valign=\"top\">\n       \n       \n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe\" align=\"center\">\n           <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p30t es-p30b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-image es-p10t es-p10b\" style=\"font-size: 0px;\"><a target=\"_blank\"><img src=\"https://fblkeki.stripocdn.email/content/guids/CABINET_67e080d830d87c17802bd9b4fe1c0912/images/55191618237638326.png\" alt=\"\" style=\"display: block;\" width=\"100\"></a></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10b es-m-txt-c\"><h1 style=\"font-size: 46px; line-height: 100%;\">Rocky Connect Account Verified</h1></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Hi, Your Rocky Connect account is verified. No further action is required from you.&nbsp;</p></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10t es-p5b\"><p>If you did not register with us, please disregard this email.</p></td>\n                     </tr>\n                     \n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Rocky Connect... Nowhere is beyond reach</p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-footer\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-footer-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"640\" style=\"background-color: transparent;\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20t es-p20b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"600\" class=\"esd-container-frame\" align=\"left\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-social es-p15t es-p15b\" style=\"font-size:0\">\n                       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-table-not-adapt es-social\">\n                        <tbody>\n                         <tr>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Facebook\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png\" alt=\"Fb\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"X.com\" src=\"https://localfiles.stripocdn.email/content/assets/img/social-icons/logo-black/x-logo-black.png\" alt=\"X\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Instagram\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png\" alt=\"Inst\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\"><a target=\"_blank\" href=\"\"><img title=\"Youtube\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png\" alt=\"Yt\" width=\"32\"></a></td>\n                         </tr>\n                        </tbody>\n                       </table></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p35b\"><p>&nbsp;© 2021 Rocky Connect, Inc. All Rights Reserved.</p><p>1543 West Jackson st, Macomb, Illinois, US, 61455</p></td>\n                     </tr>\n                     \n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content esd-footer-popover\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"background-color: transparent;\" bgcolor=\"rgba(0, 0, 0, 0)\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-infoblock\"><p><a target=\"_blank\"></a>No longer want to receive these emails?&nbsp;<a href=\"\" target=\"_blank\">Unsubscribe</a>.<a target=\"_blank\"></a></p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table></td>\n     </tr>\n    </tbody>\n   </table>\n  </div>\n \n</body></html>";
                    string send = UtilityService.SendEmail(MessageBody, user.Email, "Account Verification");
                    if (send == "00")
                    {
                        response.status = "Account Successfully Verified";
                        response.statusCode = "00";


                    }
                    else
                    {
                        response.status = "Account verification was successful, but email was unsuccessfully sent.";
                        response.statusCode = "01";


                    }

                }
                else
                {
                    response.statusCode = "01";
                    response.status = "request was unsuccessful";
                }
            }
            else
            {
                return res;
            }
            return response;
        }

        //internal static Response VerifyPhone(PhoneVerification number)
        //{
        //    Response response = new Response();
        //    string MessageBody = "<p> Hi, </p> <p> Your account has been successfully verified. No further action is neededfrome your end.</p>";
        //    string result = UtilityService.SendPhone(MessageBody, number.PhoneNumber);
        //    if (result == "00")
        //    {
        //        response.status = "Account Successfully Verified";
        //        response.statusCode = "00";


        //    }
        //    else
        //    {
        //        response.status = "Account verification was unsuccessfull, try again later. ";
        //        response.statusCode = "01";


        //    }
        //    return response;
        //}

        internal static Response ValidateOTP(string code, string email)
        {
            Response response = new Response();
            OTP result = UserData.GetUserOtp(code, email);
            
                if (result.ID != 0)
                {
                    TimeSpan time = DateTime.Now - result.DateCreated;
                    if (time.TotalMinutes >= 15)
                    {
                        result.Status = "Expired";

                        string result2 = UserData.UpdateOTP(result.Email, result.Code, result.Status);
                        response.statusCode = "01";
                        response.status = "Expired otp";
                        response.data = null;

                    }
                    else
                    {
                        result.Status = "Verified";
                        string result2 = UserData.UpdateOTP(result.Email, result.Code, result.Status);
                        if (result2 == "00")
                        {
                            response.statusCode = "00";
                            response.status = "Verified otp";
                            response.data = null;
                        }
                        else
                        {
                            response.statusCode = "01";
                            response.status = "unable to verify otp try again";
                            response.data = null;
                        }
                    }

                }
                else
                {
                    response.statusCode = "01";
                    response.status = "request was unsuccessful. User not found";
                    response.data = email;
                }


            
            return response;
        }

        internal static Response ResendOTP(Email email)
        {
            Response response = new Response();
            User user = new User();
            user.Email = email.UserEmail;
            if (email.Otptype == 1)
            {
                string result = SendOTP(user, "Password Reset");
                if (result == "00")
                {
                    response.statusCode = "00";
                    response.status = "OTP sent to email";
                    response.data = email;
                }
                else
                {
                    response.statusCode = "01";
                    response.status = "OTP failed to send";
                    response.data = email;
                }
            }
            else {
                string result = SendOTP(user, "Email Verification");
                if (result == "00")
                {
                    response.statusCode = "00";
                    response.status = "OTP sent to email";
                    response.data = email;
                }
                else
                {
                    response.statusCode = "01";
                    response.status = "OTP failed to send";
                    response.data = email;
                }
            }

            return response;
        }

        internal static Response SuspendAccount(string email)
        {
            var response = new Response();
            string result = string.Empty;
            User user = UserData.GetUserUsingEmail(email);

            if (user.Email == string.Empty)
            {
                response.statusCode = "01";
                response.status = "request was unsuccessful. User not found";
                return response;
            }


            result = UserData.SuspendAccount(user);
            if (result == "00")
            {
                string MessageBody = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\"><head>\n  <meta charset=\"UTF-8\">\n  <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\">\n  <meta name=\"x-apple-disable-message-reformatting\">\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n  <meta content=\"telephone=no\" name=\"format-detection\">\n  <title></title>\n  <!--[if (mso 16)]>\n    <style type=\"text/css\">\n    a {{text-decoration: none;}}\n    </style>\n    <![endif]-->\n  <!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]-->\n  <!--[if gte mso 9]>\n<xml>\n    <o:OfficeDocumentSettings>\n    <o:AllowPNG></o:AllowPNG>\n    <o:PixelsPerInch>96</o:PixelsPerInch>\n    </o:OfficeDocumentSettings>\n</xml>\n<![endif]-->\n <!--[if mso]>\n <style type=\"text/css\">\n     ul {{\n  margin: 0 !important;\n  }}\n  ol {{\n  margin: 0 !important;\n  }}\n  li {{\n  margin-left: 47px !important;\n  }}\n\n </style><![endif]\n--></head>\n <body class=\"body\">\n  <div dir=\"ltr\" class=\"es-wrapper-color\">\n   <!--[if gte mso 9]>\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\n\t\t\t\t<v:fill type=\"tile\" color=\"#fafafa\"></v:fill>\n\t\t\t</v:background>\n\t\t<![endif]-->\n   <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\n    <tbody>\n     <tr>\n      <td class=\"esd-email-paddings\" valign=\"top\">\n       \n       \n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe\" align=\"center\">\n           <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p30t es-p30b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-image es-p10t es-p10b\" style=\"font-size: 0px;\"><a target=\"_blank\"><img src=\"https://fblkeki.stripocdn.email/content/guids/CABINET_67e080d830d87c17802bd9b4fe1c0912/images/55191618237638326.png\" alt=\"\" style=\"display: block;\" width=\"100\"></a></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10b es-m-txt-c\"><h1 style=\"font-size: 46px; line-height: 100%;\">Rocky Connect Account Suspended</h1></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Hi {user.FirstName}, taking a break is necessary, we will await your return. Your account was successfuly Suspended. No further action is required from you.&nbsp;</p></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10t es-p5b\"><p>If you did not register with us, please disregard this email.</p></td>\n                     </tr>\n                     \n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Rocky Connect... Nowhere is beyond reach</p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-footer\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-footer-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"640\" style=\"background-color: transparent;\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20t es-p20b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"600\" class=\"esd-container-frame\" align=\"left\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-social es-p15t es-p15b\" style=\"font-size:0\">\n                       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-table-not-adapt es-social\">\n                        <tbody>\n                         <tr>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Facebook\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png\" alt=\"Fb\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"X.com\" src=\"https://localfiles.stripocdn.email/content/assets/img/social-icons/logo-black/x-logo-black.png\" alt=\"X\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Instagram\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png\" alt=\"Inst\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\"><a target=\"_blank\" href=\"\"><img title=\"Youtube\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png\" alt=\"Yt\" width=\"32\"></a></td>\n                         </tr>\n                        </tbody>\n                       </table></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p35b\"><p>&nbsp;© 2021 Rocky Connect, Inc. All Rights Reserved.</p><p>1543 West Jackson st, Macomb, Illinois, US, 61455</p></td>\n                     </tr>\n                     \n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content esd-footer-popover\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"background-color: transparent;\" bgcolor=\"rgba(0, 0, 0, 0)\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-infoblock\"><p><a target=\"_blank\"></a>No longer want to receive these emails?&nbsp;<a href=\"\" target=\"_blank\">Unsubscribe</a>.<a target=\"_blank\"></a></p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table></td>\n     </tr>\n    </tbody>\n   </table>\n  </div>\n \n</body></html>";
                string send = UtilityService.SendEmail(MessageBody, user.Email, "Account Deleted");
                if (send == "00")
                {
                    response.status = "Account Successfully Deleted";
                    response.statusCode = "00";


                }
                else
                {
                    response.status = "Account Deletion was successful, but email was unsuccessfully sent.";
                    response.statusCode = "01";


                }

            }
            else
            {
                response.statusCode = "01";
                response.status = "request was unsuccessful.";
            }
            return response;
        }
        internal static Response DeleteAccount(string email)
        {
            var response = new Response();
            string result = string.Empty;
            User user = UserData.GetUserUsingEmail(email);

            if (user.Email == string.Empty)
            {
                response.statusCode = "01";
                response.status = "request was unsuccessful. User not found";
                return response;
            }


            result = UserData.DeleteAccount(user);
            if (result == "00")
            {
                string MessageBody = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\"><head>\n  <meta charset=\"UTF-8\">\n  <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\">\n  <meta name=\"x-apple-disable-message-reformatting\">\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n  <meta content=\"telephone=no\" name=\"format-detection\">\n  <title></title>\n  <!--[if (mso 16)]>\n    <style type=\"text/css\">\n    a {{text-decoration: none;}}\n    </style>\n    <![endif]-->\n  <!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]-->\n  <!--[if gte mso 9]>\n<xml>\n    <o:OfficeDocumentSettings>\n    <o:AllowPNG></o:AllowPNG>\n    <o:PixelsPerInch>96</o:PixelsPerInch>\n    </o:OfficeDocumentSettings>\n</xml>\n<![endif]-->\n <!--[if mso]>\n <style type=\"text/css\">\n     ul {{\n  margin: 0 !important;\n  }}\n  ol {{\n  margin: 0 !important;\n  }}\n  li {{\n  margin-left: 47px !important;\n  }}\n\n </style><![endif]\n--></head>\n <body class=\"body\">\n  <div dir=\"ltr\" class=\"es-wrapper-color\">\n   <!--[if gte mso 9]>\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\n\t\t\t\t<v:fill type=\"tile\" color=\"#fafafa\"></v:fill>\n\t\t\t</v:background>\n\t\t<![endif]-->\n   <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\n    <tbody>\n     <tr>\n      <td class=\"esd-email-paddings\" valign=\"top\">\n       \n       \n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe\" align=\"center\">\n           <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p30t es-p30b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-image es-p10t es-p10b\" style=\"font-size: 0px;\"><a target=\"_blank\"><img src=\"https://fblkeki.stripocdn.email/content/guids/CABINET_67e080d830d87c17802bd9b4fe1c0912/images/55191618237638326.png\" alt=\"\" style=\"display: block;\" width=\"100\"></a></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10b es-m-txt-c\"><h1 style=\"font-size: 46px; line-height: 100%;\">Rocky Connect Account Deleted</h1></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Hi {user.FirstName}, we are sad to see you go, Your account was successfuly deleted. No further action is required from you.&nbsp;</p></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p10t es-p5b\"><p>If you did not register with us, please disregard this email.</p></td>\n                     </tr>\n                     \n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p5t es-p5b es-p40r es-p40l es-m-p0r es-m-p0l\"><p>Rocky Connect... Nowhere is beyond reach</p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-footer\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-footer-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"640\" style=\"background-color: transparent;\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20t es-p20b es-p20r es-p20l\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"600\" class=\"esd-container-frame\" align=\"left\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-social es-p15t es-p15b\" style=\"font-size:0\">\n                       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-table-not-adapt es-social\">\n                        <tbody>\n                         <tr>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Facebook\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png\" alt=\"Fb\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"X.com\" src=\"https://localfiles.stripocdn.email/content/assets/img/social-icons/logo-black/x-logo-black.png\" alt=\"X\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\" class=\"es-p40r\"><a target=\"_blank\" href=\"\"><img title=\"Instagram\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png\" alt=\"Inst\" width=\"32\"></a></td>\n                          <td align=\"center\" valign=\"top\"><a target=\"_blank\" href=\"\"><img title=\"Youtube\" src=\"https://fblkeki.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png\" alt=\"Yt\" width=\"32\"></a></td>\n                         </tr>\n                        </tbody>\n                       </table></td>\n                     </tr>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-p35b\"><p>&nbsp;© 2021 Rocky Connect, Inc. All Rights Reserved.</p><p>1543 West Jackson st, Macomb, Illinois, US, 61455</p></td>\n                     </tr>\n                     \n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table>\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content esd-footer-popover\" align=\"center\">\n        <tbody>\n         <tr>\n          <td class=\"esd-stripe esd-synchronizable-module\" align=\"center\">\n           <table class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"background-color: transparent;\" bgcolor=\"rgba(0, 0, 0, 0)\">\n            <tbody>\n             <tr>\n              <td class=\"esd-structure es-p20\" align=\"left\">\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                <tbody>\n                 <tr>\n                  <td width=\"560\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n                    <tbody>\n                     <tr>\n                      <td align=\"center\" class=\"esd-block-text es-infoblock\"><p><a target=\"_blank\"></a>No longer want to receive these emails?&nbsp;<a href=\"\" target=\"_blank\">Unsubscribe</a>.<a target=\"_blank\"></a></p></td>\n                     </tr>\n                    </tbody>\n                   </table></td>\n                 </tr>\n                </tbody>\n               </table></td>\n             </tr>\n            </tbody>\n           </table></td>\n         </tr>\n        </tbody>\n       </table></td>\n     </tr>\n    </tbody>\n   </table>\n  </div>\n \n</body></html>";
                string send = UtilityService.SendEmail(MessageBody, user.Email, "Account Deleted");
                if (send == "00")
                {
                    response.status = "Account Successfully Deleted";
                    response.statusCode = "00";


                }
                else
                {
                    response.status = "Account Deletion was successful, but email was unsuccessfully sent.";
                    response.statusCode = "01";


                }

            }
            else
            {
                response.statusCode = "01";
                response.status = "request was unsuccessful.";
            }
            return response;
        }

        internal static Response UpdateAccount(UserUpdateRequest customer)
        {
            var response = new Response();
            string result;

            User user = UserData.GetUserUsingEmail(customer.Email);
            if (user.Email is not null)
            {

                user.FirstName = customer.FirstName;
                user.LastName = customer.LastName;
                user.PhoneNumber = customer.PhoneNumber;

                result = UserData.UpdateData(user);
                if (result == "00")
                {
                    response.statusCode = "00";
                    response.status = "Profile was Successfully Updated";

                }
                else
                {
                    response.statusCode = "01";
                    response.status = "Profile update was unsuccessful";
                }
            }
            else {
                response.statusCode = "01";
                response.status = "User account not found";
            }
            return response;

        }

        internal static Response ForgotPassword(PasswordForgotRequest request)
        {
            Response response = new Response();
            // User user = new User();
            string result;

            User user = UserData.GetUserUsingEmail(request.Email);
            if (user.Email is not null)
            {
                string pass = UtilityService.HashKeys(request.Password.ToLower());
                user.Password = pass;


                result = UserData.UpdateData(user);
                if (result == "00")
                {
                    response.statusCode = "00";
                    response.status = "Password Reset Successful";

                }
                else
                {
                    response.statusCode = "01";
                    response.status = "Password Reset Failed";
                }
            }
            else
            {
                response.statusCode = "01";
                response.status = "request was unsuccessful. User not found";
            }
            return response;

        }

        internal static Response GetUserAccount(string email)
        {
            var status = new Response();

            User result = UserData.GetUserUsingEmail(email);
            if (result.Email is not null) {
                status.statusCode = "00";
                status.status = "Successfull";
                result.Password = "";
                status.data = result;
            }
            else
            {

                status.statusCode = "01";
                status.status = "No User Account with that email exist";
                status.data = null;
            }
            return status;
        }
        internal static Response GetDriver(string email)
        {
            var status = new Response();

            Driver result = UserData.GetDriver(email);
            if (result.Email is not null)
            {
                status.statusCode = "00";
                status.status = "Successfull";
                status.data = result;
            }
            else
            {

                status.statusCode = "00";
                status.status = "Successfull";
                status.data = null;
            }
            return status;
        }

        internal static Response VerifyOtp(VerifyOTP request)
        {
            throw new NotImplementedException();
        }
    }
    }