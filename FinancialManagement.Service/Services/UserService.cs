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
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        public UserService(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _UserRepository.GetAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _UserRepository.GetAsync(id);
        }

        public async Task<User> PostUserAsync(User value)
        {
            return await _UserRepository.PostAsync(value);
        }

        public async Task<User> PutUserAsync(int id, User value)
        {
            return await _UserRepository.PutAsync(id, value);
        }
        public async Task<User> DeleteUserAsync(int id)
        {
            return await _UserRepository.DeleteAsync(id);
        }

    }
}
