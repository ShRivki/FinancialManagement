using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.Repositories
{
    public interface ILoanRepository
    {
        public Task<IEnumerable<Loan>> GetAsync();

       // public Task<IEnumerable<Loan>> GetInactiveLoansAsync();
        public Task<Loan> GetAsync(int id);
        public Task<Loan> PostAsync(Loan value);

        public Task<Loan> PutAsync(int id, Loan value);

        public Task<Loan> DeleteAsync(int id , double? repaymentAmount);
    }
}
