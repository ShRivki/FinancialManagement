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
        public Task<IEnumerable<Guarantee>> GetAllAsync();


        public Task<Guarantee> GetGuaranteeByIdAsync(int id);


        public Task<Guarantee> PostGuaranteeAsync(Guarantee value);


        public Task<Guarantee> PutGuaranteeAsync(int id, Guarantee value);


        public Task<Guarantee> DeleteGuaranteeAsync(int id);
    }
}
