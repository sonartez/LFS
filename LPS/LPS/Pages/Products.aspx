<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Lafien.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="LPS.Pages.Products" %>
<%@ Register Src="~/Pages/UserControls/CtrlUploadImageAjax.ascx" TagName="UploadImageAjax" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>LAFIEN :: Products Listing</title>
    <script type="text/javascript" src="<%=Page.ResolveUrl("js/ajaxupload.3.5.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Products <small>Products Management</small>
            </h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12 ">
            <h2>
                <button type="button" id="btnPCAdd" name="btnPCAdd" onclick=" $('#frmPCEdit').slideDown();$('#btnPCAdd').hide();" class="btn btn-primary">Add New</button>
            </h2>
            <% if(mess.Length>0) { %>
             <div class="alert alert-info ">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                            <i class="fa fa-info-circle"></i>  <strong><%=mess %></strong>
                        </div>
            <% } %>
        </div>
    </div>
    <div class="row <% if(saved) {%> hidden-form <%} %>" id="frmPCEdit" >
     <div class="col-lg-8">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="form-group">
                        <label>Product category</label>
                         <asp:DropDownList ID="ddlCat" runat="server"  CssClass="form-control" >
                </asp:DropDownList>
                        <p class="help-block">Select a category of new product.</p>
                    </div>
                     
                    <div class="form-group">
                        <label>Product Name</label>
                        <input runat="server" id="txtPCName" class="form-control">
                        <p class="help-block">Name of new product.</p>
                    </div>

                     <div class="form-group">
                        <label>Product Image</label>
                         <uc1:UploadImageAjax ID="uploadImageAjax"  runat="server" />
                        <p class="help-block">Image of product.</p>
                    </div>

                     <div class="form-group">
                        <label>Specification</label>
                        <asp:TextBox Rows="4" runat="server" TextMode="MultiLine" ID="txtSpecification"   CssClass="form-control"/>
                        <p class="help-block">Specification input:<i> Height<b>:</b>80<b>,</b> Ø Out<b>:</b>76<b>,</b> Thread<b>:</b>3/4 "16 UNF<b>,</b>...</i></p>
                    </div>
                     <div class="checkbox"  id="chkAcitvate">
                        <label>
                            <input runat="server" id="chkPCActive" type="checkbox">
                            Enable this product on Search Engines
                        </label>
                    </div>
                   
                    <asp:Button ID="btnSave" CssClass="btn btn-default" runat="server" Text="Update" OnClick="btnSave_Click" />

 <button type="reset" class="btn btn-default" onclick="CloseForm()">Cancel</button>
                    <asp:HiddenField ID="CatID" EnableViewState="true" runat="server"  />
                    <asp:HiddenField ID="BackUrl" EnableViewState="true" runat="server"  />
                </div>
            </div>
        </div>
     <div  class="col-lg-4">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="form-group">
                        <label>Owner Car Brand </label>
                        <asp:DropDownList ID="ddlCarBrand"  runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                    </asp:DropDownList>
                        <p class="help-block">Select a parent brand.</p>
                    </div>
                     
                    <div class="form-group">
                        <label>Number</label>
                        <input runat="server" id="txtNumber" class="form-control">
                       
                    </div>

                   
                    <asp:Button ID="btnSaveRef" CssClass="btn btn-default" runat="server" Text="Add Owner" OnClick="btnSaveRef_Click" />
                    <br /><br />
                    <div class="table-responsive">
                        <asp:GridView ID="gvRef" CssClass="table table-hover" runat="server" GridLines="None" AutoGenerateColumns="False" OnRowCommand="gvRef_RowCommand" >
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>                                     
                                    OWNER
                                    </HeaderTemplate>
                                    <HeaderStyle   />
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# GetCatName(Convert.ToInt32(Eval("CatID")))%>'></asp:Label>
                                       
                                    </ItemTemplate><ItemStyle CssClass="col-center" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <HeaderTemplate> 
                                        Name
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      
                                         <asp:Label ID="lblName" runat="server" Text='<%#Eval("RefProductID").ToString()%>'></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                     
                                    </HeaderTemplate>
                                    <HeaderStyle Width="50px" CssClass="text-right" />
                                    <ItemTemplate>
                                        

                                       <asp:Button Text="Delete" ID="DeleteRefItem"  CssClass="btn btn-default btn-xs" runat="server" CommandName="DeleteRefItem" CommandArgument='<%# Bind("RefProductID") %>'>
                                       </asp:Button>
                                        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender_btnDeleteRefItem" runat="server" TargetControlID="DeleteRefItem" ConfirmText='<%# "Are your want to remove : " + Eval("RefProductID")   %>'>
                                        </ajaxToolkit:ConfirmButtonExtender>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                            </Columns>
                            
                            
                            <EmptyDataTemplate>
                                <asp:Label ID="lblEmptyData" runat="server" Font-Bold="True" Font-Size="18px" 
                                    Text="Empty"></asp:Label>
                            </EmptyDataTemplate>
                            
                        
                        
                        </asp:GridView>

                        
                    </div>
                   
                </div>
            </div>
        </div>
     </div>
    <div class=" row">
        <div class="col-xs-6 col-sm-4">
            <h2>
                <asp:DropDownList ID="ddlParentCat" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlParentCat_SelectedIndexChanged">
                </asp:DropDownList>

            </h2>
        </div>
        <div class="col-xs-6 col-sm-4"></div>
        <div class="col-xs-6 col-sm-4 form-inline text-right">

            <h2>
                <input type="text" runat="server" class="form-control" id="txtSearchValue" placeholder="Product name " />

                <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-default" Text="Find" OnClick="btnSearch_Click" />

            </h2>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12 ">
            <div class="panel panel-default">
                <div class="panel-body">
                    <h2>Listing</h2>

                    <div class="table-responsive">
                        <asp:GridView ID="gvListItem" CssClass="table table-hover" runat="server" GridLines="None" AutoGenerateColumns="False" OnRowCommand="gvListItem_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        #ID
                                    </HeaderTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemTemplate>
                                      <asp:Label ID="lblId" runat="server" Text='<%#Eval("ProductId").ToString()%>'></asp:Label> <br />
                                    
                                   
                                    </ItemTemplate>
                                    <ItemStyle CssClass="col-center" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                       Image
                                    </HeaderTemplate>
                                    <HeaderStyle Width="150px" />
                                    <ItemTemplate>
                                       
                                        <img src="<%# Page.ResolveUrl( ((Eval("Image")!=null)?Eval("Image"):"").ToString())%>" class="img-rounded" style="width:130px;"></img>
                                   
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Product Details
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" Font-Bold="true" runat="server" Text='<%#Eval("ProductName").ToString()%>'></asp:Label> <br />
                                        <b>Category: </b>   <asp:Label ID="lblCatName" runat="server" Text='<%#Eval("ProductCategory.ProductCatName").ToString()%>'></asp:Label>
                                        <p>
                                        <b>Specification: </b>     <asp:Label ID="lblCatNamr" runat="server" Text='<%#Eval("Specification")%>'></asp:Label>
                                        </p>
                                        <p>
                                          <b>Owners: </b>   <asp:Label ID="Label1" runat="server" Text='<%# FormatProductOwnerList((ICollection<LPS.ProductOwnerReference>)Eval("ProductOwnerReferences"))%>'></asp:Label>
                                        </p>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Active 
                                    </HeaderTemplate>
                                    <HeaderStyle Width="50px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" Enabled="false" runat="server" Checked='<%# Eval("Avtive") %>' />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Controls
                                    </HeaderTemplate>
                                    <HeaderStyle Width="115px" CssClass="text-right" />
                                    <ItemTemplate>
                                        <a class="btn btn-primary btn-xs" href="ProductEdit.aspx?Id=<%#Eval("ProductId").ToString() %>">Edit
                                        </a>

                                        <asp:Button Text="Delete" ID="btnDeleteItem" CssClass="btn btn-default btn-xs" runat="server" CommandName="DeleteItem" CommandArgument='<%# Bind("ProductId") %>'></asp:Button>
                                        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender_btnDeleteItem" runat="server" TargetControlID="btnDeleteItem" ConfirmText='<%# "Are your want to delete : " + Eval("ProductName")   %>'>
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
