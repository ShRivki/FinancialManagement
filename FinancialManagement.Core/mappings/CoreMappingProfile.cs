﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FinancialManagement.Core.DTOs;
using FinancialManagement.Core.Entities;

namespace Solid.Core.mappings
{
    public class CoreMappingProfile :Profile     
    {
        public CoreMappingProfile()
        {
            CreateMap<LoanDto, Loan>().ReverseMap();
            CreateMap<LoanDtoB, Loan>().ReverseMap();
            CreateMap<GuaranteeDtoB, UserGuarantee>().ReverseMap();
            CreateMap<GuaranteeDto, UserGuarantee>().ReverseMap();
            CreateMap<DepositGuaranteeDtoA, DepositGuarantee>().ReverseMap();
            CreateMap<DepositGuaranteeDtoB, DepositGuarantee>().ReverseMap();
            CreateMap<UserDtoB, User>().ReverseMap();
            CreateMap<UserDtoA, User>().ReverseMap();
            CreateMap<DepositDto, Deposit>().ReverseMap();
            CreateMap<DepositDtoB, Deposit>().ReverseMap();
            CreateMap<DonationDto, Donation>().ReverseMap();

        }
    }
}
