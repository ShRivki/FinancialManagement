using AutoMapper;
using FinancialManagement.API.models;
using FinancialManagement.Core.DTOs;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Services;
using FinancialManagement.Service.Services;
using Microsoft.AspNetCore.Mvc;
using static FinancialManagement.Service.Services.CurrencyService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancialManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalVariablesController : ControllerBase
    {

        private readonly IGlobalVariablesService _GlobalVariablesService;
        private readonly IMapper _mapper;
        private readonly CurrencyService _CurrencyService;

        public GlobalVariablesController(IGlobalVariablesService globalVariablesService, IMapper mapper)
        {
            _GlobalVariablesService = globalVariablesService;
            _mapper = mapper;
            _CurrencyService = new CurrencyService();

        }
        // GET: api/<GlobalVariables>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GlobalVariables>>> Get()
        {
            return Ok(await _GlobalVariablesService.GetAllAsync());
        }
        [HttpPut("{operatingExpenses}")]
        public async Task<ActionResult> Put(double operatingExpenses)
        {

            if (operatingExpenses <= 0)
            {
                return BadRequest("Invalid amount.");
            }

            await _GlobalVariablesService.PutAsync(-operatingExpenses, CurrencyType.ILS);
            return Ok();

        }
        [HttpGet("rates")]
        public async Task<IActionResult> GetRates()
        {
            var rates = await _CurrencyService.GetRatesAsync();

            return Ok(new
            {
                UsdRate = rates.UsdRate,
                EurRate = rates.EurRate,
                GbpRate = rates.GbpRate
            });



        }
    }

}