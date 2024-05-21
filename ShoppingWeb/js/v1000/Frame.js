let userAccount = null;

$(document).ready(function () {

    //一開始登入時顯示在左邊的身分，要做權限可使用功能的顯示與隱藏
    GetUserPermission();

    //按登出按鈕，清空Session["userInfo"]
    $("#btnSignOut").click(function () {
        $.ajax({
            type: "POST",
            url: "/Ajax/UserHandler.aspx/DeleteSession",
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
        url: "/Ajax/UserHandler.aspx/GetUserPermission",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            if (response.d === 102) {
                $("#labUserRoles").text(langFont["errorLog"]);
            } else {
                userAccount = response.d.Account
                $('#labUserAccount').text($('#labUserAccount').text() + userAccount);
                switch (response.d.Roles) {
                    case "1":
                        break;
                    case "2":
                        $("#adminPanel").remove();
                        $("#productPanel").remove();
                        break;
                    case "3":
                        $("#adminPanel").remove();
                        $("#memberPanel").remove();
                        $("#orderPanel").remove();
                        break;
                    default:
                        $("#labUserAccount").text(langFont["mistake"]);
                        break;
                }
            }

        },
        error: function (error) {
            $("#labUserAccount").text(langFont["ajaxError"]);
            addToErrorQueue("HTTP狀態碼: " + error.status + "'\n'HTTP狀態碼文本描述: " + error.statusText + "'\n'詳細訊息: " + error.responseText);
        }
    });
}

//切換語言
function ChangeLanguage(language) {
    $.ajax({
        type: "POST",
        url: "/Ajax/UserHandler.aspx/SetLanguage",
        data: JSON.stringify({ language: language }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            parent.location.reload();
            $('#labUserAccount').text($('#labUserAccount').text() + userAccount);
        },
        error: function (error) {
            $("#labUserAccount").text(langFont["ajaxError"]);
            addToErrorQueue("HTTP狀態碼: " + error.status + "'\n'HTTP狀態碼文本描述: " + error.statusText + "'\n'詳細訊息: " + error.responseText);
        }
    });
}