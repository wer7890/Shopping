﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductManagement.aspx.cs" Inherits="ShoppingWeb.Web.ProductManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查詢商品</title>
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/I18n.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= basePageLanguage %>.js"></script>
    <script src="/js/<%= jsVersion %>/ProductCategories.js"></script>
    <script src="/js/<%= jsVersion %>/ProductManagement.js"></script>
</head>
<body>
    <div class="w-auto mx-3">
        <h2 class="text-center i18n" data-key="product">商品</h2>
        <br />
        <div class="row mx-auto col-12 mb-3">
            <div class="col px-0" id="divCategories">
            </div>
            <div class="col" id="divMinorCategory">
                <label for="minorCategory" class="form-label i18n" data-key="minorCategories">子類型</label>
                <select id="minorCategory" class="form-select">
                    <option value="" class="i18n" data-key="chooseType">請先選擇類型</option>
                </select>
            </div>
            <div class="col ps-0" id="divBrand">
            </div>
            <div class="col px-0">
                <label for="txbProductSearch" class="form-label i18n" data-key="productNameSelect">商品名稱搜尋</label>
                <input type="text" class="form-control i18n" id="txbProductSearch" data-placeholder-key="productNameSelect" placeholder="商品名稱搜尋" />
            </div>
            <div class="col-1 px-0 d-flex justify-content-center align-items-end">
                <button id="btnSearchProduct" type="submit" class="btn btn-outline-primary i18n" data-key="select">查詢</button>
            </div>
            <div class="col-2 px-0 d-flex justify-content-center align-items-end">
                <button id="btnAddProduct" type="submit" class="btn btn-outline-primary i18n" data-key="addProduct">新增商品</button>
            </div>
        </div>

        <div class="row" id="productTableDiv">
            <table id="myTable" class="table table-striped table-hover table-bordered">
                <thead>
                    <tr>
                        <th>
                            <button type="button" class="btn btn-light btn-sm i18n" data-key="id">ID</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm i18n" data-key="name">名稱</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm i18n" data-key="categories">類型</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm i18n" data-key="price">價格</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm i18n" data-key="stock">庫存</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm i18n" data-key="open">開放</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm i18n" data-key="introduce">描述</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm i18n" data-key="img">圖片</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm i18n" data-key="edit">更改</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm i18n" data-key="del">刪除</button></th>

                    </tr>
                </thead>
                <tbody id="tableBody">
                    <!-- 內容 -->
                </tbody>
            </table>
        </div>
        <div class="row">
            <div id="pagination" class="text-center d-flex justify-content-center">
                <ul class="pagination" id="ulPagination">
                    <!-- 分頁按鈕 -->
                </ul>
            </div>
            <span id="labSearchProduct" class="col-12 col-sm-12 text-center text-success"></span>
        </div>
    </div>
 
</body>
</html>
