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
    public class GuaranteeService:IGuaranteeService
    {
        private readonly IGuaranteeRepository _GuaranteeRepository;
        public GuaranteeService(IGuaranteeRepository GuaranteeRepository)
        {
            _GuaranteeRepository = GuaranteeRepository;
        }

        public async Task<IEnumerable<Guarantee>> GetAllAsync()
        {
            return await _GuaranteeRepository.GetAsync();
        }

        public async Task<Guarantee> GetGuaranteeByIdAsync(int id)
        {
            return await _GuaranteeRepository.GetAsync(id);
        }

        public async Task<Guarantee> PostGuaranteeAsync(Guarantee value)
        {
            return await _GuaranteeRepository.PostAsync(value);
        }

        public async Task<Guarantee> PutGuaranteeAsync(int id, Guarantee value)
        {
            return await _GuaranteeRepository.PutAsync(id, value);
        }
        public async Task<Guarantee> DeleteGuaranteeAsync(int id)
        {
            return await _GuaranteeRepository.DeleteAsync(id);
        }

    }
}
