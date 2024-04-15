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
                    let row = '<tr data-bs-toggle="collapse" data-bs-target="#collapse_' + index + '" onclick="showOrderDetail(this, \'' + item.f_id + '\')">' +
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
                        '<td>NT$' + item.f_total + '</td>' +
                        '</tr>' +
                        '<tr id="collapse_' + index + '" class="collapse">' +
                        '<td colspan="8"><div id="orderDetail_' + index + '"></div></td>' +
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

// 顯示訂單詳細內容
function showOrderDetail(element, orderId) {
    $.ajax({
        url: '/Ajax/OrderHandler.aspx/GetOrderDetailsData',
        data: JSON.stringify({ orderId: orderId }),
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d == "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else if (response.d === "權限不足") {
                alert("權限不足");
                parent.location.reload();
            } else {
                let data = JSON.parse(response.d);
                let index2 = $(element).index() / 2;
                let detailElement = $('#orderDetail_' + index2);
                let detailHtml = '<table id="orderDetailTable" class="table table-striped table-hover table-bordered">' +
                    '<thead>' +
                    '<tr>' +
                    '<th>訂單ID</th>' +
                    '<th>商品ID</th>' +
                    '<th>商品名稱</th>' +
                    '<th>商品價格</th>' +
                    '<th>商品類型</th>' +
                    '<th>數量</th>' +
                    '<th>小記</th>' +
                    '</tr>' +
                    '</thead>' +
                    '<tbody id="orderDetailTableBody">';

                $.each(data, function (index, item) { 
                    detailHtml += '<tr>' +
                        '<td>' + item.f_id + '</td>' +
                        '<td>' + item.f_productId + '</td>' +
                        '<td>' + item.f_productName + '</td>' +
                        '<td>' + item.f_productPrice + '</td>' +
                        '<td>' + CategoryCodeToText(item.f_productCategory.toString()) + '</td>' +
                        '<td>' + item.f_quantity + '</td>' +
                        '<td>' + item.f_subtotal + '</td>' +
                        '</tr>';
                });
                detailHtml += '</tbody></table>';

                detailElement.html(detailHtml);
            }

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });

}

