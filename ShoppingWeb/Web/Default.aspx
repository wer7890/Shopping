<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShoppingWeb.Web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>預設畫面</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />

    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/Default.js"></script>
</head>
<body>
    <div class="container mt-2">
        <h2 class="text-center i18n" data-key="title1Default">歡迎登入後台系統</h2>
    </div>

</body>
</html>
