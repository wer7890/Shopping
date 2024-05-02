<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="ShoppingWeb.Web.AddUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增帳號</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/I18n.js"></script>
    <script src="/js/<%= jsVersion %>/<%= basePageLanguage %>/AddUser.js"></script>
    <script src="/js/<%= jsVersion %>/AddUser.js"></script>
</head>
<body>
    <div class="container">
        <h2 class="text-center i18n" data-key="titleAddUser">新增帳號</h2>
        <br />
        <div class="row">
            <div class="mx-auto col-12 col-md-7 mt-2">
                <label for="txbAccount" class="form-label i18n" data-key="labAccount">帳號</label>
                <input type="text" id="txbAccount" class="form-control" />
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <label for="txbPwd" class="form-label i18n" data-key="labPwd">密碼</label>
                <input type="password" id="txbPwd" class="form-control" />
            </div>
            <div class="mx-auto mt-3 col-12 col-md-7 mt-2">
                <label for="ddlRoles" class="form-label i18n" data-key="labRoles">角色</label>

                <select id="ddlRoles" class="form-select">
                    <option value="1" class="i18n" data-key="superAdmin">超級管理員</option>
                    <option value="2" class="i18n" data-key="memberAdmin">會員管理員</option>
                    <option value="3" class="i18n" data-key="productAdmin">商品管理員</option>
                </select>
            </div>
            
            <button id="btnAddUser" class="btn btn-outline-primary mx-auto mt-4 col-12 col-md-6 i18n" data-key="btnAddUser">新增</button>
        </div>
        <br />
        <div class="row">
            <label id="labAddUser" class="col-12 col-sm-12 text-center text-success"></label>
        </div>
    </div>

</body>
</html>
