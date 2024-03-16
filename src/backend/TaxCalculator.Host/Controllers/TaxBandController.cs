using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaxCalculator.BL.Interfaces;
using TaxCalculator.BL.Models;
using TaxCalculator.Host.Models;

namespace TaxCalculator.Host.Controllers
{
    [ApiController]
    public class TaxBandController : ControllerBase
    {
        private readonly ITaxService _taxService;

        public TaxBandController(ITaxService taxService)
        {
            _taxService = taxService;
        }

        [HttpGet]
        [Route("/taxcalculator/$calculateTaxes")]
        [Consumes("application/json")]
        [SwaggerResponse(statusCode: 200, type: typeof(TaxDetails), description: "The request has succeeded.")]
        [SwaggerResponse(statusCode: 400, type: typeof(ServiceErrorResponce), description: "The server could not understand the request due to invelid syntax.")]
        public async Task<IActionResult> CalculateTaxesBySalary([FromQuery] int salary)
        {
            return Ok(await _taxService.CalculateTaxes(salary));
        }
    }
}
