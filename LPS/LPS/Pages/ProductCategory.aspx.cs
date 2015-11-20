using LPS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LPS.Pages
{
    public partial class ProductCategory : System.Web.UI.Page
    {
        private lafien_products_dbEntities db = new lafien_products_dbEntities();
        public List<LPS.ProductCategory> dataList = new List<LPS.ProductCategory>();
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                LoadData();
            }

        }

        private void LoadData()
        {
            dataList = db.ProductCategories.ToList();
            gvListItem.DataSource = dataList;
            gvListItem.DataBind();
            MakeGridViewPrinterFriendly(gvListItem);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                LPS.ProductCategory productcategory = new LPS.ProductCategory();
                productcategory.Active = chkPCActive.Checked;
                productcategory.ProductCatName = txtPCName.Value.Trim();
                db.ProductCategories.Add(productcategory);
                db.SaveChanges();
                LoadData();
            }
            catch (Exception ex)
            {


            }
        }
        private void MakeGridViewPrinterFriendly(GridView gridView)
        {
            if (gridView.Rows.Count > 0)
            {
                gridView.UseAccessibleHeader = true;
                gridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvListItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           

           

            int id = Int32.Parse( e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            Label lblTitle = (Label)row.Cells[1].FindControl("lblName");



            if (e.CommandName.ToLower() == "deleteitem")
            {
               
                LPS.ProductCategory productcategory = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(productcategory);
                db.SaveChanges();
                LoadData();

              
            }
        }
    }
}