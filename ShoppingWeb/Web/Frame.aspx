﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frame.aspx.cs" Inherits="ShoppingWeb.Web.Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>後臺管理</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>

    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/I18n.js"></script>
    <script src="/js/<%= jsVersion %>/<%= basePageLanguage %>/Frame.js"></script>

</head>
<body>
    <div class="container mt-5">
        <div class="row mt-2">
            <!--左欄-->
            <div class="col-12 col-md-2">
                <div class="list-group">
                    <a href="javascript:void(0);" class="list-group-item list-group-item-action i18n" id="adminPanel" data-key="adminPanel">管理員系統</a>
                    <a href="javascript:void(0);" class="list-group-item list-group-item-action i18n" id="memberPanel" data-key="memberPanel">會員系統</a>
                    <a href="javascript:void(0);" class="list-group-item list-group-item-action i18n" id="productPanel" data-key="productPanel">商品系統</a>
                    <a href="javascript:void(0);" class="list-group-item list-group-item-action i18n" id="orderPanel" data-key="orderPanel">訂單系統</a>
                </div>
                <div class="row mt-2">
                    <label id="labUserAccount" class="fs-5 text-center align-middle mt-2 i18n" data-key="labUserAccount">帳號: </label>
                    <br />
                    <button id="btnSignOut" class="btn btn-outline-dark mt-3 i18n" data-key="btnSignOut">登出</button>
                </div>

                <div class="row justify-content-center align-self-center mt-5">
                    <button id="btnChinese" class="btn btn-outline-secondary btn-lg col fs-6 btn-sm" onclick="ChangeLanguage('zh')">中文</button>
                    <button id="btnEnglish" class="btn btn-outline-secondary btn-lg col fs-6 btn-sm" onclick="ChangeLanguage('en')">English</button>
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
