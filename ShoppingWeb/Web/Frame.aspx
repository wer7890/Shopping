﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frame.aspx.cs" Inherits="ShoppingWeb.Web.Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>後臺管理</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <script src="/js/<%= jsVersion %>/jquery-3.7.1.min.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= cookieLanguage %>.js"></script>
    <script src="/js/<%= jsVersion %>/Frame.js"></script>
</head>
<body>
    <div class="container mt-5">
        <div class="row mt-2">
            <!--左欄-->
            <div class="col-12 col-md-2">
                <div class="list-group">
                    <a href="javascript:void(0);" class="list-group-item list-group-item-action" id="adminPanel"><%= Resources.Resource.adminSystem %></a>
                    <a href="javascript:void(0);" class="list-group-item list-group-item-action" id="memberPanel"><%= Resources.Resource.memberSystem %></a>
                    <a href="javascript:void(0);" class="list-group-item list-group-item-action" id="productPanel"><%= Resources.Resource.productSystem %></a>
                    <a href="javascript:void(0);" class="list-group-item list-group-item-action" id="orderPanel"><%= Resources.Resource.orderSystem %></a>
                </div>
                <div class="row mt-2">
                    <label id="labUserAccount" class="fs-5 text-center align-middle mt-2"><%= Resources.Resource.account %> : </label>
                    <br />
                    <button id="btnSignOut" class="btn btn-outline-dark mt-3"><%= Resources.Resource.signOut %></button>
                </div>

                <div class="row justify-content-center align-self-center mt-5">
                    <button id="btnChinese" class="btn btn-outline-secondary btn-lg col fs-6 btn-sm" onclick="ChangeLanguage('TW')">中文</button>
                    <button id="btnEnglish" class="btn btn-outline-secondary btn-lg col fs-6 btn-sm" onclick="ChangeLanguage('EN')">English</button>
                </div>

            </div>

            <!--右欄-->
            <div class="col-12 col-md-10">
                <iframe id="iframeContent" src="Default.aspx" style="width: 100%; height: 90vh; border: none;"></iframe>
            </div>

        </div>
    </div>

</body>
</html>
