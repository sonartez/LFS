using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LPS
{
    public partial class LPSMaster : System.Web.UI.MasterPage
    {
        public List<CarCategory> carCats;
        lafien_products_dbEntities db;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new lafien_products_dbEntities();

            carCats = db.CarCategories.Where(x => x.CatLevel == 0).OrderBy(x => x.CatName).ToList();
            ddlBrand.DataSource = carCats;
            ddlBrand.DataTextField = "CatName";
            ddlBrand.DataValueField = "CatID";
            ddlBrand.DataBind();
            

        }
    }
}