<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ShoppingWeb.Web.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>後臺管理</title>
    <link rel="icon" type="image/x-icon" href="data:image/x-icon;," />  
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="../js/Index.js"></script>
</head>
<body>
    <div class="container mt-5">
        <div class="row">
            <!--左欄-->
            <div class="col-12 col-md-3">
                <div class="accordion accordion-flush" id="accordionFlushExample">

                    <div class="accordion-item">
                        <h2 class="accordion-header" id="flush-headingOne">
                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-collapseOne" aria-expanded="false" aria-controls="flush-collapseOne">
                                帳號管理
                            </button>
                        </h2>
                        <div id="flush-collapseOne" class="accordion-collapse collapse" aria-labelledby="flush-headingOne" data-bs-parent="#accordionFlushExample">
                            <a href="#" class="list-group-item list-group-item-action">新增帳號</a>
                            <a href="#" class="list-group-item list-group-item-action">查詢帳號</a>
                        </div>
                    </div>

                    <div class="accordion-item">
                        <h2 class="accordion-header" id="flush-headingTwo">
                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-collapseTwo" aria-expanded="false" aria-controls="flush-collapseTwo">
                                會員系統
                            </button>
                        </h2>
                        <div id="flush-collapseTwo" class="accordion-collapse collapse" aria-labelledby="flush-headingTwo" data-bs-parent="#accordionFlushExample">
                            <a href="#" class="list-group-item list-group-item-action">會員系統</a>
                        </div>
                    </div>

                    <div class="accordion-item">
                        <h2 class="accordion-header" id="flush-headingThree">
                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-collapseThree" aria-expanded="false" aria-controls="flush-collapseThree">
                                商品系統
                            </button>
                        </h2>
                        <div id="flush-collapseThree" class="accordion-collapse collapse" aria-labelledby="flush-headingThree" data-bs-parent="#accordionFlushExample">
                            <a href="#" class="list-group-item list-group-item-action">商品系統</a>
                        </div>
                    </div>

                    <div class="accordion-item">
                        <h2 class="accordion-header" id="flush-headingFour">
                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-collapseFour" aria-expanded="false" aria-controls="flush-collapseFour">
                                訂單系統
                            </button>
                        </h2>
                        <div id="flush-collapseFour" class="accordion-collapse collapse" aria-labelledby="flush-headingFour" data-bs-parent="#accordionFlushExample">
                            <a href="#" class="list-group-item list-group-item-action">訂單系統</a>
                        </div>
                    </div>

                </div>

                <div class="row mt-3">
                    <label id="labUserRoles" class="fs-5 text-center align-middle mt-2"></label>
                    <br />
                    <button id="btnSignOut" class="btn btn-outline-dark mt-3">登出</button>
                </div>

            </div>

            <!--右欄-->
            <div class="col-12 col-md-9">
                <div class="container">
                    <h2 class="text-center">新增帳號</h2>
                    <br />
                    <div class="row">
                        <div class="mx-auto col-12 col-md-8 mt-2">
                            <label for="txbUserName" class="form-label">管理員名稱</label>
                            <input id="txbUserName" class="form-control" />

                        </div>
                        <div class="mx-auto col-12 col-md-8 mt-2">
                            <label for="txbPwd" class="form-label">密碼</label>
                            <input id="txbPwd" class="form-control" />
                        </div>
                        <div class="mx-auto mt-3 col-12 col-md-8 mt-2">
                            <label for="ddlRoles" class="form-label">角色</label>

                            <select id="ddlRoles" class="form-select">
                                <option value="1">超級管理員</option>
                                <option value="2">會員管理員</option>
                                <option value="3">商品管理員</option>
                            </select>
                        </div>

                        <button id="btnAddUser" class="btn btn-outline-primary mx-auto mt-3 col-12 col-md-5">新增</button>
                    </div>
                    <br />
                    <div class="row">
                        <label id="labAddUser" class="col-12 col-sm-12 text-center text-success"></label>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>

</body>
</html>
