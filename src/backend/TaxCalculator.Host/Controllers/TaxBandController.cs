using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using TaxCalculator.BL.Interfaces;
using TaxCalculator.BL.Models;
using TaxCalculator.Common.Models;
using TaxCalculator.Host.Models;
using TaxCalculator.InternalModels;

namespace TaxCalculator.Host.Controllers
{
    [ApiController]
    public class TaxBandController : ControllerBase
    {
        private readonly ITaxService _taxService;
        private readonly IOutputCacheStore _cache;
        public TaxBandController(ITaxService taxService, IOutputCacheStore cache)
        {
            _taxService = taxService;
            _cache = cache;
        }

        [HttpGet]
        [Route("/taxcalculator/v1/$calculateTaxes/{salary}")]
        [Consumes("application/json")]
        [OutputCache(PolicyName = Constants.SALARY_POLICY_NAME)]
        [SwaggerResponse(statusCode: 200, type: typeof(TaxDetails), description: "The request has succeeded.")]
        [SwaggerResponse(statusCode: 400, type: typeof(ServiceErrorResponce), description: "The server could not understand the request due to invelid syntax.")]
        [SwaggerResponse(statusCode: 404, type: typeof(ServiceErrorResponce), description: "The server could not find the requested resource.")]
        [SwaggerResponse(statusCode: 500, type: typeof(ServiceErrorResponce), description: "Server error.")]
        [SwaggerResponse(statusCode: 501, type: typeof(ServiceErrorResponce), description: "Server error.")]
        [SwaggerResponse(statusCode: 503, type: typeof(ServiceErrorResponce), description: "Server error.")]
        public async Task<IActionResult> CalculateTaxesBySalary([FromRoute(Name = "salary")] [Required] int salary)
        {
            return Ok(await _taxService.CalculateTaxesAsync(salary));
        }

        [HttpGet]
        [Route("/taxcalculator/v1/{id}")]
        [Consumes("application/json")]
        [OutputCache(PolicyName = Constants.TAXBAND_TAG)]
        [SwaggerResponse(statusCode: 200, type: typeof(InternalTaxBand), description: "The request has succeeded.")]
        [SwaggerResponse(statusCode: 400, type: typeof(ServiceErrorResponce), description: "The server could not understand the request due to invelid syntax.")]
        [SwaggerResponse(statusCode: 404, type: typeof(ServiceErrorResponce), description: "The server could not find the requested resource.")]
        [SwaggerResponse(statusCode: 500, type: typeof(ServiceErrorResponce), description: "Server error.")]
        [SwaggerResponse(statusCode: 501, type: typeof(ServiceErrorResponce), description: "Server error.")]
        [SwaggerResponse(statusCode: 503, type: typeof(ServiceErrorResponce), description: "Server error.")]
        public async Task<IActionResult> TaxBandRead([FromRoute(Name = "id")] [Required] Guid id)
        {
            return Ok(await _taxService.GetTaxBandAsync(id));
        }


