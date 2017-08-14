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
            public int FeedbackEntryID { get; set; }
            public string DisplayName { get; set; }
            public string EmailAddress { get; set; }
            public string MobilePhone { get; set; }
            public string HomePhone { get; set; }
            public string MembershipStatus { get; set; }
            public string ParishName { get; set; }
            public string Shepherd { get; set; }
            public string Description { get; set; }

        }


        public List<Prayer> GetPrayers()
        {
            // Local vars
            var api = PowerApi.CreateWebApiClient(); //.CreateWebApiClient();        

            APIStoredProcRequest sp = new APIStoredProcRequest()
            {
                StoredProcedure = "api_PerimeterStaff_GetCurrentPrayers"

            };
            dynamic response = api.ExecuteStoredProcedure(sp);

            // Get contacts
            foreach (var single in response[0])
            {
                Prayer Prayer = new Prayer();
                Prayer.FeedbackEntryID = single.feedbackEntryID;
                Prayer.DisplayName = single.displayName;
                Prayer.EmailAddress = single.emailAddress;
                Prayer.MobilePhone = single.mobilePhone;
                Prayer.HomePhone = single.homePhone;
                Prayer.MembershipStatus = single.membershipStatus;
                Prayer.ParishName = single.parishName;
                Prayer.Shepherd = single.shepherd;
                Prayer.Description = single.description;
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