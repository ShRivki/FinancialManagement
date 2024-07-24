using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Repositories;
using FinancialManagement.Core.Services;

namespace FinancialManagement.Service.Services
{
    public class DonationService: IContributionService
    {
        private readonly IDonationRepository _ContributionRepository;
        public DonationService(IDonationRepository ContributionRepository)
        {
            _ContributionRepository = ContributionRepository;
        }

        public async Task<IEnumerable<Donation>> GetAllAsync()
        {
            return await _ContributionRepository.GetAsync();
        }

        public async Task<Donation> GetContributionByIdAsync(int id)
        {
            return await _ContributionRepository.GetAsync(id);
        }

        public async Task<Donation> PostContributionAsync(Donation value)
        {
            return await _ContributionRepository.PostAsync(value);
        }

        public async Task<Donation> PutContributionAsync(int id, Donation value)
        {
            return await _ContributionRepository.PutAsync(id, value);
        }
        public async Task<Donation> DeleteContributionAsync(int id)
        {
            return await _ContributionRepository.DeleteAsync(id);
        }

    }
}
