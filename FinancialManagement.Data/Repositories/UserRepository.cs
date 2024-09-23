using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext DC)
        {
            _context = DC;
           
        }
      
        public async Task<IEnumerable<User>> GetAsync()
        {
            //return await _context.Loans
            //        .Include(l => l.Guarantees)          // כולל את כל הערביות השייכות להלוואה
            //            .ThenInclude(g => g.Guarantor)  // כלל את המידע על הערב
            //        .Include(l => l.Borrower)           // כולל את המשתמש המשויך להלוואה
            //        .ToListAsync();
            //  var list = await _context.Users.ToListAsync();
            var list = await _context.Users
        .Include(x => x.Guarantees.Where(g => g.Loan.Status == true)) // סינון ערביות פעילות
        .Include(x => x.Donations)
        .Include(x => x.Deposits)
        .Include(u => u.Loans)
            .ThenInclude(e => e.Guarantees.Where(g => g.Loan.Status == true)) // סינון ערביות פעילות
            .ThenInclude(p => p.Guarantor)
        .ToListAsync();
            return list;
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.Users.Include(x => x.Guarantees).Include(x => x.Donations).Include(x => x.Deposits).Include(x => x.Loans).ThenInclude(e => e.Guarantees).ThenInclude(p => p.Guarantor).FirstOrDefaultAsync(e => e.Id == id);
            //  return await _context.Users.FindAsync(id);
        }

        public async Task<User> PostAsync(User value)
        {
            _context.Users.Add(value);

            await _context.SaveChangesAsync();
            return await _context.Users.FindAsync(value.Id);
        }

        public async Task<User> PutAsync(int id, User value)
        {
            User user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.Email = value.Email;
             
                user.FirstName = value.FirstName;
                user.LastName = value.LastName;
                user.Phone = value.Phone;
                user.Phone2 = value.Phone2;
                //user.Loans= value.Loans;
                //user.Donations = value.Donations;
                //user.Deposits= value.Deposits;
                //user.Guarantees= value.Guarantees;
                await _context.SaveChangesAsync();
            }
            return user;
        }
        public async Task<User> PutReliabilityAsync(int id, bool reliability)
        {
            User user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.IsReliable = reliability;
                await _context.SaveChangesAsync();
            }
            return user;

        }
        public async Task<User> DeleteAsync(int id)
        {
            User user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
               
            }
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
