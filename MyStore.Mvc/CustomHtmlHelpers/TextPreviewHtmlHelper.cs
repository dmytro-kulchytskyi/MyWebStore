using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Mvc.CustomHtmlHelpers
{
    public static class TextPreviewHtmlHelper
    {
        public static MvcHtmlString PreviewText(this HtmlHelper htmlHelper, string text, int maxLength, string lineEnding = null)
        {
            if (maxLength < 1)
                throw new ArgumentException($"{maxLength} must be positive number");

            text = text?.Trim() ?? "";

            if (text.Length > maxLength)
                text = text.Substring(0, maxLength).Trim() + (lineEnding ?? AppConfiguration.LineOwerflowEnding);

            return new MvcHtmlString(text);
        }
    }
}