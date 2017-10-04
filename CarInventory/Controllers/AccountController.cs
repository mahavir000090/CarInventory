using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInventory.Controllers
{
    public class AccountController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(AccountController));

        public AccountController()
        {

        }

        // GET: Account
        public ActionResult Index()
        {
            try
            {

            }
            catch (Exception ex)
            {
                log.Info(ex.ToString());
            }
            return View();
        }

    }
}