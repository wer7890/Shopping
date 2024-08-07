﻿﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frame.aspx.cs" Inherits="ShoppingWeb.Web.Frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>後臺管理</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/PopupWindow.css" />
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/Pagination.css" />
    <script src="/js/<%= jsVersion %>/jquery-3.7.1.min.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/vue.min.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= cookieLanguage %>.js"></script>
    <script src="/js/<%= jsVersion %>/SetNLog.js"></script>
</head>
<body>
    <div id="app"></div>

    <script src="/js/<%= jsVersion %>/sw/components/frame/components/TableComponent.js"></script>
    <script src="/js/<%= jsVersion %>/sw/components/frame/components/PaginationComponent.js"></script>
    <script src="/js/<%= jsVersion %>/sw/components/frame/components/PopWindowComponent.js"></script>
    <script src="/js/<%= jsVersion %>/sw/components/frame/Order/CheckOrderComponent.js"></script>
    <script src="/js/<%= jsVersion %>/sw/components/frame/Product/WarnComponent.js"></script>
    <script src="/js/<%= jsVersion %>/sw/components/frame/Product/AddProductComponent.js"></script>
    <script src="/js/<%= jsVersion %>/sw/components/frame/Product/EditProductComponent.js"></script>
    <script src="/js/<%= jsVersion %>/sw/components/frame/User/AddUserComponent.js"></script>
    <script src="/js/<%= jsVersion %>/sw/components/frame/User/EditUserComponent.js"></script>
    <script src="/js/<%= jsVersion %>/sw/components/frame/DefaultComponent.js"></script>
    <script src="/js/<%= jsVersion %>/sw/components/frame/MemberComponent.js"></script>
    <script src="/js/<%= jsVersion %>/sw/components/frame/OrderComponent.js"></script>
    <script src="/js/<%= jsVersion %>/sw/components/frame/ProductComponent.js"></script>
    <script src="/js/<%= jsVersion %>/sw/components/frame/UserComponent.js"></script>
    <script src="/js/<%= jsVersion %>/sw/components/FrameComponent.js"></script>
    <script src="/js/<%= jsVersion %>/sw/components/MenuComponent.js"></script>
    <script src="/js/<%= jsVersion %>/sw/App.js"></script>
</body>
</html>
