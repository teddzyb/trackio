using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace trackio.Controllers
{
    public class MetaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult DoSignIn(FormCollection form)
        {
            trackioDBEntities accs = new trackioDBEntities();

            var email = form["email"];
            var pass = form["password"];

            var user = (from acc in accs.UserAccounts
                           where acc.EmailAddress == email && acc.Password == pass
                           select acc).FirstOrDefault();

            if (user != null)
            {
                return RedirectToAction("Index", "Home", new { id = user.UserID });
            } else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult DoRegister(FormCollection form)
        {
            trackioDBEntities accs = new trackioDBEntities();
            UserAccount newAcc = new UserAccount();

            newAcc.Username = form["username"];
            newAcc.EmailAddress = form["email"];
            newAcc.Password = form["password"];

            accs.UserAccounts.Add(newAcc);
            accs.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}