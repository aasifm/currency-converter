using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CurrencyConverter.Models;
using CurrencyConverter.Data;

namespace CurrencyConverter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            CurrencyDropDown model = new CurrencyDropDown();
            model.CurrencyDropDownProperty = getCurrenciesForDropDown();
            return View(model);
        }

        public IActionResult History()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            List<CurrencyConversionAudit> audit = context.CurrencyConversionAudit.OrderByDescending(a => a.AuditDate).ToList();
            return View(audit);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<Currency> getCurrenciesForDropDown() {
            ApplicationDbContext context = new ApplicationDbContext();
            List<Currency> currencies = context.Currency.ToList();
            return currencies;
        }

        [HttpPost]
        public JsonResult DoConvert(string inputVal, string selectedCurrency, double selectedCurrencyValue)
        {
            double userInput, result;

            if (inputVal == null) {
                return Json("No input has been entered");
            }

            try
            {
                userInput = Convert.ToDouble(inputVal);
            }
            catch(FormatException) {
                return Json("Input is not numeric");
            }

            result = userInput * selectedCurrencyValue;

            ApplicationDbContext context = new ApplicationDbContext();

            CurrencyConversionAudit currencyConversionAudit = new CurrencyConversionAudit() {CurrencyName = selectedCurrency,
                                                                                             CurrencyValue = selectedCurrencyValue,
                                                                                             UserValue = userInput,
                                                                                             ConvertedValue = result,
                                                                                             AuditDate = DateTime.Now};

            context.CurrencyConversionAudit.Add(currencyConversionAudit);
            context.SaveChanges();
            return Json(result);
        }
    }
}
