<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="ShoppingWeb.Web.AddProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增商品</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/I18n.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= Request.Cookies["language"].Value.ToString() %>.js"></script>
    <script src="/js/<%= jsVersion %>/ProductCategories.js"></script>
    <script src="/js/<%= jsVersion %>/AddProduct.js"></script>
</head>
<body>
    <div class="container">
        <h2 class="text-center i18n" data-key="addProduct">新增商品</h2>
        <br />
        <div class="row">
            <div class="mx-auto col-12 col-md-7 mt-2">
                <label for="txbProductName" class="form-label i18n" data-key="productNameTW">商品中文名稱</label>
                <input type="text" id="txbProductName" class="form-control" />
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <label for="txbProductNameEN" class="form-label i18n" data-key="productNameEN">商品英文名稱</label>
                <input type="text" id="txbProductNameEN" class="form-control" />
            </div>
            <div class="row mx-auto col-12 col-md-7 mt-3">

                <div class="col px-0" id="divCategories">
                </div>
                <div class="col" id="divMinorCategory">
                    <label for="minorCategory" class="form-label i18n" data-key="minorCategories">子類型</label>
                    <select id="minorCategory" class="form-select">
                        <option value="" class="i18n" data-key="chooseType">請先選擇類型</option>
                    </select>
                </div>
                <div class="col px-0" id="divBrand">
                </div>

            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbProductImg" class="form-label i18n" data-key="productImg">商品圖示</label>
                <input type="file" id="txbProductImg" class="form-control" />
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbProductPrice" class="form-label i18n" data-key="price">價格</label>
                <input type="number" id="txbProductPrice" class="form-control" />
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbProductStock" class="form-label i18n" data-key="stock">庫存量</label>
                <input type="number" id="txbProductStock" class="form-control" />
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="productIsOpen" class="form-label i18n" data-key="isOpen">是否開放</label>
                <select id="productIsOpen" class="form-select">
                    <option value="0" class="i18n" data-key="no">否</option>
                    <option value="1" class="i18n" data-key="yes">是</option>
                </select>
            </div>

            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbProductIntroduce" class="form-label i18n" data-key="productIntroduceTW">商品中文描述</label>
                <textarea rows="3" class="form-control" id="txbProductIntroduce"></textarea>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbProductIntroduceEN" class="form-label i18n" data-key="productIntroduceEN">商品英文描述</label>
                <textarea rows="3" class="form-control" id="txbProductIntroduceEN"></textarea>
            </div>

            <button id="btnAddProduct" class="btn btn-outline-primary mx-auto mt-4 col-12 col-md-6 i18n" data-key="addProduct">新增商品</button>
        </div>
        <br />
        <div class="row">
            <label id="labAddProduct" class="col-12 col-sm-12 text-center text-success"></label>
        </div>
    </div>

</body>
</html>
