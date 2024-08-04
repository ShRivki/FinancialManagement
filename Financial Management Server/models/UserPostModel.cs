

using FinancialManagement.Core.Entities;

namespace FinancialManagement.API.models
{
    public class UserPostModel
    {
        public string Identity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? Phone2 { get; set; }
        public string Email { get; set; }

    }
}
