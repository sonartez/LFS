<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Lafien.Master" AutoEventWireup="true" CodeBehind="CarEdit.aspx.cs" Inherits="LPS.Pages.CarEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" type="text/css" href="css/bootstrap-multiselect.css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("js/bootstrap-multiselect.js")%>"></script>
    <script>
        var selectedCarItems = [];
        $(document).ready(function () {

            //$("#btnPCAdd").click(function() {
            //    //console.log('me');
            //    $("#frmPCEdit").show();
            //    $("#btnPCAdd").hide();
            //});
            //$(".selectList").dropdownchecklist({ icon: {}, maxDropHeight: 150, width: 150 });

              <%
        foreach (var item in productCat)
        {
                                    %>
            $("#ContentPlaceHolder1_ddlCat<%=item.ProductCatID%>").multiselect({
                maxHeight: 200,
                enableFiltering: true,
                enableClickableOptGroups: true,
                onChange: function (option, checked, select) {
                    console.log(option[0].value);
                    console.log(checked);
                    console.log(select);
                    var catId = "<%=item.ProductCatID%>";
                    if (checked) {
                        // add
                        var isExists = false;
                        for (var i = 0; i < selectedCarItems.length; i++) {
                            if (selectedCarItems[i].ProductID == option[0].value) {
                                // exists 
                                isExists = true;
                                break
                            }

                        }
                        if (!isExists) {
                            selectedCarItems.push({ ProductID: option[0].value, CatID: catId });
                        }

                    } else {
                        //remove
                        var selectIndex = -1;
                        for (var i = 0; i < selectedCarItems.length; i++) {
                            if (selectedCarItems[i].ProductID == option[0].value) {
                                // exists 
                                selectIndex = i;
                                break
                            }

                        }
                        if (selectIndex > -1) {
                            selectedCarItems.splice(i, 1);
                        }
                    }

                    console.log(selectedCarItems);
                    var storageValue = "";
                    for (var i = 0; i < selectedCarItems.length; i++) {
                        storageValue += selectedCarItems[i].CatID + "|" + selectedCarItems[i].ProductID;
                        if (i < selectedCarItems.length - 1) storageValue += ";"
                    }
                    $("#carItemsSelectedData").val(storageValue);
                }
            });
            <%
        }
                                    %>


        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Edit Car <small>Update <asp:Label runat="server" ID="lblCatName" ></asp:Label>  infomation</small>
            </h1>
            
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12 ">
            <h2>
                      <asp:Button ID="btnBack" CssClass="btn btn-default" runat="server" Text="Back to List" OnClick="btnBack_Click"/>

            </h2>
            <% if (mess.Length > 0)
               { %>
            <div class="alert alert-info ">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <i class="fa fa-info-circle"></i><strong><%=mess %></strong>
            </div>
            <% } %>



            <div id="frmPCEdit" class="row   panel panel-default ">
                <div class="panel-body">
                    <div class="col-lg-4 col-md-8">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="form-group">

                                    <label>Car Brand</label>
                                    <asp:DropDownList ID="ddlParentCat" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <ajaxToolkit:CascadingDropDown runat="server" UseContextKey="True"
                                                ContextKey='CatID' ID="selectSubCarBrand" Category="CatID" ParentControlID="ddlParentCat"
                                                TargetControlID="ddlCarCategory" ServiceMethod="GetCarBrandByParent"
                                                ServicePath="~/CarCategory/GetCarBrandByParent">
                                            </ajaxToolkit:CascadingDropDown>
                                            <label>Class</label>
                                            <asp:DropDownList ID="ddlCarCategory" runat="server" AppendDataBoundItems="false" CssClass="form-control">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>

                                <div class="form-group">
                                    <label>Model</label>
                                    <input runat="server" id="txtModel" class="form-control" />
                                    <p class="help-block">Model of car.</p>
                                </div>

                                <div class="form-group ">
                                    <label>From month</label>

                                    <input runat="server" type="date" pattern="\d{1,2}/\d{2}" id="txtFromDate" class="form-control" />

                                    <p class="help-block"></p>
                                </div>
                                <div class="form-group ">
                                    <label>To month</label>

                                    <input runat="server" type="date" pattern="\d{1,2}/\d{2}" id="txtToDate" class="form-control" />

                                    <p class="help-block">Leave blank for present</p>
                                </div>



                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="form-group">
                                    <label>Engery </label>
                                    <input runat="server" id="txtEngery" class="form-control"/>
                                    <p class="help-block"></p>
                                </div>
                                <div class="form-group">
                                    <label>Engery Volumn</label>
                                    <input runat="server" id="txtEngVol" class="form-control"/>
                                    <p class="help-block"></p>
                                </div>
                                <div class="form-group">
                                    <label>Engery Number</label>
                                    <input runat="server" id="txtEngNo" class="form-control"/>
                                    <p class="help-block"></p>
                                </div>
                                <div class="form-group">
                                    <label>BODY</label>
                                    <input runat="server" id="txtBodyNo" class="form-control"/>
                                    <p class="help-block"></p>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <div class="panel panel-default">
                            <div class="panel-body" style="position: relative">
                                <asp:PlaceHolder ID="placehoder1" EnableViewState="true" runat="server"></asp:PlaceHolder>
                                <asp:HiddenField ID="carItemsSelectedData" ClientIDMode="Static" runat="server" Value="" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12 col-md-12"  >
                        
                            <asp:Button ID="btnSave" CssClass="btn btn-default" runat="server" Text="Save" OnClick="btnSave_Click" />

                      
                         <asp:HiddenField ID="CarID" EnableViewState="true" runat="server"  />
                    <asp:HiddenField ID="BackUrl" EnableViewState="true" runat="server"  />
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
