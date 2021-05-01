using BS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BS.Api.Providers
{
    public class MailProvider
    {
        public static bool SendMail(Store store, string Recipient, bool IsHtml, string Subject, string Message)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(store.SenderEmail);
                message.To.Add(new MailAddress(Recipient));
                message.CC.Add(new MailAddress(store.SenderEmail));
                message.Subject = Subject;
                message.IsBodyHtml = IsHtml;
                message.Body = (IsHtml) ? InitMailBody(store, Message) : Message;

                if(!string.IsNullOrEmpty(store.Port))
                    smtp.Port = Convert.ToInt32(store.Port);

                smtp.Host = store.Host;
                smtp.EnableSsl = store.IsSslEnabled;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential(store.SenderEmail, store.SenderEmailPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static string InitMailBody(Store store, string message)
        {
            try
            {
                string body = "<div style='background: #f5f4f4;padding: 50px;'>"
                            + " <div style='background: #fff;width:100%;box-shadow: 2px, 3px, 3px 3px #ccc !important;'>"
                            + "     <div style='background: #051bb9;padding: 30px;'>"
                            + "         <h1 style='margin: auto; text-align: center; color: #fff'> " + store.Name + " </h1>"
                            + "     </div>"
                            + "     <div style='padding: 30px;'>"
                            + message
                            + "         <h5 style='margin-top: 85px;'> Best Regards,</h5>"
                            + "         <span> " + store.Name + " Management </span>"
                            + "     </div>"
                            + " </div>"
                            + "</div>";
                return body;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
