using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Solicita.TG.Sender
{
    public class RunnerTask
    {
        public void SendMail()
        {
            String path = ConfigurationManager.AppSettings["FolderMails"].ToString();

            CheckDirectories();

            ReadXML(path);
        }

        private void CheckDirectories()
        {
            if (!Directory.Exists(ConfigurationManager.AppSettings["FolderMails"].ToString()))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["FolderMails"].ToString());

            if (!Directory.Exists(ConfigurationManager.AppSettings["FolderDefault"].ToString()))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["FolderDefault"].ToString());
        }

        public void ReadXML(string path)
        {
            string[] fileEntries = Directory.GetFiles(path);

            foreach (string fileName in fileEntries)
            {
                XDocument xmlDocument = new XDocument();
                xmlDocument = XDocument.Load(fileName);

                String to = xmlDocument.Element("Mail").Element("To").Attribute("name").Value;
                String subject = xmlDocument.Element("Mail").Element("Subject").Attribute("name").Value;
                String body = xmlDocument.Element("Mail").Element("Body").Attribute("name").Value;

                Mail mail = new Mail() { To = to, Body = body, Subject = subject };

                try
                {
                    Send(mail);
                    Console.WriteLine("Enviando email para " + to + "...");
                    File.Delete(fileName);
                    Console.WriteLine("Email enviado.");
                }
                catch(Exception e)
                {
                    Console.WriteLine("Erro ao enviar email para o destinatario: " + to + " ás: " + DateTime.Now.TimeOfDay.ToString());
                }
            }
        }


        public static void Send(Mail mail)
        {
            MailMessage mailMessage = new MailMessage(ConfigurationManager.AppSettings["MailSender"].ToString(), mail.To);
            SmtpClient client = new SmtpClient("smtp.live.com", 587);
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(ConfigurationManager.AppSettings["MailSender"].ToString(), ConfigurationManager.AppSettings["PwdSender"].ToString());
            client.EnableSsl = true;
            client.Credentials = credentials;
            mailMessage.Subject = mail.Subject;
            mailMessage.Body = mail.Body;
            mailMessage.IsBodyHtml = true;
            client.Send(mailMessage);
        }
    }
}
