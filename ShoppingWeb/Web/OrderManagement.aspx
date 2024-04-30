<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderManagement.aspx.cs" Inherits="ShoppingWeb.Web.OrderManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查詢訂單</title>
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/OrderManagement.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/I18n.js"></script>
    <script src="/js/<%= jsVersion %>/ProductCategories.js"></script>
    <script src="/js/<%= jsVersion %>/OrderManagement_<%= basePageLanguage %>.js"></script>
</head>
<body>
    <div class="container">
        <h2 class="text-center i18n" data-key="titleOrder">訂單</h2>
        <br />
        <div class="row">
            <div class="btn-group me-2" role="group" aria-label="First group">
                <button type="button" class="btn btn-outline-secondary i18n" id="btnDeliveryStatus_0" onclick="SearchAllOrder()" data-key="thAll">全部(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary i18n" id="btnDeliveryStatus_1" onclick="ShowOrder(1)" data-key="thShipping">發貨中(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary i18n" id="btnDeliveryStatus_2" onclick="ShowOrder(2)" data-key="thShipped">已發貨(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary i18n" id="btnDeliveryStatus_3" onclick="ShowOrder(3)" data-key="thArrived">已到貨(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary i18n" id="btnDeliveryStatus_4" onclick="ShowOrder(4)" data-key="thReceived">已取貨(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary i18n" id="btnDeliveryStatus_5" onclick="ShowOrder(5)" data-key="thReturning">退貨中(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary i18n" id="btnDeliveryStatus_6" onclick="ShowOrder(6)" data-key="thReturned">已退貨(<span>0</span>)</button>
                <button type="button" class="btn btn-outline-secondary i18n" id="btnOrderStatus_2" onclick="ShowReturnOrder()" data-key="thReturnRequested">申請退貨(<span>0</span>)</button>
            </div>
        </div>
        <br />
        <div class="row">
            <table id="myTable" class="table table-striped">
                <thead>
                    <tr>
                        <th class="i18n" data-key="thId">訂單編號</th>
                        <th class="i18n" data-key="thSerialNumber">訂購者</th>
                        <th class="i18n" data-key="thCreatedTime">下單時間</th>
                        <th class="i18n" data-key="thOrderStatus">訂單狀態</th>
                        <th class="i18n" data-key="thDeliveryStatus">配送狀態</th>
                        <th class="i18n" data-key="thDeliveryMethod">配送方式</th>
                        <th class="i18n" data-key="thTotal">總金額</th>
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
</body>
</html>
