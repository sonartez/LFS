using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace LPS.Services
{
    /// <summary>
    /// Summary description for lpsService
    /// </summary>
    [WebService(Namespace = "http://localhost/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class lpsService : System.Web.Services.WebService
    {
        #region Public Member Declare Here
        private List<CascadingDropDownNameValue> ListCascading;
        #endregion

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession=true)]        
        public CascadingDropDownNameValue[] GetCarBrandByParent(string knownCategoryValues, string category, string contextKey)
        {
            try
            {


                ListCascading = new List<CascadingDropDownNameValue>();
                lafien_products_dbEntities db = new lafien_products_dbEntities();
                StringDictionary itmsIdList = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
                var itmId = Convert.ToInt32(itmsIdList["CatID"]);
                var catList = db.CarCategories.Where(x => x.CatParentID == itmId).OrderBy(x => x.CatName).ToList();

                foreach (var itm in catList)
                {

                    string itemIdString = itm.CatID.ToString();
                    string nameString = itm.CatName.ToString();
                    ListCascading.Add(new CascadingDropDownNameValue(nameString, itemIdString));
                }

                return ListCascading.ToArray();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
