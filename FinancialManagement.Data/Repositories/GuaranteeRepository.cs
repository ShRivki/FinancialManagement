using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Data.Repositories
{
    public class GuaranteeRepository : IGuaranteeRepository
    {
        private readonly DataContext _context;

        public GuaranteeRepository(DataContext DC)
        {
            _context = DC; 
        }

        public async Task<IEnumerable<Guarantee>> GetAsync()
        {
            return await _context.Guarantees.ToListAsync();

        }

        public async Task<Guarantee> GetAsync(int id)
        {
            return await _context.Guarantees.FindAsync(id);
        }

        public async Task<Guarantee> PostAsync(Guarantee value)
        {
            _context.Guarantees.Add(value);
            await _context.SaveChangesAsync();
            return await _context.Guarantees.FindAsync(value.Id);
        }

        public async Task<Guarantee> PutAsync(int id, Guarantee value)
        {

            Guarantee guarantee = await _context.Guarantees.FindAsync(id);
            if (guarantee != null)
            {
                guarantee.Loan = value.Loan;
                guarantee.Guarantor = value.Guarantor;
                guarantee.GuarantorId = value.GuarantorId;
                guarantee.LoanId = value.LoanId;
                await _context.SaveChangesAsync();
            }
            return guarantee;
        }
        public async Task<Guarantee> DeleteAsync(int id)
        {
            Guarantee guarantee = await _context.Guarantees.FindAsync(id);
            if (guarantee != null)
            {
                _context.Guarantees.Remove(guarantee);
                await _context.SaveChangesAsync();
            }
            return guarantee;
        }
    }
}
