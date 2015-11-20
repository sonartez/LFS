using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LPS.Core;

namespace LPS.Pages
{
    public partial class ProductCategoryEdit : System.Web.UI.Page
    {
        public Common comClass = new Common();
         private lafien_products_dbEntities db = new lafien_products_dbEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            Account logged = (Account)Session["LoggedAccount"];
            if (logged.AccountType != Common.TypeAdmin)
                Response.Redirect("~/CMS/");
            if (!IsPostBack) {
                LoadData();
                BackUrl.Value = Request.UrlReferrer.ToString();
            }
        }
        private void LoadData()
        {
            if (!comClass.IsNumeric(Request.QueryString.Get("Id")))
            {
                Response.Redirect(BackUrl.Value);
            }
            else
            {
                int Id = Int32.Parse(Request.QueryString.Get("Id"));
                LPS.ProductCategory cat = new LPS.ProductCategory();
                cat = db.ProductCategories.FirstOrDefault(x => x.ProductCatID == Id);

                CatID.Value = cat.ProductCatID.ToString();
                txtPCName.Value = cat.ProductCatName;
                chkPCActive.Checked = cat.Active;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = Int32.Parse(CatID.Value);
                LPS.ProductCategory productcategory = new LPS.ProductCategory();
                productcategory = db.ProductCategories.FirstOrDefault(x => x.ProductCatID == Id);
                productcategory.Active = chkPCActive.Checked;
                productcategory.ProductCatName = txtPCName.Value.Trim();
                
                db.SaveChanges();
                LoadData();
            }
            catch (Exception ex)
            {


            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(BackUrl.Value);
        }
    }
}