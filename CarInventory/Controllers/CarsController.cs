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
    public class CarsController : Controller
    {

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
            return View(cars.ToList());
        }
    }
}