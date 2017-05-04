using ActiveUp.Net.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            var mailRepository = new GoogleImapEmail(
                            "imap.gmail.com",
                            993,
                            true,
                            "911.land.com@gmail.com",
                            "password"
                        );

            var emailList = mailRepository.GetAllMails("inbox");

            foreach (Message email in emailList)
            {
                string message = string.Format("<p>{0}: {1}</p><p>{2}</p>", email.From, email.Subject, email.BodyHtml.Text);
                Console.WriteLine(message);
                if (email.Attachments.Count > 0)
                {
                    foreach (MimePart attachment in email.Attachments)
                    {
                        Console.WriteLine("<p>Attachment: {0} {1}</p>", attachment.ContentName, attachment.ContentType.MimeType);
                    }
                }
                WriteLog(message);
            }
            Console.ReadLine();
        }

        public static void WriteLog(string message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Email.txt", true);
                sw.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
