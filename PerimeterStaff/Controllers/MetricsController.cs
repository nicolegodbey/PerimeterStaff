using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThinkMinistry.RESTAPIWrapper;

namespace PerimeterStaff.Controllers
{
    public class MetricsController : Controller
    {
        // GET: Metrics
        public ActionResult Index()
        {
            return View();
        }
    }
}