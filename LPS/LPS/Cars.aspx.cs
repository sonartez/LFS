using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LPS
{
    public partial class Cars : System.Web.UI.Page
    {
        public bool noResult = true;
        lafien_products_dbEntities db;
        public LPS.Product product = new Product();
        public List<ProductCategory> productCat;
        public List<LPS.Car> cars;

        public string key = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string key = Request.Params["key"];
            if (!string.IsNullOrEmpty(key))
            {

                db = new lafien_products_dbEntities();

                string[] tmps = key.Trim().Split('$');

                string[] tmpClass = tmps[0].TrimEnd(";".ToCharArray()).Split(';');
                List<int> carCats = new List<int>();
                for (int i = 0; i < tmpClass.Length; i++)
                {
                    int tmpValue = 0;
                    if (Int32.TryParse(tmpClass[i], out tmpValue))
                        carCats.Add(tmpValue);
                }


                string[] tmpY = tmps[1].TrimEnd(";".ToCharArray()).Split(';');
                List<int> carYears = new List<int>();
                for (int i = 0; i < tmpY.Length; i++)
                {
                    int tmpValue = 0;
                    if (Int32.TryParse(tmpY[i], out tmpValue))
                        carYears.Add(tmpValue);
                }


                string[] tmpEngine = tmps[2].TrimEnd(";".ToCharArray()).Split(';');



                string[] tmpBody = tmps[3].TrimEnd(";".ToCharArray()).Split(';');



                cars = db.Cars.Where(x => carCats.Contains(x.CarCatID) && ((x.FromDate.Year <= carYears.Max() && x.ToDate == null) || (x.ToDate.Year >= carYears.Min() && x.FromDate.Year <= carYears.Max()))).ToList();

                if (tmpEngine.Length > 0)
                    if (!string.IsNullOrEmpty(tmpEngine[0]))
                    {
                        cars = cars.Where(x => tmpEngine.Contains(x.Engine)).ToList();
                    }

                if (tmpBody.Length > 0)
                    if (!string.IsNullOrEmpty(tmpBody[0]))
                    {
                        cars = cars.Where(x => tmpBody.Contains(x.Body)).ToList();
                    }



                if (cars.Count == 0)
                {
                    noResult = true;
                }
                else
                {
                    noResult = false;
                }


                if (noResult == false)
                {
                    productCat = db.ProductCategories.Where(x => x.Active == true).ToList();
                }

            }

        }

        public string FormatSpec(string spec)
        {
            string res = string.Empty;

            spec = spec.Trim();

            if (spec.EndsWith(","))
                spec = spec.Remove(spec.Length - 1);

            res = "<tr><td>" + spec.Replace(":", "</td><td>").Replace(",", "</td></tr><tr><td>") + "</td></tr>";

            return res;
        }

        public string GetYears(Car car)
        {
            return car.FromDate.ToString("MM/yy") + "~" + car.ToDate.ToString("MM/yy");
        }
        public string GetFullCatLocation(CarCategory cat)
        {
            if (cat.CatLevel == 1)
                return db.CarCategories.FirstOrDefault(x => x.CatID == cat.CatParentID).CatName + " » " + cat.CatName;
            else
                return cat.CatName;


        }
    }
}