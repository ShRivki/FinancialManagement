using System.ComponentModel.Design;
using AutoMapper;
using FinancialManagement.API.models;
using FinancialManagement.Core.Entities;



namespace Solid.API.mappings
{
    public class ApiMappingProfile:Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<DepositPostModel, Deposit>().ReverseMap();
            CreateMap<UserPostModel, User>().ReverseMap();
            CreateMap<LoanPostModel, Loan>().ReverseMap();
            CreateMap<DonationPostModel, Donation>().ReverseMap();
            CreateMap<GuaranteePostModel, Guarantee>().ReverseMap();
        }
    }
}
