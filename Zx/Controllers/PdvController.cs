using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zx.Models;

namespace Zx.Controllers
{
    public class PdvController : Controller
    {        
        private JArray GetPdvs()
        {
            const string pdvsCacheKey = "pdvs";
            var cached = HttpRuntime.Cache.Get(pdvsCacheKey);
            var result = new JArray();
            if(cached != null)
            {
                return (JArray)cached;
            }
            else
            {
                var rawObj = JObject.Parse(System.IO.File.ReadAllText(Server.MapPath(@"~\App_Data\pdvs.json")));
                result = (JArray)rawObj["pdvs"];
                HttpRuntime.Cache.Insert(pdvsCacheKey, result);
                return result;
            } 
        }

        public ActionResult Get(int id)
        {
            var pdvs = GetPdvs();
            var pdv = (JObject)pdvs.FirstOrDefault(x => (int)x["id"] == id);
            return Content(pdv.ToString(), "application/json");
        }

        [HttpPost]
        public ActionResult Create()
        {
            var errors = new List<string>();

            //Read json from request
            var req = Request.InputStream;
            req.Seek(0, SeekOrigin.Begin);
            var jsonRequest = new StreamReader(req).ReadToEnd();
            var pdv = JObject.Parse(jsonRequest);

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

            return Content(pdv.ToString(), "application/json");
        }
    }
}