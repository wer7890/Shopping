<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderManagement.aspx.cs" Inherits="ShoppingWeb.Web.OrderManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查詢訂單</title>
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/OrderManagement.css" />
    <script src="/js/<%= jsVersion %>/jquery-3.7.1.min.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= cookieLanguage %>.js"></script>
    <script src="/js/<%= jsVersion %>/Pagination2.js"></script>
    <script src="/js/<%= jsVersion %>/ProductCategories.js"></script>
    <script src="/js/<%= jsVersion %>/OrderManagement.js"></script>
</head>
<body>
    <div class="container">
        <h2 class="text-center"><%= Resources.Resource.titleOrder %></h2>
        <br />
        <div class="row">
            <div class="btn-group me-2" role="group" aria-label="First group">
                <button type="button" class="btn btn-outline-secondary btnHand" id="btnDeliveryStatus_0"><%= Resources.Resource.all %></button>
                <button type="button" class="btn btn-outline-secondary btnHand" id="btnDeliveryStatus_1"><%= Resources.Resource.shipping %></button>
                <button type="button" class="btn btn-outline-secondary btnHand" id="btnDeliveryStatus_2"><%= Resources.Resource.shipped %></button>
                <button type="button" class="btn btn-outline-secondary btnHand" id="btnDeliveryStatus_3"><%= Resources.Resource.arrived %></button>
                <button type="button" class="btn btn-outline-secondary btnHand" id="btnDeliveryStatus_4"><%= Resources.Resource.received %></button>
                <button type="button" class="btn btn-outline-secondary btnHand" id="btnDeliveryStatus_5"><%= Resources.Resource.returning %></button>
                <button type="button" class="btn btn-outline-secondary btnHand" id="btnDeliveryStatus_6"><%= Resources.Resource.returned %></button>
                <button type="button" class="btn btn-outline-secondary btnHand" id="btnDeliveryStatus_7"><%= Resources.Resource.returnn %></button>
            </div>
        </div>
        <br />
        <div class="row" id="orderTableDiv">
            <table id="myTable" class="table table-striped">
                <thead>
                    <tr>
                        <th><%= Resources.Resource.orderId %></th>
                        <th><%= Resources.Resource.serialNumber %></th>
                        <th><%= Resources.Resource.createdTime %></th>
                        <th><%= Resources.Resource.orderStatus %></th>
                        <th><%= Resources.Resource.deliveryStatus %></th>
                        <th><%= Resources.Resource.deliveryMethod %></th>
                        <th><%= Resources.Resource.total %></th>
                    </tr>
                </thead>
                <tbody id="tableBody">
                    <!-- 內容 -->
                </tbody>
            </table>          
        </div>

        <div class="row" id="paginationDiv">
            <div id="pagination" class="text-center d-flex justify-content-center col-12 col-sm-12">
                <ul class="pagination" id="ulPagination">
                    <!-- 分頁按鈕 -->
                </ul>
            </div>
            <div id="paginationInfo" class="text-center text-center d-flex justify-content-center col-4 mx-auto">
                <!-- 動態生成頁數選項，總頁數，筆數選項 -->
            </div>            
        </div>

        <div class="row">
            <span id="labSearchOrder" class="col-12 col-sm-12 text-center text-success"></span>
        </div>

        <div id="overlay">
            <div id="box"></div>
        </div>
    </div>
</body>
</html>
