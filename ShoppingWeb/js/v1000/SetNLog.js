let errorQueue = [];

$(document).ready(function () {
    setInterval(function () {
        if (errorQueue.length > 0) {
            let errorsToSend = errorQueue;
            errorQueue = [];
            $.ajax({
                type: "POST",
                url: "/Ajax/UserHandler.aspx/LogClientError",
                data: JSON.stringify({ errorDetails: errorsToSend }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log("Errors logged successfully.");
                },
                error: function (error) {
                    console.error('Failed to log errors:', error);
                }
            });
        }
    }, 3000); // 每3秒發送一次錯誤訊息
});

function addToErrorQueue(errorDetails) {
    errorQueue.push(errorDetails);
}