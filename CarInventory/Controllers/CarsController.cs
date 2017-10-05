using CarInventory.DataAccess;
using CarInventory.DataAccess.Infrastructure;
using CarInventory.DataAccess.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInventory.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CarsController));
        private IUnitOfWork UOW = null;
        private CarRepository CarRepo = null;

        public CarsController()
        {
            UOW = new UnitOfWork();
            CarRepo = new CarRepository(UOW);
        }

        // GET: Cars
        public ActionResult Index()
        { 
            return View();
        }

        // GET: Cars
        public PartialViewResult GetCar()
        {
            var cars = CarRepo.GetAll(); 
            return PartialView("_CarPartial", cars);
        } 
       
        //
        // POST: /Car/Create

        [HttpPost]
        public ActionResult Create(Car car)
        {
            car.UserId = Convert.ToInt64(Session["UserID"]);
            if (ModelState.IsValid)
            {
                car.UserId = Convert.ToInt64(Session["UserID"]);
                if (car.Id > 0)
                {
                    CarRepo.Update(car);
                }
                else {
                    CarRepo.Insert(car);
                } 
                var cars = CarRepo.GetAll();
                return PartialView("_CarPartial", cars);
            }

            return View(car);
        }

        public ActionResult Edit(int id = 0)
        {
            Car car = CarRepo.SingleOrDefault(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return Json( new {Id=car.Id ,Brand = car.Brand, Model = car.Model, Year = car.Year, Price = car.Price, New = car.New }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Delete(Car car)
        {
            if (ModelState.IsValid)
            {
                CarRepo.Delete(car);
                var cars = CarRepo.GetAll();
                return PartialView("_CarPartial", cars);
            }
            return View(car);
        }
    }
}