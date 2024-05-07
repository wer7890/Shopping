<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="ShoppingWeb.Web.AddUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增帳號</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <script src="/js/<%= jsVersion %>/jquery-3.7.1.min.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= Request.Cookies["language"].Value.ToString() %>.js"></script>
    <script src="/js/<%= jsVersion %>/AddUser.js"></script>
</head>
<body>
    <div class="container">
        <h2 class="text-center"><%= Resources.Resource.titleAddUser %></h2>
        <br />
        <div class="row">
            <div class="mx-auto col-12 col-md-7 mt-2">
                <label for="txbAccount" class="form-label"><%= Resources.Resource.account %></label>
                <input type="text" id="txbAccount" class="form-control" />
            </div>
            <div class="mx-auto col-12 col-md-7 mt-2">
                <label for="txbPwd" class="form-label"><%= Resources.Resource.pwd %></label>
                <input type="password" id="txbPwd" class="form-control" />
            </div>
            <div class="mx-auto mt-3 col-12 col-md-7 mt-2">
                <label for="ddlRoles" class="form-label"><%= Resources.Resource.roles %></label>

                <select id="ddlRoles" class="form-select">
                    <option value="1"><%= Resources.Resource.superAdmin %></option>
                    <option value="2"><%= Resources.Resource.memberAdmin %></option>
                    <option value="3"><%= Resources.Resource.productAdmin %></option>
                </select>
            </div>
            
            <button id="btnAddUser" class="btn btn-outline-primary mx-auto mt-4 col-12 col-md-6"><%= Resources.Resource.addUser %></button>
        </div>
        <br />
        <div class="row">
            <label id="labAddUser" class="col-12 col-sm-12 text-center text-success"></label>
        </div>
    </div>

</body>
</html>
