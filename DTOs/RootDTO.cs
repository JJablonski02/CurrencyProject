using Newtonsoft.Json;
using System.Collections.Generic;

namespace CurrencyProject.DTOs
{
    public class RootDTO
    {
        //[JsonProperty("table")]
        public string table { get; set; }
        public string currency { get; set; }
        public string code { get; set; }
        public List<RateDTO> rates { get; set; }
    }
}
