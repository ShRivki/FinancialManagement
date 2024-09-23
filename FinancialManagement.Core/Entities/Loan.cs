using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.DTOs;

namespace FinancialManagement.Core.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int BorrowerId { get; set; }
        public User Borrower { get; set; }
        public DateTime LoanDate { get; set; }=DateTime.Now;
        public DateTime RepaymentDate { get; set; }///תשלום ראשון 
        public CurrencyType Currency { get; set; }
        public PaymentMethods paymentMethods { get; set; }
        public double Amount { get; set; }
        public List<UserGuarantee> Guarantees { get; set; }
        public List<DepositGuarantee> DepositGuarantee { get; set; }
        public Loan()
        {
            Guarantees = new List<UserGuarantee>();
            DepositGuarantee = new List<DepositGuarantee>();
        }
        public int Frequency { get; set; }//תדירות החזר 
        public double MonthlyRepayment { get;  set; }//סכום ההחזר הבא
        public int TotalPayments { get; set; } = 1; //כמות התשלומים 
        public int CurrentPayment { get; set; } = 0;//תשלום נוכחי
        public double RemainingAmount { get; set; }///סכום שנותר לתשלום 
        public bool Status { get; set; } = true;

    }
}
