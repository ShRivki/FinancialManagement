using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.Services
{
    public interface IDepositService
    {
        public Task<IEnumerable<Deposit>> GetAllAsync();


        public Task<Deposit> GetDepositByIdAsync(int id);


        public Task<Deposit> PostDepositAsync(Deposit value);


        public Task<Deposit> PutDepositAsync(int id, Deposit value);


        public Task<Deposit> DeleteDepositAsync(int id);
    }
}
