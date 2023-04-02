using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace CurrencyProject.Models
{
    
        public class Rates
        {
            public string currency { get; set; }
            public string code { get; set; }
            public double mid { get; set; }
        }

        public class Root
        {
            public string table { get; set; }
            public string no { get; set; }
            public string effectiveDate { get; set; }
            public List<Rates> rates { get; set; }
        
    }
    public class SelRates
    {
        SelectList selectListCodes { get; set;}
    }


    public class CurrencyApiResponse
    {
        public List<Currency> Currencies { get; set; }
    }

    public class ExchangeRateApiResponse
    {
        public string Currency { get; set; }
        public List<ExchangeRate> Rates { get; set; }
    }

    public class Currency
    {
        public string code { get; set; }
        public string currency { get; set; }
    }

    //public class ExchangeRate
    //{
    //    public decimal mid { get; set; }
    //}
    //public class CurrencyViewModel
    //{
    //    public List<string> Currencies { get; set; }
    //    public string SelectedCurrency { get; set; }
    //}

    
    public class ExchangeRate
    {
        public string No { get; set; }
        public string EffectiveDate { get; set; }
        public List<Rate> Rates { get; set; }
    }

    public class Rate
    {
        public string Code { get; set; }
        public string Currency { get; set; }
        public decimal Mid { get; set; }
    }

    public class CurrencyViewModel
    {
        public List<SelectListItem> Currencies { get; set; }
        public string SelectedCurrency { get; set; }
    }
}

