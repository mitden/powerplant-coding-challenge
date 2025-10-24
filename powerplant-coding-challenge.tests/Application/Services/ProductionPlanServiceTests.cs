using powerplantcodingchallenge.application.Interfaces;
using powerplantcodingchallenge.application.Services;
using powerplantcodingchallenge.application.Dtos;
using System.Text.Json;

namespace powerplantcodingchallenge.tests;

public class ProductionPlanServiceTests
{

    private IProductionPlanService _ProductionPlanService;

    [SetUp]
    public void Setup()
    {
        _ProductionPlanService = new ProductionPlanService();
    }





    [Test]
    public void ComputePowerPLanProductionShloudReturnEmptyListIfTheRequestIsNull()
    {
        IList<ProductionPlanResponse> exeptedResult = [];
        var result = _ProductionPlanService.ComputePowerPLanProduction(null);
        Assert.AreEqual(result, exeptedResult);
    }


    [Test]
    public void ComputePowerPLanProductionWIthPayload3ShloudReturnResponse3()
    {

        var payloadJson = """
                        {
              "load": 910,
              "fuels":
              {
                "gas(euro/MWh)": 13.4,
                "kerosine(euro/MWh)": 50.8,
                "co2(euro/ton)": 20,
                "wind(%)": 60
              },
              "powerplants": [
                {
                  "name": "gasfiredbig1",
                  "type": "gasfired",
                  "efficiency": 0.53,
                  "pmin": 100,
                  "pmax": 460
                },
                {
                  "name": "gasfiredbig2",
                  "type": "gasfired",
                  "efficiency": 0.53,
                  "pmin": 100,
                  "pmax": 460
                },
                {
                  "name": "gasfiredsomewhatsmaller",
                  "type": "gasfired",
                  "efficiency": 0.37,
                  "pmin": 40,
                  "pmax": 210
                },
                {
                  "name": "tj1",
                  "type": "turbojet",
                  "efficiency": 0.3,
                  "pmin": 0,
                  "pmax": 16
                },
                {
                  "name": "windpark1",
                  "type": "windturbine",
                  "efficiency": 1,
                  "pmin": 0,
                  "pmax": 150
                },
                {
                  "name": "windpark2",
                  "type": "windturbine",
                  "efficiency": 1,
                  "pmin": 0,
                  "pmax": 36
                }
              ]
            }
            
            """;

        var responseJson = """
            [
          {
              "name": "windpark1",
              "p": 90.0
          },
          {
              "name": "windpark2",
              "p": 21.6
          },
          {
              "name": "gasfiredbig1",
              "p": 460.0
          },
          {
              "name": "gasfiredbig2",
              "p": 338.4
          },
          {
              "name": "gasfiredsomewhatsmaller",
              "p": 0.0
          },
          {
              "name": "tj1",
              "p": 0.0
          }
      ]
      """;

        var payload = JsonSerializer.Deserialize<ProductionPlanRequest>(payloadJson);
        var exeptedResult = JsonSerializer.Deserialize<IList<ProductionPlanResponse>>(responseJson);

        
        var result = _ProductionPlanService.ComputePowerPLanProduction(payload);
        Assert.AreEqual(result.Select(r => r.Name), exeptedResult.Select(r => r.Name));
        Assert.AreEqual(result.Select(r => r.Power), exeptedResult.Select(r => r.Power));
        Assert.AreEqual(result.Sum(r => r.Power), exeptedResult.Sum(r => r.Power));

    }




    [Test]   
    public void CalcCostForPowerPlantByFuelTypeWindturbineShouldReturnZero()
    {
        Powerplant powerplant = new Powerplant { Name = "windpark1", Type= "windturbine", Pmin = 0,Pmax = 0,Efficiency = 1};
        Fuels fuels = new Fuels { WindInPercent = 60, GasInEuroPerMwh = 13.4, KerosineInEuroPerMwh = 50.8, Co2InEuroPerTon = 20 };

        var coast = _ProductionPlanService.CalcCostForPowerPlantByFuelType(powerplant,fuels);
  
        Assert.AreEqual(coast, 0);

    }

    [Test]
    public void CalcCostForPowerPlantByFuelTypeGasturbineShouldReturnCorrectCost()
    {
        Powerplant powerplant = new Powerplant { Name = "Gas", Type = "gasfired", Pmin = 0, Pmax = 0, Efficiency = 0.5 };
        Fuels fuels = new Fuels { WindInPercent = 60, GasInEuroPerMwh = 13.4, KerosineInEuroPerMwh = 50.8, Co2InEuroPerTon = 20 };

        var coast = _ProductionPlanService.CalcCostForPowerPlantByFuelType(powerplant, fuels);

        Assert.AreEqual(coast, 26.8);

    }

}
