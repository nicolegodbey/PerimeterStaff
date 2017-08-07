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

        public List<Registered> eventsums = new List<Registered>();
        public class Registered
        {
            public int eventID { get; set; }
            public string eventTitle { get; set; }
            public DateTime eventDate { get; set; }
            public int registered { get; set; }
            public int attended { get; set; }
        }

        public List<Registered> GetRegistered()
        {
            var api = PowerApi.CreateWebApiClient(); //.CreateWebApiClient();        

            APIStoredProcRequest sp = new APIStoredProcRequest()
            {
                StoredProcedure = "api_12Stone_Custom_GetRegistrationCounts"

            };
            dynamic response = api.ExecuteStoredProcedure(sp);

            foreach (var single in response[0])
            {
                Registered eventsum = new Registered();
                eventsum.eventID = single.eventID;
                eventsum.eventTitle = single.eventTitle;
                //eventsum.eventDate = single.eventDate;
                eventsum.registered = single.registered;
                eventsum.attended = single.attended;
                eventsums.Add(eventsum);
            }

            return eventsums;
        }

        public ActionResult EventCounts()
        {
            GetRegistered();
            return View(eventsums);
        }
    }
}