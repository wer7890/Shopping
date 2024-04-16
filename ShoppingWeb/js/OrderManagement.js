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
    SearchAllOrder();

    // 訂單狀態下拉選單
    $('#tableBody').on('change', '#orderStatusSelect', function () {
        let selectedStatus = $(this).val();
        console.log("訂單狀態變動為：" + selectedStatus);
    });

    // 付款狀態下拉選單變動
    $('#tableBody').on('change', '#paymentStatusSelect', function () {
        let selectedStatus = $(this).val();
        console.log("付款狀態變動為：" + selectedStatus);
    });

    // 配送狀態下拉選單
    $('#tableBody').on('change', '#deliveryStatusSelect', function () {
        let selectedStatus = $(this).val();
        console.log("配送狀態變動為：" + selectedStatus);
    });

    // 配送方式下拉選單
    $('#tableBody').on('change', '#deliveryMethodSelect', function () {
        let selectedMethod = $(this).val();
        console.log("配送方式變動為：" + selectedMethod);
    });
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
                    let row = '<tr data-bs-toggle="collapse" data-bs-target="#collapse_' + index + '" onclick="ShowOrderDetail(this, \'' + item.f_id + '\', \'' + item.f_orderStatus + '\', \'' + item.f_paymentStatus + '\', \'' + item.f_deliveryStatus + '\', \'' + item.f_deliveryMethod + '\')">' +
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
function ShowOrderDetail(element, orderId, orderStatusNum, paymentStatusNum, deliveryStatusNum, deliveryMethodNum) {
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
                let trIndex = $(element).index() / 2;
                let detailElement = $('#orderDetail_' + trIndex);
                detailElement.empty();

                let selectHtml = '<div class="row justify-content-center mt-2">';
                // 訂單狀態
                selectHtml += '<div class="col"><label for="orderStatusSelect" class="form-label">訂單狀態</label><select id="orderStatusSelect" class="form-select">';
                $.each(orderStatus, function (key, value) {
                    selectHtml += (key == orderStatusNum) ? '<option value="' + key + '" selected >' + value.name + '</option>' : '<option value="' + key + '">' + value.name + '</option>';
                });
                selectHtml += '</select></div>';

                // 付款狀態
                selectHtml += '<div class="col"><label for="paymentStatusSelect" class="form-label">付款狀態</label><select id="paymentStatusSelect" class="form-select">';
                $.each(paymentStatus, function (key, value) {
                    selectHtml += (key == paymentStatusNum) ? '<option value="' + key + '" selected >' + value.name + '</option>' : '<option value="' + key + '">' + value.name + '</option>';
                });
                selectHtml += '</select></div>';

                // 配送狀態
                selectHtml += '<div class="col"><label for="deliveryStatusSelect" class="form-label">配送狀態</label><select id="deliveryStatusSelect" class="form-select">';
                $.each(deliveryStatus, function (key, value) {
                    selectHtml += (key == deliveryStatusNum) ? '<option value="' + key + '" selected >' + value.name + '</option>' : '<option value="' + key + '">' + value.name + '</option>';
                });
                selectHtml += '</select></div>';

                // 配送方式
                selectHtml += '<div class="col"><label for="deliveryMethodSelect" class="form-label">配送方式</label><select id="deliveryMethodSelect" class="form-select">';
                $.each(deliveryMethod, function (key, value) {
                    selectHtml += (key == deliveryMethodNum) ? '<option value="' + key + '" selected>' + value + '</option>' : '<option value="' + key + '">' + value + '</option>';
                });
                selectHtml += '</select></div>';
                selectHtml += '</div>';

                detailElement.append(selectHtml);

                //明細
                let detailHtml = '<table id="orderDetailTable" class="table table-striped table-hover table-bordered mt-4">' +
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

                detailElement.append(detailHtml);
            }

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

