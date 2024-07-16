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
    public class ContributionRepository : IContributionRepository
    {
        private readonly DataContext _context;

        public ContributionRepository(DataContext DC)
        {
            _context = DC;
        }
      

        public async Task<IEnumerable<Contribution>> GetAsync()
        {
            return await _context.Contributions.Include(d => d.Donor).ToListAsync();
            //return await _context.Contributions.ToListAsync();
        }

        public async Task<Contribution> GetAsync(int id)
        {
            return await _context.Contributions.Include(d => d.Donor).FirstOrDefaultAsync(e => e.Id == id);
            //return await _context.Contributions.FindAsync(id);
        }

        public async Task<Contribution> PostAsync(Contribution value)
        {
            _context.Contributions.Add(value);
            await _context.SaveChangesAsync();
            return await _context.Contributions.FindAsync(value.Id);
        }

        public async Task<Contribution> PutAsync(int id, Contribution value)
        {
            Contribution contribution = await _context.Contributions.FindAsync(id);
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
        public async Task<Contribution> DeleteAsync(int id)
        {
            Contribution contribution = await _context.Contributions.FindAsync(id);
            if (contribution != null)
            {
                _context.Contributions.Remove(contribution);
                await _context.SaveChangesAsync();
            }
            return contribution;
        }
    }
}
