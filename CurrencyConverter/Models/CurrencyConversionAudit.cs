using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
{
    public class CurrencyConversionAudit
    {
        [Key]
        public int AuditId { get; set; }

        [Required]
        [Display(Name="Exchange Rate")]
        public double CurrencyValue { get; set; }

        [Required]
        [Display(Name = "Amount to convert (£)")]
        public double UserValue { get; set; }

        [Display(Name = "Amount after conversion")]
        [Required]
        public double ConvertedValue { get; set; }
        
        [Required]
        [Display(Name = "Date")]
        public DateTime AuditDate { get; set; }

        [Display(Name = "Currency")]
        [Required]
        public string CurrencyName { get; set; }

        [ForeignKey("CurrencyName")]
        public Currency Currency { get; set; }
    }
}
