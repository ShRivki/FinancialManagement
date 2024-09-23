using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Repositories;
using FinancialManagement.Core.Services;


namespace FinancialManagement.Service.Services
{
    public class GlobalVariablesService:IGlobalVariablesService
    {
        private readonly IGlobalVariablesRepository _globalVariablesRepository;
        private readonly CurrencyService _CurrencyService;

        public GlobalVariablesService(IGlobalVariablesRepository globalVariablesRepository)
        {
            _globalVariablesRepository = globalVariablesRepository;
        }
        public async Task<IEnumerable<GlobalVariables>> GetAllAsync()
        {
            return await _globalVariablesRepository.GetAsync();
        }
        public async Task<IEnumerable<GlobalVariables>> PutAsync(double value, CurrencyType currencyType)
        {
            return await _globalVariablesRepository.PutAsync(value, currencyType);
        }

    }
}
