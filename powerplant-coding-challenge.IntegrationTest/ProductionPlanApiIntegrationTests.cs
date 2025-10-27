using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Net;
using System.Security.Policy;
using System.Text;

namespace powerplantcodingchallenge.IntegrationTest
{
    [TestFixture]

    public class ProductionPlanApiIntegrationTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void OneTimeSetup() => _factory = new WebApplicationFactory<Program>();

        [SetUp]
        public void Setup() => _client = _factory.CreateClient();



    




        [Test]
        public async Task Should_ReturnOk_When_PayloadIsCorrect()
        {
            // Arrange
            var client = _factory.CreateClient();

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
            var content = new StringContent(payloadJson, Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await client.PostAsync("/productionplan", content);
            string responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Should_ReturnBadRequest_When_PayloadIsEmpty()
        {
            var content = new StringContent("", Encoding.UTF8, "application/json");


            // Act
            HttpResponseMessage response = await _client.PostAsync("/productionplan", content);
           

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }
    }
}