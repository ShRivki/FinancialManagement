using AutoMapper;
using FinancialManagement.API.models;
using FinancialManagement.Core.DTOs;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancialManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;
        private readonly IMapper _mapper;
        public UserController(IUserService UserService, IMapper mapper)
        {
            _UserService = UserService;
            _mapper = mapper;

        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDtoB>>> Get()
        {
            var list = await _UserService.GetAllAsync();
            var list1 = list.Select(l => _mapper.Map<UserDtoB>(l));
            return Ok(list1);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var res = await _UserService.GetUserByIdAsync(id);
            var resDto = _mapper.Map<UserDtoB>(res);
            return res != null ? Ok(resDto) : NotFound();
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserPostModel value)
        {
            var user = _mapper.Map<User>(value);
            var res = await _UserService.PostUserAsync(user);
            var resDto = _mapper.Map<UserDtoB>(res);
            return res != null ? Ok(resDto) : NotFound(value);

        }
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UserPostModel value)
        {
            var user = _mapper.Map<User>(value);
            var res = await _UserService.PutUserAsync(id, user);
            return res != null ? Ok(res) : NotFound(res);
        }
        [HttpPut("{id}/reliability")]
        public async Task<ActionResult> UpdateReliability(int id, [FromBody] bool isReliable)
        {
            var res = await _UserService.PutReliabilityAsync(id, isReliable);
            return res != null ? Ok(res) : NotFound(res);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await _UserService.DeleteUserAsync(id);
            return res != null ? Ok(res) : NotFound(res);
        }
    }
}
