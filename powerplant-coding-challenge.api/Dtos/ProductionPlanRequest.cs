using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace powerplantcodingchallenge.application.Dtos
{
    public class ProductionPlanRequest
    {
        [JsonPropertyName("load")]
        public double Load { get; set; }
        [JsonPropertyName("fuels")]
        public Fuels Fuels { get; set; }
        [JsonPropertyName("powerplants")]
        public List<Powerplant> Powerplants { get; set; }

    }


    public class Fuels
    {
        [JsonPropertyName("gas(euro/MWh)")]
        public double GasInEuroPerMwh {  get; set; }
        [JsonPropertyName("kerosine(euro/MWh)")]
        public double KerosineInEuroPerMwh { get; set; }
        [JsonPropertyName("co2(euro/ton)")]
        public double Co2InEuroPerTon {  get; set; }
        [JsonPropertyName("wind(%)")]
        public double WindInPercent { get; set; }
    }

    public class Powerplant {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("efficiency")]
        public double Efficiency { get; set; }
        [JsonPropertyName("pmin")]
        public double Pmin { get; set; }
        [JsonPropertyName("pmax")]
        public double Pmax { get; set; }
    }

}
