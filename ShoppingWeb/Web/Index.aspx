﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ShoppingWeb.Web.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>後臺管理</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="../js/Index.js"></script>

</head>
<body>
    <div class="container mt-5">
        <div class="row mt-2">
            <!--左欄-->
            <div class="col-12 col-md-2">
                <div class="list-group">
                    <a href="javascript:void(0);" class="list-group-item list-group-item-action" id="searchUser">管理員系統</a>
                    <a href="javascript:void(0);" class="list-group-item list-group-item-action" id="searchMember">會員系統</a>
                    <a href="javascript:void(0);" class="list-group-item list-group-item-action" id="searchProduct">商品系統</a>
                    <a href="javascript:void(0);" class="list-group-item list-group-item-action" id="searchOrder">訂單系統</a>
                </div>
                <div class="row mt-2">
                    <label id="labUserRoles" class="fs-5 text-center align-middle mt-2"></label>
                    <br />
                    <button id="btnSignOut" class="btn btn-outline-dark mt-3">登出</button>
                </div>

            </div>

            <!--右欄-->
            <div class="col-12 col-md-10">
                <iframe id="iframeContent" src="SearchUser.aspx" style="width: 100%; height: 100vh; border: none;"></iframe>
            </div>

        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>

</body>
</html>
