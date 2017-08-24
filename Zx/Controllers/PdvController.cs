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
        public void Post([FromBody]string value)
        {
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
