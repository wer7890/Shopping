<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberManagement.aspx.cs" Inherits="ShoppingWeb.Web.MemberManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查詢會員</title>
    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/I18n.js"></script>
    <script src="/js/<%= jsVersion %>/language/Language_<%= basePageLanguage %>.js"></script>
    <script src="/js/<%= jsVersion %>/MemberManagement.js"></script>
</head>
<body>
    <div class="w-auto mx-3">
        <div class="row">
            <div class="col-10 d-flex justify-content-center">
                <h2 class="text-center i18n" data-key="titleMember">會員帳號</h2>
            </div>
            <div class="col-2 d-flex justify-content-center">
                <button id="btnAddMember" type="submit" class="btn btn-outline-primary i18n" data-key="addMember">新增會員</button>
            </div>
        </div>
        <br />
        <div class="row">
            <table id="myTable" class="table table-striped table-hover table-bordered">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th class="i18n" data-key="account">帳號</th>
                        <th class="i18n" data-key="pwd">密碼</th>
                        <th class="i18n" data-key="name">名稱</th>
                        <th class="i18n" data-key="level">等級</th>
                        <th class="i18n" data-key="phoneNumber">電話</th>
                        <th class="i18n" data-key="accountStatus">狀態</th>
                        <th class="i18n" data-key="wallet">錢包</th>
                        <th class="i18n" data-key="totalSpent">總花費</th>
                    </tr>
                </thead>
                <tbody id="tableBody">
                    <!-- 內容 -->
                </tbody>
            </table>
        </div>
        <div class="row mx-auto">
            <span id="labSearchMember" class="col-12 col-sm-12 text-center text-success"></span>
        </div>
    </div>

</body>
</html>
