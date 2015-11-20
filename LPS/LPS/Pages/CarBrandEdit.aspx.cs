using LPS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LPS.Pages
{
    public partial class CarBrandEdit : System.Web.UI.Page
    {
        private lafien_products_dbEntities db = new lafien_products_dbEntities();
        public List<LPS.CarCategory> dataList = new List<LPS.CarCategory>();
        public List<LPS.CarCategory> parentList = new List<LPS.CarCategory>();
        public List<LPS.CarCategory> ddlDataList = new List<LPS.CarCategory>();
        public Common comClass = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                if (Request.UrlReferrer != null)
                {
                    BackUrl.Value = Request.UrlReferrer.ToString();
                }
                else {
                    BackUrl.Value = "CarBrands.aspx";
                }
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
                try
                {

              
                int Id = Int32.Parse(Request.QueryString.Get("Id"));

                dataList = db.CarCategories.Where(x => x.CatLevel == 0 && x.CatID!=Id).OrderBy(x => x.CatName).ToList();               
                ddlDataList.Add(new LPS.CarCategory() { CatID = 0, CatLevel = 0, CatName = "Root" });
                ddlDataList.AddRange(dataList);
                ddlParentCat.DataSource = ddlDataList;
                ddlParentCat.DataTextField = "CatName";
                ddlParentCat.DataValueField = "CatId";
                ddlParentCat.DataBind();

                LPS.CarCategory cat = new LPS.CarCategory();
                cat = db.CarCategories.FirstOrDefault(x => x.CatID == Id);

                CatID.Value = cat.CatID.ToString();
                txtPCName.Value = cat.CatName;
                ddlParentCat.SelectedValue = cat.CatParentID.ToString();

                lblCatName.Text = cat.CatName;

                }
                catch (Exception ex)
                {

                    Response.Redirect(BackUrl.Value);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = Int32.Parse(CatID.Value);
                LPS.CarCategory cat = new LPS.CarCategory();
                cat = db.CarCategories.FirstOrDefault(x => x.CatID == Id);
                cat.CatParentID = Convert.ToInt32(ddlParentCat.SelectedValue);
                cat.CatName = txtPCName.Value.Trim();
                if (cat.CatParentID == 0)
                {
                    cat.CatLevel = 0;
                }
                else
                {
                    cat.CatLevel = 1;
                }
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