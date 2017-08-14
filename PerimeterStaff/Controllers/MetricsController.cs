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
            public int EventID { get; set; }
            public string EventTitle { get; set; }
            public DateTime EventDate { get; set; }
            public int RegisteredPeople { get; set; }
            public int Attended { get; set; }
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
                eventsum.EventID = single.eventID;
                eventsum.EventTitle = single.eventTitle;
                //eventsum.eventDate = single.eventDate;
                eventsum.RegisteredPeople = single.registered;
                eventsum.Attended = single.attended;
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
            public int MetricsID { get; set; }
            public DateTime StartDate { get; set; }
            public int WorshipAttendance { get; set; }
            public float DowntownAttendance { get; set; }
            public int KidsQuestAttendance { get; set; }
            public decimal TotalGiving { get; set; }
            public int TotalDonors { get; set; }
            public int FirstTimeDonors { get; set; }
            public int Members { get; set; }
            public int InDiscipleship { get; set; }
            public int DiscipleshipLeaders { get; set; }
            public int InConnectGroup { get; set; }
            public int Serving { get; set; }
            public int Officers { get; set; }

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
                metric.MetricsID = single.Warehouse_KPI_ID;
                metric.StartDate = single.Start_Date;
                metric.WorshipAttendance = single.Worship_Attendance_This_Week;
                metric.DowntownAttendance = single.Downtown_Attendance;

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