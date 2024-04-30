let translations = {
    'titleAddUser': {
        'en': 'Add User'
    },
    'labAccount': {
        'en': 'Account'
    },
    'labPwd': {
        'en': 'Password'
    },
    'labRoles': {
        'en': 'Roles'
    },
    'superAdmin': {
        'en': 'Super Administrator'
    },
    'memberAdmin': {
        'en': 'Member Administrator'
    },
    'productAdmin': {
        'en': 'Product Administrator'
    },
    'btnAddUser': {
        'en': 'Add'
    }
};


$(document).ready(function () {
    TranslateLanguage("en");

    //按下新增按鈕
    $("#btnAddUser").click(function () {
        let account = $("#txbAccount").val();
        let pwd = $("#txbPwd").val();
        let roles = $("#ddlRoles").val();
        $("#labAddUser").text("");

        if (!IsSpecialChar(account, pwd)) {
            return;
        }

        $.ajax({
            type: "POST",
            url: "/Ajax/UserHandler.aspx/RegisterNewUser",  // 這裡指定後端方法的位置
            data: JSON.stringify({ account: account, pwd: pwd, roles: roles }),
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
                        $("#labAddUser").text("Format error");
                        break;
                    case 100:
                        alert("Addition successful");
                        window.location.href = "UserManagement.aspx";
                        break;
                    case 101:
                        $("#labAddUser").text("Duplicate account");
                        break;
                    default:
                        $("#labAddUser").text("Internal error occurred, please check the logs");

                }
            },
            error: function (error) {
                console.error('AJAX Error:', error);
                $("#labAddUser").text("AJAX Error");
            }
        });
    });
});


//判斷特殊字元和長度
function IsSpecialChar(account, pwd) {

    if (typeof account === 'undefined' || typeof pwd === 'undefined') {
        $("#labAddUser").text("undefined");
        return false;
    }

    let regex = /^[A-Za-z0-9]{6,16}$/;

    let accountValid = regex.test(account);
    let pwdValid = regex.test(pwd);

    if (!accountValid || !pwdValid) {
        $("#labAddUser").text("Format error");
    }

    return accountValid && pwdValid;
}
