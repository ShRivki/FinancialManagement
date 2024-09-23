using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagement.Core.Entities
{
    public class UserGuarantee
    { 
        public int Id { get; set; }    
        public int GuarantorId { get; set; }
        public User Guarantor { get; set; }
        public int LoanId { get; set; }
        public Loan Loan { get; set; }

    }
}
