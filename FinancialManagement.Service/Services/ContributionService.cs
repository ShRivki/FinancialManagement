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
    public class ContributionService: IContributionService
    {
        private readonly IContributionRepository _ContributionRepository;
        public ContributionService(IContributionRepository ContributionRepository)
        {
            _ContributionRepository = ContributionRepository;
        }

        public async Task<IEnumerable<Contribution>> GetAllAsync()
        {
            return await _ContributionRepository.GetAsync();
        }

        public async Task<Contribution> GetContributionByIdAsync(int id)
        {
            return await _ContributionRepository.GetAsync(id);
        }

        public async Task<Contribution> PostContributionAsync(Contribution value)
        {
            return await _ContributionRepository.PostAsync(value);
        }

        public async Task<Contribution> PutContributionAsync(int id, Contribution value)
        {
            return await _ContributionRepository.PutAsync(id, value);
        }
        public async Task<Contribution> DeleteContributionAsync(int id)
        {
            return await _ContributionRepository.DeleteAsync(id);
        }

    }
}
