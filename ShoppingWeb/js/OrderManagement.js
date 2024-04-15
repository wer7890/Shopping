// 訂單狀態
let orderStatus = {
    "1": { name: "處理中", color: "bg-warning" },
    "2": { name: "已確認", color: "bg-success" },
    "3": { name: "已完成", color: "bg-white" },
    "4": { name: "已取消", color: "bg-danger" }
};

// 付款狀態
let paymentStatus = {
    "1": { name: "未付款", color: "bg-warning" },
    "2": { name: "付款失敗", color: "bg-danger" },
    "3": { name: "超過付款時間", color: "bg-danger" },
    "4": { name: "已付款", color: "bg-success" },
    "5": { name: "退款中", color: "bg-warning" },
    "6": { name: "已退款", color: "bg-danger" }
};

// 配送狀態
let deliveryStatus = {
    "1": { name: "備貨中", color: "bg-warning" },
    "2": { name: "發貨中", color: "bg-warning" },
    "3": { name: "已發貨", color: "bg-success" },
    "4": { name: "已到達", color: "bg-white" },
    "5": { name: "已取貨", color: "bg-white" },
    "6": { name: "已退回", color: "bg-danger" },
    "7": { name: "退回中", color: "bg-warning" }
};

// 配送方式
let deliveryMethod = {
    "1": "超商取貨",
    "2": "店到店",
    "3": "宅配"
};


$(document).ready(function () {
    window.parent.getUserPermission();
    SearchAllOrder();


});

//全部訂單資料
function SearchAllOrder() {
    $.ajax({
        url: '/Ajax/OrderHandler.aspx/GetAllOrderData',
        type: 'POST',
        contentType: 'application/json',
        success: function (response) {
            if (response.d == "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else if (response.d === "權限不足") {
                alert("權限不足");
                parent.location.reload();
            } else {
                let data = JSON.parse(response.d);
                let tableBody = $('#tableBody');

                tableBody.empty();

                $.each(data, function (index, item) {
                    let row = '<tr>' +
                        '<td>' + item.f_id + '</td>' +
                        '<td>' + item.f_memberId + '</td>' +
                        '<td>' + item.f_createdTime + '</td>' +
                        '<td>' +
                        '<span class="px-2 rounded ' + orderStatus[item.f_orderStatus].color + '">' + orderStatus[item.f_orderStatus].name + '</span>' +
                        '</td>' +
                        '<td>' +
                        '<span class="px-2 rounded ' + paymentStatus[item.f_paymentStatus].color + '">' + paymentStatus[item.f_paymentStatus].name + '</span>' +
                        '</td>' +
                        '<td>' +
                        '<span class="px-2 rounded ' + deliveryStatus[item.f_deliveryStatus].color + '">' + deliveryStatus[item.f_deliveryStatus].name + '</span>' +
                        '</td>' +
                        '<td>' + deliveryMethod[item.f_deliveryMethod] + '</td>' +
                        '<td>' + item.f_total + '</td>' +
                        '</tr>';

                    tableBody.append(row);
                });
            }

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}