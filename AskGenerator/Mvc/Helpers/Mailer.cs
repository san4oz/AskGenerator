using AskGenerator.Mvc.Helpers;
using Resources;
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
        public static Dictionary<string, string> DefaultTags { get; private set; }
        static Mailer()
        {
            TextHtml = new ContentType("text/html");
            TextPlain = new ContentType("text/plain");
            From = new MailAddress("networkofmentorsofficial@gmail.com", "Evaluate from NoM");
            Mails = MailsData.ResourceManager;

            DefaultTags = new Dictionary<string,string>(10);
            DefaultTags.Add("siteURL", "http://ztu-fikt.azurewebsites.net/");
            DefaultTags.Add("siteName", "Evaluate");
            DefaultTags.Add("vkURL", Resource.vkURL);
            DefaultTags.Add("fbURL", Resource.fbURL);
            DefaultTags.Add("vkdekURL", Resource.vkdekURL);
            DefaultTags.Add("nomURL", Resource.nomURL);
            DefaultTags.Add("bestURL", Resource.bestURL);
        }

        /// <summary>
        /// Sends message to the specified adress.
        /// </summary>
        /// <param name="mailName">Name of the message template.</param>
        /// <param name="to">Email adress send message to.</param>
        /// <param name="tags">Dictionary of replacement tags.</param>
        /// <param name="Bcc">The list of Bcc emails.</param>
        /// <remarks>
        /// Add subject and body to the MailsData resourses with <paramref name="mailName"/> + '_Subj'/ + "_Body" names.
        /// Use http://www.quackit.com/html/online-html-editor/ to format mails.
        /// Use '[tag]' marks in text to replace them with tags.
        /// </remarks>
        public static void Send(string mailName, string to, IDictionary<string, string> tags, IEnumerable<string> Bcc = null)
        {
            if (tags == null)
                tags = new Dictionary<string, string>();
            foreach(var pair in DefaultTags)
            {
                if (!tags.ContainsKey(pair.Key))
                    tags[pair.Key] = pair.Value;
            }

            var subject = Mails.GetString(mailName + "_Subj").ReplaceTags(tags);
            var body = Mails.GetString(mailName + "_Body").ReplaceTags(tags);
            var plain = Mails.GetString(mailName + "_Plain").ReplaceTags(tags);
            Send(subject, body, to, plain, Bcc);
        }

        /// <summary>
        /// Sends message to the specified adress.
        /// </summary>
        /// <param name="subject">The subject of the message.</param>
        /// <param name="to">Email adress send message to.</param>
        /// <param name="body">The body of the message.</param>
        /// <param name="plainBody">The alterante plane text message.</param>
        /// <param name="Bcc">The list of Bcc emails.</param>
        public static void Send(string subject, string body, string to, string plainBody = null, IEnumerable<string> Bcc = null)
        {
            var message = new MailMessage(From, new MailAddress(to));
            if (Bcc != null)
                foreach (var email in Bcc)
                    message.Bcc.Add(email);

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
