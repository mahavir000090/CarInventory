using CarInventory.DataAccess;
using CarInventory.DataAccess.Infrastructure;
using CarInventory.DataAccess.Infrastructure.Contract;
using CarInventory.DataAccess.Model;
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
            var cars = CarRepo.GetAll().Where(x=>x.UserId==Convert.ToInt64(Session["UserID"])); 
            return PartialView("_CarPartial", cars);
        } 
       
        //
        // POST: /Car/Create

        [HttpPost]
        public ActionResult Create(CarViewModel car)
        {
            if(car.Id == 0)
            {
                ModelState.Remove("Id");
            }

            Car objCar = new Car();
            objCar.Id = car.Id;
            objCar.UserId = car.UserId;
            objCar.Brand = car.Brand;
            objCar.Model = car.Model;
            objCar.Year = car.Year;
            objCar.Price = car.Price;
            objCar.New = car.New;

            objCar.UserId = Convert.ToInt64(Session["UserID"]);
            if (ModelState.IsValid)
            {
                car.UserId = Convert.ToInt64(Session["UserID"]);
                if (car.Id > 0)
                {
                    CarRepo.Update(objCar);
                }
                else {
                    CarRepo.Insert(objCar);
                } 
                var cars = CarRepo.GetAll().Where(x => x.UserId == Convert.ToInt64(Session["UserID"]));;
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
                var cars = CarRepo.GetAll().Where(x => x.UserId == Convert.ToInt64(Session["UserID"]));
                return PartialView("_CarPartial", cars);
            }
            return View(car);
        }

        // GET: Car Search
        public PartialViewResult GetSearchCar(string SearchString)
        {
            var cars = CarRepo.GetAll().Where(x => x.UserId == Convert.ToInt64(Session["UserID"]));
            if (!String.IsNullOrEmpty(SearchString))
            {
                cars = cars.Where(x => x.Brand.ToUpper().Contains(SearchString.ToUpper()) || x.Model.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }

            return PartialView("_CarPartial", cars);
        }
    }
}