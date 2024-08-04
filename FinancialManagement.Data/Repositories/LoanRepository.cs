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
    public class LoanRepository : ILoanRepository
    {
        private readonly DataContext _context;

        public LoanRepository(DataContext DC)
        {
            _context = DC;

        }
        public async Task<IEnumerable<Loan>> GetAsync()
        {
            //return await _context.Loans.ToListAsync();

            return await _context.Loans
                    .Include(l => l.Guarantees)          // כולל את כל הערביות השייכות להלוואה
                        .ThenInclude(g => g.Guarantor)  // כלל את המידע על הערב
                    .Include(l => l.Borrower)           // כולל את המשתמש המשויך להלוואה
                    .ToListAsync();
        }

        public async Task<Loan> GetAsync(int id)
        {

            return await _context.Loans.Include(e => e.Guarantees).ThenInclude(p => p.Guarantor).FirstOrDefaultAsync(e => e.Id == id);
           // return await _context.Loans.FindAsync(id);
        }

        public async Task<Loan> PostAsync(Loan value)
        {
            _context.Loans.Add(value);
            await _context.SaveChangesAsync();
            return await _context.Loans.FindAsync(value.Id);
        }

        public async Task<Loan> PutAsync(int id, Loan value)
        {

            Loan loan = await _context.Loans.FindAsync(id);
            if (loan != null)
            {
                loan.Frequency = value.Frequency;
                loan.Status= value.Status;
                loan.LoanDate = value.LoanDate;
                loan.Borrower= value.Borrower;
                loan.BorrowerId= value.BorrowerId;
                loan.CurrentPayment= value.CurrentPayment;
                loan.Guarantees= value.Guarantees;
                loan.RepaymentDate= value.RepaymentDate;
                loan.TotalPayments= value.TotalPayments;
                await _context.SaveChangesAsync();
            }
            return  await _context.Loans.Include(e => e.Guarantees).ThenInclude(p => p.Guarantor).FirstOrDefaultAsync(e => e.Id == id);
          
 

    }
    public async Task<Loan> DeleteAsync(int id)
        {
            Loan loan = await _context.Loans.FindAsync(id);
            if (loan != null)
            {
                loan.CurrentPayment++;
                if (loan.CurrentPayment==loan.TotalPayments)
                     loan.Status =false;
                
                await _context.SaveChangesAsync();
            }
           
            return loan;
               
            
        }
    }
}
