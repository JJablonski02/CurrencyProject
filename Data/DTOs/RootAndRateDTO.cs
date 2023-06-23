using System.Collections.Generic;

namespace CurrencyProject.Data.DTOs
{
    public class RootAndRateDTO
    {
        public string no { get; set; }
        public string effectiveDate { get; set; }
        public decimal mid { get; set; } // Zamiast double zastosowany typ decimal
        public string table { get; set; }
        public string code {  get; set; }
        public string currency { get; set; }
        public List<RateDTO> rates { get; set; }
    }
}
