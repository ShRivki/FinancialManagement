using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagement.Core.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int BorrowerId { get; set; }
        public User Borrower { get; set; }
        public DateTime LoanDate { get; set; }
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
        public int Frequency { get; set; }
        public int TotalPayments { get; set; }
        public int CurrentPayment { get; set; }
        public bool Status { get; set; }

    }
}
