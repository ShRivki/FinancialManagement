using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagement.Core.Entities
{
   
    public enum FundraiserType
    {
        ELIYAHU_SHRAIBER,
        YAKOV_FRIDMAN,
        ELIYAHU_GORFEIN,
        CLALI 
    }
    public class Donation
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
        public User Donor { get; set; }
        public double Amount { get; set; }
        public CurrencyType Currency { get; set; } 
        public FundraiserType Fundraiser { get; set; }
        public string Notes { get; set; }
    }

}
