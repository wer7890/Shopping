<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ShoppingWeb.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>後臺登入</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">
</head>
<body>
    <div class="container">
        <div class="row">
            <h1 class="text-center mt-3">登入頁面</h1>
        </div>
        <hr />
        <div class="row">
            <form id="form1" runat="server" class="mx-auto col-12 col-md-8 ">
                <div class="form-group">
                    <label for="username" class="control-label">用戶名:</label>
                    <div >
                        <asp:TextBox ID="txbUserName" runat="server" CssClass="form-control" placeholder="請輸入用戶名"></asp:TextBox>
                    </div>
                </div>

                <br />
                <div class="form-group">
                    <label for="password" class="control-label">密碼:</label>
                    <div>
                        <asp:TextBox ID="txbPassword" type="password" runat="server" CssClass="form-control" placeholder="請輸入密碼"></asp:TextBox>
                    </div>
                </div>

                <br />
                <div class="row justify-content-center align-self-center">
                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-outline-secondary btn-lg col-md-offset-3 col-md-6 " Text="登入" OnClick="btnLogin_Click" />
                </div>
            </form>
        </div>

        <br />
        <div class="row">
            <asp:Label ID="labLogin" runat="server" Text="" Font-Size="21px" CssClass="col-12 col-sm-12 text-center text-success"></asp:Label>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>

</body>
</html>
