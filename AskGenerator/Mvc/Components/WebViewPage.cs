using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Mvc.Components
{
    public class BaseWebViewPage<TModel> : WebViewPage<TModel>
    {
        public override void Execute()
        {
        }
    }

    public class BaseWebViewPage : WebViewPage
    {
        public override void Execute()
        {
        }
    }
}
