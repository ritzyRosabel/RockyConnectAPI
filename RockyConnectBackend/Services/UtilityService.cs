﻿using System;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace RockyConnectBackend.Services
{
	public class UtilityService
	{
		public UtilityService()
		{
		}
        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsPhoneNbr(string number)
        {
            string motif = @"^([\+]?33[-]?|[0])?[1-9][0-9]{8}$";

            if (number != null) return Regex.IsMatch(number, motif);
            else return false;
        }

        public static string SendEmail (string body, string To, string Subject)
        {
            string fromMail = "rockyconnect32@gmail.com";
            string fromPassword = "irevzenvrfsaxxwy";
            string response = "";
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = Subject;
            message.To.Add(new MailAddress(To));
            message.Body = $"<html><body>{body}</body></html>";
            message.IsBodyHtml = true;

            
            
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            try
            {
                smtpClient.Send(message);
                response = "00";
            }
            catch(Exception e)
            {
                response = "01";
            }
            return response;
          
        }
        public static string RandomOTPGenerator() {

            Random r = new Random();
            int randNum = r.Next(1000000);
            string otp = randNum.ToString("D6");

            return otp;

        }

        internal static string SendPhone(string messageBody, string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}

