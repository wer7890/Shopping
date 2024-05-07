<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="ShoppingWeb.Web.EditUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改帳號</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <script src="/js/<%= jsVersion %>/jquery-3.7.1.min.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= Request.Cookies["language"].Value.ToString() %>.js"></script>
    <script src="/js/<%= jsVersion %>/EditUser.js"></script>
</head>
<body>
    <div class="container">
        <h2 class="text-center"><%= Resources.Resource.titleEditUser %></h2>
        <br />
        <div class="row">
            <div class="mx-auto col-12 col-md-7 mt-2">
                <span class="text-dark fs-6"><%= Resources.Resource.userId %> : </span>
                <span id="labUserId" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <span class="text-dark fs-6"><%= Resources.Resource.account %> : </span>
                <span id="labAccount" class="fs-6"></span>
            </div>
            <div class="mx-auto mt-3 col-12 col-md-7 mt-3">
                <span class="text-dark fs-6"><%= Resources.Resource.roles %> : </span>
                <span id="labUserRoles" class="fs-6"></span>
            </div>
            <div class="mx-auto col-12 col-md-7 mt-3">
                <label for="txbPwd" class="form-label"><%= Resources.Resource.pwd %></label>
                <input type="password" id="txbPwd" class="form-control" />
            </div>

            <button id="btnUpData" class="btn btn-outline-primary mx-auto mt-3 col-12 col-md-6"><%= Resources.Resource.edit %></button>
        </div>
        <br />
        <div class="row">
            <span id="labRenewUser" class="col-12 col-sm-12 text-center text-success"></span>
        </div>
    </div>

</body>
</html>
