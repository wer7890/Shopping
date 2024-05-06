<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ShoppingWeb.Web.Login"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>後臺登入</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>

    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= Request.Cookies["language"].Value.ToString() %>.js"></script>
    <script src="/js/<%= jsVersion %>/Login.js"></script>

</head>
<body>
    <div class="container">
        <div class="row">
            <h1 class="text-center mt-3"><%= Resources.Resource.titleLogin %></h1>
        </div>
        <hr />
        <div class="row mx-auto col-12 col-md-5">
            <div class="form-group">
                <label for="txbAccount" class="control-label"><%= Resources.Resource.account %>:</label>
                <div>
                    <input type="text" id="txbAccount" class="form-control mt-2" placeholder="<%= Resources.Resource.txbAccount %>" />
                </div>
            </div>

            <br />
            <div class="form-group mt-3">
                <label for="txbPassword" class="control-label"><%= Resources.Resource.pwd %>:</label>
                <div>
                    <input type="password" id="txbPassword" class="form-control mt-2" placeholder="<%= Resources.Resource.txbPassword %>" />
                </div>
            </div>

            <br />
            <div class="row justify-content-center align-self-center mt-3">
                <button id="btnLogin" class="btn btn-outline-primary btn-lg col-md-offset-3 col-md-6"><%= Resources.Resource.btnLogin %></button>
            </div>

            <div class="row justify-content-center align-self-center mt-5">
                <button id="btnChinese" class="btn btn-outline-secondary btn-lg col-md-offset-3 col-md-2 fs-6 btn-sm" onclick="ChangeLanguage('TW')">中文</button>
                <button id="btnEnglish" class="btn btn-outline-secondary btn-lg col-md-offset-3 col-md-2 fs-6 btn-sm" onclick="ChangeLanguage('EN')">English</button>
            </div>
        </div>
        <br />
        <div class="row">
            <label id="labLogin" class="col-12 col-sm-12 text-center text-success"></label>
        </div>

    </div>

</body>
</html>
