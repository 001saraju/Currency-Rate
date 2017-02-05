using CurrencyRate.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web;

namespace CurrencyRate
{
    public class RateSchedular
    {
        private Timer _fatchRate;
        public RateSchedular()
        {
            int interval = Convert.ToInt32(ConfigurationManager.AppSettings["RateIntervalInMinutes"].ToString());
            _fatchRate = new Timer(interval * 60 * 1000);
            _fatchRate.Elapsed += OnFatchCurrencyRate;
            _fatchRate.Start();
        }
        private void OnFatchCurrencyRate(object sender, ElapsedEventArgs e)
        {
            try
            {
                List<string> currencyList = new List<string> { "USD", "GBP", "AUD", "EUR", "CAD", "SGD" };
                currencyList.ForEach(currency =>
                {
                    Task t = new Task((state) =>
                    {
                        try
                        {
                            RestClient client = new RestClient { BaseUrl = new Uri("https://www.google.com") };
                            string resource = string.Format("/finance/converter?a={2}&from={0}&to={1}", currency, "inr", 1);
                            var request = new RestRequest(resource, Method.GET);

                            IRestResponse res = client.Execute(request);
                            var response = res.Content;
                            var split = response.Split((new string[] { "<span class=bld>" }), StringSplitOptions.None);
                            var value = split[1].Split(' ')[0];
                            decimal conversionRate = decimal.Parse(value, CultureInfo.InvariantCulture);
                            using (RateDbContext db = new RateDbContext())
                            {
                                Rate rate = new Rate
                                {
                                    CurrencyCode = currency,
                                    ConversionRate = conversionRate,
                                    AddedOn = DateTime.Now
                                };
                                db.Rates.Add(rate);
                                db.SaveChanges();
                            }
                        }
                        catch
                        {
                        }
                    }, new { Currency = currency });
                    t.Start();
                });
            }
            catch
            {
            }
        }

    }
}