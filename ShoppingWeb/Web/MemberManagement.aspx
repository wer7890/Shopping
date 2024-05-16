<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberManagement.aspx.cs" Inherits="ShoppingWeb.Web.MemberManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查詢會員</title>
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/Pagination.css" />
    <script src="/js/<%= jsVersion %>/jquery-3.7.1.min.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= cookieLanguage %>.js"></script>
    <script src="/js/<%= jsVersion %>/Pagination.js"></script>
    <script src="/js/<%= jsVersion %>/MemberManagement.js"></script>
</head>
<body>
    <div class="w-auto mx-3">
        <div class="row">
            <div class="col-10 d-flex justify-content-center">
                <h2 class="text-center"><%= Resources.Resource.titleMember %></h2>
            </div>
            <div class="col-2 d-flex justify-content-center">
                <button id="btnAddMember" type="submit" class="btn btn-outline-primary"><%= Resources.Resource.addMember %></button>
            </div>
        </div>
        <br />
        <div class="row">
            <table id="myTable" class="table table-striped table-hover table-bordered">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th><%= Resources.Resource.account %></th>
                        <th><%= Resources.Resource.pwd %></th>
                        <th><%= Resources.Resource.name %></th>
                        <th><%= Resources.Resource.level %></th>
                        <th><%= Resources.Resource.phoneNumber %></th>
                        <th><%= Resources.Resource.accountStatus %></th>
                        <th><%= Resources.Resource.wallet %></th>
                        <th><%= Resources.Resource.totalSpent %></th>
                    </tr>
                </thead>
                <tbody id="tableBody">
                    <!-- 內容 -->
                </tbody>
            </table>
        </div>

        <div class="row" id="pagination">
            <!-- 分頁內容 -->
        </div>
    </div>

</body>
</html>
