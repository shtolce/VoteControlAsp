using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;

namespace VoteProject.Services
{
    public class EmailService:IMessageSender
    {
        private MailAddress _emailFrom;
        private MailAddress _emailTo;
        private MailMessage _mailMessage;
        public EmailService(string from,string to) {
            _emailFrom = new MailAddress(from);
            _emailTo = new MailAddress(to);
            _mailMessage = new MailMessage(_emailFrom, _emailTo);
        }

        public void sendCallBack(object sender, AsyncCompletedEventArgs e)
        {
            String token = (string)e.UserState;
            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent." + token);

            }
        }

        public void Send(string FIO,string tel,string comment,string commentAlt) {
            _mailMessage.Subject = "Unacceptable rate";
            _mailMessage.Body = $@"<h2>Dear Premium Bank, 
                                    You received from 'Baş ofis' branch on 'Xidmət səviyyəsi service'.
                                  </h2>
                                   <p>Mark: {comment}</p>
                                   <p>Mark page: {commentAlt}</p>
                                   <p>contact {FIO} tel.{tel}</p>
                                   <p>Feedback: # </p>
                                   <p>You can react now to improve the customer service quality</p>
                                   <p>Don’t hesitate to email us with any question you have!</p>
                                   <p>Best regards,</p>

                                   <p> Qmeter team</p>
                                   <p> Qmeter LLC</p>
                                   <p> +994 12 555 55 55| +994 55 555 55 55(mobile) | info @qer.net | www.qmete.com</p>
                                   <p> SKS Plaza, Fuzuli k. 49, P.İ.AZ1014</p>
                                   <p> Moscow</p>
                                    ";
            _mailMessage.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            smtp.Credentials = new NetworkCredential("testerS123456", "testerS1234567");
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.SendCompleted += new SendCompletedEventHandler(sendCallBack);
            smtp.SendAsync(_mailMessage, "ok");
        }


    }
}
