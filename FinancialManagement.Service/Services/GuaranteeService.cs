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

        public async Task<IEnumerable<UserGuarantee>> GetAllAsync()
        {
            return await _GuaranteeRepository.GetAsync();
        }

        public async Task<UserGuarantee> GetGuaranteeByIdAsync(int id)
        {
            return await _GuaranteeRepository.GetAsync(id);
        }

        public async Task<UserGuarantee> PostGuaranteeAsync(UserGuarantee value)
        {
            return await _GuaranteeRepository.PostAsync(value);
        }

        public async Task<UserGuarantee> PutGuaranteeAsync(int id, UserGuarantee value)
        {
            return await _GuaranteeRepository.PutAsync(id, value);
        }
        public async Task<UserGuarantee> DeleteGuaranteeAsync(int id)
        {
            return await _GuaranteeRepository.DeleteAsync(id);
        }

    }
}
