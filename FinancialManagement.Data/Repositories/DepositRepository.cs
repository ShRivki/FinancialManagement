using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Repositories;
using FinancialManagement.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Data.Repositories
{
    public class DepositRepository : IDepositRepository
    {
        private readonly DataContext _context;

        public DepositRepository(DataContext DC)
        {
            _context = DC;
        }

        public async Task<IEnumerable<Deposit>> GetAsync()
        {
            //return await _context.Deposits.ToListAsync();
            return await _context.Deposits.Include(d=> d.Depositor).ToListAsync();
        }

        public async Task<Deposit> GetAsync(int id)
        {
            return await _context.Deposits.Include(d => d.Depositor).FirstOrDefaultAsync(e => e.Id == id);
            // return await _context.Deposits.FindAsync(id);
        }

        public async Task<Deposit> PostAsync(Deposit value)
        {
            _context.Deposits.Add(value);
            await _context.SaveChangesAsync();
            return await _context.Deposits.FindAsync(value.Id);
        }

        public async Task<Deposit> PutAsync(int id, Deposit value)
        {
            Deposit deposit = await _context.Deposits.FindAsync(id);
            if (deposit != null)
            {
                //deposit.DepositDate = value.DepositDate;
                deposit.DateOfMaturity = value.DateOfMaturity;
                //deposit.Status= value.Status;
                deposit.Depositor= value.Depositor;
                deposit.Notes= value.Notes;
                deposit.Amount= value.Amount;
            
                await _context.SaveChangesAsync();
            }
            return deposit;
        }
        public async Task<Deposit> DeleteAsync(int id)
        {
            Deposit deposit = await _context.Deposits.FindAsync(id);
            if (deposit != null)
            {
                deposit.Status =false;
                await _context.SaveChangesAsync();
            }
            return deposit;
        }
    }
}
