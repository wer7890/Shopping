$(document).ready(function () {   
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
            url: "/api/Controller/user/RegisterNewUser",  // 這裡指定後端方法的位置
            data: JSON.stringify({ account: account, pwd: pwd, roles: roles }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                switch (response) {
                    case 0:
                        alert(langFont["duplicateLogin"]);
                        window.parent.location.href = "Login.aspx";
                        break;
                    case 1:
                        alert(langFont["accessDenied"]);
                        parent.location.reload();
                        break;
                    case 2:
                        $("#labAddUser").text(langFont["addFormat"]);
                        break;
                    case 100:
                        alert(langFont["addSuccessful"]);
                        window.location.href = "UserManagement.aspx";
                        break;
                    case 101:
                        $("#labAddUser").text(langFont["duplicateAccount"]);
                        break;
                    default:
                        $("#labAddUser").text(langFont["errorLog"]);
                }
            },
            error: function (error) {
                $("#labAddUser").text(langFont["ajaxError"]);
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
        $("#labAddUser").text(langFont["addSpecialChar"]);
    }

    return accountValid && pwdValid;
}