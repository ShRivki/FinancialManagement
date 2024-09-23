using AutoMapper;
using FinancialManagement.API.models;
using FinancialManagement.Core.DTOs;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Services;
using FinancialManagement.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancialManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult> Put(int id, [FromBody] LoanPostModel value)
        {
            var loan = _mapper.Map<Loan>(value);
            var res = await _LoanService.PutLoanAsync(id, loan);
            var resDto = _mapper.Map<LoanDto>(res);
            return res != null ? Ok(resDto) : NotFound(resDto);

        }

        // DELETE api/<LoanController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, double? repaymentAmount = null)
        {
            var res = await _LoanService.DeleteLoanAsync(id, repaymentAmount);
            var resDto = _mapper.Map<LoanDto>(res);
            return res != null ? Ok(resDto) : NotFound(resDto);
        }
    }
}
