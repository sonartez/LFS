using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LPS.Pages
{
    public partial class Products : System.Web.UI.Page
    {
        private List<LPS.ProductOwnerReference> refList = new List<ProductOwnerReference>();
        private lafien_products_dbEntities db = new lafien_products_dbEntities();
        public List<LPS.Product> dataList, viewList = new List<LPS.Product>();
        public List<LPS.ProductCategory> ddlDataList = new List<LPS.ProductCategory>();
        public List<LPS.CarCategory> carBrandList = new List<CarCategory>();
        public string mess = "";
        public bool saved = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            uploadImageAjax.UploadFolder = @"~/Upload/Products/";
            //uploadImageAjax.
            if (!IsPostBack)
            {      
                LoadData();

                ViewState["reflist"] = refList;
            }
            else
            {
                refList = (List<ProductOwnerReference>)ViewState["reflist"];
            }
        }
        private void LoadData()
        {
            
            dataList = db.Products.ToList();
            //Product p = new Product();
            //p.ProductOwnerReferences
            gvListItem.DataSource = dataList;
            gvListItem.DataBind();
            MakeGridViewPrinterFriendly(gvListItem);

            ddlCat.DataSource = db.ProductCategories.OrderBy(x => x.ProductCatName).ToList();
            ddlCat.DataTextField = "ProductCatName";
            ddlCat.DataValueField = "ProductCatID";
            ddlCat.DataBind();

            carBrandList = db.CarCategories.Where(x => x.CatLevel == 0).OrderBy(x => x.CatName).ToList();
            ddlCarBrand.DataSource = carBrandList;
            ddlCarBrand.DataTextField = "CatName";
            ddlCarBrand.DataValueField = "CatID";
            ddlCarBrand.DataBind();

            ddlDataList.Add(new LPS.ProductCategory() { ProductCatID = 0, ProductCatName = "All Categories" });
            ddlDataList.AddRange(db.ProductCategories.OrderBy(x=>x.ProductCatName).ToList());
            ddlParentCat.DataSource = ddlDataList;
            ddlParentCat.DataTextField = "ProductCatName";
            ddlParentCat.DataValueField = "ProductCatID";
            ddlParentCat.DataBind();

            gvRef.DataSource = refList;
            gvRef.DataBind();

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
            try
            {

           
            string id = e.CommandArgument.ToString();
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            Label lblTitle = (Label)row.Cells[1].FindControl("lblName");

            if (e.CommandName.ToLower() == "deleteitem")
            {
                LPS.Product obj = db.Products.FirstOrDefault(x=>x.ProductID==id);
                if (obj != null)
                {
                    if (db.CarItems.Count(x => x.ProductID == obj.ProductID) > 0)
                    {
                        mess = "Can't not delete this product, because this is using in some cars ";
                        
                    }
                    else
                    {
                        var lispPO = db.ProductOwnerReferences.Where(x => x.ProductID == obj.ProductID).ToList();
                        foreach (var item in lispPO)
                        {
                            db.ProductOwnerReferences.Remove(item);
                        }
                        db.SaveChanges();
                        db.Products.Remove(obj);
                        db.SaveChanges();
                    }
                }
                LoadData();
            }
            }
            catch (Exception ex)
            {
                mess = ex.Message;
               
                
            }
        }
        public string FormatProductOwnerList(ICollection<ProductOwnerReference> list) {

            string res = "";
            foreach (var item in list)
            {
                res += item.CarCategory.CatName + ":" + item.RefProductID + ", ";
            }
            return res;
        
        }

        protected void ddlParentCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataList = db.Products.ToList();
            int catId = Convert.ToInt32(ddlParentCat.SelectedValue);
            if (catId != 0)
            {
                gvListItem.DataSource = dataList.Where(x => x.ProductCatID == catId);
            }
            else {
                gvListItem.DataSource = dataList;
            }
            gvListItem.DataBind();
            MakeGridViewPrinterFriendly(gvListItem);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string productName = txtSearchValue.Value.Trim();
            if (!String.IsNullOrEmpty(productName))
            {
                dataList = db.Products.ToList();
                gvListItem.DataSource = dataList.Where(x => x.ProductName.ToLower().Contains(productName.ToLower()) || x.ProductID.ToLower().Contains(productName.ToLower()) );
                gvListItem.DataBind();
               
            }
            MakeGridViewPrinterFriendly(gvListItem);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                LPS.Product product = new Product();

                product.ProductID = txtPCName.Value.ToUpper().Trim();
                product.ProductName = txtPCName.Value.Trim();
                if (db.Products.Count(x => x.ProductID == product.ProductID) > 0)
                {
                    mess = "The product with ID : " + product.ProductID + " already exist.";
                    saved = false;
                    return;
                }
                else {
                    product.ProductCatID = Convert.ToInt32(ddlCat.SelectedValue);
                    product.Specification = txtSpecification.Text.Trim();

                    for (int i = 0; i < refList.Count; i++)
                    {
                        refList[i].ProductID = product.ProductID; ;
                    }
                    
                    product.ProductOwnerReferences = refList;
                    product.Image = uploadImageAjax.TextboxUpload;
                    product.Avtive = chkPCActive.Checked;
                    db.Products.Add(product);
                    db.SaveChanges();
                    LoadData();

                    // Reset
                    ResetForm();
                    mess = "Create new product was successful";
                    saved = true;
                }

            }
            catch (Exception ex)
            {
                saved = false;
                mess = "A problem was found : "+ex.Message ;
                return;
            }
        }

        private void ResetForm()
        {
            txtPCName.Value = "";
            txtSpecification.Text = "";
            refList = new List<ProductOwnerReference>();
            gvRef.DataSource = refList;
            gvRef.DataBind();
            uploadImageAjax.ImageUpload = string.Empty;
            uploadImageAjax.TextboxUpload = string.Empty;
            chkPCActive.Checked = true;
            txtNumber.Value = "";
        }

        protected void gvRef_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            Label lblTitle = (Label)row.Cells[1].FindControl("lblName");

            if (e.CommandName.ToLower() == "deleterefitem")
            {

              var obj = refList.Find(x=>x.RefProductID==id);

              if (obj != null) {
                  refList.Remove(obj);
              }
              gvRef.DataSource = refList;
              gvRef.DataBind();
            }
        }

        protected void btnSaveRef_Click(object sender, EventArgs e)
        {
            string id = txtNumber.Value.Trim().ToUpper();
            if (string.IsNullOrEmpty(id))
            {
                mess = "No Owner Number !";
                saved = false;
                return;
            }
            if (refList.Count(x => x.RefProductID == id) > 0) {
                mess = "Owner Number was existed!";
                saved = false;
                return;
            }

            ProductOwnerReference prf = new ProductOwnerReference();
            prf.RefProductID = id;
            prf.CatID = Convert.ToInt32( ddlCarBrand.SelectedValue);

            refList.Add(prf);
            gvRef.DataSource = refList;
            gvRef.DataBind();
            ViewState["reflist"] = refList;

            saved = false;
        }
        public string GetCatName(int catID) {
            try
            {
                carBrandList = db.CarCategories.Where(x => x.CatLevel == 0).ToList();
                 return carBrandList.FirstOrDefault(x => x.CatID == catID).CatName;
            }
            catch 
            {
                return "No found";
            }
           
        }
    }
}