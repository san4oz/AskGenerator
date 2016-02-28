using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System
{
    public static class StringExtentions
    {
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string FormatWith(this string str, object arg)
        {
            return string.Format(str, arg);
        }

        public static string FormatWith(this string str, object arg0, object arg1)
        {
            return string.Format(str, arg0, arg1);
        }

        public static string FormatWith(this string str, object arg0, object arg1, string arg2)
        {
            return string.Format(str, arg0, arg1, arg2);
        }

        public static string FormatWith(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        public static string Or(this string str, string defaultStr)
        {
            return str.IsEmpty() ? defaultStr : str;
        }

        /// <summary>
        /// Creates HTML string with 'Yes' text if string is not null or whitespace.
        /// Otherwise 'No'.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>Created HTML string.</returns>
        public static MvcHtmlString YesNo(this string str)
        {
            return MvcHtmlString.Create(str.IsNullOrWhiteSpace() ? "No" : "Yes");
        }
    }
}
