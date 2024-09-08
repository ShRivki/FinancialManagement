using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagement.Core.DTOs
{
    public class LoanDtoB
    {
        public DateTime LoanDate { get; set; }
        public DateTime RepaymentDate { get; set; }
        public double Amount { get; set; }
        public List<GuaranteeDto> Guarantees { get; set; }
        public int Frequency { get; set; }
        public int TotalPayments { get; set; }
        public int CurrentPayment { get; set; }
        public double RemainingAmount { get; set; }
        public bool Status { get; set; }
    }
}
