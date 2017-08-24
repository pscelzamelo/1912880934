using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zx.Controllers
{
    public class PdvController : Controller
    {        
        private JArray GetPdvs()
        {
            var rawObj = JObject.Parse(System.IO.File.ReadAllText(Server.MapPath(@"~\App_Data\pdvs.json")));
            return (JArray)rawObj["pdvs"]; 
        }

        public ActionResult Get(int id)
        {
            var pdvs = GetPdvs();
            var pdv = (JObject)pdvs.FirstOrDefault(x => (int)x["id"] == id);
            return Content(pdv.ToString(), "application/json");
        }


    }
}