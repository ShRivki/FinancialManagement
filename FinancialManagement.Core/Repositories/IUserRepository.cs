using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAsync();

        public Task<User> GetAsync(int id);

        public Task<User> PostAsync(User value);

        public Task<User> PutAsync(int id, User value);

        public Task<User> DeleteAsync(int id);
    }
}
