using FinancialManagement.Core.Entities;
using Solid.API.models;

namespace FinancialManagement.models
{
    public class DepositPostModel
    {
        public int DepositorId { get; set; }
        public double Amount { get; set; }
        public DateTime DepositDate { get; set; }
        public DateTime DateOfMaturity { get; set; }
        public string Notes { get; set; }
    }
}
