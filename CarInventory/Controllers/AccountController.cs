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
                TempData["Success"] = "Registration has been completed successfully.";
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
            var user = UserRepo.GetAll().Where(x=>x.Email.ToLower()==login.Email.ToLower() & x.Password.ToLower() == login.Password.ToLower()).FirstOrDefault();
            if (user!=null)
            {
                Session["UserID"]= user.Id;
                FormsAuthentication.SetAuthCookie(user.Email, false);
                TempData["Success"] = "User has been logged in successfully.!";
                return RedirectToAction("Index","Cars");
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

        // For Check Email Exist
        [HttpGet]
        public ActionResult CheckExistingEmail(string Email)
        {
            bool ifEmailExist = false;
            try
            {
                var user = UserRepo.GetAll().Where(x => x.Email.ToLower() == Email.ToLower()).FirstOrDefault();
                ifEmailExist = user != null ? true : false;
                return Json(!ifEmailExist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }

}