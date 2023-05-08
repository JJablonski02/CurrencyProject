using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CurrencyProject.Data.DTOs
{
    public class RootDTO
    {
        //[JsonProperty("table")]
        public string table { get; set; }
        public string currency { get; set; }
        public string code { get; set; }
        public List<RateDTO> rates { get; set; }
        public DateTime Date { get; set; }
    }
}
