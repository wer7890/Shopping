﻿$(document).ready(function () {
    //按下登入按鈕
    $("#btnLogin").click(function () {
        let account = $("#txbAccount").val();
        let pwd = $("#txbPassword").val();
        $("#labLogin").text("");

        if (!IsSpecialChar(account, pwd)){
            return;
        }

        $.ajax({
            type: "POST",
            url: "/Ajax/UserHandler.aspx/LoginUser",  // 這裡指定後端方法的位置
            data: JSON.stringify({ account: account, pwd: pwd }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                switch (response.d) {
                    case 2:
                        $("#labLogin").text("帳號和密碼不能含有非英文和數字且長度應在6到16之間");
                        break;
                    case 100:
                        window.location.href = "Frame.aspx";
                        break;
                    case 101:
                        $("#labLogin").text("帳號密碼錯誤");
                        break;
                    default:
                        $("#labLogin").text("發生發生內部錯誤，請看日誌");

                }
            },
            error: function (error) {
                console.error('AJAX Error:', error);
                $("#labLogin").text("發生錯誤，請查看控制台");
            }
        });
    });
});


//判斷特殊字元和長度 
function IsSpecialChar(account, pwd) {

    if (typeof account === 'undefined' || typeof pwd === 'undefined') {
        $("#labLogin").text("undefined");
        return false;
    }

    let regex = /^[A-Za-z0-9]{6,16}$/;

    let accountValid = regex.test(account);
    let pwdValid = regex.test(pwd);


    if (!accountValid || !pwdValid) {
        $("#labLogin").text("帳號和密碼不能含有非英文和數字且長度應在6到16之間");
    }

    return accountValid && pwdValid;
}