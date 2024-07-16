using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancialManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuaranteeController : ControllerBase
    {
        private readonly IGuaranteeService _GuaranteeService;
        public GuaranteeController(IGuaranteeService GuaranteeService)
        {
            _GuaranteeService = GuaranteeService;

        }
        // GET: api/<GuaranteeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Guarantee>>> Get()
        {
            var list = await _GuaranteeService.GetAllAsync();
            return Ok(list);
        }

        // GET api/<GuaranteeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var res = await _GuaranteeService.GetGuaranteeByIdAsync(id);
            return res != null ? Ok(res) : NotFound();
        }

        // POST api/<GuaranteeController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Guarantee value)
        {
            var res = await _GuaranteeService.PostGuaranteeAsync(value);
            return res != null ? Ok(value) : NotFound(value);
        }

        // PUT api/<GuaranteeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Guarantee value)
        {
            var res = await _GuaranteeService.PutGuaranteeAsync(id, value);
            return res != null ? Ok(res) : NotFound(res);
        }

        // DELETE api/<GuaranteeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await _GuaranteeService.DeleteGuaranteeAsync(id);
            return res != null ? Ok(res) : NotFound(res);
        }
    }
}
