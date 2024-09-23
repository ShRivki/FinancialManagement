using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.Services
{
    public interface ILoanService
    {
        public Task<IEnumerable<Loan>> GetAllAsync();
        //public Task<IEnumerable<Loan>> GetInactiveLoansAsync();


        public Task<Loan> GetLoanByIdAsync(int id);


        public Task<Loan> PostLoanAsync(Loan value);


        public Task<Loan> PutLoanAsync(int id, Loan value);


        public Task<Loan> DeleteLoanAsync(int id, double? repaymentAmount);
    }
}
