<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Lafien.Master" AutoEventWireup="true" CodeBehind="CarBrands.aspx.cs" Inherits="LPS.Pages.CarBrands1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>LAFIEN :: Car Brands Listing</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Car Brands <small>Car Brands Management</small>
            </h1>
           
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
                        <label>Parent Brand</label>
                        <asp:DropDownList ID="ddlParentCat"  runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                    </asp:DropDownList>
                        <p class="help-block">Select a parent brand or root.</p>
                    </div>
                     
                    <div class="form-group">
                        <label>New brand name</label>
                        <input runat="server" id="txtPCName" class="form-control">
                        <p class="help-block">Name of new car-brand.</p>
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
                                        #<asp:Label ID="lblId" runat="server" Text='<%#Eval("CatID").ToString()%>'></asp:Label>
                                       
                                    </ItemTemplate><ItemStyle CssClass="col-center" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <HeaderTemplate> 
                                        Name
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      
                                         <asp:Label runat="server" ID="tabSpace" Visible='<%# Convert.ToInt32(Eval("CatLevel"))==1?true:false %>' Text="&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"></asp:Label>
                                         <asp:Label ID="lblName" runat="server" Text='<%#Eval("CatName").ToString()%>'></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                      Controls
                                    </HeaderTemplate>
                                    <HeaderStyle Width="115px" CssClass="text-right" />
                                    <ItemTemplate>
                                        <a class="btn btn-primary btn-xs" href="CarBrandEdit.aspx?Id=<%#Eval("CatID").ToString() %>">
                                           Edit
                                        </a>

                                       <asp:Button Text="Delete" ID="btnDeleteItem"  CssClass="btn btn-default btn-xs" runat="server" CommandName="DeleteItem" CommandArgument='<%# Bind("CatID") %>'>
                                       </asp:Button>
                                        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender_btnDeleteItem" runat="server" TargetControlID="btnDeleteItem" ConfirmText='<%# "Are your want to delete : " + Eval("CatName")   %>'>
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
