<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberManagement.aspx.cs" Inherits="ShoppingWeb.Web.MemberManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>查詢會員</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="../js/MemberManagement.js"></script>
</head>
<body>
    <div class="container">
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
                        <th>名稱</th>
                        <th>密碼</th>
                        <th>等級</th>
                        <th>錢包</th>
                        <th>電話</th>
                        <th>信箱</th>
                        <th>創建時間</th>
                        <th>狀態</th>
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



    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>
</body>
</html>
