﻿using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Tools
{
    public class EmailHelper
    {
        SmtpClient client;

        private string email = "asanateam.az@gmail.com";
        private string password = "asana12345";

      

        public EmailHelper()
        {
            client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(email, password);
        }

        public void Send(string subject, string message, string receiver = "asanateam.az@gmail.com", string sender = "asanateam.az@gmail.com")
        {
            try
            {
                MailMessage mailMessage = new MailMessage(sender, receiver, subject, message);
                client.Send(mailMessage);
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
            }
        }


        public void SendRegisterActivationCode(string receiver, string sender = "asanateam.az@gmail.com")
        {
            try
            {
                string newhtml = HtmlParser.InsertInto('^', FileHelper.FindFile("//Resources//verify.html"));
                MailMessage mailMessage = new MailMessage(sender, receiver, "Register Activation Code!", newhtml);
                mailMessage.IsBodyHtml = true;
                client.Send(mailMessage);
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
            }
        }

        public void SendForgotPasswordCode(string receiver, string sender = "asanateam.az@gmail.com")
        {
            try
            {
                string newhtml = HtmlParser.InsertInto('^', FileHelper.FindFile("//Resources//forgotpassword.html"));
                MailMessage mailMessage = new MailMessage(sender, receiver, "Forgot Password", newhtml);
                mailMessage.IsBodyHtml = true;
                client.Send(mailMessage);
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
            }
        }

    }
}
