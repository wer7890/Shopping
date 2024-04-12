<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderManagement.aspx.cs" Inherits="ShoppingWeb.Web.OrderManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查詢訂單</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="../js/OrderManagement.js"></script>
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-10 d-flex justify-content-center">
                <h2 class="text-center">訂單</h2>
            </div>
            <div class="col-2 d-flex justify-content-center">
                <button id="btnAddUser" type="submit" class="btn btn-outline-primary">新增訂單</button>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="btn-group me-2" role="group" aria-label="First group">
                <button type="button" class="btn btn-outline-secondary">全部(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary">備貨中(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary">發貨中(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary">已發貨(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary">已到達(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary">已取貨(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary">已退回(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary">退回中(<span>0</span>)</button>
            </div>
        </div>
        <br />
        <div class="row">
            <table id="myTable" class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th>訂單編號</th>
                        <th>訂購人</th>
                        <th>訂單時間</th>
                        <th>訂單狀態</th>
                        <th>付款狀態</th>
                        <th>配送狀態</th>
                        <th>配送方式</th>
                        <th>總金額</th>
                    </tr>
                </thead>
                <tbody id="tableBody">
                    <!-- 內容 -->
                </tbody>
            </table>
        </div>


    </div>


    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>
</body>
</html>
