$(document).ready(function () {
    let accountValue = GetAccountCookie("account");
    //如果cookie["account"]存在，就顯示在帳號輸入框，且勾選記住帳號
    if (accountValue) {
        $("#txbAccount").val(accountValue);
        $("#flexCheckDefault").prop("checked", "true");
    }
    
    //按下登入按鈕
    $("#btnLogin").click(function () {
        let account = $("#txbAccount").val();
        let pwd = $("#txbPassword").val();
        $("#labLogin").text("");

        if (!IsSpecialChar(account, pwd)){
            return;
        }

        $.ajax({
            type: "POST",
            url: "/Ajax/UserHandler.aspx/LoginUser",  // 這裡指定後端方法的位置
            data: JSON.stringify({ account: account, pwd: pwd }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                switch (response.d) {
                    case 2:
                        $("#labLogin").text(langFont["loginFormat"]);
                        break;
                    case 100:
                        //如果有勾選記住帳號，就紀錄cookie["account"]，反之則清除
                        if ($("#flexCheckDefault").is(":checked")) {
                            document.cookie = 'account=' + account + '; max-age=2592000; path=/';  //30天到期，path=/該cookie整個網站都是可看見的
                        } else {
                            document.cookie = 'account=0; max-age=0; path=/';  //馬上過期   
                        }

                        window.location.href = "Frame.aspx";
                        break;
                    case 101:
                        $("#labLogin").text(langFont["loginFailed"]);
                        break;
                    default:
                        $("#labLogin").text(langFont["errorLog"]);
                }
            },
            error: function (error) {
                $("#labLogin").text(langFont["ajaxError"]);
            }
        });
    });
});


//判斷特殊字元和長度 
function IsSpecialChar(account, pwd) {

    if (typeof account === 'undefined' || typeof pwd === 'undefined') {
        $("#labLogin").text("undefined");
        return false;
    }

    let regex = /^[A-Za-z0-9]{6,16}$/;

    let accountValid = regex.test(account);
    let pwdValid = regex.test(pwd);


    if (!accountValid || !pwdValid) {
        $("#labLogin").text(langFont["loginFormat"]);
    }

    return accountValid && pwdValid;
}

//切換語言
function ChangeLanguage(language) {
    $.ajax({
        type: "POST",
        url: "/Ajax/UserHandler.aspx/SetLanguage",
        data: JSON.stringify({ language: language }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            parent.location.reload();
        },
        error: function (error) {
            $("#labLogin").text(langFont["ajaxError"]);
        }
    });
}

//查看cookie中有無帳號的cookie，如果有就拿出來
function GetAccountCookie(name) {
    var cookies = document.cookie.split(';');

    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i].trim();
        if (cookie.startsWith(name + '=')) {
            return cookie.substring(name.length + 1);
        }
    }
    return null;
}
