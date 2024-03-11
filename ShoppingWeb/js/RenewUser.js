$(document).ready(function () {
    
    //一開始input預設值
    $.ajax({
        type: "POST",
        url: "/Ajax/RenewUserHandler.aspx/GetUserDataForEdit",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            // 直接設定 input 元素的值
            $("#labUserId").text(data.d.UserId);
            $("#txbUserName").val(data.d.UserName);
            $("#txbPwd").val(data.d.Password);
            $("#ddlRoles").val(data.d.Roles);
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });

    $("#btnUpData").click(function () {
        let userId = $("#labUserId").text();
        let userName = $("#txbUserName").val();
        let pwd = $("#txbPwd").val();
        let roles = $("#ddlRoles").val();
        $("#labRenewUser").text("");

        if (!IsSpecialChar(userName, pwd)) {
            return;
        }  

        $.ajax({
            type: "POST",
            url: "/Ajax/RenewUserHandler.aspx/EditUser", 
            data: JSON.stringify({ userId: userId, userName: userName, pwd: pwd, roles: roles }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d === "修改成功") {
                    alert("修改成功");
                    window.location.href = "SearchUser.aspx" 
                } else {
                    $("#labRenewUser").text(response.d);
                }
            },
            error: function (error) {
                console.error('Error:', error);
            }
        });
    })
});

//判斷特殊字元和長度 
function IsSpecialChar(userName, pwd) {

    if (typeof userName === 'undefined' || typeof pwd === 'undefined') {
        $("#labLogin").text("錯誤");
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
