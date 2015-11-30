using ASTE.Modules.APIDiscovery.db.Context;
using ASTE.Modules.APIDiscovery.db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASTE.Modules.APIDiscovery.Controllers
{
    /// <summary>
    /// Main page of the API DIscovery
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Index View of the API Discovery
        /// </summary>
        /// <returns>Index View</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}