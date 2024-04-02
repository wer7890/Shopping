$(document).ready(function () {
    window.parent.getUserPermission();

    //新增按鈕
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
                if (response.d === "1") {
                    alert("新增成功");
                    window.location.href = "UserManagement.aspx"
                } else if (response.d === "0") {
                    $("#labAddUser").text("帳號重複");
                } else if (response.d === "重複登入") {
                    alert("重複登入，已被登出");
                    window.parent.location.href = "Login.aspx";
                } else {
                    $("#labAddUser").text(response.d);
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
