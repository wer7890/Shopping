let userAccount = null;

$(document).ready(function () {

    //一開始登入時顯示在左邊的身分，要做權限可使用功能的顯示與隱藏
    GetUserPermission();

    //按登出按鈕，清空Session["userInfo"]
    $("#btnSignOut").click(function () {
        $.ajax({
            type: "POST",
            url: "/api/Controller/login/DeleteSession",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                window.location.href = "Login.aspx";
            },
            error: function (error) {
                $("#labUserAccount").text(langFont["ajaxError"]);
            }
        });
    });

    $("#adminPanel").click(function () {
        $("#iframeContent").attr("src", "UserManagement.aspx");
    });
    $("#productPanel").click(function () {
        $("#iframeContent").attr("src", "ProductManagement.aspx");
    });
    $("#memberPanel").click(function () {
        $("#iframeContent").attr("src", "MemberManagement.aspx");
    });
    $("#orderPanel").click(function () {
        $("#iframeContent").attr("src", "OrderManagement.aspx");
    });

});

//取得身分和帳號
function GetUserPermission() {
    $.ajax({
        type: "POST",
        url: "/api/Controller/login/GetUserPermission",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            userAccount = response.Account
            $('#labUserAccount').text($('#labUserAccount').text() + userAccount);
            switch (response.Roles) {
                case 1:
                    break;
                case 2:
                    $("#adminPanel").remove();
                    $("#productPanel").remove();
                    break;
                case 3:
                    $("#adminPanel").remove();
                    $("#memberPanel").remove();
                    $("#orderPanel").remove();
                    break;
                default:
                    $("#adminPanel").remove();
                    $("#memberPanel").remove();
                    $("#productPanel").remove();
                    $("#orderPanel").remove();
                    $("#labUserAccount").text(langFont["mistake"]);
                    break;
            }
        },
        error: function (error) {
            $("#labUserAccount").text(langFont["ajaxError"]);
        }
    });
}

//切換語言
function ChangeLanguage(language) {
    $.ajax({
        type: "POST",
        url: "/api/Controller/login/SetLanguage",
        data: JSON.stringify({ language: language }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            parent.location.reload();
            $('#labUserAccount').text($('#labUserAccount').text() + userAccount);
        },
        error: function (error) {
            $("#labUserAccount").text(langFont["ajaxError"]);
        }
    });
}