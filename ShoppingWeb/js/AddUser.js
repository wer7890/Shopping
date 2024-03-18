﻿$(document).ready(function () {
    //新增按鈕
    $("#btnAddUser").click(function () {

        if (!CheckAnyoneLonginRedirect()) {
            return;
        }

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
        $("#labAddUser").text("undefined");
        return false;
    }

    let regex = /^[A-Za-z0-9]{6,16}$/;

    let userNameValid = regex.test(userName);
    let pwdValid = regex.test(pwd);

    if (!userNameValid || !pwdValid) {
        $("#labAddUser").text("名稱和密碼不能含有非英文和數字且長度應在6到16之間");
    }

    return userNameValid && pwdValid;
}

//是否有重複登入
function CheckAnyoneLonginRedirect() {
    var result;
    $.ajax({
        type: "POST",
        url: "/Ajax/IndexHandler.aspx/AnyoneLongin",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {

            if (response.d === false) {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
                result = false;
            } else{
                result = true;
            }

        },
        error: function (error) {
            console.error('Error:', error);
            result = false;
        }
    });
    return result;
}
