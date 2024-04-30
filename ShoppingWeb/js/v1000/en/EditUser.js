let translations = {
    'titleEditUser': {
        'en': 'Edit User'
    },
    'spanUserId': {
        'en': 'Admin ID : '
    },
    'spanAccount': {
        'en': 'Account : '
    },
    'labPwd': {
        'en': 'Password'
    },
    'spanRoles': {
        'en': 'Roles : '
    },
    'btnEdit': {
        'en': 'Edit'
    },
    'superAdmin': {
        'en': 'Super Administrator'
    },
    'memberAdmin': {
        'en': 'Member Administrator'
    },
    'productAdmin': {
        'en': 'Product Administrator'
    }
};

$(document).ready(function () {

    //一開始input預設值
    $.ajax({
        type: "POST",
        url: "/Ajax/UserHandler.aspx/GetUserDataForEdit",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d === 102) {
                $("#labRenewUser").text("Internal error occurred, please check the logs");
            } else {
                // 直接設定 input 元素的值
                $("#labUserId").text(data.d.UserId);
                $("#labAccount").text(data.d.Account);
                switch (data.d.Roles) {
                    case 1:
                        $("#labUserRoles").text("Super Administrator");
                        break;
                    case 2:
                        $("#labUserRoles").text("Super Administrator");
                        break;
                    case 3:
                        $("#labUserRoles").text("Product Administrator");
                        break;
                    default:
                        $("#labUserRoles").text("Error");
                        break;
                }

                TranslateLanguage("en");
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
            data: JSON.stringify({ pwd: pwd }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                switch (response.d) {
                    case 0:
                        alert("Duplicate login detected, logged out");
                        window.parent.location.href = "Login.aspx";
                        break;
                    case 1:
                        alert("Insufficient permissions");
                        parent.location.reload();
                        break;
                    case 2:
                        $("#labRenewUser").text("Format error");
                        break;
                    case 100:
                        alert("修改成功");
                        window.location.href = "UserManagement.aspx";
                        break;
                    case 101:
                        $("#labRenewUser").text("change identity");
                        break;
                    default:
                        $("#labRenewUser").text("Internal error occurred, please check the logs");

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
        $("#labRenewUser").text("Format error");
    }

    return pwdValid;
}