using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.DTOs
{
    public class UserDtoB
    {
        public string UserName { get; set; }
        public string Identity { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string Address { get; set; }
        public List<LoanDtoB> Loans { get; set; }//רשימת הלוואות 
        public List<DepositDto> Deposits { get; set; }
        public List<ContributionDto> Contributions { get; set; }
        public List<GuaranteeDtoB> Guarantees { get; set; }//רשימת ערבויות 

    }
}
