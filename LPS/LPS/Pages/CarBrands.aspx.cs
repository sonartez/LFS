using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LPS.Pages
{
    public partial class CarBrands1 : System.Web.UI.Page
    {
        private lafien_products_dbEntities db = new lafien_products_dbEntities();
        public List<LPS.CarCategory> dataList,viewList = new List<LPS.CarCategory>();
        public List<LPS.CarCategory> parentList = new List<LPS.CarCategory>();
        public List<LPS.CarCategory> ddlDataList = new List<LPS.CarCategory>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        private void LoadData()
        {
            dataList = db.CarCategories.ToList();

            parentList = dataList.Where(x => x.CatLevel == 0).OrderBy(x=>x.CatName).ToList();

            foreach (CarCategory item in parentList)
            {
                viewList.Add(item);
                viewList.AddRange(dataList.Where(x => x.CatParentID == item.CatID).OrderBy(x => x.CatName));
            }

            gvListItem.DataSource = viewList;
            gvListItem.DataBind();
            MakeGridViewPrinterFriendly(gvListItem);

            ddlDataList.Add(new LPS.CarCategory(){CatID=0,CatLevel=0,CatName="Root"});
            ddlDataList.AddRange(parentList);
            ddlParentCat.DataSource = ddlDataList;
            ddlParentCat.DataTextField = "CatName";
            ddlParentCat.DataValueField = "CatId";
            ddlParentCat.DataBind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                LPS.CarCategory cat = new LPS.CarCategory();
                cat.CatParentID = Convert.ToInt32(ddlParentCat.SelectedValue); 
                cat.CatName = txtPCName.Value.Trim();
                if (cat.CatParentID == 0)
                {
                    cat.CatLevel = 0;
                }
                else {
                    cat.CatLevel = 1;
                }
                db.CarCategories.Add(cat);
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
            int id = Int32.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            Label lblTitle = (Label)row.Cells[1].FindControl("lblName");

            if (e.CommandName.ToLower() == "deleteitem")
            {

                LPS.CarCategory obj = db.CarCategories.Find(id);
                db.CarCategories.Remove(obj);
                db.SaveChanges();
                LoadData();


            }
        }
    }
}