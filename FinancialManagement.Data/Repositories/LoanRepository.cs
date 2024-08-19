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

            return await _context.Loans.Include(l => l.Guarantees).ThenInclude(g => g.Guarantor).Include(l => l.Borrower).ToListAsync();
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
            // Load the loan including its guarantees and their guarantors
            Loan loan = await _context.Loans
                                      .Include(e => e.Guarantees)
                                      .ThenInclude(p => p.Guarantor)
                                      .FirstOrDefaultAsync(e => e.Id == id);

            if (loan != null)
            {
                // Update loan properties
                loan.Frequency = value.Frequency;
                loan.Status = value.Status;
                loan.LoanDate = value.LoanDate;
                loan.Borrower = value.Borrower;
                loan.BorrowerId = value.BorrowerId;
               // loan.CurrentPayment = value.CurrentPayment;
                loan.RepaymentDate = value.RepaymentDate;
                loan.TotalPayments = value.TotalPayments;
                loan.Amount = value.Amount;

                // Update guarantees
                // Remove guarantees that are not in the new value
                var guaranteesToRemove = loan.Guarantees
                                              .Where(g => !value.Guarantees.Any(vg => vg.Id == g.Id))
                                              .ToList();
                foreach (var guarantee in guaranteesToRemove)
                {
                    loan.Guarantees.Remove(guarantee);
                    _context.Guarantees.Remove(guarantee);
                }

                // Add or update guarantees
                foreach (var newGuarantee in value.Guarantees)
                {
                    var existingGuarantee = loan.Guarantees.FirstOrDefault(g => g.Id == newGuarantee.Id);
                    
                        // Add new guarantee
                        loan.Guarantees.Add(new Guarantee
                        {
                            GuarantorId = newGuarantee.GuarantorId,
                            Guarantor = newGuarantee.Guarantor,
                            LoanId = loan.Id
                        });
                    
                }

                await _context.SaveChangesAsync();
            }

            return await _context.Loans
                                 .Include(e => e.Guarantees)
                                 .ThenInclude(p => p.Guarantor).Include(l => l.Borrower)
                                 .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Loan> DeleteAsync(int id)
        {
            Loan loan = await _context.Loans
                                      .Include(e => e.Guarantees)
                                      .ThenInclude(p => p.Guarantor).Include(l => l.Borrower)
                                      .FirstOrDefaultAsync(e => e.Id == id);
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
