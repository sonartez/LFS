using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LPS
{
    public partial class Product1 : System.Web.UI.Page
    {
        public bool noResult = true;
        lafien_products_dbEntities db;
        public LPS.Product product = new Product();
        public List<ProductCategory> productCat;
        public string mess = "";
        public string key = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string key = Request.Params["key"];
            if (!string.IsNullOrEmpty(key))
            {

                db = new lafien_products_dbEntities();

                product = db.Products.Where(x => x.ProductID.ToUpper() == key.ToUpper()).FirstOrDefault();

                if (product == null)
                {
                    var tmp = db.ProductOwnerReferences.Where(x => x.RefProductID.ToUpper() == key.ToUpper()).FirstOrDefault();
                    if (tmp != null)
                    {
                        product = tmp.Product;
                        mess = key + " reference to " + product.ProductName; 
                        noResult = false;
                    }
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