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
        public double ActiveLoans { get; set; }
        public double TotalLoansGranted { get; set; } 
    }
}
