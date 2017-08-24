using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Zx.Controllers
{
    public class PdvController : ApiController
    {
        private JArray GetPdvs()
        {
            const string pdvsCacheKey = "pdvs";
            var cached = HttpRuntime.Cache.Get(pdvsCacheKey);
            var result = new JArray();
            if (cached != null)
            {
                return (JArray)cached;
            }
            else
            {
                var rawObj = JObject.Parse(System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(@"~\App_Data\pdvs.json")));
                result = (JArray)rawObj["pdvs"];
                HttpRuntime.Cache.Insert(pdvsCacheKey, result);
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
        public void Post(JObject pdv)
        {
            var errors = new List<string>();
            
            //Fetch full db for validating unique document
            var pdvs = GetPdvs();

            //Validate mandatory fields
            if ((int?)pdv["id"] < 1) errors.Add("Invalid Id");
            if (string.IsNullOrEmpty((string)pdv["tradingName"])) errors.Add("Invalid Trading Name");
            if (string.IsNullOrEmpty((string)pdv["ownerName"])) errors.Add("Invalid Owner Name");
            if (string.IsNullOrEmpty((string)pdv["document"])) errors.Add("Invalid Document");
            //coverageArea (...)
            //address (...)
            if ((int?)pdv["deliveryCapacity"] == null) errors.Add("Invalid Capacity");

            //Validate existing CNPJ
            if (pdvs.Any(x => (string)x["document"] == (string)pdv["document"])) errors.Add("Document must be unique within database");
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
