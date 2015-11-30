using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASTE.Demos.Mielenterveysseura.Controllers
{
    /// <summary>
    /// Default controller for demo
    /// </summary>
    [RouteArea("lapset-puheeksi")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Index page
        /// </summary>
        /// <returns>The main view</returns>
        [Route("Lokikirja")]
        public ActionResult Index()
        {
            return View();
        }
    }
}