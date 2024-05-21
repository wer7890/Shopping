$(document).ready(function () {

    //一開始input預設值
    $.ajax({
        type: "POST",
        url: "/Ajax/UserHandler.aspx/GetUserDataForEdit",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d === 102) {
                $("#labRenewUser").text(langFont["errorLog"]);
            } else {
                // 直接設定 input 元素的值
                $("#labUserId").text(data.d.UserId);
                $("#labAccount").text(data.d.Account);
                switch (data.d.Roles) {
                    case 1:
                        $("#labUserRoles").text(langFont["superAdmin"]);
                        break;
                    case 2:
                        $("#labUserRoles").text(langFont["memberAdmin"]);
                        break;
                    case 3:
                        $("#labUserRoles").text(langFont["productAdmin"]);
                        break;
                    default:
                        $("#labUserRoles").text(langFont["mistake"]);
                        break;
                }
            }
        },
        error: function (error) {
            $("#labRenewUser").text(langFont["ajaxError"]);
            AddToErrorQueue("HTTP狀態碼: " + error.status + "'\n'HTTP狀態碼文本描述: " + error.statusText + "'\n'詳細訊息: " + error.responseText);
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
            data: JSON.stringify({ pwd: pwd }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                switch (response.d) {
                    case 0:
                        alert(langFont["duplicateLogin"]);
                        window.parent.location.href = "Login.aspx";
                        break;
                    case 1:
                        alert(langFont["accessDenied"]);
                        parent.location.reload();
                        break;
                    case 2:
                        $("#labRenewUser").text(langFont["editFormat"]);
                        break;
                    case 100:
                        alert(langFont["editSuccessful"]);
                        window.location.href = "UserManagement.aspx";
                        break;
                    case 101:
                        $("#labRenewUser").text(langFont["editFail"]);
                        break;
                    default:
                        $("#labRenewUser").text(langFont["errorLog"]);

                }
            },
            error: function (error) {
                $("#labAddUser").text(langFont["ajaxError"]);
                AddToErrorQueue("HTTP狀態碼: " + error.status + "'\n'HTTP狀態碼文本描述: " + error.statusText + "'\n'詳細訊息: " + error.responseText);
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
        $("#labRenewUser").text(langFont["editFormat"]);
    }

    return pwdValid;
}