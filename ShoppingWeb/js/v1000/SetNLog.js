// onerror事件
window.onerror = function (message, source, lineno, colno, error) {
    AddToErrorQueue("\n錯誤訊息: " + message + "\n發生錯誤的腳本: " + source + "\n發生錯誤的行號: " + lineno + "\n發生錯誤的列號: " + colno + "\nError對象: " + error);
    return true;
}

let errorQueue = [];

$(document).ready(function () {
    setInterval(function () {
        if (errorQueue.length > 0) {
            let errorsToSend = errorQueue;
            errorQueue = [];
            $.ajax({
                type: "POST",
                url: "/api/Controller/user/LogClientError",
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