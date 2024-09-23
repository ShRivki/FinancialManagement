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
        public int Id { get; set; }
        public string Identity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public bool IsReliable { get; set; }
        public List<LoanDto> Loans { get; set; }//רשימת הלוואות 
        public List<DepositDto> Deposits { get; set; }
        public List<DonationDto> Donations { get; set; }
        public List<GuaranteeDtoB> Guarantees { get; set; }//רשימת ערבויות 

    }
}
