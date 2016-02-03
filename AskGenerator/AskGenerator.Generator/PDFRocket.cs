using AskGenerator.Business.InterfaceDefinitions.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AskGenerator.Generator
{
    public class PDFRocket : IPDFGenerator
    {
        public FileStreamResult Generate(HttpContext context,string fileName, string url)
        {
            using (var client = new WebClient())
            {
                // Build the conversion options 
                NameValueCollection options = new NameValueCollection();
                options.Add("apikey", Credentials.Key);
                options.Add("value", url);

                // Call the API convert to a PDF
                MemoryStream ms = new MemoryStream(client.UploadValues(Credentials.Api, options));

                // Make the file a downloadable attachment - comment this out to show it directly inside
                context.Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".pdf");

                // Return the file as a PDF
                return new FileStreamResult(ms, "application/pdf");
            }
        }
    }
}
