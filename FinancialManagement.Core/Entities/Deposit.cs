using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagement.Core.Entities
{
    public class Deposit
    {
        public int Id { get; set; }
        public int DepositorId { get; set; }
        public User Depositor { get; set; }
        public double Amount { get; set; }
        public DateTime DepositDate { get; set; }
        public DateTime DateOfMaturity { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
    }
}
