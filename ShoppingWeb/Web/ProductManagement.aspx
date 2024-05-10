<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductManagement.aspx.cs" Inherits="ShoppingWeb.Web.ProductManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查詢商品</title>
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <script src="/js/<%= jsVersion %>/jquery-3.7.1.min.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= cookieLanguage %>.js"></script>
    <script src="/js/<%= jsVersion %>/Pagination.js"></script>
    <script src="/js/<%= jsVersion %>/ProductCategories.js"></script>
    <script src="/js/<%= jsVersion %>/ProductManagement.js"></script>
</head>
<body>
    <div class="w-auto mx-3">
        <h2 class="text-center"><%= Resources.Resource.product %></h2>
        <br />
        <div class="row mx-auto col-12 mb-3">
            <div class="col px-0" id="divCategories">
            </div>
            <div class="col" id="divMinorCategory">
                <label for="minorCategory" class="form-label"><%= Resources.Resource.minorCategories %></label>
                <select id="minorCategory" class="form-select">
                    <option value=""><%= Resources.Resource.chooseType %></option>
                </select>
            </div>
            <div class="col ps-0" id="divBrand">
            </div>
            <div class="col px-0">
                <label for="txbProductSearch" class="form-label"><%= Resources.Resource.productNameSelect %></label>
                <input type="text" class="form-control" id="txbProductSearch" placeholder="<%= Resources.Resource.productNameSelect %>" />
            </div>
            <div class="col-1 px-0 d-flex justify-content-center align-items-end">
                <button id="btnSearchProduct" type="submit" class="btn btn-outline-primary"><%= Resources.Resource.select %></button>
            </div>
            <div class="col-2 px-0 d-flex justify-content-center align-items-end">
                <button id="btnAddProduct" type="submit" class="btn btn-outline-primary"><%= Resources.Resource.addProduct %></button>
            </div>
        </div>

        <div class="row" id="productTableDiv">
            <table id="myTable" class="table table-striped table-hover table-bordered">
                <thead>
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

        <div class="row">
            <div id="pagination" class="text-center d-flex justify-content-center col-12 col-sm-12">
                <ul class="pagination" id="ulPagination">
                    <!-- 分頁按鈕 -->
                </ul>
            </div>
            <div id="paginationInfo" class="text-center text-center d-flex justify-content-center col-4 mx-auto">
                <!-- 動態生成頁數選項，總頁數，筆數選項 -->
            </div>
            <span id="labSearchProduct" class="col-12 col-sm-12 text-center text-success"></span>
        </div>
    </div>
 
</body>
</html>
