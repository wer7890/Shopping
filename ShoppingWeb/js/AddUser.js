﻿$(document).ready(function () {
    //新增按鈕
    $("#btnAddUser").click(function () {
        let userName = $("#txbUserName").val();
        let pwd = $("#txbPwd").val();
        let roles = $("#ddlRoles").val();
        $("#labAddUser").text("");

        if (!IsSpecialChar(userName, pwd)) {
            return;
        }    

        $.ajax({
            type: "POST",
            url: "/Ajax/AddUserHandler.aspx/RegisterNewUser",  // 這裡指定後端方法的位置
            data: JSON.stringify({ userName: userName, pwd: pwd, roles: roles }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d === "1") {
                    alert("新增成功");
                    window.location.href = "SearchUser.aspx" 
                } else if (response.d === "0") {
                    $("#labAddUser").text("管理員名稱重複");
                } else {
                    $("#labAddUser").text(response.d);
                }
            },
            error: function (error) {
                console.error('AJAX Error:', error);
                $("#labAddUser").text("發生錯誤，請查看控制台");
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
    let nonAlphanumericRegex = /[^A-Za-z0-9]/;

    let userNameValid = regex.test(userName);
    let pwdValid = regex.test(pwd);
    let nonAlphanumericUserName = nonAlphanumericRegex.test(userName);
    let nonAlphanumericPwd = nonAlphanumericRegex.test(pwd);

    if (!userNameValid && !pwdValid) {
        $("#labLogin").text("使用者名稱和密碼均不符合規則");
    } else if (!userNameValid) {
        if (nonAlphanumericUserName) {
            $("#labLogin").text("使用者名稱含有非英文字母和數字");
        } else {
            $("#labLogin").text("用戶名長度應在6到16之間");
        }
    } else if (!pwdValid) {
        if (nonAlphanumericPwd) {
            $("#labLogin").text("密碼含有非英文字母和數字");
        } else {
            $("#labLogin").text("密碼長度應在6到16之間");
        }
    }

    return userNameValid && pwdValid;
}