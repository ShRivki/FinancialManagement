using AutoMapper;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Services;
using FinancialManagement.Service.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancialManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalVariablesController : ControllerBase
    {
        
         private readonly IGlobalVariablesService _GlobalVariablesService;
        private readonly IMapper _mapper;
      
        public GlobalVariablesController(IGlobalVariablesService globalVariablesService, IMapper mapper)
        {
            _GlobalVariablesService = globalVariablesService;
            _mapper = mapper;
          

        }
        // GET: api/<GlobalVariables>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GlobalVariables>>>Get()
        {
            return Ok(await _GlobalVariablesService.GetAllAsync());
        }
     

    }
}
