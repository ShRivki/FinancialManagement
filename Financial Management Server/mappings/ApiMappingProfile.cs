using System.ComponentModel.Design;
using AutoMapper;
using FinancialManagement.API.models;
using FinancialManagement.Core.Entities;
using FinancialManagement.models;
using Solid.API.models;


namespace Solid.API.mappings
{
    public class ApiMappingProfile:Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<DepositPostModel, Deposit>().ReverseMap();
            CreateMap<UserPostModel, User>().ReverseMap();
            CreateMap<LoanPostModel, Loan>().ReverseMap();
            CreateMap<ContributionPostModel, Contribution>().ReverseMap();
            CreateMap<GuaranteePostModel, Guarantee>().ReverseMap();
        }
    }
}
