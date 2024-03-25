﻿$(document).ready(function () {
    //一開始登入時顯示在左邊的身分，要做權限可使用功能的顯示與隱藏
    $.ajax({
        type: "POST",
        url: "/Ajax/IndexHandler.aspx/GetUserPermission",  
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            switch (response.d) {
                case "0":
                    $("#labUserRoles").text("身分 : root");
                    break;
                case "1":
                    $("#labUserRoles").text("身分 : 超級管理員");
                    break;
                case "2":
                    $("#labUserRoles").text("身分 : 會員管理員");
                    $("#divAdminPanel").hide();
                    $("#divProductPanel").hide();
                    break;
                case "3":
                    $("#labUserRoles").text("身分 : 商品管理員");
                    $("#divAdminPanel").hide();
                    $("#divMemberPanel").hide();
                    $("#divOrderPanel").hide();
                    break;
                default:
                    $("#labUserRoles").text("身分 : 讀取錯誤");
                    break;
            } 
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });

    //按登出按鈕，清空Session
    $("#btnSignOut").click(function () {
        $.ajax({
            type: "POST",
            url: "/Ajax/IndexHandler.aspx/DeleteSession",  
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                window.location.href = "Login.aspx";
            },
            error: function (error) {
                console.error('Error:', error);
            }
        }); 
    });

    $("#searchUser").click(function () {
        $("#iframeContent").attr("src", "SearchUser.aspx");
    });
    $("#searchProduct").click(function () {
        $("#iframeContent").attr("src", "SearchProduct.aspx");
    });
    $("#searchMember").click(function () {
        //$("#iframeContent").attr("src", "SearchMember.aspx");
    });
    $("#searchOrder").click(function () {
        $("#iframeContent").attr("src", "SearchOrder.aspx");
    });
    
});