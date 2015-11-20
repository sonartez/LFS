using LPS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace LPS.Pages
{
    public partial class ProductEdit : System.Web.UI.Page
    {
        private List<LPS.ProductOwnerReference> refList = new List<ProductOwnerReference>();
        private lafien_products_dbEntities db = new lafien_products_dbEntities();
        public List<LPS.Product> dataList, viewList = new List<LPS.Product>();
        public List<LPS.ProductCategory> ddlDataList = new List<LPS.ProductCategory>();
        public List<LPS.CarCategory> carBrandList = new List<CarCategory>();
        public string mess = "";
        public bool saved = true;
        public Common comClass = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            uploadImageAjax.UploadFolder = @"~/Upload/Products/";
            //uploadImageAjax.
            if (!IsPostBack)
            {
                LoadData();

                ViewState["reflist"] = refList;
                if (Request.UrlReferrer != null)
                {
                    BackUrl.Value = Request.UrlReferrer.ToString();
                }
                else
                {
                    BackUrl.Value = "Products.aspx";
                }
            }
            else
            {
                refList = (List<ProductOwnerReference>)ViewState["reflist"];
            }
           
        }
        private void LoadData()
        {
            if (string.IsNullOrEmpty(Request.QueryString.Get("Id")))
            {
                Response.Redirect(BackUrl.Value);
            }
            else
            {
                try
                {
                    string Id = Request.QueryString.Get("Id");

                    Product product = new Product();
                    product = db.Products.FirstOrDefault(x => x.ProductID == Id);
                    if (product == null) {
                        throw new Exception("notfound");
                    }

                    ddlCat.DataSource = db.ProductCategories.OrderBy(x => x.ProductCatName).ToList();
                    ddlCat.DataTextField = "ProductCatName";
                    ddlCat.DataValueField = "ProductCatID";
                    ddlCat.DataBind();

                    carBrandList = db.CarCategories.Where(x => x.CatLevel == 0).OrderBy(x => x.CatName).ToList();
                    ddlCarBrand.DataSource = carBrandList;
                    ddlCarBrand.DataTextField = "CatName";
                    ddlCarBrand.DataValueField = "CatID";
                    ddlCarBrand.DataBind();


                    txtPCName.Value = product.ProductName;
                    ProductID.Value = product.ProductID;
                    txtSpecification.Text = product.Specification;
                    refList = product.ProductOwnerReferences.ToList();
                    uploadImageAjax.ImageUpload = product.Image;
                    uploadImageAjax.TextboxUpload = product.Image;
                    chkPCActive.Checked = product.Avtive;
                    ddlCat.SelectedValue = product.ProductCatID.ToString();

                    gvRef.DataSource = refList;
                    gvRef.DataBind();

                    lblCatName.Text = product.ProductID;

                }
                catch (Exception ex)
                {

                    Response.Clear();
                    Response.StatusCode = 404;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                LPS.Product product = new Product();
                product = db.Products.FirstOrDefault(x => x.ProductID == ProductID.Value);
               
                if (product!=null)
                {
                    for (int i = 0; i < product.ProductOwnerReferences.Count; i++)
                    {
                        if (refList.Count(x => x.RefProductID == product.ProductOwnerReferences.ToList()[i].RefProductID) == 0)
                        {
                            product.ProductOwnerReferences.Remove(product.ProductOwnerReferences.ToList()[i]);
                        }
                    }
                    for (int i = 0; i < refList.Count; i++)
                    {
                        if ( product.ProductOwnerReferences.Count(x => x.RefProductID == refList[i].RefProductID) == 0)
                        {
                            refList[i].ProductID = product.ProductID;
                            product.ProductOwnerReferences.Add(refList[i]);
                        }
                    }
                    db.SaveChanges();
                    
                    product = db.Products.FirstOrDefault(x => x.ProductID == ProductID.Value);
                    product.ProductName = txtPCName.Value.Trim();
                    product.ProductCatID = Convert.ToInt32(ddlCat.SelectedValue);
                    product.Specification = txtSpecification.Text.Trim();
                   
                    product.Image = uploadImageAjax.TextboxUpload;
                    product.Avtive = chkPCActive.Checked;
                   
                    int rs =  db.SaveChanges();
                    LoadData();                    
                   
                    mess = "Product update successful";
                    saved = true;
                }
            }
            catch (Exception ex)
            {
                saved = false;
                mess = "A problem was found : " + ex.Message;
                return;
            }
        }

        protected void gvRef_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            Label lblTitle = (Label)row.Cells[1].FindControl("lblName");

            if (e.CommandName.ToLower() == "deleterefitem")
            {

                var obj = refList.Find(x => x.RefProductID == id);

                if (obj != null)
                {
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
            if (refList.Count(x => x.RefProductID == id) > 0)
            {
                mess = "Owner Number was existed!";
                saved = false;
                return;
            }

            ProductOwnerReference prf = new ProductOwnerReference();
            prf.RefProductID = id;
            prf.CatID = Convert.ToInt32(ddlCarBrand.SelectedValue);

            refList.Add(prf);
            gvRef.DataSource = refList;
            gvRef.DataBind();
            ViewState["reflist"] = refList;

        }
        public string GetCatName(int catID)
        {
            try
            {
                carBrandList = db.CarCategories.Where(x => x.CatLevel == 0).ToList();
                return carBrandList.FirstOrDefault(x => x.CatID == catID).CatName;
            }
            catch
            {

                return "Not found";
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(BackUrl.Value);
        }
    }
}