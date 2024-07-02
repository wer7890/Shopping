// onerror事件
window.addEventListener('error', function (event) {
    var time = new Date();
    AddToErrorQueue("前端錯誤時間: " + time.toLocaleString() + " 訊息: " + event.message + " 位置: " + event.filename + " 行號: " + event.lineno + " 帳號: " + GetAccountCookie("account"));
    event.preventDefault();  //停止事件的默認動作，不會把錯誤印在console上
});

//Vue錯誤攔截
Vue.config.errorHandler = function (err, vm, info) {
    var time = new Date();
    AddToErrorQueue("前端錯誤時間: " + time.toLocaleString() + " 訊息: " + err.message + " 組件名稱: " + vm.$options.name + " 帳號: " + GetAccountCookie("account"));   
}


var errorQueue = [];

$(document).ready(function () {
    setInterval(function () {
        if (errorQueue.length > 0) {
            var errorsToSend = errorQueue; 
            errorQueue = [];
            $.ajax({
                type: "POST",
                url: "/api/Controller/base/LogClientError",
                data: JSON.stringify(errorsToSend),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                },
                error: function (error) {
                }
            });
        }
    }, 3000); // 每3秒發送一次錯誤訊息
});

function AddToErrorQueue(errorDetails) {
    errorQueue.push(errorDetails);
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