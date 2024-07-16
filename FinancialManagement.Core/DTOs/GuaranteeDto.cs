using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.DTOs
{
    public class GuaranteeDto
    {
        public UserDtoA Guarantor { get; set; }
    }
}
