using CurrencyProject.Api;
using Intuit.Ipp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nager.Date;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Globalization;
using CurrencyProject.DTOs;
using static System.Net.WebRequestMethods;


namespace CurrencyProject.Controllers
{
    public class CurrencyDateController : Controller
    {
        [HttpGet]
        public IActionResult DateReview()
        {
            var currencyCodes = DateReviewList();
            var currencyList = new SelectList(currencyCodes);
            ViewBag.currencyList = currencyList;


            return View();
            
        }
        [HttpPost]

        public async Task<ActionResult> DateReviewSubmitted(string fromCurrency, DateTime fromDate)
        {
            var exchangeRate = await DateReviewResult(fromCurrency, fromDate);
            ViewBag.exchangeRate = exchangeRate;
            

            var currencyCodes = DateReviewList();

            var currencyList = new SelectList(currencyCodes);
            ViewBag.currencyList = currencyList;

            return View();
        }

        private const string nbpApiTableUrl = RatesExchangeServices.datetimeReviewUrl;
        public const string datetimeReviewUrl = "http://api.nbp.pl/api/exchangerates/rates/a/{0}/{1}/";
        [HttpPost]
        public async Task<ActionResult>DateReviewResult(string fromCurrency, DateTime fromDate)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(datetimeReviewUrl, fromCurrency, fromDate.ToString("yyyy-MM-dd"));
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<RootDTO>(json);

                    ViewBag.fromCurrency = fromCurrency;
                    ViewBag.fromDate = fromDate;

                    var rate = data.rates[0].mid;

                    ViewBag.rate = rate;
                }
                else if (!response.IsSuccessStatusCode)
                {
                    var urlMinus = fromDate.AddDays(-1);
                    var newUrl = string.Format(datetimeReviewUrl, fromCurrency, urlMinus.ToString("yyyy-MM-dd"));
                    var responseSecond = await client.GetAsync(newUrl);

                    var json = await responseSecond.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<RootDTO>(json);

                    ViewBag.fromCurrency = fromCurrency;
                    ViewBag.fromDate = fromDate;

                    var rate = data.rates[0].mid;

                    ViewBag.rate = rate;

                }
                else
                {
                    throw new Exception("Invalid connection with Api");
                }
                return View();
            }
        }
        // Pobiera listę walut z Api

        private const string nbpApiUrl = RatesExchangeServices.exchangeratesUrl;
        private List<string> DateReviewList()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(nbpApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("tables/a").Result;

                if(response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    dynamic data = JsonConvert.DeserializeObject(json);
                    var currencies = new List<string>();

                    foreach(var rate in data[0].rates)
                    {
                        currencies.Add(rate.code.ToString());
                    }
                    return currencies;
                }
                else
                {
                    throw new Exception("Invalid connection with Api");
                }
            }
        }
    }
}
    

        
