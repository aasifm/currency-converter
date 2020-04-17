using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyConverter.Data;
using CurrencyConverter.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CurrencyConverter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            createDatabaseAndInsertSeedData();
        }

        private void createDatabaseAndInsertSeedData()
        {
            using (var context = new ApplicationDbContext())
            {
                context.Database.EnsureCreated();

                var USDCurrency = context.Currency.FirstOrDefault(c => c.CurrencyName == "USD");
                if (USDCurrency == null)
                {
                    context.Currency.Add(new Currency { CurrencyName = "USD", CurrencyValue = 1.30 });
                }

                var AUDCurrency = context.Currency.FirstOrDefault(c => c.CurrencyName == "AUD");
                if (AUDCurrency == null)
                {
                    context.Currency.Add(new Currency { CurrencyName = "AUD", CurrencyValue = 1.88 });
                }

                var EURCurrency = context.Currency.FirstOrDefault(c => c.CurrencyName == "EUR");
                if (EURCurrency == null)
                {
                    context.Currency.Add(new Currency { CurrencyName = "EUR", CurrencyValue = 1.17 });
                }

                context.SaveChanges();
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
