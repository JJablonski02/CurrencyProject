using CurrencyProject.Api;
using CurrencyProject.DTOs;
using CurrencyProject.Models;
using Intuit.Ipp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace CurrencyProject.Controllers
{
    public class CurrencyController : Controller
    {



        // GET: HOME
        [HttpGet]
        public async Task<IActionResult> Home()
        {

            IList<Rates> rates = new List<Rates>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(RatesExchangeServices.baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("api/exchangerates/tables/A");

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<List<Root>>(results);
                    var currencies = data.First().rates;
                    return View(currencies);
                }
                else
                {
                    Console.WriteLine("Valid connection with Api.");
                }
                ViewData.Model = rates;
                
            }
            return View();
        }

        private const string nbpApiUrl = RatesExchangeServices.exchangeratesUrl;

        //Akcja wyświetlająca widok
        
        public IActionResult ConvertCurrencies()
        {

            var currencyCodes = GetCurrencyCodes();
            var currencyList = new SelectList(currencyCodes);
            ViewBag.currencyList = currencyList;

            return View();
        }

        //Akcja przeliczająca waluty

        [HttpPost]
        public async Task<ActionResult> Convertion(string fromCurrency, string toCurrency, decimal amount)
        {
            //Pobieramy kursy walut
            var exchangeRate1 = await GetExchangeRateFrom(fromCurrency);
            var exchangeRate2 = await GetExchangeRateTo(toCurrency);
            var result = amount * exchangeRate1[fromCurrency] / exchangeRate2[toCurrency];

            var roundedResult = Math.Round(result, 4);

            ViewBag.Result = roundedResult;

            var amountNumber = amount;
            ViewBag.amountNumber = amountNumber;

            var fromCurrencyCode = fromCurrency;
            ViewBag.fromCurrencyCode = fromCurrencyCode;

            var toCurrencyCode = toCurrency;
            ViewBag.toCurrencyCode = toCurrencyCode;

            var currencyCodes = GetCurrencyCodes();
            var currencyList = new SelectList(currencyCodes);

            ViewBag.currencyList = currencyList;

            return View();
        }
        //Metoda pobierająca listę kodów walut
        private List<string> GetCurrencyCodes()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(nbpApiUrl);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("tables/a").Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    dynamic data = JsonConvert.DeserializeObject(json);
                    var currencies = new List<string>();

                    foreach (var rate in data[0].rates)
                    {
                        currencies.Add(rate.code.ToString());
                    }

                    return currencies;
                }
                else
                {
                    throw new Exception("Valid connection with Api.");
                }
            }
        }
        // Metoda[1] pobierająca kursy walut 
        private async Task<Dictionary<string, decimal>> GetExchangeRateFrom(string fromCurrency)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(nbpApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"rates/a/{fromCurrency}/");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject<RootDTO>(json);
                    var rates = new Dictionary<string, decimal>();

                    foreach (var rate in data.rates)
                    {
                        rates[data.code] = Convert.ToDecimal(rate.mid);  
                    }

                    return rates;
                }
                else
                {
                    throw new Exception("Valid connection with Api.");
                }
            }
        }
        // Metoda[2] pobierająca kursy walut 
        private async Task<Dictionary<string, decimal>> GetExchangeRateTo(string toCurrency)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(nbpApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"rates/a/{toCurrency}/");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject<RootDTO>(json);
                    var rates = new Dictionary<string, decimal>();

                    foreach (var rate in data.rates)
                    {
                        rates[data.code] = Convert.ToDecimal(rate.mid);
                    }
                    return rates;
                }
                else
                {
                    throw new Exception("Valid connection with Api.");
                }
            }
        }

        public ActionResult Policy()
        {
            return View();
        }


        // GET: CurrencyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CurrencyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CurrencyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CurrencyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}


        
    