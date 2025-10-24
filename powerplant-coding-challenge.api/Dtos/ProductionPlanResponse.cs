using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace powerplantcodingchallenge.application.Dtos
{
    public class ProductionPlanResponse
    {
        [JsonPropertyName("name")]
        public string Name {  get; set; }
        [JsonPropertyName("p")]
        public double Power { get; set; }

    }
}
