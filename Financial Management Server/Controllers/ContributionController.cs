using System.Xml.Linq;
using AutoMapper;
using FinancialManagement.Core.DTOs;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Services;
using FinancialManagement.models;
using FinancialManagement.Service.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancialManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributionController : ControllerBase
    {
        private readonly IContributionService _ContributionService;
        private readonly IMapper _mapper;
        private readonly IUserService _UserService;
        public  ContributionController(IContributionService ContributionService, IMapper mapper,IUserService userService)
        {
            _ContributionService = ContributionService;
            _mapper = mapper;
            _UserService = userService;

        }
        // GET: api/<ContributionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContributionDto>>> Get()
        {
            var list = await _ContributionService.GetAllAsync();
            var list1 = list.Select(l => _mapper.Map<ContributionDto>(l));
            return Ok(list1);
        }

        // GET api/<ContributionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var res = await _ContributionService.GetContributionByIdAsync(id);
            var resDto = _mapper.Map<ContributionDto>(res);
            return res != null ? Ok(resDto) : NotFound();
        }

        // POST api/<ContributionController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ContributionPostModel value)
        {
            var contribution=_mapper.Map<Contribution>(value); 
            var res = await _ContributionService.PostContributionAsync(contribution);
            return res != null ? Ok(value) : NotFound(value);

        }
            // PUT api/<ContributionController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ContributionPostModel value)
        {
            var contribution = _mapper.Map<Contribution>(value);
            var res = await _ContributionService.PutContributionAsync(id, contribution);
            return res != null ? Ok(res) : NotFound(res);
        }

        // DELETE api/<ContributionController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await _ContributionService.DeleteContributionAsync(id);
            return res != null ? Ok(res) : NotFound(res);
        }
    }
}
