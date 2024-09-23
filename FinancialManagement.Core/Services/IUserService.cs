using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetAllAsync();


        public Task<User> GetUserByIdAsync(int id);


        public Task<User> PostUserAsync(User value);


        public Task<User> PutUserAsync(int id, User value);
        public Task<User> PutReliabilityAsync(int id, bool reliability);

        public Task<User> DeleteUserAsync(int id);
    }
}
