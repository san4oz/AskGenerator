using AskGenerator.Mvc.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
namespace AskGenerator.Helpers
{
    public static class Mailer
    {
        public static MailAddress From { get; private set; }
        public static System.Resources.ResourceManager Mails { get; private set; }
        public static ContentType TextHtml { get; private set; }
        public static ContentType TextPlain { get; private set; }
        static Mailer()
        {
            TextHtml = new ContentType("text/html");
            TextPlain = new ContentType("text/plain");
            From = new MailAddress("networkofmentorsofficial@gmail.com", "Evaluate from NoM");
            Mails = MailsData.ResourceManager;
        }

        /// <summary>
        /// Sends message to the specified adress.
        /// </summary>
        /// <param name="mailName">Name of the message template.</param>
        /// <param name="to">Email adress send message to.</param>
        /// <param name="tags">Dictionary of replacement tags.</param>
        /// <remarks>
        /// Add subject and body to the MailsData resourses with <paramref name="mailName"/> + '_Subj'/ + "_Body" names.
        /// Use http://www.quackit.com/html/online-html-editor/ to format mails.
        /// Use '[tag]' marks in text to replace them with tags.
        /// </remarks>
        public static void Send(string mailName, string to, IDictionary<string, string> tags)
        {
            var subject = Mails.GetString(mailName + "_Subj").ReplaceTags(tags);
            var body = Mails.GetString(mailName + "_Body").ReplaceTags(tags);
            var plain = Mails.GetString(mailName + "_Plain").ReplaceTags(tags);
            Send(subject, body, to, plain);
        }

        /// <summary>
        /// Sends message to the specified adress.
        /// </summary>
        /// <param name="subject">The subject of the message.</param>
        /// <param name="to">Email adress send message to.</param>
        /// <param name="body">The body of the message.</param>
        /// <param name="plainBody">The alterante plane text message.</param>
        public static void Send(string subject, string body, string to, string plainBody = null)
        {
            var message = new MailMessage(From, new MailAddress(to));
            message.Subject = subject;

            message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(plainBody ?? string.Empty, TextPlain));
            if (string.IsNullOrEmpty(body))
            {
                message.Body = plainBody ?? string.Empty;
            }
            else
            {
                message.IsBodyHtml = true;
                message.Body = body;
                message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, TextHtml));
            }
            
            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential
                {
                    UserName = "networkofmentorsofficial@gmail.com",
                    Password = "networkofmentorsofficial1"
                };
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }

        public static string ReplaceTags(this string template, IDictionary<string, string> tags)
        {
            if (string.IsNullOrWhiteSpace(template) || !tags.Any())
                return template;

            var sb = new StringBuilder(template);
            foreach (var tag in tags)
                sb.Replace('[' + tag.Key + ']', tag.Value);
            return sb.ToString();
        }
    }


}
