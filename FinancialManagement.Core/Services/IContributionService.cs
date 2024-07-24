using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.Services
{
    public interface IContributionService
    {
        public Task<IEnumerable<Donation>> GetAllAsync();


        public Task<Donation> GetContributionByIdAsync(int id);


        public Task<Donation> PostContributionAsync(Donation value);


        public Task<Donation> PutContributionAsync(int id, Donation value);


        public Task<Donation> DeleteContributionAsync(int id);
    }
}
