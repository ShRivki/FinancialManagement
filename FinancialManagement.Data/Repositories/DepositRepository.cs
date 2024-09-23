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
using Newtonsoft.Json.Linq;

namespace FinancialManagement.Data.Repositories
{
    public class DepositRepository : IDepositRepository
    {
        private readonly DataContext _context;
        private readonly IGlobalVariablesService _GlobalVariablesService;
        public DepositRepository(DataContext DC, IGlobalVariablesService globalVariablesService)
        {
            _context = DC;
            _GlobalVariablesService = globalVariablesService;
        }

        public async Task<IEnumerable<Deposit>> GetAsync()
        {
            //return await _context.Deposits.ToListAsync();
            return await _context.Deposits.Where(d => d.Status == true).Include(l => l.DepositGuarantees).ThenInclude(g => g.Loan).Include(d => d.Depositor).ToListAsync();
        }

        public async Task<Deposit> GetAsync(int id)
        {
            return await _context.Deposits.Include(d => d.Depositor).FirstOrDefaultAsync(e => e.Id == id);
            // return await _context.Deposits.FindAsync(id);
        }

        public async Task<Deposit> PostAsync(Deposit value)
        {
            _context.Deposits.Add(value);
            var globalVariables = await _context.GlobalVariables.FirstOrDefaultAsync();
            if (globalVariables != null)
            {
                _GlobalVariablesService.PutAsync(value.Amount, value.Currency);
                //globalVariables.TotalFundBalance += value.Amount;
            }
            await _context.SaveChangesAsync();
            return await _context.Deposits.Include(d => d.Depositor).FirstOrDefaultAsync(d => d.Id == value.Id);
        }

        public async Task<Deposit> PutAsync(int id, Deposit value)
        {
            Deposit deposit = await _context.Deposits.FindAsync(id);
            if (deposit != null)
            {
                //deposit.DepositDate = value.DepositDate;
                deposit.DateOfMaturity = value.DateOfMaturity;
                //deposit.Status= value.Status;
                deposit.Depositor = value.Depositor;
                deposit.Notes = value.Notes;
                deposit.Amount = value.Amount;

                await _context.SaveChangesAsync();
            }
            return deposit;
        }
        public async Task<Deposit> DeleteAsync(int id, double? repaymentAmount = null)
        {
            Deposit deposit = await _context.Deposits.Include(d => d.Depositor).FirstOrDefaultAsync(d => d.Id == id);
            if (deposit == null)
            {
                throw new ArgumentException($"פיקדון עם מזהה {id} לא נמצא.");
            }

            var globalVariables = await _context.GlobalVariables.FirstOrDefaultAsync();
            if (globalVariables == null)
            {
                throw new InvalidOperationException("לא ניתן למצוא את המשתנים הגלובליים.");
            }

            if (repaymentAmount.HasValue && repaymentAmount.Value > deposit.Amount - deposit.AmountRefunded)
            {
                throw new InvalidOperationException("ערך לא חוקי.");
            }

            if (repaymentAmount.HasValue)
            {
                _GlobalVariablesService.PutAsync(-repaymentAmount.Value, deposit.Currency);
                deposit.AmountRefunded += repaymentAmount.Value;

                // שלח מייל על החזר חלקי בצורה אסינכרונית
                _ = SendEmailAsync(deposit, repaymentAmount.Value, "partial");
            }
            else
            {
                _GlobalVariablesService.PutAsync(-deposit.Amount, deposit.Currency);
                deposit.AmountRefunded = deposit.Amount;
            }

            if (deposit.Amount == deposit.AmountRefunded)
            {
                deposit.Status = false;
                _ = SendEmailAsync(deposit, deposit.Amount, "completed");
            }

            await _context.SaveChangesAsync();
            return deposit;
        }

        private async Task SendEmailAsync(Deposit deposit, double amount, string type)
        {
            string emailBody;
            if (type == "partial")
            {
                emailBody = $@"
        <html>
          <body>
            <div style='width: 500px; padding: 40px; background-color: #f0f8ff; border-radius: 15px; font-family: Arial, sans-serif;'>
                <h2 style='color: #333;'>שלום {deposit.Depositor.FirstName},</h2>
                <p style='color: #333; font-size: 18px;'>אנו שמחים להודיעך כי קיבלנו חלק מההחזר על הפיקדון שלך.</p>
                <p style='color: #333; font-size: 16px;'>פרטי ההחזר:</p>
                <ul style='color: #333; font-size: 16px;'>
                    <li>תאריך ההפקדה: {deposit.DepositDate.ToString("dd/MM/yyyy")}</li>
                    <li>סכום שהוחזר: {amount} {deposit.Currency}</li>
                    <li>סכום נותר להחזר: {deposit.Amount - deposit.AmountRefunded} ש""ח</li>
                </ul>
                <p style='color: #333; font-size: 16px;'>תודה על שיתוף הפעולה.</p>
                <br/>
                <p style='color: #333; font-size: 14px;'>בברכה,</p>
                <p style='color: #333; font-size: 14px;'><strong>גמ""ח רץ כצבי</strong></p>
            </div>
          </body>
        </html>";
                await EmailService.SendEmailAsync(deposit.Depositor.Email, "החזר פיקדון חלקי", emailBody);
            }
            else if (type == "completed")
            {
                emailBody = $@"
        <html>
          <body>
            <div style='width: 500px; padding: 40px; background-color: #f0f8ff; border-radius: 15px; font-family: Arial, sans-serif;'>
                <h2 style='color: #333;'>שלום {deposit.Depositor.FirstName},</h2>
                <p style='color: #333; font-size: 18px;'>אנו שמחים להודיעך כי הפיקדון שלך הוחזר במלואו.</p>
                <p style='color: #333; font-size: 16px;'>פרטי ההפקדה:</p>
                <ul style='color: #333; font-size: 16px;'>
                    <li>תאריך ההפקדה: {deposit.DepositDate.ToString("dd/MM/yyyy")}</li>
                    <li>סכום ההחזר הכולל: {deposit.Amount}  {deposit.Currency}</li>
                </ul>
                <p style='color: #333; font-size: 16px;'>תודה על שיתוף הפעולה.</p>
                <br/>
                <p style='color: #333; font-size: 14px;'>בברכה,</p>
                <p style='color: #333; font-size: 14px;'><strong>גמ""ח רץ כצבי</strong></p>
            </div>
          </body>
        </html>";
                await EmailService.SendEmailAsync(deposit.Depositor.Email, "החזר פיקדון הושלם", emailBody);
            }
        }

    }

}
