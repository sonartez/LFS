<%@ Page Title="" Language="C#" MasterPageFile="~/LPSMaster.Master" AutoEventWireup="true" CodeBehind="Cars.aspx.cs" Inherits="LPS.Cars" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <% 
            if (noResult)
            {
        %>
        <div class="col-lg-12">
            <div class="alert alert-info ">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <i class="fa fa-info-circle"></i>Product not found!
                       
            </div>
        </div>
        <% }
            else
            { %>
        <div class="col-lg-12 col-md-12">
            <h4>Filter results </h4>
        </div>

        <div class="col-lg-12 col-md-12">
            <div style="width: 100%" class="table-responsive">
                <%
                //List<LPS.Car> cars = product.CarItems.Select(x => x.Car).ToList();
                
                %>
                <table class="table table-hover table-bordered table-striped">
                    <thead>
                        <tr>
                            <th style="min-width: 120px">YEARS</th>
                            <th style="width: 100px">MODEL</th>
                            <th style="width: 100px">EngNo</th>
                            <th style="width: 100px">EngVol</th>
                            <th style="width: 100px">Body</th>

                            <%
                foreach (var item in productCat)
                {
                            %>
                            <th style=""><%=item.ProductCatName.ToUpper() %></th>
                            <%
                }
                            %>
                        </tr>
                    </thead>
                    <tbody>
                        <%
                var catList = cars.Select(x => x.CarCategory).Distinct().OrderBy(x => x.CatName).ToList();
                foreach (var cat in catList)
                {
                    var carSubList = cars.Where(x => x.CarCatID == cat.CatID).OrderByDescending(x => x.CarID).ToList();
                    if (carSubList.Count > 0)
                    {
                        %>
                        <tr>
                            <th colspan="<%=5+productCat.Count %>">
                                <b><%=GetFullCatLocation(cat) %></b>
                                <div class="btn btn-primary btn-xs right" onclick="$('.sub_<%=cat.CatID %>').toggle()">+</div>
                            </th>
                        </tr>
                        <%
                                                foreach (var car in carSubList)
                                                {
                                                                    
                        %>
                        <tr class="sub_<%=cat.CatID %>" style="display: none">
                            <td><%=GetYears(car) %></td>
                            <td><%=car.Model %></td>
                            <td><%=car.EngNo %></td>
                            <td><%=car.EngVol %></td>
                            <td><%=car.Body %></td>
                            <%
                               foreach (var item in productCat)
                               {
                                   var carItem = car.CarItems.Where(x => x.ProductCatID == item.ProductCatID);
                            %>
                            <td>
                                <% if (carItem != null)
                                       foreach (var obj in carItem)
                                       {
                                %>
                                <a href="Product.aspx?key=<%=obj.ProductID %>" title="<%=obj.ProductID %>" style="display: block">

                                    <%=obj.ProductID %></a>
                                <% 
                                       } %>
                                                       

                            </td>
                            <%
                            }
                            %>
                        </tr>
                        <% 
                           } %>

                        <%
                                            }
                                        }  
                        %>
                    </tbody>
                </table>
            </div>

        </div>
        <% } %>
    </div>
</asp:Content>
