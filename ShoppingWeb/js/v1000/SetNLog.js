// onerror事件
window.addEventListener('error', function (event) {
    let time = new Date();
    AddToErrorQueue("前端錯誤時間: " + time.toLocaleString() + " 訊息: " + event.message + " 位置: " + event.filename + " 行號: " + event.lineno + " 帳號: " + GetAccountCookie("accoun"));
    event.preventDefault();  //停止事件的默認動作，不會把錯誤印在console上
});

let errorQueue = [];

$(document).ready(function () {
    setInterval(function () {
        if (errorQueue.length > 0) {
            let errorsToSend = errorQueue; 
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