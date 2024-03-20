<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchMember.aspx.cs" Inherits="ShoppingWeb.Web.SearchMember" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>查詢會員</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="../js/SearchMember.js"></script>
</head>
<body>
    <div class="container">
        <h2 class="text-center">會員帳號</h2>
        <br />
        <div class="row">
            <table id="myTable" class="table table-striped table-hover table-bordered">
                <thead>
                    <tr>
                        <th><button type="button" class="btn btn-light btn-sm ">ID</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">名稱</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">密碼</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">等級</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">錢包</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">電話</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">信箱</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">創建時間</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">狀態</button></th>
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
