using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Mvc.CustomHtmlHelpers
{
    public static class PriceFormatHtmlHelper
    {
        public static MvcHtmlString PriceFormat(this HtmlHelper htmlHelper, double price)
        {
            return MvcHtmlString.Create(price.ToString("C2", CultureInfo.CurrentCulture));
        }
    }
}