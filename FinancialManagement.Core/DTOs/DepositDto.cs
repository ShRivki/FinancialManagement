using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.DTOs
{
    public class DepositDto
    {
        public int Id { get; set; }
        public UserDtoA Depositor { get; set; }
        public double Amount { get; set; }
        public DateTime DepositDate { get; set; }
        public DateTime DateOfMaturity { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
    }
}
