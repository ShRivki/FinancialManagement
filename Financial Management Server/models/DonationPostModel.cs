using FinancialManagement.Core.Entities;


namespace  FinancialManagement.API.models
{
    public class DonationPostModel
    {
        public int DonorId { get; set; }

        public double Amount { get; set; }
        public CurrencyType Currency { get; set; }
        public FundraiserType Fundraiser { get; set; }
        public string Notes { get; set; }
    }
}
