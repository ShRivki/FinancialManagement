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
        public double Amount { get; set; }

        //public int GuaranteeAId { get; set; }
        //public Guarantee GuaranteeA { get; set; }
        //public int GuaranteeBId { get; set; }
        //public Guarantee GuaranteeB { get; set; }
        public List<Guarantee> Guarantees { get; set; }
        public Loan()
        {
            Guarantees = new List<Guarantee>();
        }
        public int Frequency { get; set; }//תדירות החזר 
        public int TotalPayments { get; set; } = 1;
        public int CurrentPayment { get; set; } = 0;
        public double RemainingAmount { get; set; }
        public bool Status { get; set; } = true;

    }
}
