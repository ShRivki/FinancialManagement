using AutoMapper;
using FinancialManagement.Core.DTOs;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Services;
using FinancialManagement.models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancialManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private readonly IDepositService _DepositService;
        private readonly IMapper _mapper;
        public DepositController(IDepositService DepositService, IMapper mapper)
        {
            _DepositService = DepositService;
            _mapper = mapper;
        }
        // GET: api/<DepositController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepositDto>>>Get()
        {
            var list = await _DepositService.GetAllAsync();
            var list1 = list.Select(l => _mapper.Map<DepositDto>(l));
            return Ok(list1);
        }

        // GET api/<DepositController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var res = await _DepositService.GetDepositByIdAsync(id);
            var resDto = _mapper.Map<DepositDto>(res);
            return res != null ? Ok(res) : NotFound();
        }

        // POST api/<DepositController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DepositPostModel value)
        {
            var deposit = _mapper.Map<Deposit>(value);
            var res = await _DepositService.PostDepositAsync(deposit);
            return res != null ? Ok(value) : NotFound(value);
        }

        // PUT api/<DepositController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DepositPostModel value)
        {
            var deposit = _mapper.Map<Deposit>(value);
            var res = await _DepositService.PutDepositAsync(id, deposit);
            return res != null ? Ok(res) : NotFound(res);
        }

        // DELETE api/<DepositController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await _DepositService.DeleteDepositAsync(id);
            return res != null ? Ok(res) : NotFound(res);
        }
    }
}
