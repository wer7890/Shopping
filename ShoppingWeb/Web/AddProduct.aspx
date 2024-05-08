<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="ShoppingWeb.Web.AddProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增商品</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <script src="/js/<%= jsVersion %>/jquery-3.7.1.min.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= cookieLanguage %>.js"></script>
    <script src="/js/<%= jsVersion %>/ProductCategories.js"></script>
    <script src="/js/<%= jsVersion %>/AddProduct.js"></script>
</head>
<body>
    <div class="container">
        <h2 class="text-center"><%= Resources.Resource.addProduct %></h2>
        <br />
        <div class="row">
            <div class="mx-auto col-12 col-md-7 mt-2">
                <label for="txbProductName" class="form-label"><%= Resources.Resource.productNameTW %></label>
                <input type="text" id="txbProductName" class="form-control" />
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <label for="txbProductNameEN" class="form-label"><%= Resources.Resource.productNameEN %></label>
                <input type="text" id="txbProductNameEN" class="form-control" />
            </div>
            <div class="row mx-auto col-12 col-md-7 mt-3">

                <div class="col px-0" id="divCategories">
                </div>
                <div class="col" id="divMinorCategory">
                    <label for="minorCategory" class="form-label"><%= Resources.Resource.minorCategories %></label>
                    <select id="minorCategory" class="form-select">
                        <option value=""><%= Resources.Resource.chooseType %></option>
                    </select>
                </div>
                <div class="col px-0" id="divBrand">
                </div>

            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbProductImg" class="form-label"><%= Resources.Resource.productImg %></label>
                <input type="file" id="txbProductImg" class="form-control" />
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbProductPrice" class="form-label"><%= Resources.Resource.price %></label>
                <input type="number" id="txbProductPrice" class="form-control" />
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbProductStock" class="form-label"><%= Resources.Resource.stock %></label>
                <input type="number" id="txbProductStock" class="form-control" />
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="productIsOpen" class="form-label"><%= Resources.Resource.isOpen %></label>
                <select id="productIsOpen" class="form-select">
                    <option value="0"><%= Resources.Resource.no %></option>
                    <option value="1"><%= Resources.Resource.yes %></option>
                </select>
            </div>

            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbProductIntroduce" class="form-label"><%= Resources.Resource.productIntroduceTW %></label>
                <textarea rows="3" class="form-control" id="txbProductIntroduce"></textarea>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbProductIntroduceEN" class="form-label"><%= Resources.Resource.productIntroduceEN %></label>
                <textarea rows="3" class="form-control" id="txbProductIntroduceEN"></textarea>
            </div>

            <button id="btnAddProduct" class="btn btn-outline-primary mx-auto mt-4 col-12 col-md-6"><%= Resources.Resource.addProduct %></button>
        </div>
        <br />
        <div class="row">
            <label id="labAddProduct" class="col-12 col-sm-12 text-center text-success"></label>
        </div>
    </div>

</body>
</html>
