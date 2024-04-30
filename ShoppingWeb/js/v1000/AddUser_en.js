let translations = {
    'titleAddUser': {
        'zh': '新增帳號',
        'en': 'Add User'
    },
    'labAccount': {
        'zh': '帳號:',
        'en': 'Account'
    },
    'labPwd': {
        'zh': '密碼:',
        'en': 'Password'
    },
    'labRoles': {
        'zh': '角色',
        'en': 'Roles'
    },
    'superAdmin': {
        'zh': '超級管理員',
        'en': 'Super Administrator'
    },
    'memberAdmin': {
        'zh': '會員管理員',
        'en': 'Member Administrator'
    },
    'productAdmin': {
        'zh': '商品管理員',
        'en': 'Product Administrator'
    },
    'btnAddUser': {
        'zh': '新增',
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
                        alert("重複登入，已被登出");
                        window.parent.location.href = "Login.aspx";
                        break;
                    case 1:
                        alert("權限不足");
                        parent.location.reload();
                        break;
                    case 2:
                        $("#labAddUser").text("帳號和密碼不能含有非英文和數字且長度應在6到16之間且腳色不能為空");
                        break;
                    case 100:
                        alert("新增成功");
                        window.location.href = "UserManagement.aspx";
                        break;
                    case 101:
                        $("#labAddUser").text("帳號重複");
                        break;
                    default:
                        $("#labAddUser").text("發生發生內部錯誤，請看日誌");

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
function IsSpecialChar(account, pwd) {

    if (typeof account === 'undefined' || typeof pwd === 'undefined') {
        $("#labAddUser").text("undefined");
        return false;
    }

    let regex = /^[A-Za-z0-9]{6,16}$/;

    let accountValid = regex.test(account);
    let pwdValid = regex.test(pwd);

    if (!accountValid || !pwdValid) {
        $("#labAddUser").text("帳號和密碼不能含有非英文和數字且長度應在6到16之間");
    }

    return accountValid && pwdValid;
}
