<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RenewUser.aspx.cs" Inherits="ShoppingWeb.Web.RenewUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改帳號</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />  
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="../js/RenewUser.js"></script>
</head>
<body>
    <div class="container">
        <h2 class="text-center">修改帳號</h2>
        <br />
        <div class="row">
             <div class="mx-auto col-12 col-md-8 mt-2">
                <label for="txbUserId" class="form-label">管理員ID</label>
                 <label id="labUserId" class="form-control"></label>

            </div>
            <div class="mx-auto col-12 col-md-8 mt-2">
                <label for="txbUserName" class="form-label">管理員名稱</label>
                <input type="text" id="txbUserName" class="form-control" />
            </div>
            <div class="mx-auto col-12 col-md-8 mt-2">
                <label for="txbPwd" class="form-label">密碼</label>
                <input type="password" id="txbPwd" class="form-control" />
            </div>
            <div class="mx-auto mt-3 col-12 col-md-8 mt-2">
                <label for="ddlRoles" class="form-label">角色</label>

                <select id="ddlRoles" class="form-select">
                    <option value="1">超級管理員</option>
                    <option value="2">會員管理員</option>
                    <option value="3">商品管理員</option>
                </select>
            </div>

            <button id="btnUpData" class="btn btn-outline-primary mx-auto mt-3 col-12 col-md-5">更改</button>
        </div>
        <br />
        <div class="row">
            <label id="labRenewUser" class="col-12 col-sm-12 text-center text-success"></label>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>

</body>
</html>
