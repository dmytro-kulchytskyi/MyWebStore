using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace DimasikStore.Mvc.Identity.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage identityMessage)
        {
            if (identityMessage == null)
            {
                throw new ArgumentNullException(nameof(identityMessage));
            }

            using (var message = new MailMessage())
            using (var smtpServer = new SmtpClient())
            {
                message.To.Add(identityMessage.Destination);
                message.Subject = identityMessage.Subject;
                message.Body = identityMessage.Body;
                message.IsBodyHtml = true;

                await smtpServer.SendMailAsync(message);
            }
        }
    }
}