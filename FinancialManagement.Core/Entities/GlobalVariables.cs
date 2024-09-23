using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagement.Core.Entities
{
    public class GlobalVariables
    {
        public int Id {  get; set; }
        public double TotalFundBalance { get; set; }
        public double TotalFundBalanceILS { get; set; }  // ש"ח
        public double TotalFundBalanceUSD { get; set; }  // דולר
        public double TotalFundBalanceGBP { get; set; }  // שטרלינג
        public double TotalFundBalanceEUR { get; set; }  // יורו
        public double ActiveLoans { get; set; }
        public double TotalLoansGranted { get; set; } 
    }
}
