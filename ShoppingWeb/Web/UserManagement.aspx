<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="ShoppingWeb.Web.UserManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查詢帳號</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>

    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/I18n.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= basePageLanguage %>.js"></script>
    <script src="/js/<%= jsVersion %>/UserManagement.js"></script>
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-10 d-flex justify-content-center">
                <h2 class="text-center i18n" data-key="titleUser">管理員帳號</h2>
            </div>
            <div class="col-2 d-flex justify-content-center">
                <button id="btnAddUser" type="submit" class="btn btn-outline-primary btn-sm i18n" data-key="addUser">新增管理員</button>
            </div>
        </div>
        <br />
        <div class="row">
            <table id="myTable" class="table table-striped table-hover ">
                <thead>
                    <tr>
                        <th>
                            <button type="button" class="btn btn-light btn-sm i18n" data-key="userId">管理員ID</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm i18n" data-key="account">帳號</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm i18n" data-key="roles">角色</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm i18n" data-key="edit" disabled>編輯</button></th>
                        <th>
                            <button type="button" class="btn btn-light btn-sm i18n" data-key="del" disabled>刪除</button></th>
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
