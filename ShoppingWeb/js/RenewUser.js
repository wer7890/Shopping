$(document).ready(function () {
    
    //一開始input預設值
    $.ajax({
        type: "POST",
        url: "../Ajax/RenewUserHandler.aspx/GetUserDataForEdit",
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

        //判斷長度
        if (userName === "" || pwd === "") {
            $("#labRenewUser").text("用戶名和密碼不能為空");
            return;
        } else if (userName.length < 6 || pwd.length < 6) {
            $("#labRenewUser").text("用戶名跟密碼長度不能小於6");
            return;
        } else if (userName.length > 16 || pwd.length > 16) {
            $("#labRenewUser").text("用戶名跟密碼長度不能大於16");
            return;
        }

        if (!isSpecialChar(userName, pwd)) {
            $("#labRenewUser").text("用戶名和密碼不能包含特殊字元");
            return;
        }

        $.ajax({
            type: "POST",
            url: "../Ajax/RenewUserHandler.aspx/EditUser", 
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

//判斷特殊字元
function isSpecialChar(userName, pwd) {
    let regUserName = /^[A-Za-z0-9]+$/;
    let regPwd = /^[A-Za-z0-9]+$/;

    return regUserName.test(userName) && regPwd.test(pwd);
}
