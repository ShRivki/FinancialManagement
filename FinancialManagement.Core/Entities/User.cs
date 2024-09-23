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
        public string Identity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Address { get; set; }      
        public string Phone { get; set; }
        public string? Phone2 { get; set; }
        public string Email { get; set; }
        public bool IsReliable { get; set; } = true; // מציין אם המשתמש אמין

        public List<Loan> Loans { get; set; }//רשימת הלוואות 
        public List<Donation> Donations { get; set; }//רשימת תרומות
        public List<Deposit> Deposits { get; set; }//רשימת הפקדות
        public List<UserGuarantee> Guarantees { get; set; }//רשימת ערבויות 
        public User()
        {
            Loans = new List<Loan>();
            Donations = new List<Donation>();
            Deposits = new List<Deposit>();
            Guarantees= new List<UserGuarantee>();
        }
    }

}


