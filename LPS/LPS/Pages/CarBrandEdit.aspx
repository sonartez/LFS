<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Lafien.Master" AutoEventWireup="true" CodeBehind="CarBrandEdit.aspx.cs" Inherits="LPS.Pages.CarBrandEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Edit Car Brands <small>Update <asp:Label runat="server" ID="lblCatName" ></asp:Label> infomation</small>
            </h1>
           
        </div>
    </div>

    <div class="row">
        <h2>
             <asp:Button ID="btnBack" CssClass="btn btn-default" runat="server" Text="Back to List" OnClick="btnBack_Click"/>

                   
        </h2>

        <div id="frmPCEdit" class="col-lg-12  ">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="form-group">
                        <label>Parent Brand</label>
                        <asp:DropDownList ID="ddlParentCat"  runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                    </asp:DropDownList>
                        <p class="help-block">Select a parent brand or root.</p>
                    </div>
                     
                    <div class="form-group">
                        <label>Car brand name</label>
                        <input runat="server" id="txtPCName" class="form-control">
                        <p class="help-block">Name of new car-brand.</p>
                    </div>

                   
                    <asp:Button ID="btnSave" CssClass="btn btn-default" runat="server" Text="Update" OnClick="btnSave_Click" />

                    <asp:HiddenField ID="CatID" EnableViewState="true" runat="server"  />
                    <asp:HiddenField ID="BackUrl" EnableViewState="true" runat="server"  />
                </div>
            </div>
        </div>
        

    </div>
</asp:Content>
