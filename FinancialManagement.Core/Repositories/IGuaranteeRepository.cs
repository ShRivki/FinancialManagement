using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.Repositories
{
    public interface IGuaranteeRepository
    {
        public Task<IEnumerable<UserGuarantee>> GetAsync();

        public Task<UserGuarantee> GetAsync(int id);

        public Task<UserGuarantee> PostAsync(UserGuarantee value);

        public Task<UserGuarantee> PutAsync(int id, UserGuarantee value);

        public Task<UserGuarantee> DeleteAsync(int id);
    }
}
