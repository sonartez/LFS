using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LPS.Controllers
{
    public class ProductController : Controller
    {
        private lafien_products_dbEntities db = new lafien_products_dbEntities();

        //
        // GET: /Product/

        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductCategory);
            return View(products.ToList());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(string id = null)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.ProductCatID = new SelectList(db.ProductCategories, "ProductCatID", "ProductCatName");
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCatID = new SelectList(db.ProductCategories, "ProductCatID", "ProductCatName", product.ProductCatID);
            return View(product);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(string id = null)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCatID = new SelectList(db.ProductCategories, "ProductCatID", "ProductCatName", product.ProductCatID);
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCatID = new SelectList(db.ProductCategories, "ProductCatID", "ProductCatName", product.ProductCatID);
            return View(product);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(string id = null)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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