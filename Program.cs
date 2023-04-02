using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RatesExchangeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CurrencyProject
{
    public class Program
    {
  
private const string ApiKey = "PMAK-63f00896ec69d322d6f5fed9-XXXX";
private static readonly List<string> IsoCurrencies = new List<string> { "USD", "CHF", "GBP", "PLN", "AUD", "JPY" };

public static void Main(string[] args)
{
    CreateHostBuilder(args).Build().Run();
    try
    {
        var client = new RatesExchangeApiService(ApiKey);
        CheckIfApiIsOnline(client).Wait();
    }
    catch (Exception exception)
    {
        Console.WriteLine($"{exception.Message}");
    }
    Console.ReadKey();
}
private static async Task CheckIfApiIsOnline(RatesExchangeApiService client)
{
    Console.WriteLine("-- Check if API is online");
    var parsed = JsonConvert.SerializeObject(await client.CheckIfApiIsOnline(), Formatting.Indented);
    Console.WriteLine(parsed);
}

private static async Task GetLatestRates(RatesExchangeApiService client, string baseCurrency, List<string> currencies)
{
    Console.WriteLine("--Get latest rates from ECB...");
    var parsed = JsonConvert.SerializeObject(await client.GetLatestRates(baseCurrency, currencies), Formatting.Indented);
    Console.WriteLine(parsed);
}
private static async Task ConvertCurrency(RatesExchangeApiService client, string fromCurrency, string amount, string date, List<string> currencies)
{
    Console.WriteLine($"--Convert currency {amount} {fromCurrency} to {currencies}.");
    var parsed = JsonConvert.SerializeObject(await client.ConvertCurrency(fromCurrency, amount, date, currencies), Formatting.Indented);
    Console.WriteLine(parsed);
}

public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
    }
    
}
