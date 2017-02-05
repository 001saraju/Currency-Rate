using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyRate.ViewModel
{
    public class RateViewModel
    {
        public string SourceCurrency { get; set; }
        public decimal ConversionRate { get; set; }
        public decimal Amount { get; set; }
        public decimal Total { get; set; }
        public int returncode { get; set; }
        public string err { get; set; }
        public long timestamp { get; set; }

    }
}