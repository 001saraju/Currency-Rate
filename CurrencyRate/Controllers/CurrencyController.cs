using CurrencyRate.filters;
using CurrencyRate.Models;
using CurrencyRate.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurrencyRate.Controllers
{
    public class CurrencyController : Controller
    {
        [HttpPost]
        [ValidateRate]
        [OutputCache(Duration = 120, VaryByParam = "CurrencyCode")]
        public ActionResult Rate(string CurrencyCode = "USD", decimal Amount = 1)
        {
            RateViewModel data = new RateViewModel();
            using (RateDbContext db = new RateDbContext())
            {
                Rate latestRate = db.Rates.Where(x => x.CurrencyCode == CurrencyCode).OrderByDescending(x => x.Id).FirstOrDefault();
                if (latestRate != null)
                {
                    data.SourceCurrency = latestRate.CurrencyCode;
                    data.ConversionRate = Math.Round(latestRate.ConversionRate, 2);
                    data.Amount = Amount;
                    data.Total = Math.Round(latestRate.ConversionRate * Amount, 2);
                    data.returncode = 1;
                    data.err = "success";
                    data.timestamp = latestRate.AddedOn.Ticks;
                }
                else
                {
                    data.SourceCurrency = CurrencyCode;
                    data.Amount = Amount;
                    data.returncode = 2;
                    data.err = "error";
                }
            }
            return Json(data);
        }
    }
}