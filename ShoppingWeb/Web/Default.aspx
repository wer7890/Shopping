<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShoppingWeb.Web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>預設畫面</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />

    <link rel="stylesheet" type="text/css" href="/css/<%= cssVersion %>/bootstrap.min.css" />
    <script src="/js/<%= jsVersion %>/jquery-3.7.1.min.js"></script>
    <script src="/js/<%= jsVersion %>/bootstrap.bundle.min.js"></script>
    <script src="/js/<%= jsVersion %>/Pagination.js"></script>

</head>
<body>

    <div class="container mt-2">
        <h2 class="text-center"><%= Resources.Resource.title1Default %></h2>
    </div>



    <div class="container">
        <div class="row">
            <div id="pagination" class="text-center d-flex justify-content-center col-12 col-sm-12">
                <ul class="pagination" id="ulPagination">
                    <!-- 分頁按鈕 -->
                </ul>
            </div>
            <div id="paginationInfo" class="text-center text-center d-flex justify-content-center col-4 mx-auto">
                <!-- 動態生成頁數選項，總頁數，筆數選項 -->
                <ul class="pagination" id="ulPagination2">
                    <!-- 分頁按鈕 -->
                </ul>
            </div>
        </div>
    </div>

    

    <script src="/js/<%= jsVersion %>/Default.js"></script>
</body>
</html>
