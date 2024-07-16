using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.DTOs
{
    public class ContributionDto
    {
        public UserDtoA Donor { get; set; }

        public double Amount { get; set; }

        public string Notes { get; set; }

    }
}
