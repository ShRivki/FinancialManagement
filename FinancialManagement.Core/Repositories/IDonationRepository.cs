using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.Repositories
{
    public interface IDonationRepository
    {
        public Task<IEnumerable<Donation>> GetAsync();

        public Task<Donation> GetAsync(int id);

        public Task<Donation> PostAsync(Donation value);

        public Task<Donation> PutAsync(int id, Donation value);

        public Task<Donation> DeleteAsync(int id);
    }
}
