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
    public class DonationRepository : IDonationRepository
    {
        private readonly DataContext _context;

        public DonationRepository(DataContext DC)
        {
            _context = DC;
        }
      

        public async Task<IEnumerable<Donation>> GetAsync()
        {
            return await _context.Donations.Include(d => d.Donor).ToListAsync();
            //return await _context.Contributions.ToListAsync();
        }

        public async Task<Donation> GetAsync(int id)
        {
            return await _context.Donations.Include(d => d.Donor).FirstOrDefaultAsync(e => e.Id == id);
            //return await _context.Contributions.FindAsync(id);
        }

        public async Task<Donation> PostAsync(Donation value)
        {
            _context.Donations.Add(value);
            await _context.SaveChangesAsync();
            return await _context.Donations.FindAsync(value.Id);
        }

        public async Task<Donation> PutAsync(int id, Donation value)
        {
            Donation contribution = await _context.Donations.FindAsync(id);
            if (contribution != null)
            {
                contribution.Amount = value.Amount;
                contribution.Notes= value.Notes;
                contribution.Donor= value.Donor;
                contribution.DonorId= value.DonorId;    

                 await _context.SaveChangesAsync();
            }
            return contribution;
        }
        public async Task<Donation> DeleteAsync(int id)
        {
            Donation contribution = await _context.Donations.FindAsync(id);
            if (contribution != null)
            {
                _context.Donations.Remove(contribution);
                await _context.SaveChangesAsync();
            }
            return contribution;
        }
    }
}
