using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagement.Core.Entities
{
   public class DepositGuarantee
    {
        public int Id { get; set; }
        public int DepositId { get; set; }
        public Deposit Deposit { get; set; }
        public int LoanId { get; set; }
        public Loan Loan { get; set; }
    }
}
