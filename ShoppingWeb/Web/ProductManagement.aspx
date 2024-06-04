<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductManagement.aspx.cs" Inherits="ShoppingWeb.Web.ProductManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查詢商品</title>
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/Pagination.css" />
    <script src="/js/<%= jsVersion %>/jquery-3.7.1.min.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= cookieLanguage %>.js"></script>
    <script src="/js/<%= jsVersion %>/Pagination.js"></script>
    <script src="/js/<%= jsVersion %>/ProductCategories.js"></script>
    <script src="/js/<%= jsVersion %>/SetNLog.js"></script>
    <script src="/js/<%= jsVersion %>/ProductManagement.js"></script>
</head>
<body>
    <div class="w-auto mx-3" id="allProductDiv">
        <h2 class="text-center"><%= Resources.Resource.product %></h2>
        <br />
        <div class="row mx-auto col-12 mb-3">
            <div class="col-2 px-0" id="divCategories">
            </div>
            <div class="col-2" id="divMinorCategory">
                <label for="minorCategory" class="form-label"><%= Resources.Resource.minorCategories %></label>
                <select id="minorCategory" class="form-select">
                    <option value=""><%= Resources.Resource.chooseType %></option>
                </select>
            </div>
            <div class="col-2 ps-0" id="divBrand">
            </div>
            <div class="col-2 px-0">
                <label for="txbProductSearch" class="form-label"><%= Resources.Resource.productNameSelect %></label>
                <input type="text" class="form-control" id="txbProductSearch" placeholder="<%= Resources.Resource.productNameSelect %>" />
            </div>
            <div class="col px-0 d-flex justify-content-center align-items-end">
                <button id="btnSearchProduct" type="submit" class="btn btn-outline-primary"><%= Resources.Resource.select %></button>
            </div>
            <div class="col px-0 d-flex justify-content-center align-items-end">
                <button id="btnAddProduct" type="submit" class="btn btn-outline-primary"><%= Resources.Resource.addProduct %></button>
            </div>
            <div class="col px-0 d-flex justify-content-center align-items-end">
                <button id="btnLowProduct" type="submit" class="btn btn-outline-primary"><%= Resources.Resource.lowStock %></button>
            </div>
        </div>

        <div class="row" id="productTableDiv">
            <table id="myTable" class="table table-striped table-hover table-bordered">
                <thead id="tableHead">
                    <tr>
                        <th>
                            <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.id %></button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.name %></button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.categories %></button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.price %></button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.stock %></button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.open %></button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.introduce %></button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.img %></button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.edit %></button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.del %></button></th>

                    </tr>
                </thead>
                <tbody id="tableBody">
                    <!-- 內容 -->
                </tbody>
            </table>
        </div>

        <div id="pagination">
            <!-- 分頁內容 -->
        </div>

        <div class="row">
            <span id="labSearchProduct" class="col-12 col-sm-12 text-center text-success"></span>
        </div>
    </div>

    <!-- 商品庫存預警 -->
    <div id="lowStockProductsDiv" class="w-auto mx-3 mt-4" style="display: none;">
        <h3 class="text-center text-danger"><%= Resources.Resource.lowStock %></h3>
        <div class="table-responsive">
            <table id="lowStockTable" class="table table-striped table-hover table-bordered">
                <thead>
                    <tr>
                        <th><%= Resources.Resource.id %></th>
                        <th><%= Resources.Resource.name %></th>
                        <th><%= Resources.Resource.stock %></th>
                        <th><%= Resources.Resource.edit %></th>
                    </tr>
                </thead>
                <tbody id="lowStockTableBody">
                </tbody>
            </table>
        </div>
    </div>

</body>
</html>
