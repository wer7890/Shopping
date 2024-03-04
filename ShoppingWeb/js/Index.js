$(document).ready(function () {
    //一開始登入時顯示在左邊的身分，要做權限可使用功能的顯示與隱藏
    $.ajax({
        type: "POST",
        url: "../Web/Index.aspx/CheckUserPermission",  // 這裡指定後端方法的位置
        data: JSON.stringify(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            switch (response.d) {
                case "0":
                    $("#labUserRoles").text("root");
                    break;
                case "1":
                    $("#labUserRoles").text("超級管理員");
                    break;
                case "2":
                    $("#labUserRoles").text("會員管理員");
                    $("#divAdminPanel").hide();
                    $("#divProductPanel").hide();
                    break;
                case "3":
                    $("#labUserRoles").text("商品管理員");
                    $("#divAdminPanel").hide();
                    $("#divMemberPanel").hide();
                    $("#divOrderPanel").hide();
                    break;
                default:
                    $("#labUserRoles").text("讀取錯誤");
                    break;
            }

   
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });

    //按登出按鈕
    $("#btnSignOut").click(function () {
        //卻少把Session清空
        window.location.href = "Login.aspx";
    });

    $("#addUser").click(function () {
        $("#iframeContent").attr("src", "AddUser.aspx");
    });
    $("#searchUser").click(function () {
        $("#iframeContent").attr("src", "SearchUser.aspx");
    });

    



});


