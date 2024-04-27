<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ShoppingWeb.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>後臺登入</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>

    <link id="btCssLink" rel="stylesheet" type="text/css" />
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/Login.js"></script>

</head>
<body>
    <div class="container">
        <div class="row">
            <h1 class="text-center mt-3">登入頁面</h1>
        </div>
        <hr />
        <div class="row mx-auto col-12 col-md-5">
            <div class="form-group">
                <label for="txbAccount" class="control-label">帳號:</label>
                <div>
                    <input type="text" id="txbAccount" class="form-control mt-2" placeholder="請輸入帳號" />
                </div>
            </div>

            <br />
            <div class="form-group mt-3">
                <label for="txbPassword" class="control-label">密碼:</label>
                <div>
                    <input type="password" id="txbPassword" class="form-control mt-2" placeholder="請輸入密碼" />
                </div>
            </div>

            <br />
            <div class="row justify-content-center align-self-center mt-3">
                <button id="btnLogin" class="btn btn-outline-secondary btn-lg col-md-offset-3 col-md-6">登入</button>
            </div>
        </div>
        <br />
        <div class="row">
            <label id="labLogin" class="col-12 col-sm-12 text-center text-success"></label>
        </div>
    </div>

</body>
</html>
