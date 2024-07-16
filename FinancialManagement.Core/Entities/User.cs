using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagement.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Identity { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string Address { get; set; }
        public List<Loan> Loans { get; set; }//רשימת הלוואות 
        public List<Contribution> Contributions { get; set; }//רשימת תרומות
        public List<Deposit> Deposits { get; set; }//רשימת הפקדות
        public List<Guarantee> Guarantees { get; set; }//רשימת ערבויות 
        public User()
        {
            Loans = new List<Loan>();
            Contributions = new List<Contribution>();
            Deposits = new List<Deposit>();
            Guarantees= new List<Guarantee>();
        }
    }

}


