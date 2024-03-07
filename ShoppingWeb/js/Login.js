$(document).ready(function () {
    $("#btnLogin").click(function () {
        let userName = $("#txbUserName").val();
        let pwd = $("#txbPassword").val();
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
            url: "../Ajax/LoginHandler.aspx/LoginUser",  // 這裡指定後端方法的位置
            data: JSON.stringify({ userName: userName, pwd: pwd }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d === true) {
                    window.location.href = "index.aspx";
                } else {
                    $("#labLogin").text("帳號密碼錯誤");
                }
            },
            error: function (error) {
                console.error('AJAX Error:', error);
                $("#labLogin").text("發生錯誤，請查看控制台");
            }
        });
    });
});


//判斷特殊字元
function isSpecialChar(userName, pwd) {
    let regUserName = /^[A-Za-z0-9]+$/;
    let regPwd = /^[A-Za-z0-9]+$/;

    return regUserName.test(userName) && regPwd.test(pwd);
}

