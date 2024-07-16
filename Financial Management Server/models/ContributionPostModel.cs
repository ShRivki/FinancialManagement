using FinancialManagement.Core.Entities;
using Solid.API.models;

namespace FinancialManagement.models
{
    public class ContributionPostModel
    {
        public int DonorId { get; set; }

        public double Amount { get; set; }

        public string Notes { get; set; }
    }
}
