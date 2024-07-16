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
    public class LoanService : ILoanService
    {

        private readonly ILoanRepository _LoanRepository;
        public LoanService(ILoanRepository LoanRepository)
        {
            _LoanRepository = LoanRepository;
        }


        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _LoanRepository.GetAsync();
        }

        public async Task<Loan> GetLoanByIdAsync(int id)
        {
            return await _LoanRepository.GetAsync(id);
        }

        public async Task<Loan> PostLoanAsync(Loan value)
        {
            return await _LoanRepository.PostAsync(value);
        }

        public async Task<Loan> PutLoanAsync(int id, Loan value)
        {
            return await _LoanRepository.PutAsync(id, value);
        }
        public async Task<Loan> DeleteLoanAsync(int id)
        {
            return await _LoanRepository.DeleteAsync(id);   
        }
    }
}
