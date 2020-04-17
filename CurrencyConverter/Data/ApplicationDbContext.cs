using CurrencyConverter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Currency> Currency { get; set; }
        public DbSet<CurrencyConversionAudit> CurrencyConversionAudit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CurrencyConverter;Trusted_Connection=True");
        }
    }
}
