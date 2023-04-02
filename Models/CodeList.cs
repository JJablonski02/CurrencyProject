using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace CurrencyProject.Models
{
    public class CodeList
    {
        [DisplayName("Z")]
        public string CurrCodeFrom { get; set; }
        [DisplayName("Wprowadź wartość: ")]
        public string CurrTextFrom { get; set; }
        [DisplayName("DO")]
        public string CurrCodeTo { get; set; }
        [DisplayName("Po wymianie: ")]
        public string CurrTextTo { get; set; }

        public List<SelectListItem> Rates { get; set; }

    }
    
}
