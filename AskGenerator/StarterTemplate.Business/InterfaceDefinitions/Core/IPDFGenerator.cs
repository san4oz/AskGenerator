using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AskGenerator.Business.InterfaceDefinitions.Core
{
    public interface IPDFGenerator
    {
        FileStreamResult Generate(HttpContext context, string fileName, string url);

        byte[] GetBytes(HttpContext context, string fileName, string url);
    }
}
