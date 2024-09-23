using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.Repositories
{
    public interface IGlobalVariablesRepository
    {
        public  Task<IEnumerable<GlobalVariables>> GetAsync();
        public Task<IEnumerable<GlobalVariables>> PutAsync(double amount, CurrencyType currencyType);

    }
}
