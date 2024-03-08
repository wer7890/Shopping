$(document).ready(function () {
    //新增按鈕
    $("#btnAddUser").click(function () {
        let userName = $("#txbUserName").val();
        let pwd = $("#txbPwd").val();
        let roles = $("#ddlRoles").val();

        $("#labAddUser").text("");

        //判斷長度
        if (userName === "" || pwd === "") {
            $("#labAddUser").text("用戶名和密碼不能為空");
            return;
        } 
        if (userName.length < 6 || pwd.length < 6 || userName.length > 16 || pwd.length > 16) {
            $("#labAddUser").text("用戶名跟密碼長度應在6到16之間");
            return ;
        }

        //判斷特殊字元
        if (!isSpecialChar(userName, pwd)) {
            $("#labAddUser").text("用戶名和密碼不能包含特殊字元");
            return;
        }     

        $.ajax({
            type: "POST",
            url: "/Ajax/AddUserHandler.aspx/RegisterNewUser",  // 這裡指定後端方法的位置
            data: JSON.stringify({ userName: userName, pwd: pwd, roles: roles }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d === "新增成功") {
                    alert("新增成功");
                    window.location.href = "SearchUser.aspx" 
                } else if (response.d === null) {
                    $("#labAddUser").text("新增失敗");
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


//判斷特殊字元
function isSpecialChar(userName, pwd) {
    let regUserName = /^[A-Za-z0-9]+$/;
    let regPwd = /^[A-Za-z0-9]+$/;

    return regUserName.test(userName) && regPwd.test(pwd);
}