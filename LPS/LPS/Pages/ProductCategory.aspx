<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Lafien.Master" AutoEventWireup="true" CodeBehind="ProductCategory.aspx.cs" Inherits="LPS.Pages.ProductCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>LAFIEN :: Product Categories </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
                                       
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Product Categories <small>Product Category Management</small>
            </h1>
            <ol class="breadcrumb">
                <li class="active">
                    <i class="fa fa-dashboard"></i>Product Categories
                            </li>
            </ol>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">

            <h2>
                <button type="button" id="btnPCAdd" name="btnPCAdd" onclick=" $('#frmPCEdit').slideDown();$('#btnPCAdd').hide();" class="btn btn-primary">Add New</button>
            </h2>
        </div>

        <div id="frmPCEdit" class="col-lg-12  hidden-form ">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="form-group">
                        <label>Category Name</label>
                        <input runat="server" id="txtPCName" class="form-control">
                        <p class="help-block">Name of product category.</p>
                    </div>

                    <div class="checkbox"  id="chkAcitvate">
                        <label>
                            <input runat="server" id="chkPCActive" type="checkbox">
                            Enable this category on Search Engines
                        </label>
                    </div>
                    <asp:Button ID="btnSave" CssClass="btn btn-default" runat="server" Text="Save" OnClick="btnSave_Click" />
                   
                    <button type="reset" class="btn btn-default" onclick="CloseForm()">Cancel</button>
                </div>
            </div>
        </div>
        <div class="col-lg-12 ">
            <div class="panel panel-default">
                <div class="panel-body">
                    <h2>Listing</h2>
               
                    <div class="table-responsive">
                        <asp:GridView ID="gvListItem" CssClass="table table-hover" runat="server" GridLines="None" AutoGenerateColumns="False" OnRowCommand="gvListItem_RowCommand" >
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>                                     
                                    #ID
                                    </HeaderTemplate>
                                    <HeaderStyle Width="50px"  />
                                    <ItemTemplate>
                                        #<asp:Label ID="lblId" runat="server" Text='<%#Eval("ProductCatID").ToString()%>'></asp:Label>
                                       
                                    </ItemTemplate><ItemStyle CssClass="col-center" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <HeaderTemplate> 
                                        Name
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        
                                         <asp:Label ID="lblName" runat="server" Text='<%#Eval("ProductCatName").ToString()%>'></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField>
                                    <HeaderTemplate> 
                                        Enable 
                                    </HeaderTemplate>
                                     <HeaderStyle Width="50px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" Enabled="false" runat="server" Checked='<%# Eval("Active") %>' />
                                         
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                      Controls
                                    </HeaderTemplate>
                                    <HeaderStyle Width="115px" CssClass="text-right" />
                                    <ItemTemplate>
                                        <a class="btn btn-primary btn-xs" href="ProductCategoryEdit.aspx?Id=<%#Eval("ProductCatID").ToString() %>">
                                           Edit
                                        </a>

                                       <asp:Button Text="Delete" ID="btnDeleteItem"  CssClass="btn btn-default btn-xs" runat="server" CommandName="DeleteItem" CommandArgument='<%# Bind("ProductCatID") %>'>
                                       </asp:Button>
                                        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender_btnDeleteItem" runat="server" TargetControlID="btnDeleteItem" ConfirmText='<%# "Are your want to delete : " + Eval("ProductCatName")   %>'>
                                        </ajaxToolkit:ConfirmButtonExtender>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                            </Columns>
                            
                            
                            <EmptyDataTemplate>
                                <asp:Label ID="lblEmptyData" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Red"
                                    Text="Không có dữ liệu"></asp:Label>
                            </EmptyDataTemplate>
                            
                        
                        
                        </asp:GridView>

                        
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
