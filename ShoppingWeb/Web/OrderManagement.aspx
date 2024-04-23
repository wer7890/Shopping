﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderManagement.aspx.cs" Inherits="ShoppingWeb.Web.OrderManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查詢訂單</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous" />
    <link rel="stylesheet" type="text/css" href="../css/OrderManagement.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="../js/ProductCategories.js"></script>
    <script src="../js/OrderManagement.js"></script>
</head>
<body>
    <div class="container">
        <h2 class="text-center">訂單</h2>
        <br />
        <div class="row">
            <div class="btn-group me-2" role="group" aria-label="First group">
                <button type="button" class="btn btn-outline-secondary" id="btnDeliveryStatus_0" onclick="SearchAllOrder()">全部(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary" id="btnDeliveryStatus_1" onclick="ShowOrder(1)">發貨中(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary" id="btnDeliveryStatus_2" onclick="ShowOrder(2)">已發貨(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary" id="btnDeliveryStatus_3" onclick="ShowOrder(3)">已到貨(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary" id="btnDeliveryStatus_4" onclick="ShowOrder(4)">已取貨(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary" id="btnDeliveryStatus_5" onclick="ShowOrder(5)">退貨中(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary" id="btnDeliveryStatus_6" onclick="ShowOrder(6)">已退貨(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary" id="btnOrderStatus_2" onclick="ShowReturnOrder()">申請退貨(<span>0</span>)</button>
            </div>
        </div>
        <br />
        <div class="row">
            <table id="myTable" class="table table-striped">
                <thead>
                    <tr>
                        <th>訂單編號</th>
                        <th>訂購者</th>
                        <th>下單時間</th>
                        <th>訂單狀態</th>
                        <th>配送狀態</th>
                        <th>配送方式</th>
                        <th>總金額</th>
                    </tr>
                </thead>
                <tbody id="tableBody">
                    <!-- 內容 -->
                </tbody>
            </table>
            <span id="labSearchOrder" class="col-12 col-sm-12 text-center text-success"></span>
        </div>

        <div id="overlay">
            <div id="box"></div>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>
</body>
</html>
