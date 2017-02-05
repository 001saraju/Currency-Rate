using CurrencyRate.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurrencyRate.filters
{
    public class ValidateRate: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currencyCode = filterContext.ActionParameters["currencyCode"];
            var validCurrency = new List<string> { "USD", "GBP", "AUD", "EUR", "CAD", "SGD" };
            if (currencyCode != null && !validCurrency.Contains(currencyCode.ToString().ToUpper()))
            {
                RateViewModel data = new RateViewModel
                {
                    SourceCurrency = currencyCode.ToString(),
                    err = "invalid currency code",
                };
                var result = new JsonResult();
                result.Data = data;
                filterContext.Result = result;
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}