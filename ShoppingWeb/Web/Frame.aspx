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
    <script src="/js/<%= jsVersion %>/vue.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= cookieLanguage %>.js"></script>
    <script src="/js/<%= jsVersion %>/SetNLog.js"></script>
</head>
<body>
    <div id="app"></div>

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
