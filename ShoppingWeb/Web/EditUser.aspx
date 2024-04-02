<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="ShoppingWeb.Web.EditUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改帳號</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="../js/EditUser.js"></script>
</head>
<body>
    <div class="container">
        <h2 class="text-center">修改帳號</h2>
        <br />
        <div class="row">
            <div class="mx-auto col-12 col-md-7 mt-2">
                <span class="text-dark fs-6">管理員ID : </span>
                <span id="labUserId" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <span class="text-dark fs-6">帳號 : </span>
                <span id="labAccount" class="fs-6"></span>
            </div>
            <div class="mx-auto mt-3 col-12 col-md-7 mt-3">
                <span class="text-dark fs-6">角色 : </span>
                <span id="labUserRoles" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbPwd" class="form-label">密碼</label>
                <input type="password" id="txbPwd" class="form-control" />
            </div>

            <button id="btnUpData" class="btn btn-outline-primary mx-auto mt-3 col-12 col-md-6">更改</button>
        </div>
        <br />
        <div class="row">
            <span id="labRenewUser" class="col-12 col-sm-12 text-center text-success"></span>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>

</body>
</html>
