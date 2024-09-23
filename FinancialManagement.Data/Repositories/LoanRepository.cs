using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Repositories;
using FinancialManagement.Core.Services;
using FinancialManagement.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;

namespace FinancialManagement.Data.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly DataContext _context;
        private readonly IGlobalVariablesService _GlobalVariablesService;
        public LoanRepository(DataContext DC, IGlobalVariablesService globalVariablesService)
        {
            _context = DC;
            _GlobalVariablesService= globalVariablesService;
        }
        public async Task<IEnumerable<Loan>> GetAsync()
        {

            return await _context.Loans.Where(l => l.Status == true).Include(l => l.Guarantees).ThenInclude(g => g.Guarantor).Include(l => l.Borrower).ToListAsync();
        }
        public async Task<Loan> GetAsync(int id)
        {

            return await _context.Loans.Include(e => e.Guarantees).ThenInclude(p => p.Guarantor).FirstOrDefaultAsync(e => e.Id == id);
            // return await _context.Loans.FindAsync(id);
        }

        public async Task<Loan> PostAsync(Loan value)
        {
            value.LoanDate = DateTime.Now;
            _context.Loans.Add(value);
            var globalVariables = await _context.GlobalVariables.FirstOrDefaultAsync();
            if (globalVariables != null)
            {
                _GlobalVariablesService.PutAsync(-value.Amount, value.Currency);
                globalVariables.ActiveLoans += value.Amount;
                globalVariables.TotalLoansGranted += value.Amount;
            }
            await _context.SaveChangesAsync();
            return await _context.Loans.Include(l => l.DepositGuarantee).ThenInclude(g => g.Deposit).Include(e => e.Guarantees).ThenInclude(p => p.Guarantor).Include(l => l.Borrower).FirstOrDefaultAsync(e => e.Id == value.Id);
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
                    loan.Guarantees.Add(new UserGuarantee
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

        public async Task<Loan> DeleteAsync(int id, double? repaymentAmount)
        {
            // מציאת ההלוואה מהקשר
            Loan loan = await _context.Loans
                                      .Include(e => e.Guarantees)
                                      .ThenInclude(p => p.Guarantor)
                                      .Include(l => l.Borrower)
                                      .FirstOrDefaultAsync(e => e.Id == id);

            if (loan != null)
            {
                if (repaymentAmount.HasValue)
                {
                    double totalRepaymentAmount = repaymentAmount.Value;
                    loan.RemainingAmount -= totalRepaymentAmount;


                    loan.CurrentPayment = loan.TotalPayments - (int)(Math.Ceiling(loan.RemainingAmount / loan.MonthlyRepayment));
                    if (loan.RemainingAmount < loan.MonthlyRepayment)
                        loan.MonthlyRepayment = loan.RemainingAmount;

                    var globalVariables = await _context.GlobalVariables.FirstOrDefaultAsync();
                    if (globalVariables != null)
                    {
                        _GlobalVariablesService.PutAsync(totalRepaymentAmount, loan.Currency);
                        globalVariables.ActiveLoans -= totalRepaymentAmount;
                    }

                    // שלח מייל אם ההלוואה עדיין פעילה
                    if (loan.RemainingAmount > 0)
                    {
                        string body = $@"
    <html>
      <body>
        <div style='width: 500px; padding: 40px; background-color: #f0f8ff; border-radius: 15px; font-family: Arial, sans-serif;'>
            <h2 style='color: #333;'>שלום {loan.Borrower.FirstName},</h2>
            <p style='color: #333; font-size: 18px;'>ברצוננו להודיעך כי קיבלנו את התשלום שלך בסך {repaymentAmount} ש""ח עבור ההלוואה שלך.</p>
            <p style='color: #333; font-size: 16px;'>נותר לך להחזיר סך של {loan.RemainingAmount} מתוך {loan.Amount} להשלמת ההלוואה.</p>
            <p style='color: #333; font-size: 16px;'>התשלום הבא שלך נקבע ל-{loan.RepaymentDate:dd/MM/yyyy}, וסכום התשלום הבא הוא {loan.MonthlyRepayment} ש""ח.</p>
            <br/>
            <p style='color: #333; font-size: 14px;'>בברכה,</p>
            <p style='color: #333; font-size: 14px;'><strong>גמ""ח רץ כצבי </ strong ></ p >
        
                </ div >
        
              </ body >
        
            </ html > ";

                        await EmailService.SendEmailAsync(
                            loan.Borrower.Email, // כתובת המייל של הלווה
                            "עדכון תשלום הלוואה", // נושא המייל
                            body // גוף המייל המעוצב
                        );
                    }

                    // שלח מייל אם ההלוואה הושלמה
                    if (loan.CurrentPayment == loan.TotalPayments || loan.RemainingAmount == 0)
                    {
                        loan.Status = false; // עדכון מצב ההלוואה כסגורה
                        loan.RemainingAmount = 0;

                        string completedBody = $@"
    <html>
      <body>
        <div style='width: 500px; padding: 40px; background-color: #f0f8ff; border-radius: 15px; font-family: Arial, sans-serif;'>
            <h2 style='color: #333;'>שלום {loan.Borrower.FirstName},</h2>
            <p style='color: #333; font-size: 18px;'>אנו שמחים להודיעך כי ההלוואה שלך בסך {loan.Amount} {loan.Currency} הושלמה בהצלחה!</p>
            <p style='color: #333; font-size: 16px;'>לא נותר לך עוד להחזיר סכום כלשהו.</p>
            <p style='color: #333; font-size: 16px;'>אנו מודים לך על שיתוף הפעולה ונשמח לעמוד לשירותך בעתיד.</p>
            <br/>
            <p style='color: #333; font-size: 14px;'>בברכה,</p>
            <p style='color: #333; font-size: 14px;'><strong>גמ""ח רץ כצבי </ strong ></ p >
        
                </ div >
        
              </ body >
        
            </ html > ";

                        await EmailService.SendEmailAsync(
                            loan.Borrower.Email, // כתובת המייל של הלווה
                            "השלמת הלוואה בהצלחה", // נושא המייל
                            completedBody // גוף המייל המעוצב לסיום ההלוואה
                        );
                        // שלח מייל לערבים
                        foreach (var guarantee in loan.Guarantees) { string guarantorBody = $@" <html> <body> <div style='width: 500px; padding: 40px; background-color: #f0f8ff; border-radius: 15px; font-family: Arial, sans-serif;'> <h2 style='color: #333;'>שלום {guarantee.Guarantor.FirstName},</h2> <p style='color: #333; font-size: 18px;'>ברצוננו להודיעך כי ההלוואה בסך {loan.Amount}{loan.Currency} ש-{loan.Borrower.FirstName} {loan.Borrower.LastName} לקח הסתיימה בהצלחה!</p> <br/> <p style='color: #333; font-size: 14px;'>בברכה,</p> <p style='color: #333; font-size: 14px;'><strong>גמ""ח רץ כצבי</strong></p> </div> </body> </html>"; 
                            await EmailService.SendEmailAsync( guarantee.Guarantor.Email, "השלמת הלוואה של הלווה", guarantorBody ); }   

                    }

                    await _context.SaveChangesAsync();
                }
            }

            return loan;
        }



    }
}
