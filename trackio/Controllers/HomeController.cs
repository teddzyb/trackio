using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace trackio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var userID = (string)this.RouteData.Values["id"];

            if (userID != null)
            {
                trackioDBEntities trackio = new trackioDBEntities();

                var user = Convert.ToInt16(userID);

                var entries = (from entry in trackio.DailyLogs
                               where entry.UserID == user
                               select entry).ToList();

                var activityLogs = (from log in trackio.ActivityLogs
                               where log.UserID == user
                               select log).ToList();

                var hydrationLogs = (from log in trackio.HydrationLogs
                                 where log.UserID == user
                                 select log).ToList();

                var sleepLogs = (from log in trackio.SleepLogs
                                 where log.UserID == user
                                 select log).ToList();

                var creds = (from acc in trackio.UserAccounts
                             where acc.UserID == user
                             select acc).ToList();

                ViewData["logEntries"] = entries;
                ViewData["activityLogs"] = activityLogs;
                ViewData["hydrationLogs"] = hydrationLogs;
                ViewData["sleepLogs"] = sleepLogs;
                ViewData["accDetails"] = creds;

                return View();
            } else
            {
                return RedirectToAction("Index", "Meta");
            }
        }

        // DAILY LOG --------------------
        public ActionResult DailyLog()
        {
            var userID = (string)this.RouteData.Values["id"];

            if (userID != null)
            {
                trackioDBEntities trackio = new trackioDBEntities();

                var user = Convert.ToInt16(userID);

                var entries = (from entry in trackio.DailyLogs
                             where entry.UserID == user
                             select entry).ToList();

                var creds = (from acc in trackio.UserAccounts
                             where acc.UserID == user
                             select acc).ToList();

                ViewData["logEntries"] = entries;
                ViewData["accDetails"] = creds;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Meta");
            }
        }

        public ActionResult AddEntry(FormCollection form)
        {
            var userID = Convert.ToInt16(Request.QueryString["id"]);

            trackioDBEntities trackio = new trackioDBEntities();
            DailyLog entry = new DailyLog();

            entry.UserID = userID;
            entry.Date = DateTime.Now;
            entry.Title = form["title"];
            entry.Mood = form["mood"];
            entry.Description = form["description"];
            entry.Details = form["details"];

            trackio.DailyLogs.Add(entry);
            trackio.SaveChanges();

            return RedirectToAction("DailyLog", "Home", new { id = userID });
        }

        public ActionResult DeleteEntry()
        {
            var entryID = Convert.ToInt16(Request.QueryString["id"]);

            trackioDBEntities trackio = new trackioDBEntities();

            var entry = (from log in trackio.DailyLogs
                         where log.EntryID == entryID
                         select log).FirstOrDefault();

            trackio.DailyLogs.Remove(entry);
            trackio.SaveChanges();

            return RedirectToAction("DailyLog", "Home", new { id = entry.UserID });
        }

        public ActionResult UpdateLog()
        {
            var userID = (string)this.RouteData.Values["id"];
            var entryID = Convert.ToInt16(Request.QueryString["entry"]);

            if (userID != null)
            {
                trackioDBEntities trackio = new trackioDBEntities();

                var user = Convert.ToInt16(userID);

                var creds = (from acc in trackio.UserAccounts
                             where acc.UserID == user
                             select acc).ToList();

                var entry = (from log in trackio.DailyLogs
                             where log.EntryID == entryID
                             select log).ToList();

                ViewData["accDetails"] = creds;
                ViewData["entryDeets"] = entry;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Meta");
            }
        }

        public ActionResult DoUpdateLog(FormCollection form)
        {
            var entryID = Convert.ToInt16(Request.QueryString["id"]);

            trackioDBEntities trackio = new trackioDBEntities();

            var entry = (from log in trackio.DailyLogs
                         where log.EntryID == entryID
                         select log).FirstOrDefault();

            entry.Title = form["title"];
            entry.Mood = form["mood"];
            entry.Description = form["description"];
            entry.Details = form["details"];

            trackio.SaveChanges();

            return RedirectToAction("DailyLog", "Home", new { id = entry.UserID });
        }

        // ACTIVITY TRACKER ------------
        public ActionResult ActivityTracker()
        {
            var userID = (string)this.RouteData.Values["id"];

            if (userID != null)
            {
                trackioDBEntities trackio = new trackioDBEntities();

                var user = Convert.ToInt16(userID);

                var logs = (from log in trackio.ActivityLogs
                            where log.UserID == user
                            select log).ToList();

                var creds = (from acc in trackio.UserAccounts
                             where acc.UserID == user
                             select acc).ToList();

                ViewData["activityLogs"] = logs;
                ViewData["accDetails"] = creds;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Meta");
            }
        }

        public ActionResult AddActivityLog(FormCollection form)
        {
            var userID = Convert.ToInt16(Request.QueryString["id"]);

            trackioDBEntities trackio = new trackioDBEntities();
            ActivityLog log = new ActivityLog();

            log.UserID = userID;
            log.Activity = form["activity"];
            log.Date = DateTime.Now;
            log.Time = form["time"];
            log.Description = form["description"];
            log.Details = form["details"];

            trackio.ActivityLogs.Add(log);
            trackio.SaveChanges();

            return RedirectToAction("ActivityTracker", "Home", new { id = userID });
        }

        public ActionResult DeleteActivityLog()
        {
            var logID = Convert.ToInt16(Request.QueryString["id"]);

            trackioDBEntities trackio = new trackioDBEntities();

            var target = (from log in trackio.ActivityLogs
                          where log.ActivityLogID == logID
                          select log).FirstOrDefault();

            trackio.ActivityLogs.Remove(target);
            trackio.SaveChanges();

            return RedirectToAction("ActivityTracker", "Home", new { id = target.UserID });
        }

        public ActionResult UpdateActivityLog()
        {
            var userID = (string)this.RouteData.Values["id"];
            var logID = Convert.ToInt16(Request.QueryString["log"]);

            if (userID != null)
            {
                trackioDBEntities trackio = new trackioDBEntities();

                var user = Convert.ToInt16(userID);

                var creds = (from acc in trackio.UserAccounts
                             where acc.UserID == user
                             select acc).ToList();

                var target = (from log in trackio.ActivityLogs
                              where log.ActivityLogID == logID
                              select log).ToList();

                ViewData["accDetails"] = creds;
                ViewData["activityLog"] = target;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Meta");
            }
        }

        public ActionResult DoUpdateActivityLog(FormCollection form)
        {
            var logID = Convert.ToInt16(Request.QueryString["log"]);

            trackioDBEntities trackio = new trackioDBEntities();

            var target = (from log in trackio.ActivityLogs
                          where log.ActivityLogID == logID
                          select log).FirstOrDefault();

            target.Activity = form["activity"];
            target.Time = form["time"];
            target.Description = form["description"];
            target.Details = form["details"];

            trackio.SaveChanges();

            return RedirectToAction("ActivityTracker", "Home", new { id = target.UserID });
        }

        // HYDRATION TRACKER -----------
        public ActionResult HydrationTracker()
        {
            var userID = (string)this.RouteData.Values["id"];

            if (userID != null)
            {
                trackioDBEntities trackio = new trackioDBEntities();

                var user = Convert.ToInt16(userID);

                var logs = (from log in trackio.HydrationLogs
                            where log.UserID == user
                            select log).ToList();

                var creds = (from acc in trackio.UserAccounts
                             where acc.UserID == user
                             select acc).ToList();

                ViewData["hydrationLogs"] = logs;
                ViewData["accDetails"] = creds;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Meta");
            }
        }

        public ActionResult AddHydrationLog(FormCollection form)
        {
            var userID = Convert.ToInt16(Request.QueryString["id"]);

            trackioDBEntities trackio = new trackioDBEntities();
            HydrationLog log = new HydrationLog();

            log.UserID = userID;
            log.Date = DateTime.Now;
            log.Description = form["description"];
            log.Glasses = Convert.ToInt16(form["glasses"]);

            trackio.HydrationLogs.Add(log);
            trackio.SaveChanges();

            return RedirectToAction("HydrationTracker", "Home", new { id = userID });
        }

        public ActionResult DeleteHydrationLog()
        {
            var logID = Convert.ToInt16(Request.QueryString["id"]);

            trackioDBEntities trackio = new trackioDBEntities();

            var target = (from log in trackio.HydrationLogs
                          where log.HydrationLogID == logID
                          select log).FirstOrDefault();

            trackio.HydrationLogs.Remove(target);
            trackio.SaveChanges();

            return RedirectToAction("HydrationTracker", "Home", new { id = target.UserID });
        }

        public ActionResult UpdateHydrationLog()
        {
            var userID = (string)this.RouteData.Values["id"];
            var logID = Convert.ToInt16(Request.QueryString["log"]);

            if (userID != null)
            {
                trackioDBEntities trackio = new trackioDBEntities();

                var user = Convert.ToInt16(userID);

                var creds = (from acc in trackio.UserAccounts
                             where acc.UserID == user
                             select acc).ToList();

                var target = (from log in trackio.HydrationLogs
                              where log.HydrationLogID == logID
                              select log).ToList();

                ViewData["accDetails"] = creds;
                ViewData["hydrationLog"] = target;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Meta");
            }
        }

        public ActionResult DoUpdateHydrationLog(FormCollection form)
        {
            var logID = Convert.ToInt16(Request.QueryString["log"]);

            trackioDBEntities trackio = new trackioDBEntities();

            var target = (from log in trackio.HydrationLogs
                          where log.HydrationLogID == logID
                          select log).FirstOrDefault();

            target.Description = form["description"];
            target.Glasses = Convert.ToInt16(form["glasses"]);

            trackio.SaveChanges();

            return RedirectToAction("HydrationTracker", "Home", new { id = target.UserID });
        }

        // SLEEP LOG -------------------
        public ActionResult SleepLog()
        {
            var userID = (string)this.RouteData.Values["id"];

            if (userID != null)
            {
                trackioDBEntities trackio = new trackioDBEntities();

                var user = Convert.ToInt16(userID);

                var logs = (from log in trackio.SleepLogs
                               where log.UserID == user
                               select log).ToList();

                var creds = (from acc in trackio.UserAccounts
                             where acc.UserID == user
                             select acc).ToList();

                ViewData["sleepLogs"] = logs;
                ViewData["accDetails"] = creds;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Meta");
            }
        }

        public ActionResult AddSleepLog(FormCollection form)
        {
            var userID = Convert.ToInt16(Request.QueryString["id"]);

            trackioDBEntities trackio = new trackioDBEntities();
            SleepLog log = new SleepLog();

            log.UserID = userID;
            log.Date = DateTime.Now;
            log.Description = form["description"];
            log.Hours = Convert.ToInt16(form["hours"]);
            log.Details = form["details"];

            trackio.SleepLogs.Add(log);
            trackio.SaveChanges();

            return RedirectToAction("SleepLog", "Home", new { id = userID });
        }

        public ActionResult DeleteSleepLog()
        {
            var logID = Convert.ToInt16(Request.QueryString["id"]);

            trackioDBEntities trackio = new trackioDBEntities();

            var target = (from log in trackio.SleepLogs
                          where log.SleepLogID == logID
                          select log).FirstOrDefault();

            trackio.SleepLogs.Remove(target);
            trackio.SaveChanges();

            return RedirectToAction("SleepLog", "Home", new { id = target.UserID });
        }

        public ActionResult UpdateSleepLog()
        {
            var userID = (string)this.RouteData.Values["id"];
            var logID = Convert.ToInt16(Request.QueryString["log"]);

            if (userID != null)
            {
                trackioDBEntities trackio = new trackioDBEntities();

                var user = Convert.ToInt16(userID);

                var creds = (from acc in trackio.UserAccounts
                             where acc.UserID == user
                             select acc).ToList();

                var target = (from log in trackio.SleepLogs
                              where log.SleepLogID == logID
                              select log).ToList();

                ViewData["accDetails"] = creds;
                ViewData["sleepLog"] = target;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Meta");
            }
        }

        public ActionResult DoUpdateSleepLog(FormCollection form)
        {
            var logID = Convert.ToInt16(Request.QueryString["id"]);

            trackioDBEntities trackio = new trackioDBEntities();

            var target = (from log in trackio.SleepLogs
                         where log.SleepLogID == logID
                         select log).FirstOrDefault();

            target.Description = form["description"];
            target.Hours = Convert.ToInt16(form["hours"]);
            target.Details = form["details"];

            trackio.SaveChanges();

            return RedirectToAction("SleepLog", "Home", new { id = target.UserID });
        }

        // PROFILE ---------------------
        public ActionResult UserProfile()
        {
            var userID = (string)this.RouteData.Values["id"];

            if (userID != null)
            {
                trackioDBEntities trackio = new trackioDBEntities();

                var user = Convert.ToInt16(userID);

                var creds = (from acc in trackio.UserAccounts
                             where acc.UserID == user
                             select acc).ToList();

                ViewData["accDetails"] = creds;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Meta");
            }
        }

        public ActionResult UpdateAccount(FormCollection newCreds)
        {
            var userID = Convert.ToInt16(Request.QueryString["id"]);

            trackioDBEntities trackio = new trackioDBEntities();

            var creds = (from acc in trackio.UserAccounts
                         where acc.UserID == userID
                         select acc).FirstOrDefault();

            creds.Username = newCreds["username"];
            creds.EmailAddress = newCreds["email"];
            creds.FirstName = newCreds["fname"];
            creds.LastName = newCreds["lname"];
            creds.Address = newCreds["address"];
            creds.City = newCreds["city"];
            creds.Country = newCreds["country"];
            creds.AboutMe = newCreds["aboutme"];

            if (newCreds["postal"] != "")
                creds.PostalCode = Convert.ToInt16(newCreds["postal"]);

            trackio.SaveChanges();

            return RedirectToAction("UserProfile", "Home", new { id = userID });
        }

    }
}