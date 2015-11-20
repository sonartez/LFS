using LPS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LPS.Pages
{
    public partial class CarEdit : System.Web.UI.Page
    {
        private lafien_products_dbEntities db = new lafien_products_dbEntities();
        public List<Car> dataList;
        public List<CarCategory> catList;
        public List<Car> carSubList;
        public List<LPS.ProductCategory> productCat;
        private List<CarCategory> carBrandList;
        public string mess = "";
        public bool saved = true;
        public Car car;
        public Common comClass = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {

            productCat = db.ProductCategories.Where(x => x.Active == true).ToList();
                if (!IsPostBack)
                {
                    LoadData();
                    if (Request.UrlReferrer != null)
                    {
                        BackUrl.Value = Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        BackUrl.Value = "Cars.aspx";
                    }
                }
               
                CreateDynamicControls();
           
        }

        private void LoadData()
        {
            if (!comClass.IsNumeric(Request.QueryString.Get("Id")))
            {
                Response.Redirect(BackUrl.Value);
            }
            else
            {
                int thisId = Convert.ToInt32(Request.QueryString.Get("Id"));
                car = db.Cars.FirstOrDefault(x => x.CarID == thisId);
                if(car==null)
                    Response.Redirect(BackUrl.Value);

                carBrandList = db.CarCategories.Where(x => x.CatLevel == 0).OrderBy(x => x.CatName).ToList();
                ddlParentCat.DataSource = carBrandList;
                ddlParentCat.DataTextField = "CatName";
                ddlParentCat.DataValueField = "CatID";
                ddlParentCat.DataBind();

                var cat = db.CarCategories.FirstOrDefault(x => x.CatID == car.CarCatID);

                ddlParentCat.SelectedValue = cat.CatParentID.Value.ToString();

                var carClassList = db.CarCategories.Where(x => x.CatParentID == cat.CatParentID).OrderBy(x => x.CatName).ToList();
                ddlCarCategory.DataSource = carClassList;
                ddlCarCategory.DataTextField = "CatName";
                ddlCarCategory.DataValueField = "CatID";
                ddlCarCategory.DataBind();

                ddlCarCategory.SelectedValue = cat.CatID.ToString();

                CarID.Value = car.CarID.ToString();

                txtBodyNo.Value = car.Body;
                txtEngery.Value = car.Engine;
                txtEngNo.Value = car.EngNo;
                txtEngVol.Value = car.EngVol;
                txtFromDate.Value = car.FromDate.ToShortDateString();
                txtModel.Value = car.Model;
                txtToDate.Value = car.ToDate.ToShortDateString();

                string tmpstr = string.Empty;
                foreach (var item in car.CarItems)
                {
                    tmpstr += item.ProductCatID + "|" + item.ProductID + ";";
                }
                tmpstr = tmpstr.TrimEnd(';');
                
                carItemsSelectedData.Value = tmpstr;
            }

        }

        private void CreateDynamicControls()
        {
            productCat = db.ProductCategories.Where(x => x.Active == true).ToList();
            
            string[] selectedItem = carItemsSelectedData.Value.Split(';');
            List<CarItem> selectedCarItem = new List<CarItem>();
            if (!string.IsNullOrEmpty(carItemsSelectedData.Value))
            {
                for (int i = 0; i < selectedItem.Count(); i++)
                {
                    string[] tmp = selectedItem[i].Split('|');

                    selectedCarItem.Add(new CarItem() { ProductCatID = Convert.ToInt32(tmp[0].Trim()), ProductID = tmp[1].Trim() });
                }
            }

            foreach (var item in productCat)
            {

                Panel p = new Panel();
                p.CssClass = "form-group";
                p.ID = "p_" + item.ProductCatID;
                p.EnableViewState = true;
                Label l = new Label();
                l.Text = item.ProductCatName.ToUpper();

                ListBox lb = new ListBox();

                lb.ID = "ddlCat" + item.ProductCatID;
                lb.SelectionMode = ListSelectionMode.Multiple;
                lb.EnableViewState = true;
                lb.CssClass = "form-control selectList";

                lb.DataSource = db.Products.Where(x => x.Avtive == true && x.ProductCatID == item.ProductCatID).ToList();
                lb.DataTextField = "ProductName";
                lb.DataValueField = "ProductID";

                p.Controls.Add(l);
                p.Controls.Add(new LiteralControl("<br />"));
                p.Controls.Add(lb);

                placehoder1.Controls.Add(p);
                lb.DataBind();


                if (!string.IsNullOrEmpty(carItemsSelectedData.Value))
                {
                    foreach (var obj in selectedCarItem.Where(x => x.ProductCatID == item.ProductCatID))
                    {
                        foreach (ListItem opt in lb.Items)
                        {
                            if (opt.Value == obj.ProductID)
                                opt.Selected = true;
                        }
                    }
                }
            }
        }
       


        public string GetYears(Car car)
        {
            return car.FromDate.ToString("MM.yy") + "~" + car.ToDate.ToString("MM.yy");
        }
        public string GetFullCatLocation(CarCategory cat)
        {
            if (cat.CatLevel == 1)
                return db.CarCategories.FirstOrDefault(x => x.CatID == cat.CatParentID).CatName + " » " + cat.CatName;
            else
                return cat.CatName;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int thisId = Convert.ToInt32(CarID.Value);
                

                foreach (var item in db.CarItems.Where(x=>x.CarID== thisId))
                {
                    db.CarItems.Remove(item);
                }
                db.SaveChanges();
                car = db.Cars.FirstOrDefault(x => x.CarID == thisId);
                car.Body = txtBodyNo.Value.Trim();
                car.CarCatID = Convert.ToInt32(ddlCarCategory.SelectedValue);
                car.EngNo = txtEngNo.Value.Trim();
                car.EngVol = txtEngVol.Value.Trim();
                car.Engine = txtEngery.Value.Trim();
                car.Model = txtModel.Value.Trim();
                DateTime fromDate = new DateTime();
                if (DateTime.TryParse(txtFromDate.Value, out fromDate))
                {
                    car.FromDate = fromDate;
                }
                else
                {
                    car.FromDate = DateTime.Now;
                }

                DateTime toDate = new DateTime();
                if (DateTime.TryParse(txtToDate.Value, out toDate))
                {
                    car.ToDate = toDate;
                }
                else
                {
                    car.ToDate = DateTime.Now;
                }


                List<CarItem> lstCatItems = new List<CarItem>();
                productCat = db.ProductCategories.Where(x => x.Active == true).ToList();


                foreach (var cat in productCat)
                {

                    var ddlItem = (ListBox)placehoder1.FindControl("ddlCat" + cat.ProductCatID);
                    var selectedItems = from item in ddlItem.Items.OfType<ListItem>()
                                        where item.Selected
                                        select item;
                    var lst = selectedItems.ToList();
                    if (lst.Count() > 0)
                    {
                        foreach (var obj in lst)
                        {
                            lstCatItems.Add(new CarItem() { ProductID = obj.Value, ProductCatID = cat.ProductCatID, CarID = car.CarID });
                        }
                    }
                }
                car.CarItems = lstCatItems;
                
                db.SaveChanges();

                LoadData();
                // Reset
                ResetForm();
                mess = "Update new product was successful";
                saved = true;


            }
            catch (Exception ex)
            {
                saved = false;
                mess = "A problem was found : " + ex.Message;
                return;
            }
        }

        private void ResetForm()
        {
            //throw new NotImplementedException();
            txtBodyNo.Value = string.Empty;
            txtEngery.Value = string.Empty;
            txtEngNo.Value = string.Empty;
            txtEngVol.Value = string.Empty;
            txtFromDate.Value = string.Empty;
            txtToDate.Value = string.Empty;
            carItemsSelectedData.Value = string.Empty;

        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(BackUrl.Value);
        }
    }
}