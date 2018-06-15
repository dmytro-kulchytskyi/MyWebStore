using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business
{
    public class PriceFormat
    {
        public static string GetFormatedPrice(decimal price)
        {
            var numberFormatInfo = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
            numberFormatInfo.CurrencySymbol = "грн.";
            return price.ToString("C2", numberFormatInfo);
        }
    }
}
