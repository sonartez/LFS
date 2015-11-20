using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LPS.Controllers
{
    public class CarItemController : Controller
    {
        private lafien_products_dbEntities db = new lafien_products_dbEntities();

        //
        // GET: /CarItem/

        public ActionResult Index()
        {
            var caritems = db.CarItems.Include(c => c.Car).Include(c => c.Product);
            return View(caritems.ToList());
        }

        //
        // GET: /CarItem/Details/5

        public ActionResult Details(int id = 0)
        {
            CarItem caritem = db.CarItems.Find(id);
            if (caritem == null)
            {
                return HttpNotFound();
            }
            return View(caritem);
        }

        //
        // GET: /CarItem/Create

        public ActionResult Create()
        {
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Model");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Image");
            return View();
        }

        //
        // POST: /CarItem/Create

        [HttpPost]
        public ActionResult Create(CarItem caritem)
        {
            if (ModelState.IsValid)
            {
                db.CarItems.Add(caritem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Model", caritem.CarID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Image", caritem.ProductID);
            return View(caritem);
        }

        //
        // GET: /CarItem/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CarItem caritem = db.CarItems.Find(id);
            if (caritem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Model", caritem.CarID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Image", caritem.ProductID);
            return View(caritem);
        }

        //
        // POST: /CarItem/Edit/5

        [HttpPost]
        public ActionResult Edit(CarItem caritem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caritem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Model", caritem.CarID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Image", caritem.ProductID);
            return View(caritem);
        }

        //
        // GET: /CarItem/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CarItem caritem = db.CarItems.Find(id);
            if (caritem == null)
            {
                return HttpNotFound();
            }
            return View(caritem);
        }

        //
        // POST: /CarItem/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CarItem caritem = db.CarItems.Find(id);
            db.CarItems.Remove(caritem);
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