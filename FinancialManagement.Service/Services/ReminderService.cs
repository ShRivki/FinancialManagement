using FinancialManagement.Core.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinancialManagement.Service.Services;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Service
{
    public class ReminderService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly TimeSpan _interval = TimeSpan.FromDays(1);

        public ReminderService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // הגדרת טיימר שיפעל היום בשעה 9
            var timeToRun = DateTime.Today.AddHours(14).AddMinutes(45);

            // אם השעה הנוכחית כבר אחרי 9, נקבע את ההפעלה למחר
            if (DateTime.Now > timeToRun)
            {
                timeToRun = timeToRun.AddDays(1);
            }

            var initialDelay = timeToRun - DateTime.Now;

            _timer = new Timer(async state => await SendRepaymentReminders(), null,
                               initialDelay, _interval);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            _timer?.Dispose();
            return Task.CompletedTask;
        }

        private async Task SendRepaymentReminders()
        {
            Console.WriteLine("SendRepaymentReminders התחילה");

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var loanService = scope.ServiceProvider.GetRequiredService<ILoanService>();
                // לוגיקת שליחת המיילים
                // קבלת רשימת כל ההלוואות
                var allLoans = await loanService.GetAllAsync();
                // סינון הלוואות עם סטטוס פעיל (Status = true)
                var activeLoans = allLoans.Where(loan => loan.Status == true).ToList();
                foreach (var loan in activeLoans)
                {
                    Console.WriteLine(loan.Frequency);
                    Console.WriteLine(loan.CurrentPayment);
                    // חישוב התשלום הבא על בסיס תדירות ומספר תשלומים
                    var nextPaymentDate = loan.RepaymentDate.Date.AddDays(loan.Frequency * (loan.CurrentPayment));

                    // בדיקה אם התשלום הבא הוא בעוד יומיים
                    if (nextPaymentDate.Date == DateTime.Now.Date.AddDays(2))  // משווה רק את התאריכים
                    {
                        string body = $@"
                        <html>
                          <body>
                            <div style='width: 500px; padding: 40px; background-color: #f0f8ff; border-radius: 15px; font-family: Arial, sans-serif;'>
                                <h2 style='color: #333;'>שלום {loan.Borrower.FirstName}{loan.Borrower.LastName},</h2>
                                <p style='color: #333; font-size: 18px;'>בעוד יומיים בתאריך {nextPaymentDate.Date:dd/MM/yyyy} עליך לשלם את התשלום ה-{(loan.CurrentPayment ) + 1}.</p>
                                <p style='color: #333; font-size: 16px;'>פרטי התשלום:</p>
                                <ul style='color: #333; font-size: 16px;'>
                                  <li>סכום התשלום: {(loan.Amount / loan.TotalPayments):0.00} {loan.Currency}</li>
                                  <li>תאריך התשלום הבא: {loan.RepaymentDate.Date.AddDays(loan.Frequency * (loan.CurrentPayment+1)):dd/MM/yyyy}</li>
                                </ul>
                                <p style='color: #333; font-size: 16px;'>אנא הקפד לשלם בזמן.</p>
                                <br/>
                                <p style='color: #333; font-size: 14px;'>בברכה,</p>
                                <p style='color: #333; font-size: 14px;'><strong>גמ""ח רץ כצבי</strong></p>
                            </div>
                          </body>
                        </html>";

                        await EmailService.SendEmailAsync(
                            loan.Borrower.Email,
                            "תזכורת לתשלום ההלוואה",
                            body
                        );

                        Console.WriteLine("שליחת תזכורת לתשלום הלוואה.");
                    }
                    if (loan.paymentMethods.HasFlag(PaymentMethods.Check))
                    {
                        string managerEmailBody = $@"
                    <html>
                      <body>
                        <div style='width: 500px; padding: 40px; background-color: #f0f8ff; border-radius: 15px; font-family: Arial, sans-serif;'>
                            <h2 style='color: #333;'>שלום מנהל יקר,</h2>
                            <p style='color: #333; font-size: 18px;'>בעוד יומיים בתאריך {nextPaymentDate.Date:dd/MM/yyyy} עליך לוודא שהתשלום עבור ההלוואה ה-{(loan.CurrentPayment) + 1} שולם.</p>
                            <p style='color: #333; font-size: 16px;'>פרטי התשלום:</p>
                            <ul style='color: #333; font-size: 16px;'>
                              <li>סכום התשלום: {(loan.Amount / loan.TotalPayments):0.00} {loan.Currency}</li>
                              <li>תאריך התשלום הבא: {loan.RepaymentDate.Date.AddDays(loan.Frequency * (loan.CurrentPayment + 1)):dd/MM/yyyy}</li>
                              <li>מזהה הלוואה: {loan.Id}</li>
                              <li>:שם הלווה {loan.Borrower.FirstName}{loan.Borrower.LastName}</li>
                            </ul>
                            <p style='color: #333; font-size: 16px;'>אנא הקפד לפרוע את הצ'ק בזמן.</p>
                            <br/>
                            <p style='color: #333; font-size: 14px;'>בברכה,</p>
                            <p style='color: #333; font-size: 14px;'><strong>גמ""ח רץ כצבי</strong></p>
                        </div>
                      </body>
                    </html>";

                        // Replace with actual manager email address
                        await EmailService.SendEmailAsync(
                            "razkazvi@gmail.com",
                            "תזכורת לפרעון צ'ק",
                            managerEmailBody
                        );

                        Console.WriteLine("שליחת תזכורת לפרעון צ'ק למנהל.");
                    }
                }
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}