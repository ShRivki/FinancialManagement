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
    public class DepositService: IDepositService
    {
        private readonly IDepositRepository _DepositRepository;
        public DepositService(IDepositRepository DepositRepository)
        {
            _DepositRepository = DepositRepository;
        }

        public async Task<IEnumerable<Deposit>> GetAllAsync()
        {
            return await _DepositRepository.GetAsync();
        }

        public async Task<Deposit> GetDepositByIdAsync(int id)
        {
            return await _DepositRepository.GetAsync(id);
        }

        public async Task<Deposit> PostDepositAsync(Deposit value)
        {
            return await _DepositRepository.PostAsync(value);
        }

        public async Task<Deposit> PutDepositAsync(int id, Deposit value)
        {
            return await _DepositRepository.PutAsync(id, value);
        }
        public async Task<Deposit> DeleteDepositAsync(int id)
        {
            return await _DepositRepository.DeleteAsync(id);
        }

    }
}
