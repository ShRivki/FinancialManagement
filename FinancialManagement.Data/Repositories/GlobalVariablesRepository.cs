using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Data.Repositories
{
    public class GlobalVariablesRepository:IGlobalVariablesRepository
    {
        private readonly DataContext _context;
        public GlobalVariablesRepository(DataContext context) 
        { 
            _context = context;
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
    }
}
