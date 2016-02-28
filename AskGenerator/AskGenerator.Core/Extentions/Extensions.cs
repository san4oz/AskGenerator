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
    }
}