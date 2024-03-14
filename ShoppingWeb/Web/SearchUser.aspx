<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchUser.aspx.cs" Inherits="ShoppingWeb.Web.SearchUser1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查詢帳號</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />  
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="../js/SearchUser.js"></script>
</head>
<body>
    <div class="container">
        <h2 class="text-center">查詢帳號</h2>
        <br />
        <div class="row">
            <table id="myTable" class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th><button type="button" class="btn btn-light btn-sm ">管理者ID</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">名稱</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">密碼</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">角色</button></th>
                        <th><button type="button" class="btn btn-light btn-sm " disabled>編輯</button></th>
                        <th><button type="button" class="btn btn-light btn-sm " disabled>刪除</button></th>
                    </tr>
                </thead>
                <tbody id="tableBody">
                    <!-- 內容 -->
                </tbody>
            </table>
        </div>
        <label id="labSearch" class="col-12 col-sm-12 text-center text-success"></label>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>

</body>
</html>
