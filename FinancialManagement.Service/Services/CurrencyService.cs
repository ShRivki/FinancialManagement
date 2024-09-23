using System.Net.Http;
using System.Threading.Tasks;
using FinancialManagement.Core.Services;
using Newtonsoft.Json.Linq; // צריך להוסיף את חבילת Newtonsoft.Json

namespace FinancialManagement.Service.Services
{
    public class CurrencyService: ICurrencyService
    {
        public async Task<(decimal UsdRate, decimal EurRate, decimal GbpRate)> GetRatesAsync()
        {
            string url = "https://boi.org.il/PublicApi/GetExchangeRates"; // החלף ב-URL הנכון שלך

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonData = await response.Content.ReadAsStringAsync();

                // ניתוח JSON
                var json = JObject.Parse(jsonData);
                var exchangeRates = json["exchangeRates"];

                decimal usdRate = (decimal)exchangeRates.FirstOrDefault(x => (string)x["key"] == "USD")["currentExchangeRate"];
                decimal eurRate = (decimal)exchangeRates.FirstOrDefault(x => (string)x["key"] == "EUR")["currentExchangeRate"];
                decimal gbpRate = (decimal)exchangeRates.FirstOrDefault(x => (string)x["key"] == "GBP")["currentExchangeRate"];

                return (usdRate, eurRate, gbpRate);
            }
        }
    }
}