        [HttpPut]
        [Route("/taxcalculator/v1/{id}")]
        [Consumes("application/json")]
        [SwaggerResponse(statusCode: 200, type: typeof(InternalTaxBand), description: "The request has succeeded.")]
        [SwaggerResponse(statusCode: 400, type: typeof(ServiceErrorResponce), description: "The server could not understand the request due to invelid syntax.")]
        [SwaggerResponse(statusCode: 404, type: typeof(ServiceErrorResponce), description: "The server could not find the requested resource.")]
        [SwaggerResponse(statusCode: 500, type: typeof(ServiceErrorResponce), description: "Server error.")]
        [SwaggerResponse(statusCode: 501, type: typeof(ServiceErrorResponce), description: "Server error.")]
        [SwaggerResponse(statusCode: 503, type: typeof(ServiceErrorResponce), description: "Server error.")]
        public async Task<IActionResult> TaxBandUpdate([FromRoute(Name = "id")] [Required] Guid id, [FromBody] InternalTaxBand taxBandToUpdate, CancellationToken cancellationToken)
        {

            var result = await _taxService.UpdateTaxBandAsync(id, taxBandToUpdate);

            await _cache.EvictByTagAsync(Constants.SALARY_TAG, cancellationToken);
            await _cache.EvictByTagAsync(Constants.TAXBAND_TAG, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        [Route("/taxcalculator/v1")]
        [Consumes("application/json")]
        [SwaggerResponse(statusCode: 200, type: typeof(InternalTaxBand), description: "The request has succeeded.")]
        [SwaggerResponse(statusCode: 400, type: typeof(ServiceErrorResponce), description: "The server could not understand the request due to invelid syntax.")]
        [SwaggerResponse(statusCode: 404, type: typeof(ServiceErrorResponce), description: "The server could not find the requested resource.")]
        [SwaggerResponse(statusCode: 500, type: typeof(ServiceErrorResponce), description: "Server error.")]
        [SwaggerResponse(statusCode: 501, type: typeof(ServiceErrorResponce), description: "Server error.")]
        [SwaggerResponse(statusCode: 503, type: typeof(ServiceErrorResponce), description: "Server error.")]
        public async Task<IActionResult> TaxBandCreate([FromBody] InternalTaxBand taxBandToUpdate, CancellationToken cancellationToken)
        {
            var result = await _taxService.CreateTaxBandAsync(taxBandToUpdate);

            await _cache.EvictByTagAsync(Constants.SALARY_TAG, cancellationToken);
            await _cache.EvictByTagAsync(Constants.TAXBAND_TAG, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        [Route("/taxcalculator/v1")]
        [Consumes("application/json")]
        [OutputCache(PolicyName = Constants.TAXBAND_POLICY_NAME)]
        [SwaggerResponse(statusCode: 200, type: typeof(IEnumerable<InternalTaxBand>), description: "The request has succeeded.")]
        [SwaggerResponse(statusCode: 400, type: typeof(ServiceErrorResponce), description: "The server could not understand the request due to invelid syntax.")]
        [SwaggerResponse(statusCode: 404, type: typeof(ServiceErrorResponce), description: "The server could not find the requested resource.")]
        [SwaggerResponse(statusCode: 500, type: typeof(ServiceErrorResponce), description: "Server error.")]
        [SwaggerResponse(statusCode: 501, type: typeof(ServiceErrorResponce), description: "Server error.")]
        [SwaggerResponse(statusCode: 503, type: typeof(ServiceErrorResponce), description: "Server error.")]
        public async Task<IActionResult> TaxBandList()
        {
            return Ok(await _taxService.GetAllTaxBandsAsync());
        }

        [HttpDelete]
        [Route("/taxcalculator/v1/{id}")]
        [Consumes("application/json")]
        [SwaggerResponse(statusCode: 200, description: "The request has succeeded.")]
        [SwaggerResponse(statusCode: 400, type: typeof(ServiceErrorResponce), description: "The server could not understand the request due to invelid syntax.")]
        [SwaggerResponse(statusCode: 404, type: typeof(ServiceErrorResponce), description: "The server could not find the requested resource.")]
        [SwaggerResponse(statusCode: 500, type: typeof(ServiceErrorResponce), description: "Server error.")]
        [SwaggerResponse(statusCode: 501, type: typeof(ServiceErrorResponce), description: "Server error.")]
        [SwaggerResponse(statusCode: 503, type: typeof(ServiceErrorResponce), description: "Server error.")]
        public async Task<IActionResult> TaxBandRemove([FromRoute(Name = "id")][Required] Guid id, CancellationToken cancellationToken)
        {
            await _taxService.RemoveTaxBandAsync(id);

            await _cache.EvictByTagAsync(Constants.SALARY_TAG, cancellationToken);
            await _cache.EvictByTagAsync(Constants.TAXBAND_TAG, cancellationToken);

            return Ok();
        }
    }
}
