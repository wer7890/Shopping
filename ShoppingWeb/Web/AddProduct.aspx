﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="ShoppingWeb.Web.AddProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增商品</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="../js/ProductCategories.js"></script>
    <script src="../js/AddProduct.js"></script>
</head>
<body>
    <div class="container">
        <h2 class="text-center">新增商品</h2>
        <br />
        <div class="row">
            <div class="mx-auto col-12 col-md-7 mt-2">
                <label for="txbProductName" class="form-label">商品名稱</label>
                <input type="text" id="txbProductName" class="form-control" />
            </div>
            <div class="row mx-auto col-12 col-md-7 mt-3">

                <div class="col px-0" id="divCategories">
                </div>
                <div class="col" id="divMinorCategory">
                    <label for="minorCategory" class="form-label">子類型</label>
                    <select id="minorCategory" class="form-select">
                        <option value="">請先選擇類型</option>
                    </select>
                </div>
                <div class="col px-0" id="divBrand">
                </div>

            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbProductImg" class="form-label">商品圖示</label>
                <input type="file" id="txbProductImg" class="form-control" />
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbProductPrice" class="form-label">價格</label>
                <input type="number" id="txbProductPrice" class="form-control" />
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbProductStock" class="form-label">庫存量</label>
                <input type="number" id="txbProductStock" class="form-control" />
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="productIsOpen" class="form-label">是否開放</label>
                <select id="productIsOpen" class="form-select">
                    <option value="0">否</option>
                    <option value="1">是</option>
                </select>
            </div>

            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbProductIntroduce" class="form-label">商品細項描述</label>
                <textarea rows="3" class="form-control" id="txbProductIntroduce"></textarea>
            </div>

            <button id="btnAddProduct" class="btn btn-outline-primary mx-auto mt-4 col-12 col-md-6">新增</button>
        </div>
        <br />
        <div class="row">
            <label id="labAddProduct" class="col-12 col-sm-12 text-center text-success"></label>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>
</body>
</html>
