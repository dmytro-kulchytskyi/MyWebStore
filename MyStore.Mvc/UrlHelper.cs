using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace MyStore.Mvc
{
    public class UrlHelper
    {
        public static string GetUrlSafeLine(string line)
        {
            return Regex.Replace(line, @"([^\wА-Яа-яі]+)", "_", RegexOptions.Compiled);
        }
    }
}