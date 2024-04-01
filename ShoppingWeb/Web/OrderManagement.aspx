﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderManagement.aspx.cs" Inherits="ShoppingWeb.Web.OrderManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
            <button type="button" class="btn btn-outline-dark btn-sm col mx-2">
                全部(<span>0</span>)
            </button>
            <button type="button" class="btn btn-outline-primary btn-sm col mx-2">
                處理中(<span>0</span>)
            </button>
            <button type="button" class="btn btn-outline-primary btn-sm col mx-2">
                已確認(<span>0</span>)
            </button>
            <button type="button" class="btn btn-outline-primary btn-sm col mx-2">
                已完成(<span>0</span>)
            </button>
            <button type="button" class="btn btn-outline-primary btn-sm col mx-2">
                已取消(<span>0</span>)
            </button>
            <button type="button" class="btn btn-outline-primary btn-sm col mx-2">
                備貨中(<span>0</span>)
            </button>
            <button type="button" class="btn btn-outline-primary btn-sm col mx-2">
                已發貨(<span>0</span>)
            </button>
            <button type="button" class="btn btn-outline-primary btn-sm col mx-2">
                已到達(<span>0</span>)
            </button>
            <button type="button" class="btn btn-outline-primary btn-sm col mx-2">
                已取貨(<span>0</span>)
            </button>
            <button type="button" class="btn btn-outline-primary btn-sm col mx-2">
                已退貨(<span>0</span>)
            </button>
        </div>
        <br />
        <div class="row">
            <table id="myTable" class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th>編號</th>
                        <th>會員ID</th>
                        <th>建立時間</th>
                        <th>到貨日期</th>
                        <th>狀態</th>
                        <th>收貨地址</th>
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