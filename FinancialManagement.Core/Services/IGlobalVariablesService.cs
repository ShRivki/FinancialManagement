using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.Services
{
    public interface IGlobalVariablesService
    {
        public Task<IEnumerable<GlobalVariables>> GetAllAsync();
        public Task<IEnumerable<GlobalVariables>> PutAsync(double value, CurrencyType currencyType);
    }
}
