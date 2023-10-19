﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace Demo_WP1.Common
{
    public class Common
    {
        private static string email = ConfigurationManager.AppSettings["Email"];
        private static string pass = ConfigurationManager.AppSettings["Password"];
        public static bool SendMail(string name, string subject, string content, string toMail)
        {
            bool rs = false;
            try
            {
                MailMessage message = new MailMessage();
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(email, pass);
                    smtp.Timeout = 20000;
                    MailAddress fromAddress = new MailAddress(email, name);
                    message.From = fromAddress;
                    message.To.Add(toMail);
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    message.Body = content;
                    smtp.Send(message);
                    rs = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                rs = false;
            }
            return rs;
        }
    }
}