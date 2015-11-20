<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Lafien.Master" AutoEventWireup="true" CodeBehind="ProductCategoryEdit.aspx.cs" Inherits="LPS.Pages.ProductCategoryEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>LAFIEN :: Edit Product Categories </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Edit Product Categories <small>Product Category Management</small>
            </h1>
            <ol class="breadcrumb">
                <li class="active">
                    <i class="fa fa-dashboard"></i>Product Categories
                </li>
            </ol>
        </div>
    </div>

    <div class="row">
        

        <div id="frmPCEdit" class="col-lg-12   ">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="form-group">
                        <label>Category Name</label>
                        <input runat="server" id="txtPCName" class="form-control">
                        <p class="help-block">Name of product category.</p>
                    </div>

                    <div class="checkbox" id="chkAcitvate">
                        <label>
                            <input runat="server" id="chkPCActive" type="checkbox">
                            Enable this category on Search Engines
                        </label>
                    </div>
                    <asp:Button ID="btnSave" CssClass="btn btn-default" runat="server" Text="Update" OnClick="btnSave_Click" />

                    <asp:Button ID="btnBack" CssClass="btn btn-default" runat="server" Text="Back to List" OnClick="btnBack_Click"/>

                    <asp:HiddenField ID="CatID" EnableViewState="true" runat="server"  />
                    <asp:HiddenField ID="BackUrl" EnableViewState="true" runat="server"  />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
