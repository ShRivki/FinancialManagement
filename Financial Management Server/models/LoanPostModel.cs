﻿using FinancialManagement.API.models;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.API.models
{
    public class LoanPostModel
    {
        public int BorrowerId { get; set; }
        public double Amount { get; set; }
        public List<GuaranteePostModel> Guarantees { get; set; }
        public int Frequency { get; set; }
        public int TotalPayments { get; set; }
       // public int CurrentPayment { get; set; }
       // public bool Status { get; set; }


    }
}
