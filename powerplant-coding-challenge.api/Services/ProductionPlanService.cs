using powerplantcodingchallenge.application.Dtos;
using powerplantcodingchallenge.application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace powerplantcodingchallenge.application.Services
{
    public class ProductionPlanService : IProductionPlanService
    {


        IList<ProductionPlanResponse> IProductionPlanService.ComputePowerPLanProduction(ProductionPlanRequest request)
        {
            List<Powerplant> sortedByMeritOrderPowerplant = new List<Powerplant>();
            IList<ProductionPlanResponse> results = new List<ProductionPlanResponse>();

            //Return empty result if resquest is null
            if (request == null) return results;

            double load = request.Load;


            //Merit Order for powerplants
            sortedByMeritOrderPowerplant =  SortPowerPlantByMeritOrder(request.Powerplants, request.Fuels);

            foreach (Powerplant powerplant in sortedByMeritOrderPowerplant)
            {
                //Calc allocation per powerplant
                double p = 0;

                if(load > 0)
                {

                    if(powerplant.Type == "windturbine")
                    {
                        p = powerplant.Pmax * (request.Fuels.WindInPercent / 100);
                    }
                    else
                    {
                        p = load < powerplant.Pmax?  load :powerplant.Pmax;
                    }


                    //Substract the allocated power to the load.
                    load = load - p;
                }

                results.Add(new ProductionPlanResponse { Name = powerplant.Name, Power = Math.Round(p, 1) });


            }

            return results;
        }



        /// <summary>
        /// Sort powerplant with the Merit Order methode
        /// </summary>
        /// <param name="powerplants"></param>
        /// <param name="fuels"></param>
        /// <returns></returns>
        public List<Powerplant> SortPowerPlantByMeritOrder(List<Powerplant> powerplants, Fuels fuels)
        {
            return powerplants.OrderBy(p => CalcCostForPowerPlantByFuelType(p, fuels)).ToList();
        }


        /// <summary>
        /// Calculate the cost of powerplant by Fuel Type
        /// </summary>
        /// <param name="powerplant"></param>
        /// <param name="fuels"></param>
        /// <returns></returns>
        public double CalcCostForPowerPlantByFuelType(Powerplant powerplant, Fuels fuels)
        {
            double cost = 0;
            switch (powerplant.Type) {
                case "gasfired":
                    cost =   fuels.GasInEuroPerMwh / powerplant.Efficiency;
                    break;
                case "turbojet":
                    cost = fuels.KerosineInEuroPerMwh / powerplant.Efficiency;
                    break;
                case "windturbine":
                    cost = 0;
                    break;

            }
            return cost;
        }
    }

}
