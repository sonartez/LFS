using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LPS.Controllers
{
    public class CarCategoryController : Controller
    {
        private lafien_products_dbEntities db = new lafien_products_dbEntities();
        private List<CascadingDropDownNameValue> ListCascading;

        //
        // GET: /CarCategory/

        public ActionResult Index()
        {
            return View(db.CarCategories.ToList());
        }

        //
        // GET: /CarCategory/Details/5

        public ActionResult Details(int id = 0)
        {
            CarCategory carcategory = db.CarCategories.Find(id);
            if (carcategory == null)
            {
                return HttpNotFound();
            }
            return View(carcategory);
        }

        //
        // GET: /CarCategory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CarCategory/Create

        [HttpPost]
        public ActionResult Create(CarCategory carcategory)
        {
            if (ModelState.IsValid)
            {
                db.CarCategories.Add(carcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carcategory);
        }

        public JsonResult GetCarBrandByParent(int id = -1)
        {
            try
            {


                ListCascading = new List<CascadingDropDownNameValue>();

                var catList = db.CarCategories.Where(x => x.CatParentID == id).OrderBy(x => x.CatName).ToList();

                foreach (var itm in catList)
                {

                    string itemIdString = itm.CatID.ToString();
                    string nameString = itm.CatName.ToString();
                    ListCascading.Add(new CascadingDropDownNameValue(nameString, itemIdString));
                }

                return Json(ListCascading.ToArray(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult GetCarYears(string id)
        {
            try
            {
                ListCascading = new List<CascadingDropDownNameValue>();
                if (string.IsNullOrEmpty(id))
                    return Json(ListCascading.ToArray(), JsonRequestBehavior.AllowGet);
                else
                {
                    string[] tmps = id.TrimEnd(";".ToCharArray()).Split(';');
                    List<int> carCats = new List<int>();
                    for (int i = 0; i < tmps.Length; i++)
                    {
                        carCats.Add(Convert.ToInt32(tmps[i]));
                    }


                    ListCascading = new List<CascadingDropDownNameValue>();
                    var cars = db.Cars.Where(x => carCats.Contains(x.CarCatID)).ToList();
                    if (cars.Count > 0)
                    {
                        var fromYear = cars.Select(x => x.FromDate).Min().Year;
                        var toYear = cars.Where(x => x.ToDate != null && x.ToDate.Year < 9999).Select(x => x.ToDate).Max().Year;
                        for (int i = fromYear; i < toYear; i++)
                        {
                            string itemIdString = i.ToString();
                            string nameString = i.ToString();
                            ListCascading.Add(new CascadingDropDownNameValue(nameString, itemIdString));
                        }
                    }



                    return Json(ListCascading.ToArray(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //GetCarEngineAndBody
        public JsonResult GetCarEngineAndBody(string id)
        {
            try
            {
                ListCascading = new List<CascadingDropDownNameValue>();
                if (string.IsNullOrEmpty(id))
                    return Json(ListCascading.ToArray(), JsonRequestBehavior.AllowGet);
                else
                {
                    string[] tmps = id.Trim().Split('$');

                    string[] tmpClass = tmps[0].TrimEnd(";".ToCharArray()).Split(';');
                    List<int> carCats = new List<int>();
                    for (int i = 0; i < tmpClass.Length; i++)
                    {
                        carCats.Add(Convert.ToInt32(tmpClass[i]));
                    }


                    string[] tmpY = tmps[1].TrimEnd(";".ToCharArray()).Split(';');
                    List<int> carYears = new List<int>();
                    for (int i = 0; i < tmpY.Length; i++)
                    {
                        carYears.Add(Convert.ToInt32(tmpY[i]));
                    }

                    var cars = db.Cars.Where(x => carCats.Contains(x.CarCatID) && ((x.FromDate.Year <= carYears.Max() && x.ToDate == null) || (x.ToDate.Year >= carYears.Min() && x.FromDate.Year <= carYears.Max()))).ToList();

                    var bodyList = new List<CascadingDropDownNameValue>();
                    var engineList = new List<CascadingDropDownNameValue>();
                    if (cars.Count > 0)
                    {

                        foreach (var item in cars.Select(x => x.Body).Distinct())
                        {
                            string itemIdString = item;
                            string nameString = item;
                            bodyList.Add(new CascadingDropDownNameValue(nameString, itemIdString));
                        }



                        foreach (var item in cars.Select(x => x.Engine).Distinct())
                        {
                            string itemIdString = item;
                            string nameString = item;
                            engineList.Add(new CascadingDropDownNameValue(nameString, itemIdString));
                        }
                    }


                    return Json(new { body = bodyList.ToArray(), engine = engineList.ToArray() }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpPost]
        public JsonResult GetCarBrandByParent(string knownCategoryValues, string category, string contextKey)
        {
            try
            {


                ListCascading = new List<CascadingDropDownNameValue>();
                lafien_products_dbEntities db = new lafien_products_dbEntities();
                StringDictionary itmsIdList = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
                var itmId = Convert.ToInt32(itmsIdList["undefined"]);
                var catList = db.CarCategories.Where(x => x.CatParentID == itmId).OrderBy(x => x.CatName).ToList();

                foreach (var itm in catList)
                {

                    string itemIdString = itm.CatID.ToString();
                    string nameString = itm.CatName.ToString();
                    ListCascading.Add(new CascadingDropDownNameValue(nameString, itemIdString));
                }

                return Json(ListCascading.ToArray(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //
        // GET: /CarCategory/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CarCategory carcategory = db.CarCategories.Find(id);
            if (carcategory == null)
            {
                return HttpNotFound();
            }
            return View(carcategory);
        }

        //
        // POST: /CarCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(CarCategory carcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carcategory);
        }

        //
        // GET: /CarCategory/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CarCategory carcategory = db.CarCategories.Find(id);
            if (carcategory == null)
            {
                return HttpNotFound();
            }
            return View(carcategory);
        }

        //
        // POST: /CarCategory/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CarCategory carcategory = db.CarCategories.Find(id);
            db.CarCategories.Remove(carcategory);
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