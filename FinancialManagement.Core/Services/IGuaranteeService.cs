using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.Services
{
    public interface IGuaranteeService
    {
        public Task<IEnumerable<UserGuarantee>> GetAllAsync();


        public Task<UserGuarantee> GetGuaranteeByIdAsync(int id);


        public Task<UserGuarantee> PostGuaranteeAsync(UserGuarantee value);


        public Task<UserGuarantee> PutGuaranteeAsync(int id, UserGuarantee value);


        public Task<UserGuarantee> DeleteGuaranteeAsync(int id);
    }
}
