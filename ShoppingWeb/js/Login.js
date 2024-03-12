﻿$(document).ready(function () {
    $("#btnLogin").click(function () {
        let userName = $("#txbUserName").val();
        let pwd = $("#txbPassword").val();
        $("#labLogin").text("");

        if (!IsSpecialChar(userName, pwd)){
            return;
        }

        $.ajax({
            type: "POST",
            url: "/Ajax/LoginHandler.aspx/LoginUser",  // 這裡指定後端方法的位置
            data: JSON.stringify({ userName: userName, pwd: pwd }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d === true) {
                    window.location.href = "index.aspx";
                } else {
                    $("#labLogin").text("帳號密碼錯誤");
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
function IsSpecialChar(userName, pwd) {

    if (typeof userName === 'undefined' || typeof pwd === 'undefined') {
        $("#labLogin").text("undefined");
        return false;
    }

    let regex = /^[A-Za-z0-9]{6,16}$/;
    let nonAlphanumericRegex = /[^A-Za-z0-9]/; //不是字母或數字

    let userNameValid = regex.test(userName);
    let pwdValid = regex.test(pwd);


    if (!userNameValid || !pwdValid) {
        $("#labLogin").text("名稱和密碼不能含有非英文和數字且長度應在6到16之間");
    }

    return userNameValid && pwdValid;
}


