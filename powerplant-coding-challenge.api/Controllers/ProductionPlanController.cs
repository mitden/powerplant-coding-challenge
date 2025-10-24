using Microsoft.AspNetCore.Mvc;
using powerplantcodingchallenge.application.Dtos;
using powerplantcodingchallenge.application.Interfaces;
namespace powerplantcodingchallenge.api.Controllers
{
    [ApiController]
    [Route("productionplan")]
    public class ProductionPlanController : ControllerBase
    {     
        private readonly ILogger<ProductionPlanController> _logger;
        private readonly IProductionPlanService _ProductionPlanService;

        public ProductionPlanController(ILogger<ProductionPlanController> logger, IProductionPlanService productionPlanService)
        {
            _logger = logger;
            _ProductionPlanService = productionPlanService;
        }

        [HttpPost]
        public ActionResult<IList<ProductionPlanResponse>> Post([FromBody] ProductionPlanRequest request)
        {
           IList<ProductionPlanResponse> Result  = _ProductionPlanService.ComputePowerPLanProduction(request);
            return Ok(Result);
        }
    }
}
