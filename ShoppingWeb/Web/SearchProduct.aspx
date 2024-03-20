<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchProduct.aspx.cs" Inherits="ShoppingWeb.Web.SearchProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查詢商品</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="../js/SearchProduct.js"></script>
</head>
<body>
    <div class="w-auto mx-3">
        <h2 class="text-center">查詢商品</h2>
        <br />
        <div class="row mx-auto col-12 mb-3">
            <div class="col">
                <select id="productCategory" class="form-select">
                    <option value="">請選擇要搜尋的商品類型</option>
                    <option value="1">帽子</option>
                    <option value="2">上衣</option>
                    <option value="3">外套</option>
                    <option value="4">褲子</option>
                </select>
            </div>
            <div class="col">
                <label for="txbProductSearch" class="visually-hidden">商品名稱搜尋</label>
                <input type="text" class="form-control" id="txbProductSearch" placeholder="商品名稱搜尋" />
            </div>            
            <div class="col-2 d-flex justify-content-center">
                <button id="btnSearchProduct" type="submit" class="btn btn-outline-primary">查詢</button>
            </div>
            <div class="col-2 d-flex justify-content-center">
                <button id="btnAddProduct" type="submit" class="btn btn-outline-primary">新增商品</button>
            </div>
        </div>

        <div class="row" id="productTableDiv">
            <table id="myTable" class="table table-striped table-hover table-bordered">
                <thead>
                    <tr>
                        <th>
                            <button type="button" class="btn btn-light btn-sm ">ID</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm ">名稱</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm ">類型</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm ">價格</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm ">庫存</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm ">開放</button></th>
                        <%--<th>
                            <button type="button" class="btn btn-light btn-sm ">建立者</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm ">建立時間</button></th>--%>
                        <th>
                            <button type="button" class="btn btn-light btn-sm ">描述</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm ">圖片</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm ">編輯</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm ">刪除</button></th>

                    </tr>
                </thead>
                <tbody id="tableBody">
                    <!-- 內容 -->
                </tbody>
            </table>
        </div>
        <div class="row mx-auto">
            <span id="labSearchProduct" class="col-12 col-sm-12 text-center text-success"></span>
        </div>
        
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>
</body>
</html>
