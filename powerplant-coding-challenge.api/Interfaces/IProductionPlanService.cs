using powerplantcodingchallenge.application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace powerplantcodingchallenge.application.Interfaces
{
    public interface IProductionPlanService
    {
        IList<ProductionPlanResponse> ComputePowerPLanProduction(ProductionPlanRequest request);
        double CalcCostForPowerPlantByFuelType(Powerplant powerplant, Fuels fuels);
    }
}
