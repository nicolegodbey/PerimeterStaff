using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThinkMinistry.RESTAPIWrapper;

namespace PerimeterStaff.Controllers
{
    public class PrayerController : Controller
    {
        public List<Prayer> Prayers = new List<Prayer>();
        public class Prayer
        {
            public int feedbackEntryID { get; set; }
            public string displayName { get; set; }
            public string emailAddress { get; set; }
            public string mobilePhone { get; set; }
            public string homePhone { get; set; }
            public string membershipStatus { get; set; }
            public string parishName { get; set; }
            public string shepherd { get; set; }
            public string description { get; set; }

        }


        public List<Prayer> GetPrayers()
        {
            // Local vars
            var api = PowerApi.CreateWebApiClient(); //.CreateWebApiClient();        

            APIStoredProcRequest sp = new APIStoredProcRequest()
            {
                StoredProcedure = "api_12Stone_Custom_GetCurrentPrayers"

            };
            dynamic response2 = api.ExecuteStoredProcedure(sp);

            // Get contacts
            foreach (var single in response2[0])
            {
                Prayer Prayer = new Prayer();
                Prayer.feedbackEntryID = single.feedbackEntryID;
                Prayer.displayName = single.displayName;
                Prayer.emailAddress = single.emailAddress;
                Prayer.mobilePhone = single.mobilePhone;
                Prayer.homePhone = single.homePhone;
                Prayer.membershipStatus = single.membershipStatus;
                Prayer.parishName = single.parishName;
                Prayer.shepherd = single.shepherd;
                Prayer.description = single.description;
                Prayers.Add(Prayer);
            }

            // Return value
            return Prayers;
        }

        // GET: Home
        public ActionResult Weekly()
        {
            GetPrayers();
            return View(Prayers);
        }
    }
}