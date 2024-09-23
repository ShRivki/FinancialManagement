using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagement.Core.Services
{
    public interface ICurrencyService
    {
         Task<(decimal UsdRate, decimal EurRate, decimal GbpRate)> GetRatesAsync();
    }
}
