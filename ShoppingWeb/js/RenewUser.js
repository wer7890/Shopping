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
            $("#txbUserName").text(data.d.UserName);
            $("#txbPwd").val(data.d.Password);
            $("#ddlRoles").val(data.d.Roles);
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });

    $("#btnUpData").click(function () {
        let pwd = $("#txbPwd").val();
        let roles = $("#ddlRoles").val();
        $("#labRenewUser").text("");

        if (!IsSpecialChar(pwd)) {
            return;
        }  

        $.ajax({
            type: "POST",
            url: "/Ajax/RenewUserHandler.aspx/EditUser", 
            data: JSON.stringify({pwd: pwd, roles: roles }),
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
function IsSpecialChar(pwd) {

    if (typeof pwd === 'undefined') {
        $("#labRenewUser").text("undefined");
        return false;
    }

    let regex = /^[A-Za-z0-9]{6,16}$/;
    let nonAlphanumericRegex = /[^A-Za-z0-9]/;

    let pwdValid = regex.test(pwd);
    let nonAlphanumericPwd = nonAlphanumericRegex.test(pwd);

    if (!pwdValid) {
        if (nonAlphanumericPwd) {
            $("#labRenewUser").text("密碼含有非英文字母和數字");
        } else {
            $("#labRenewUser").text("密碼長度應在6到16之間");
        }
    }

    return pwdValid;
}
