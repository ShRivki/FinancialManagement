using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Repositories;
using FinancialManagement.Core.Services;
using Microsoft.VisualBasic;

namespace FinancialManagement.Service.Services
{
    public class LoanService : ILoanService
    {

        private readonly ILoanRepository _LoanRepository;
        public LoanService(ILoanRepository LoanRepository)
        {
            _LoanRepository = LoanRepository;
        }


        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _LoanRepository.GetAsync();
        }

        public async Task<Loan> GetLoanByIdAsync(int id)
        {
            return await _LoanRepository.GetAsync(id);
        }

        //public async Task<Loan> PostLoanAsync(Loan value)
        //{
        //    if (value.TotalPayments > 0)
        //    {
        //        value.MonthlyRepayment = value.Amount / value.TotalPayments;
        //    }
        //    value.RemainingAmount = value.Amount;
        //    var addedLoan = await _LoanRepository.PostAsync(value);
        //    // שלח מייל ללווה
        //    await EmailService.SendEmailAsync(
        //        addedLoan.Borrower.Email, // כתובת המייל של הלווה
        //        "Your Loan Has Been Approved", // נושא המייל
        //        $"Dear {addedLoan.Borrower.FirstName},\n\nYour loan of {addedLoan.Amount} {addedLoan.Currency}has been approved. Thank you for choosing our service.\n\nBest regards,\nYour Organization" // גוף המייל
        //    );
        //    foreach (var guarantee in addedLoan.Guarantees)
        //    {
        //        string guarantorBody = $@" <html> <body> <div style='width: 500px; padding: 40px; background-color: #f0f8ff; border-radius: 15px; font-family: Arial, sans-serif;'> <h2 style='color: #333;'>שלום {guarantee.Guarantor.FirstName},</h2> <p style='color: #333; font-size: 18px;'>ברצוננו להודיעך שההלוואה בסך {addedLoan.Amount} ש""ח ש-{addedLoan.Borrower.FirstName} {addedLoan.Borrower.LastName} אושרה בהצלחה!</p> <p style='color: #333; font-size: 16px;'>פרטי ההלוואה:</p> <ul style='color: #333; font-size: 16px;'> <li>סכום ההלוואה: {addedLoan.Amount} ש""ח</li> <li>תאריך קבלת ההלוואה: {addedLoan.LoanDate.ToString("dd/MM/yyyy")}</li> <li>תאריך ההחזר הראשון: {addedLoan.RepaymentDate.ToString("dd/MM/yyyy")}</li> <li>מספר תשלומים: {addedLoan.TotalPayments}</li> <li>תדירוץ: {addedLoan.Frequency}</li> <li>סכום כל תשלום: {addedLoan.MonthlyRepayment} ש""ח</li> </ul> <p style='color: #333; font-size: 16px;'>תודה על התמיכה שלך.</p> <br/> <p style='color: #333; font-size: 14px;'>בברכה,</p> <p style='color: #333; font-size: 14px;'><strong>גמ""ח רץ כצבי</strong></p> </div> </body> </html>";
        //        await EmailService.SendEmailAsync(guarantee.Guarantor.Email, "אישור ערב להלוואה", guarantorBody);
        //    }


