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

        //Get Event Registration Counts
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
                StoredProcedure = "api_PerimeterStaff_GetRegistrationCounts"

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

        //Get Weekly Attendance Metrics
        public List<WeeklyMetrics> metrics = new List<WeeklyMetrics>();
        public class WeeklyMetrics
        {
            public int metricsID { get; set; }
            public DateTime startDate { get; set; }
            public int worshipAttendance { get; set; }
            public float downtownAttendance { get; set; }
            public int kidsQuestAttendance { get; set; }
            public decimal totalGiving { get; set; }
            public int totalDonors { get; set; }
            public int firstTimeDonors { get; set; }
            public int members { get; set; }
            public int inDiscipleship { get; set; }
            public int discipleshipLeaders { get; set; }
            public int inConnectGroup { get; set; }
            public int serving { get; set; }
            public int officers { get; set; }

        }
        public List<WeeklyMetrics> GetWeeklyMetrics()
        {
            var api = PowerApi.CreateWebApiClient(); //.CreateWebApiClient();        

            APIStoredProcRequest sp = new APIStoredProcRequest()
            {
                StoredProcedure = "api_PerimeterStaff_WeeklyMetrics"

            };
            dynamic response = api.ExecuteStoredProcedure(sp);

            foreach (var single in response[0])
            {
                WeeklyMetrics metric = new WeeklyMetrics();
                metric.metricsID = single.Warehouse_KPI_ID;
                metric.startDate = single.Start_Date;
                metric.worshipAttendance = single.Worship_Attendance_This_Week;
                metric.downtownAttendance = single.Downtown_Attendance;

                metrics.Add(metric);
            }

            return metrics;
        }

        public ActionResult Weekly()
        {
            GetWeeklyMetrics();
            return View(metrics);
        }

    }
}