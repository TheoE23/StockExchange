using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounts.Domain;
using Accounts.Domain.Interfaces;

namespace Accounts.Domain.Services
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private Dictionary<string, decimal> exchangeRates;

        public CurrencyConverter()
        {
           
            exchangeRates = new Dictionary<string, decimal>
            {
            { "USD", 1m }, 
            { "EUR", 0.85m }, 
            { "BGN", 1.65m }, 
            { "JPY", 110m }, 
       
            };
        }

        public decimal Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            if (!exchangeRates.ContainsKey(fromCurrency) || !exchangeRates.ContainsKey(toCurrency))
            {
                throw new Exception("Invalid currency.");
            }

         
            decimal amountInUsd = amount / exchangeRates[fromCurrency];
            return amountInUsd * exchangeRates[toCurrency];
        }

        public void UpdateExchangeRate(string currency, decimal newRate)
        {
            if (!exchangeRates.ContainsKey(currency))
            {
                throw new Exception("Invalid currency.");
            }

            exchangeRates[currency] = newRate;
        }
    }

}
