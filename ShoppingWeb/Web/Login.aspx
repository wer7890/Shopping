<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ShoppingWeb.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>後臺登入</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />  
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="../js/Login.js"></script>
</head>
<body>
    <div class="container">
        <div class="row">
            <h1 class="text-center mt-3">登入頁面</h1>
        </div>
        <hr />
        <div class="row mx-auto col-12 col-md-8">
            <div class="form-group">
                <label for="txbUserName" class="control-label">用戶名:</label>
                <div>
                    <input type="text" id="txbUserName" class="form-control mt-2" placeholder="請輸入用戶名" />
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
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>

</body>
</html>
