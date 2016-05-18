using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Newtonsoft.Json;

namespace System.Web
{
    public static class WebExtentions
    {
        const string DefaultGlyphicon = "glyphicon-chevron-right";

        public static MvcHtmlString MenuItem(
            this HtmlHelper htmlHelper,
            string text,
            string action,
            string controller,
            string glyphicon = DefaultGlyphicon,
            string liCssClass = null,
            string area = ""
        )
        {
            var url = new UrlHelper(htmlHelper.ViewContext.RequestContext).Action(action, controller, new { area = area }).ToString();
            return CreateMenuItem(htmlHelper, text, null, liCssClass, url, glyphicon);
        }

        public static MvcHtmlString MenuRouteItem(
            this HtmlHelper htmlHelper,
            string text,
            string routeName,
            string glyphicon = DefaultGlyphicon,
            string liCssClass = null
        )
        {
            var url = new UrlHelper(htmlHelper.ViewContext.RequestContext).RouteUrl(routeName).ToString();
            return CreateMenuItem(htmlHelper, text, null, liCssClass, url, glyphicon);
        }

        private static MvcHtmlString CreateMenuItem(HtmlHelper htmlHelper, string text,
            bool? isActive,
            string liCssClass,
            string url,
            string glyphicon)
        {
            if (!isActive.HasValue)
            {
                var uri = new Uri(url, UriKind.RelativeOrAbsolute);
                if (!uri.IsAbsoluteUri)
                    uri = new Uri(htmlHelper.ViewContext.HttpContext.Request.Url, uri);
                isActive = htmlHelper.ViewContext.HttpContext.Request.Url.AbsolutePath
                    .Equals(uri.AbsolutePath, StringComparison.OrdinalIgnoreCase);
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
            li.InnerHtml = "<a href=\"{0}\"><i class=\"glyphicon {1}\"></i>{2}</a>"
                .FormatWith(url, glyphicon, text);
            return MvcHtmlString.Create(li.ToString());
        }

        public static MvcHtmlString SubmitBtn(this HtmlHelper helper, string text)
        {
            return "<button type=\"submit\" class=\"btn btn-primary\">{0}</button>".FormatWith(text).AsHtml();
        }

        public static MvcHtmlString ClearBtn(this HtmlHelper helper, string text)
        {
            return "<button type=\"reset\" class=\"btn btn-default\">{0}</button>".FormatWith(text).AsHtml();
        }

        public static MvcHtmlString AsHtml(this string str)
        {
            return new MvcHtmlString(str);
        }

        public static string ToJson(this object obj)
        {
            var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.None };
            var str = JsonConvert.SerializeObject(obj, settings);
            return str;
        }
    }
}