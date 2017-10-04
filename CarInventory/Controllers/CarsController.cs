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
            var cars = CarRepo.GetAll(); 
            ViewBag.CarsList = cars.ToList();
            return View();
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Car/Create

        [HttpPost]
        public ActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                car.UserId = 1;

                CarRepo.Insert(car);
                return RedirectToAction("Index");
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
            return View(car);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                CarRepo.Update(car);
                return RedirectToAction("Index");
            }
            return View(car);
        }
    }
}