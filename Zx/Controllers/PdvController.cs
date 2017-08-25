using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Zx.Models;

namespace Zx.Controllers
{
    public class PdvController : ApiController
    {
        const string _pdvsCacheKey = "pdvs";

        private JArray GetPdvs()
        {
            var cached = HttpRuntime.Cache.Get(_pdvsCacheKey);
            var result = new JArray();
            if (cached != null)
            {
                return (JArray)cached;
            }
            else
            {
                var rawObj = JObject.Parse(System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(@"~\App_Data\pdvs.json")));
                result = (JArray)rawObj["pdvs"];
                HttpRuntime.Cache.Insert(_pdvsCacheKey, result);
                return result;
            }
        }

        // GET: api/Pdv
        public JArray Get()
        {
            return GetPdvs();
        }

        // GET: api/Pdv/5
        public JObject Get(int id)
        {
            var pdvs = GetPdvs();
            return (JObject)pdvs.FirstOrDefault(x => (int)x["id"] == id);
        }

        // POST: api/Pdv
        public CommandResponse Post(JObject pdv)
        {
            var errors = new List<string>();
            
            //Validate mandatory fields
            if ((int?)pdv["id"] == null || (int?)pdv["id"] < 1) errors.Add("Invalid Id");
            if (string.IsNullOrEmpty((string)pdv["tradingName"])) errors.Add("Invalid Trading Name");
            if (string.IsNullOrEmpty((string)pdv["ownerName"])) errors.Add("Invalid Owner Name");
            if (string.IsNullOrEmpty((string)pdv["document"])) errors.Add("Invalid Document");
            if ((int?)pdv["deliveryCapacity"] == null) errors.Add("Invalid Capacity");
            var coverageArea = JsonConvert.DeserializeObject<MultiPolygon>(pdv["coverageArea"]?.ToString());
            if (coverageArea == null) errors.Add("Invalid Coverage Area");
            var address = JsonConvert.DeserializeObject<Point>(pdv["address"]?.ToString());
            if (address == null) errors.Add("Invalid Address");

            //Validate existing CNPJ
            var pdvs = GetPdvs();
            if (pdvs.Any(x => (string)x["document"] == (string)pdv["document"])) errors.Add("Document must be unique within database");

            //Return errors or persist
            if (errors.Count > 0)
            {
                return new CommandResponse(false, errors, pdv);
            }
            else
            {
                pdvs.Add(pdv);
                HttpRuntime.Cache.Insert(_pdvsCacheKey, pdvs);
                return new CommandResponse(true, pdv);
            }
        }

        // PUT: api/Pdv/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Pdv/5
        public void Delete(int id)
        {
        }
    }
}
