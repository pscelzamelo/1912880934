using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zx.Controllers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web;

namespace Zx.Tests
{
    [TestClass]
    public class PdvApiTests
    {
        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            const string pdvsCacheKey = "pdvs";
            var rawObj = JObject.Parse(System.IO.File.ReadAllText($@"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}\App_Data\pdvs.json"));
            HttpRuntime.Cache.Insert(pdvsCacheKey, (JArray)rawObj["pdvs"]);
        }

        private JObject MockValidPdv()
        {
            return JObject.Parse(@"{
	            ""id"": 51, 
	            ""tradingName"": ""Adega da Cerveja - Teste"",
	            ""ownerName"": ""Pedro Melo"",
	            ""document"": ""1234567891011/0001"",
	            ""coverageArea"": { 
	              ""type"": ""MultiPolygon"", 
	              ""coordinates"": [
		            [[[30, 20], [45, 40], [10, 40], [30, 20]]], 
		            [[[15, 5], [40, 10], [10, 20], [5, 10], [15, 5]]]
	              ]
	            },
	            ""address"": { 
	              ""type"": ""Point"",
	              ""coordinates"": [-46.57421, -21.785741]
	            },
	            ""deliveryCapacity"": 5
            }");
        }

        [TestMethod]
        public void TestCreateInvalidId()
        {
            var controller = new PdvController();
            var obj = MockValidPdv();
            obj["id"] = null;

            // Act
            var response = controller.Post(obj);

            // Assert
            Assert.IsFalse(response.success);
            Assert.IsTrue(response.errors.Count > 0);
        }

        [TestMethod]
        public void TestCreateNotUniqueCNPJ()
        {
            var controller = new PdvController();
            var obj = MockValidPdv();
            obj["document"] = "02.453.716/000170";

            // Act
            var response = controller.Post(obj);

            // Assert
            Assert.IsFalse(response.success);
            Assert.IsTrue(response.errors.Count > 0);
        }

        [TestMethod]
        public void TestCreateEmptyAddress()
        {
            var controller = new PdvController();
            var obj = MockValidPdv();
            obj["address"] = null;

            // Act
            var response = controller.Post(obj);

            // Assert
            Assert.IsFalse(response.success);
            Assert.IsTrue(response.errors.Count > 0);
        }
    }
}
