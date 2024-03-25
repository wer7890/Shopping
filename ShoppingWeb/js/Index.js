$(document).ready(function () {
    //一開始登入時顯示在左邊的身分，要做權限可使用功能的顯示與隱藏
    $.ajax({
        type: "POST",
        url: "/Ajax/IndexHandler.aspx/GetUserPermission",  
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            switch (response.d) {
                case "1":
                    $("#labUserRoles").text("身分 : 超級管理員");
                    $("#iframeContent").attr("src", "SearchUser.aspx");
                    break;
                case "2":
                    $("#labUserRoles").text("身分 : 會員管理員");
                    $("#iframeContent").attr("src", "SearchOrder.aspx");
                    $("#adminPanel").hide();
                    $("#productPanel").hide();
                    break;
                case "3":
                    $("#labUserRoles").text("身分 : 商品管理員");
                    $("#iframeContent").attr("src", "SearchProduct.aspx");
                    $("#adminPanel").hide();
                    $("#memberPanel").hide();
                    $("#orderPanel").hide();
                    break;
                default:
                    $("#labUserRoles").text("身分 : 讀取錯誤");
                    break;
            } 
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });

    //按登出按鈕，清空Session["userId"]
    $("#btnSignOut").click(function () {
        $.ajax({
            type: "POST",
            url: "/Ajax/IndexHandler.aspx/DeleteSession",  
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                window.location.href = "Login.aspx";
            },
            error: function (error) {
                console.error('Error:', error);
            }
        }); 
    });

    $("#adminPanel").click(function () {
        $("#iframeContent").attr("src", "SearchUser.aspx");
    });
    $("#productPanel").click(function () {
        $("#iframeContent").attr("src", "SearchProduct.aspx");
    });
    $("#memberPanel").click(function () {
        //$("#iframeContent").attr("src", "SearchMember.aspx");
    });
    $("#orderPanel").click(function () {
        $("#iframeContent").attr("src", "SearchOrder.aspx");
    });
    
});