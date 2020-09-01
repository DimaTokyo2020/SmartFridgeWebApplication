using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartFridgeWebApllication.Service
{
    public class EmailSenderService : IEmailSender

    {
        private string _username;
        private string _apikey;

        public EmailSenderService(string username, string apikey)
        {
            _username = username;
            _apikey = apikey;
        }

        
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            var client = new SendGridClient(_apikey);
            var message = new SendGridMessage
            {//this is the email that the new client get for autorization email
                From = new EmailAddress("dima.logven@gmail.com", _username),
                Subject = subject,
                HtmlContent = htmlMessage
            };

            message.AddTo(new EmailAddress(email));

            return client.SendEmailAsync(message);



        }
    }
}
