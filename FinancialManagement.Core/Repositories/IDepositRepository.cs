using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.Repositories
{
    public interface IDepositRepository
    {
        public Task<IEnumerable<Deposit>> GetAsync();

        public Task<Deposit> GetAsync(int id);

        public Task<Deposit> PostAsync(Deposit value);

        public Task<Deposit> PutAsync(int id, Deposit value);

        public Task<Deposit> DeleteAsync(int id, double? repaymentAmount = null);
    }
}
