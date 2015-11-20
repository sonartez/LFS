using LPS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LPS.Pages
{
    public partial class CarDelete : System.Web.UI.Page
    {
        private lafien_products_dbEntities db = new lafien_products_dbEntities();
        public Common comClass = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            string BackUrl = string.Empty;
            if (Request.UrlReferrer != null)
            {
                BackUrl = Request.UrlReferrer.ToString();
            }
            else
            {
                BackUrl = "Cars.aspx";
            }
            try
            {
                if (!comClass.IsNumeric(Request.QueryString.Get("Id")))
                {
                    Response.Redirect(BackUrl);
                }
                int id = Int32.Parse(Request.QueryString.Get("Id"));

                LPS.Car obj = db.Cars.Find(id);

                if (obj != null)
                {
                    // remove refs
                    var carItems = db.CarItems.Where(x => x.CarID == obj.CarID).ToList();
                    foreach (var item in carItems)
                    {
                        db.CarItems.Remove(item);
                    }
                    db.SaveChanges();
                    db.Cars.Remove(obj);
                    db.SaveChanges();
                }
                Response.Redirect(BackUrl);
            }
            catch
            {
                Response.Redirect(BackUrl);
            }
        }
    }
}