$(document).ready(function () {
    
    //一開始input預設值
    $.ajax({
        type: "POST",
        url: "/Ajax/UserHandler.aspx/GetUserDataForEdit",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            // 直接設定 input 元素的值
            $("#labUserId").text(data.d.UserId);
            $("#labAccount").text(data.d.Account);
            switch (data.d.Roles) {
                case 1:
                    $("#labUserRoles").text("超級管理員");
                    break;
                case 2:
                    $("#labUserRoles").text("會員管理員");
                    break;
                case 3:
                    $("#labUserRoles").text("商品管理員");
                    break;
                default:
                    $("#labUserRoles").text("錯誤");
                    break;
            }
            
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });

    //按下更改按鈕
    $("#btnUpData").click(function () {
        let pwd = $("#txbPwd").val();
        $("#labRenewUser").text("");

        if (!IsSpecialChar(pwd)) {
            return;
        }  

        $.ajax({
            type: "POST",
            url: "/Ajax/UserHandler.aspx/EditUser", 
            data: JSON.stringify({pwd: pwd}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                if (response.d === "重複登入") {
                    alert("重複登入，已被登出");
                    window.parent.location.href = "Login.aspx";
                }else if (response.d === "修改成功") {
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

    let pwdValid = regex.test(pwd);

    if (!pwdValid) {
        $("#labRenewUser").text("密碼不能含有非英文和數字且長度應在6到16之間");
    }

    return pwdValid;
}