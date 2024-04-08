<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMember.aspx.cs" Inherits="ShoppingWeb.Web.AddMember" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增會員</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/tw-city-selector@2.1.1/dist/tw-city-selector.min.js"></script>
    <script src="../js/AddMember.js"></script>
</head>
<body>
    <div class="container">
        <h2 class="text-center">新增會員</h2>
        <br />
        <div class="row">
            <div class="mx-auto col-12 col-md-7 mt-2">
                <label for="txbAccount" class="form-label">帳號</label>
                <span id="txbAccount" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <label for="txbPwd" class="form-label">密碼</label>
                <span id="txbPwd" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <label for="txbName" class="form-label">名稱</label>
                <span id="txbName" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <label for="txbBirthday" class="form-label">生日</label>
                <span id="txbBirthday" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <label for="txbPhoneNumber" class="form-label">電話</label>
                <span id="txbPhoneNumber" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <label for="txbEmail" class="form-label">信箱</label>
                <span id="txbEmail" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <label for="txbAddress" class="form-label">地址</label>
                <div role="tw-city-selector" data-bootstrap-style data-has-zipcode></div>
            </div>

            <button id="btnAddMember" class="btn btn-outline-primary mx-auto mt-4 col-12 col-md-6">新增</button>
        </div>
        <br />
        <div class="row">
            <label id="labAddUser" class="col-12 col-sm-12 text-center text-success"></label>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>

</body>
</html>
