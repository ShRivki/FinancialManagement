using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Repositories;
using FinancialManagement.Core.Services;

namespace FinancialManagement.Service.Services
{
    public class DepositService: IDepositService
    {
        private readonly IDepositRepository _DepositRepository;
        public DepositService(IDepositRepository DepositRepository)
        {
            _DepositRepository = DepositRepository;
        }

        public async Task<IEnumerable<Deposit>> GetAllAsync()
        {
            return await _DepositRepository.GetAsync();
        }

        public async Task<Deposit> GetDepositByIdAsync(int id)
        {
            return await _DepositRepository.GetAsync(id);
        }

        public async Task<Deposit> PostDepositAsync(Deposit value)
        {
            
                var addDeposit = await _DepositRepository.PostAsync(value);
                string body = $@"
       <html>
         <body>
           <div style='width: 500px; padding: 40px; background-color: #e6f7ff; border-radius: 15px; font-family: Arial, sans-serif;'>
               <h2 style='color: #333;'>שלום {value.Depositor.FirstName},</h2>
               <p style='color: #333; font-size: 18px;'>אנו מאשרים שקיבלנו בהצלחה את הפקדתך בסך {value.Amount}{value.Currency}.</p>
               <p style='color: #333; font-size: 16px;'>הפקדה זו מחזקת את הביטחון הכלכלי שלך ומאפשרת לנו להמשיך לספק לך שירותים פיננסיים ברמה הגבוהה ביותר.</p>
               <p style='color: #333; font-size: 16px;'>אנו מעריכים את האמון שאתה נותן בנו ומתחייבים להמשיך ולשמור על השירות האיכותי שציפית לו.</p>
               <br/>
               <p style='color: #333; font-size: 14px;'>בברכה,</p>
               <p style='color: #333; font-size: 14px;'><strong> גמ""ח רץ כצבי </strong></p>
           </div>
         </body>
       </html>";
                await EmailService.SendEmailAsync(
                    value.Depositor.Email, // כתובת המייל של המפקיד
                    "New Deposit Received", // נושא המייל
                    body // גוף המייל המעוצב
                );

                return addDeposit;
            }


            public async Task<Deposit> PutDepositAsync(int id, Deposit value)
        {
            return await _DepositRepository.PutAsync(id, value);
        }
        public async Task<Deposit> DeleteDepositAsync(int id ,double? repaymentAmount = null)
        {
            return await _DepositRepository.DeleteAsync(id, repaymentAmount);
        }

    }
}
