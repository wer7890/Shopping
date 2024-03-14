<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchProduct.aspx.cs" Inherits="ShoppingWeb.Web.SearchProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="../js/SearchProduct.js"></script>
</head>
<body>
    <div>
        <h2 class="text-center">查詢商品</h2>
        <br />
        <div class="row">
            <table id="myTable" class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th><button type="button" class="btn btn-light btn-sm ">商品ID</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">名稱</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">類型</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">價格</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">庫存</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">是否上架</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">建立者</button></th>
                        <th><button type="button" class="btn btn-light btn-sm ">建立時間</button></th>
                        <th><button type="button" class="btn btn-light btn-sm " disabled>描述</button></th>
                        <th><button type="button" class="btn btn-light btn-sm " disabled>圖片</button></th>
                        
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
