using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace System.Web
{
    public static class MenuExtensions
    {
        public static MvcHtmlString MenuItem(
            this HtmlHelper htmlHelper,
            string text,
            string action,
            string controller,
            string liCssClass = null,
            string area = ""
        )
        {
            var url = new UrlHelper(htmlHelper.ViewContext.RequestContext).Action(action, controller, new { area = area }).ToString();
            return CreateMenuItem(htmlHelper, text, null, liCssClass, url);
        }

        public static MvcHtmlString MenuItem(
            this HtmlHelper htmlHelper,
            string text,
            string routeName,
            string liCssClass = null
        )
        {
            var url = new UrlHelper(htmlHelper.ViewContext.RequestContext).RouteUrl(routeName).ToString();
            return CreateMenuItem(htmlHelper, text, null, liCssClass, url);
        }

        private static MvcHtmlString CreateMenuItem(HtmlHelper htmlHelper, string text, bool? isActive, string liCssClass, string url)
        {
            if (!isActive.HasValue)
            {
                var uri = new Uri(url, UriKind.RelativeOrAbsolute);
                isActive = htmlHelper.ViewContext.HttpContext.Request.Url.AbsolutePath
                    .StartsWith(uri.IsAbsoluteUri ? uri.LocalPath:url, StringComparison.OrdinalIgnoreCase);
            }
            var li = new TagBuilder("li");
            if (!String.IsNullOrEmpty(liCssClass))
            {
                li.AddCssClass(liCssClass);
            }
            if (isActive.Value)
            {
                li.AddCssClass("active");
            }
            li.InnerHtml = String.Format("<a href=\"{0}\"><i class=\"glyphicon glyphicon-chevron-right\"></i>{1}</a>",
               url
               , text);
            return MvcHtmlString.Create(li.ToString());
        }


    }

    public static class StringExtensions
    {
        public static MvcHtmlString YesNo(this string str)
        {
            return MvcHtmlString.Create(string.IsNullOrEmpty(str) ? "No" : "Yes");
        }
    }
}