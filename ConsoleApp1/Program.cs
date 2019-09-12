using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ConsoleApp1
{



    class Program
    {
        /*
        public static void sendCallBack(object sender, AsyncCompletedEventArgs e){
            String token = (string)e.UserState;
            if (e.Cancelled)
                Console.WriteLine("[{0}] Send canceled.", token);
            if (e.Error != null)
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            else
                Console.WriteLine("Message sent." + token);
        }


        static void Main(string[] args)
        {
            MailAddress _emailFrom;
            MailAddress _emailTo;
            MailMessage _mailMessage;
             
            _emailFrom = new MailAddress("kimdimka@yandex.ru");
            _emailTo = new MailAddress("kimdimka@inbox.ru");
            _mailMessage = new MailMessage(_emailFrom, _emailTo);

            _mailMessage.Subject = "Тест";
            _mailMessage.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
            _mailMessage.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            smtp.Credentials = new NetworkCredential("kimdimka", "rfhn_rkthbr");
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.SendCompleted += new SendCompletedEventHandler(sendCallBack);

            smtp.SendAsync(_mailMessage,"ok");
            Console.ReadKey();

        }
        */

        static void Main(string[] args)
        {
            string s = "%u0412%u0435%u043B%u0438%u043A%u0438%u0439 %u0424%u0438%u043B%u043B%u0438%u0430%u043B";
            string s1 = HttpUtility.UrlDecode(s);
            Console.WriteLine(s1);
            Console.ReadKey();

        }

    }
}