        //    return addedLoan;
        //}
        public async Task<Loan> PostLoanAsync(Loan loan)
        {
            if (loan.TotalPayments > 0)
            {
                loan.MonthlyRepayment = loan.Amount / loan.TotalPayments;
            }
            loan.RemainingAmount = loan.Amount;
            var addedLoan = await _LoanRepository.PostAsync(loan);

            // בדוק אם ההלוואה נוספה בהצלחה
            if (addedLoan != null)
            {
                try
                {
                    // שלח מייל מעוצב ללווה
                    string body = $@"
       <html>
         <body>
           <div style='width: 500px; padding: 40px; background-color: #f0f8ff; border-radius: 15px; font-family: Arial, sans-serif;'>
               <h2 style='color: #333;'>שלום {addedLoan.Borrower.FirstName},</h2>
               <p style='color: #333; font-size: 18px;'>שמחים להודיעך שההלוואה שלך אושרה בהצלחה!</p>
               <p style='color: #333; font-size: 16px;'>פרטי ההלוואה:</p>
               <ul style='color: #333; font-size: 16px;'>
                   <li>סכום ההלוואה: {addedLoan.Amount} {addedLoan.Currency}</li>
                   <li>תאריך קבלת ההלוואה: {addedLoan.LoanDate.ToString("dd/MM/yyyy")}</li>
                   <li>תאריך ההחזר הראשון: {addedLoan.RepaymentDate.ToString("dd/MM/yyyy")}</li>
                   <li>מספר תשלומים: {addedLoan.TotalPayments}</li>
                   <li>תדירות: {addedLoan.Frequency} ימים</li>
                   <li>סכום כל תשלום:  ש""ח</li>
               </ul>
               <p style='color: #333; font-size: 16px;'>אנו כאן לכל שאלה או בקשה נוספת.</p>
               <br/>
               <p style='color: #333; font-size: 14px;'>בברכה,</p>
               <p style='color: #333; font-size: 14px;'><strong>גמ""ח רץ כצבי</strong></p>
           </div>
         </body>
       </html>";

                    await EmailService.SendEmailAsync(
                        addedLoan.Borrower.Email, // כתובת המייל של הלווה
                        "Your Loan Has Been Approved", // נושא המייל
                        body // גוף המייל המעוצב
                    );
                    // שלח מייל לערבים
                    foreach (var guarantee in addedLoan.Guarantees) { string guarantorBody = $@" <html> <body> <div style='width: 500px; padding: 40px; background-color: #f0f8ff; border-radius: 15px; font-family: Arial, sans-serif;'> <h2 style='color: #333;'>שלום {guarantee.Guarantor.FirstName},</h2> <p style='color: #333; font-size: 18px;'>ברצוננו להודיעך שההלוואה בסך {addedLoan.Amount}{addedLoan.Currency}  -{addedLoan.Borrower.FirstName} {addedLoan.Borrower.LastName} אושרה בהצלחה!</p> <p style='color: #333; font-size: 16px;'>פרטי ההלוואה:</p> <ul style='color: #333; font-size: 16px;'> <li>סכום ההלוואה: {addedLoan.Amount} ש""ח</li> <li>תאריך קבלת ההלוואה: {addedLoan.LoanDate.ToString("dd/MM/yyyy")}</li> <li>תאריך ההחזר הראשון: {addedLoan.RepaymentDate.ToString("dd/MM/yyyy")}</li> <li>מספר תשלומים: {addedLoan.TotalPayments}</li> <li>תדירוץ: {addedLoan.Frequency}</li> <li>סכום כל תשלום: {addedLoan.MonthlyRepayment} ש""ח</li> </ul> <p style='color: #333; font-size: 16px;'>תודה על התמיכה שלך.</p> <br/> <p style='color: #333; font-size: 14px;'>בברכה,</p> <p style='color: #333; font-size: 14px;'><strong>גמ""ח רץ כצבי</strong></p> </div> </body> </html>";
                        await EmailService.SendEmailAsync(guarantee.Guarantor.Email, "אישור הלוואה - עדכון", guarantorBody); }
                
                }
                catch (Exception ex)
                {
                    // טיפול בשגיאות שליחת מייל
                    Console.WriteLine("Error sending email: " + ex.Message);
                    // ניתן להוסיף טיפול נוסף, כגון לוג שגיאות או התראה למנהל המערכת
                }
            }

            return addedLoan;
        }


        public async Task<Loan> PutLoanAsync(int id, Loan value)
        {
            return await _LoanRepository.PutAsync(id, value);
        }
        public async Task<Loan> DeleteLoanAsync(int id, double? repaymentAmount)
        {
            return await _LoanRepository.DeleteAsync(id, repaymentAmount);
        }
    }
}
