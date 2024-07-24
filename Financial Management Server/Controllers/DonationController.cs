using System.Xml.Linq;
using AutoMapper;
using FinancialManagement.API.models;
using FinancialManagement.Core.DTOs;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Services;
using FinancialManagement.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancialManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DonationController : ControllerBase
    {
        private readonly IContributionService _ContributionService;
        private readonly IMapper _mapper;
        private readonly IUserService _UserService;
        public  DonationController(IContributionService ContributionService, IMapper mapper,IUserService userService)
        {
            _ContributionService = ContributionService;
            _mapper = mapper;
            _UserService = userService;

        }
        // GET: api/<ContributionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonationDto>>> Get()
        {
            var list = await _ContributionService.GetAllAsync();
            var list1 = list.Select(l => _mapper.Map<DonationDto>(l));
            return Ok(list1);
        }

        // GET api/<ContributionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var res = await _ContributionService.GetContributionByIdAsync(id);
            var resDto = _mapper.Map<DonationDto>(res);
            return res != null ? Ok(resDto) : NotFound();
        }

        // POST api/<ContributionController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DonationPostModel value)
        {
            var contribution=_mapper.Map<Donation>(value); 
            var res = await _ContributionService.PostContributionAsync(contribution);
            return res != null ? Ok(value) : NotFound(value);

        }
            // PUT api/<ContributionController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DonationPostModel value)
        {
            var contribution = _mapper.Map<Donation>(value);
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
