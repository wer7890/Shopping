// onerror事件
window.addEventListener('error', function (event) {
    AddToErrorQueue("錯誤訊息: " + event.message + " Error對象: " + event.error.stack + " 位置: " + event.filename + " 行號: " + event.lineno + " 列號: " + event.colno);
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
                url: "/api/Controller/login/LogClientError",
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