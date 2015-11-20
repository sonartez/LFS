using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LPS.Controllers
{
    public class CarController : Controller
    {
        private lafien_products_dbEntities db = new lafien_products_dbEntities();

        //
        // GET: /Car/

        public ActionResult Index()
        {
            var cars = db.Cars.Include(c => c.CarCategory);
            return View(cars.ToList());
        }

        //
        // GET: /Car/Details/5

        public ActionResult Details(int id = 0)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        //
        // GET: /Car/Create

        public ActionResult Create()
        {
            ViewBag.CarCatID = new SelectList(db.CarCategories, "CatID", "CatName");
            return View();
        }

        //
        // POST: /Car/Create

        [HttpPost]
        public ActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarCatID = new SelectList(db.CarCategories, "CatID", "CatName", car.CarCatID);
            return View(car);
        }

        //
        // GET: /Car/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarCatID = new SelectList(db.CarCategories, "CatID", "CatName", car.CarCatID);
            return View(car);
        }

        //
        // POST: /Car/Edit/5

        [HttpPost]
        public ActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarCatID = new SelectList(db.CarCategories, "CatID", "CatName", car.CarCatID);
            return View(car);
        }

        //
        // GET: /Car/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        //
        // POST: /Car/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}