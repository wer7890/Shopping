<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="ShoppingWeb.Web.EditProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改商品</title>
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= Request.Cookies["language"].Value.ToString() %>.js"></script>
    <script src="/js/<%= jsVersion %>/ProductCategories.js"></script>
    <script src="/js/<%= jsVersion %>/EditProduct.js"></script>
</head>
<body>
    <div class="container">
        <h2 class="text-center"><%= Resources.Resource.editProduct %></h2>
        <br />
        <div class="row">
            <div class="mx-auto col-12 col-md-7">
                <span class="text-dark fs-6"><%= Resources.Resource.productId %> : </span>
                <span id="labProductId" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <span class="text-dark fs-6"><%= Resources.Resource.createdTime %> : </span>
                <span id="labProductCreatedOn" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <span class="text-dark fs-6"><%= Resources.Resource.createdUser %> : </span>
                <span id="labProductOwner" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <span class="text-dark fs-6"><%= Resources.Resource.productNameTW %> : </span>
                <span id="labProductName" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <span class="text-dark fs-6"><%= Resources.Resource.productNameEN %> : </span>
                <span id="labProductNameEN" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <span class="text-dark fs-6"><%= Resources.Resource.productType %> : </span>
                <span id="labProductCategory" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <span class="text-dark fs-6"><%= Resources.Resource.productStock %> : </span>
                <span id="labProductStock" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <img src="" id="imgProduct" class="img-fluid img-thumbnail w-25 i18n" alt="商品圖片" />
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbProductPrice" class="form-label i18n" data-key="productPrice"><%= Resources.Resource.productPrice %></label>
                <input type="number" id="txbProductPrice" class="form-control" />
            </div>
            <div class="row mx-auto col-12 col-md-7 mt-3">
                <div class="col-12 col-md-3 mt-3">
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault1" value="1" checked/>
                        <label class="form-check-label" for="flexRadioDefault1">
                            <%= Resources.Resource.addStock %>
                        </label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault2" value="0" />
                        <label class="form-check-label" for="flexRadioDefault2">
                            <%= Resources.Resource.reduceStock %>
                        </label>
                    </div>
                </div>
                <div class="col-12 col-md-9">
                    <label for="txbProductStock" class="form-label"></label>
                    <input type="number" id="txbProductStock" class="form-control" />
                </div>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-4">
                <label for="txbProductIntroduce" class="form-label"><%= Resources.Resource.productIntroduceTW %></label>
                <textarea rows="3" class="form-control" id="txbProductIntroduce"></textarea>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-4">
                <label for="txbProductIntroduceEN" class="form-label"><%= Resources.Resource.productIntroduceEN %></label>
                <textarea rows="3" class="form-control" id="txbProductIntroduceEN"></textarea>
            </div>

            <button id="btnRenewProduct" class="btn btn-outline-primary mx-auto mt-4 col-12 col-md-6"><%= Resources.Resource.edit %></button>
        </div>
        <br />
        <div class="row">
            <label id="labRenewProduct" class="col-12 col-sm-12 text-center text-success"></label>
        </div>
    </div>

</body>
</html>
