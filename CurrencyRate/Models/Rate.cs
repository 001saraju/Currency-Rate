using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CurrencyRate.Models
{
    public class Rate
    {
        [Key]
        public int Id { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ConversionRate { get; set; }
        public DateTime AddedOn { get; set; }
    }
}