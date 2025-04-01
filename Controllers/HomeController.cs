using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WorldTimeWebsite.Controllers
{
    public class HomeController : Controller
    {
        // Dictionary mapping capital cities to Windows TimeZone IDs.
        // (Extend this list as needed for more capitals.)
        private static readonly Dictionary<string, string> CapitalTimeZones = new Dictionary<string, string>
        {
            { "London", "GMT Standard Time" },
            { "Paris", "Romance Standard Time" },
            { "Tokyo", "Tokyo Standard Time" },
            { "Washington, D.C.", "Eastern Standard Time" },
            { "Ottawa", "Eastern Standard Time" },
            { "Canberra", "AUS Eastern Standard Time" },
            { "Berlin", "W. Europe Standard Time" },
            { "Moscow", "Russian Standard Time" }
        };

        // Home page with links to the different tools.
        public ActionResult Index()
        {
            return View();
        }

        // Stopwatch page (client-side functionality handled with JavaScript).
        public ActionResult Stopwatch()
        {
            return View();
        }

        // Clock page (client-side clock using JavaScript).
        public ActionResult Clock()
        {
            return View();
        }

        // World Clock page: if a valid capital is selected, convert UTC to local time.
        public ActionResult WorldClock(string city)
        {
            DateTime localTime = DateTime.MinValue;
            string message = string.Empty;

            if (!string.IsNullOrEmpty(city) && CapitalTimeZones.ContainsKey(city))
            {
                try
                {
                    TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(CapitalTimeZones[city]);
                    localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
                }
                catch (Exception ex)
                {
                    message = "Error retrieving time: " + ex.Message;
                }
            }
            else if (!string.IsNullOrEmpty(city))
            {
                message = "City not found. Please select a valid capital city.";
            }

            ViewBag.SelectedCity = city;
            ViewBag.LocalTime = localTime == DateTime.MinValue ? (object)null : localTime.ToString("F");
            ViewBag.Message = message;
            ViewBag.CapitalCities = new SelectList(CapitalTimeZones.Keys);
            return View();
        }
    }
}