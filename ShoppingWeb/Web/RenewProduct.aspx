<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RenewProduct.aspx.cs" Inherits="ShoppingWeb.Web.RenewProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改商品</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="../js/RenewProduct.js"></script>
</head>
<body>
    <div class="container">
        <h2 class="text-center">修改商品</h2>
        <br />
        <div class="row">
            <div class="mx-auto col-12 col-md-7 mt-2">
                <span class="text-dark fs-5">商品ID : </span>
                <span id="labProductId" class="text-muted fs-5"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <span class="text-dark fs-5">建立時間 : </span>
                <span id="labProductCreatedOn" class="text-muted fs-5"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <span class="text-dark fs-5">建立者 : </span>
                <span id="labProductOwner" class="text-muted fs-5"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <span class="text-dark fs-5">商品名稱 : </span>
                <span id="labProductName" class="text-muted fs-5"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <span class="text-dark fs-5">商品類型 : </span>
                <span id="labProductCategory" class="text-muted fs-5"></span>
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
                <label for="txbProductIntroduce" class="form-label">商品細項描述</label>
                <textarea rows="3" class="form-control" id="txbProductIntroduce" ></textarea>
            </div>
            
             <button id="btnRenewProduct" class="btn btn-outline-primary mx-auto mt-4 col-12 col-md-6">修改</button>
        </div>
        <br />
        <div class="row">
            <label id="labRenewProduct" class="col-12 col-sm-12 text-center text-success"></label>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>

</body>
</html>
