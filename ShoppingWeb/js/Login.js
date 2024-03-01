$(document).ready(function () {
    $("#btnLogin").click(function () {
        var userName = $("#txbUserName").val();
        var pwd = $("#txbPassword").val();
        $("#labLogin").text("");

        if (userName === "" || pwd === "") {
            $("#labLogin").text("用戶名和密碼不能為空");
            return;
        }

        if (!isSpecialChar(userName, pwd))
        {
            $("#labLogin").text("用戶名和密碼不能包含特殊字元");
            return;
        }


        $.ajax({
            type: "POST",
            url: "Login.aspx/LoginUser",  // 這裡指定後端方法的位置
            data: JSON.stringify({ userName: userName, pwd: pwd }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d === true) {
                    $("#labLogin").text("登入成功");
                    window.location.href = "AddUser.aspx";
                } else {
                    $("#labLogin").text("帳號密碼錯誤");
                }
            },
            error: function (error) {
                console.error('Error:', error);
            }
        });
    });
});


//判斷特殊字元
function isSpecialChar(userName, pwd) {
    var regUserName = /[A-Za-z0-9]/;
    var regPwd = /[A-Za-z0-9]/;

    return regUserName.test(userName) && regPwd.test(pwd);
}