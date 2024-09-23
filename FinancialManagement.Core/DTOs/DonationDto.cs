using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.DTOs
{
    public class DonationDto
    {
        public int Id { get; set; }
        public UserDtoA Donor { get; set; }

        public double Amount { get; set; }
        public CurrencyType Currency { get; set; }
        public FundraiserType Fundraiser { get; set; }

        public string Notes { get; set; }

    }
}
