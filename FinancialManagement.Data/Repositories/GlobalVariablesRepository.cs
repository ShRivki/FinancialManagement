using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Repositories;
using FinancialManagement.Core.Services;
using FinancialManagement.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Data.Repositories
{
    public class GlobalVariablesRepository:IGlobalVariablesRepository
    {
        private readonly DataContext _context;
        private readonly ICurrencyService _CurrencyService;
        public GlobalVariablesRepository(DataContext context, ICurrencyService currencyService) 
        { 
            _context = context;
            _CurrencyService = currencyService;
        }
        //public async Task<IEnumerable<GlobalVariables>> GetAsync()
        //{
        //    return (IEnumerable<GlobalVariables>)await _context.GlobalVariables.ToListAsync();

        //}
        public async Task<IEnumerable<GlobalVariables>> GetAsync()
        {
            return await _context.GlobalVariables.ToListAsync();
            //return await _context.Contributions.ToListAsync();
        }
        //public async Task<IEnumerable<GlobalVariables>> PutAsync(double operatingExpenses)
        //{
        //    var globalVariables = await _context.GlobalVariables.FirstOrDefaultAsync();
        //    if (globalVariables != null)
        //    {
        //        globalVariables.TotalFundBalance -= operatingExpenses;
        //    }

        //    await _context.SaveChangesAsync();
        //    return await _context.GlobalVariables.ToListAsync();
        //}
        public async Task<IEnumerable<GlobalVariables>> PutAsync(double amount, CurrencyType currencyType)
        {
            // מקבלים את שערי המטבעות מהשירות
            var (usdRate, eurRate, gbpRate) = await _CurrencyService.GetRatesAsync();
            // מקבלים את ה-GlobalVariables הראשון מהרשימה
            try
            {

                var globalVariablesList = await GetAsync();
                var globalVariables = globalVariablesList.FirstOrDefault();

                if (globalVariables != null)
                {
                    // שער ברירת מחדל (במקרה של ILS אין צורך בשער)
                    decimal exchangeRate = 1;

                    // קביעת שער המטבע בהתאם לסוג המטבע שנבחר
                    switch (currencyType)
                    {
                        case CurrencyType.ILS:
                            globalVariables.TotalFundBalanceILS += amount;
                            break;
                        case CurrencyType.USD:
                            globalVariables.TotalFundBalanceUSD += amount;
                            exchangeRate = usdRate; // שער דולר
                            break;
                        case CurrencyType.EUR:
                            globalVariables.TotalFundBalanceEUR += amount;
                            exchangeRate = eurRate; // שער אירו
                            break;
                        case CurrencyType.GBP:
                            globalVariables.TotalFundBalanceGBP += amount;
                            exchangeRate = gbpRate; // שער שטרלינג
                            break;
                        default:
                            throw new ArgumentException("Unsupported currency type");
                    }

                    // הכפלת הסכום בשער המטבע כדי לעדכן את היתרה הכללית
                    globalVariables.TotalFundBalance += amount * (double)exchangeRate;
                }
                }
            catch
            {
                Console.WriteLine("erorr");
            }

            // בודקים אם הרשימה אינה ריקה ומקבלים את האובייקט הראשון

            // שמירת השינויים במסד הנתונים
            await _context.SaveChangesAsync();

            // מחזירים את רשימת GlobalVariables אחרי העדכון
            return await _context.GlobalVariables.ToListAsync();
        }


    }
}
