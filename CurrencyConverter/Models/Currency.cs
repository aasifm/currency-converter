using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
{
    public class Currency
    {
        [Key]
        [MaxLength(10)]
        public string CurrencyName { get; set; }
        
        [Required]
        public double CurrencyValue { get; set; }

        public ICollection<CurrencyConversionAudit> CurrencyConversionAudit { get; set; }
    }

    public class CurrencyDropDown {
        public List<Currency> CurrencyDropDownProperty { get; set; }
    }
}
