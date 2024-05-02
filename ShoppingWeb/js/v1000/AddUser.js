$(document).ready(function () {

    GetLanguageText();
    
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
                        alert(translations["duplicateLogin"]);
                        window.parent.location.href = "Login.aspx";
                        break;
                    case 1:
                        alert(translations["accessDenied"]);
                        parent.location.reload();
                        break;
                    case 2:
                        $("#labAddUser").text(translations["addFormat"]);
                        break;
                    case 100:
                        alert(translations["addSuccessful"]);
                        window.location.href = "UserManagement.aspx";
                        break;
                    case 101:
                        $("#labAddUser").text(translations["duplicateAccount"]);
                        break;
                    default:
                        $("#labAddUser").text(translations["errorLog"]);

                }
            },
            error: function (error) {
                console.error('AJAX Error:', error);
                $("#labAddUser").text(translations["ajaxError"]);
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
        $("#labAddUser").text(translations["addSpecialChar"]);
    }

    return accountValid && pwdValid;
}