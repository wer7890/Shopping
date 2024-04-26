<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberManagement.aspx.cs" Inherits="ShoppingWeb.Web.MemberManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查詢會員</title>
    <link rel="stylesheet" type="text/css" href="/css/v1000/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="/js/v1000/MemberManagement.js"></script>
</head>
<body>
    <div class="w-auto mx-3">
        <div class="row">
            <div class="col-10 d-flex justify-content-center">
                <h2 class="text-center">會員帳號</h2>
            </div>
            <div class="col-2 d-flex justify-content-center">
                <button id="btnAddMember" type="submit" class="btn btn-outline-primary">新增會員</button>
            </div>
        </div>
        <br />
        <div class="row">
            <table id="myTable" class="table table-striped table-hover table-bordered">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>帳號</th>
                        <th>密碼</th>
                        <th>名稱</th>
                        <th>等級</th>
                        <th>電話</th>
                        <th>信箱</th>
                        <th>狀態</th>
                        <th>錢包</th>
                        <th>總花費</th>
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



    <script src="/js/v1000/bootstrap.bundle.min.js"></script>
</body>
</html>
