﻿using AutoMapper;
using FinancialManagement.Core.DTOs;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Services;
using FinancialManagement.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solid.API.models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancialManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _LoanService;
        private readonly IGuaranteeService _GuaranteeService;
        private readonly IUserService _UserService;
        private readonly IMapper _mapper;
          
        public LoanController(ILoanService LoanService ,IMapper mapper, IGuaranteeService GuaranteeService,IUserService userService)
        {
            _LoanService = LoanService;
            _mapper = mapper;
            _GuaranteeService = GuaranteeService;
            _UserService = userService;

        }
        // GET: api/<LoanController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanDto>>> Get()
        {
            var list = await _LoanService.GetAllAsync();
            var list1 = list.Select(l => _mapper.Map<LoanDto>(l));
            return Ok(list1);
        }

        // GET api/<LoanController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var res = await _LoanService.GetLoanByIdAsync(id);
            var resDto = _mapper.Map<LoanDto>(res);
            return res != null ? Ok(resDto) : NotFound();
        }

        // POST api/<LoanController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LoanPostModel value)
        {

            var loan = _mapper.Map<Loan>(value);
            var res = await _LoanService.PostLoanAsync(loan);
            var resDto = _mapper.Map<LoanDto>(res);
            return res != null ? Ok(resDto) : NotFound(resDto);
           
        } 

        // PUT api/<LoanController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Loan value)
        {
            var res = await _LoanService.PutLoanAsync(id, value);
            return res != null ? Ok(res) : NotFound(res);

        }

        // DELETE api/<LoanController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await _LoanService.DeleteLoanAsync(id);
            return res != null ? Ok(res) : NotFound(res);
        }
    }
}
