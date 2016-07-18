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
        /// <summary>
        /// Compares two strings ignoring character case and culture and supposing that <c>null</c> and empty string are also equal.
        /// </summary>
        /// <param name="str1">The first string to compare.</param>
        /// <param name="str2">The second string to compare.</param>
        /// <returns>The value indicating whether the two strings are equal.</returns>
        public static bool iEquals(this string str1, string str2)
        {
            return string.Equals(str1 ?? string.Empty, str2 ?? string.Empty, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Checks wheter current string is <c>null</c> or empty.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns>
        ///   <c>true</c> if string is <c>null</c> or empty.
        /// </returns>
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Cuts string to <see cref="maxLength"/> characters.
        /// </summary>
        /// <param name="str">String to cut.</param>
        /// <param name="maxLength">Max count of characters.</param>
        /// <returns>Cuted string.</returns>
        public static string Cut(this string str, int maxLength = 50)
        {
            var length = maxLength - 3;
            return str.Length > length ? str.Substring(0, length) + "..." : str;
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
