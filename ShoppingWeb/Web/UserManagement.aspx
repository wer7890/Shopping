<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="ShoppingWeb.Web.UserManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查詢帳號</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <script src="/js/<%= jsVersion %>/jquery-3.7.1.min.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= Request.Cookies["language"].Value.ToString() %>.js"></script>
    <script src="/js/<%= jsVersion %>/UserManagement.js"></script>
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-10 d-flex justify-content-center">
                <h2 class="text-center"><%= Resources.Resource.titleUser %></h2>
            </div>
            <div class="col-2 d-flex justify-content-center">
                <button id="btnAddUser" type="submit" class="btn btn-outline-primary btn-sm"><%= Resources.Resource.addUser %></button>
            </div>
        </div>
        <br />
        <div class="row">
            <table id="myTable" class="table table-striped table-hover ">
                <thead>
                    <tr>
                        <th>
                            <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.userId %></button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.account %></button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.roles %></button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm" disabled><%= Resources.Resource.edit %></button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm" disabled><%= Resources.Resource.del %></button></th>
                    </tr>
                </thead>
                <tbody id="tableBody">
                    <!-- 內容 -->
                </tbody>
            </table>
        </div>

        <div class="row">
            <div id="pagination" class="text-center d-flex justify-content-center">
                <ul class="pagination" id="ulPagination">
                    <!-- 分頁按鈕 -->
                </ul>
            </div>
            <span id="labSearchUser" class="col-12 col-sm-12 text-center text-success"></span>
        </div>
    </div>

</body>
</html>
