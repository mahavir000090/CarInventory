using CarInventory.DataAccess;
using CarInventory.DataAccess.Infrastructure;
using CarInventory.DataAccess.Infrastructure.Contract;
using CarInventory.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarInventory.Controllers
{
    public class AccountController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(AccountController)); 
        private IUnitOfWork UOW = null;
        private UserRepository UserRepo = null;

        public AccountController()
        {
            UOW = new UnitOfWork();
            UserRepo = new UserRepository(UOW);
        }

        // GET: Register 
        public ActionResult Register()
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

        //
        // POST: /Default1/Create

        [HttpPost] 
        public ActionResult Register(RegisterViewModel register)
        { 
            User objuser = new User();
            objuser.FirstName = register.FirstName;
            objuser.LastName = register.LastName;
            objuser.Password = register.Password;
            objuser.Email = register.Email;
            objuser.CreatedDate = DateTime.Now;
            objuser.ModifiedDate = DateTime.Now; 

            if (ModelState.IsValid)
            {
                UserRepo.Insert(objuser);
                return RedirectToAction("Login");
            } 
            return View(objuser);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult Login(LoginViewModel login)
        {
            //check username and password over here
            var user = string.Empty;
            if (user!=null)
            {
                Session["UserName"]= "";
                FormsAuthentication.SetAuthCookie(user,true); 
            } 
            return View();
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }

}