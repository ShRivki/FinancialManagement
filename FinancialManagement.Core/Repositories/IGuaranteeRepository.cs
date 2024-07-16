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
        public Task<IEnumerable<Guarantee>> GetAsync();

        public Task<Guarantee> GetAsync(int id);

        public Task<Guarantee> PostAsync(Guarantee value);

        public Task<Guarantee> PutAsync(int id, Guarantee value);

        public Task<Guarantee> DeleteAsync(int id);
    }
}
