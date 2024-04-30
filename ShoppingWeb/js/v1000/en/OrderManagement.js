let translations = {
    'titleOrder': {
        'zh': '訂單',
        'en': 'Order'
    },
    'thAll': {
        'zh': '全部',
        'en': 'All'
    },
    'thShipping': {
        'zh': '發貨中',
        'en': 'Shipping'
    },
    'thShipped': {
        'zh': '已發貨',
        'en': 'Shipped'
    },
    'thArrived': {
        'zh': '已到貨',
        'en': 'Arrived'
    },
    'thReceived': {
        'zh': '已取貨',
        'en': 'Received'
    },
    'thReturning': {
        'zh': '退貨中',
        'en': 'Returning'
    },
    'thReturned': {
        'zh': '已退貨',
        'en': 'Returned'
    },
    'thReturnRequested': {
        'zh': '申請退貨',
        'en': 'Return'
    },
    'thId': {
        'zh': '訂單編號',
        'en': 'ID'
    },
    'thSerialNumber': {
        'zh': '訂購者',
        'en': 'Orderer'
    },
    'thCreatedTime': {
        'zh': '下單時間',
        'en': 'Order Time'
    },
    'thOrderStatus': {
        'zh': '訂單狀態',
        'en': 'Order Status'
    },
    'thDeliveryStatus': {
        'zh': '配送狀態',
        'en': 'Delivery Status'
    },
    'thDeliveryMethod': {
        'zh': '配送方式',
        'en': 'Delivery Method'
    },
    'thTotal': {
        'zh': '總金額',
        'en': 'Total'
    },
    'btnEdit': {
        'zh': '修改',
        'en': 'Edit'
    },
    'btnDetails': {
        'zh': '詳情',
        'en': 'Details'
    },
    'thPaid': {
        'zh': '已付款',
        'en': 'Paid'
    },
    'thRefunding': {
        'zh': '退款中',
        'en': 'Refunding'
    },
    'thRefunded': {
        'zh': '已退款',
        'en': 'Refunded'
    },
    'thStorePickup': {
        'zh': '超商取貨',
        'en': 'Store Pickup'
    },
    'thStoreToStore': {
        'zh': '店到店',
        'en': 'Store to Store'
    },
    'thHomeDelivery': {
        'zh': '宅配',
        'en': 'Home Delivery'
    }
};

// 訂單狀態
let orderStatus = {
    "1": { name: "Paid", color: "bg-white", text: "text-dark" },
    "2": { name: "Return", color: "bg-success", text: "text-white" },
    "3": { name: "Refunding", color: "bg-warning", text: "text-white" },
    "4": { name: "Refunded", color: "bg-white", text: "text-dark" }
};

// 配送狀態
let deliveryStatus = {
    "1": { name: "Shipping", color: "bg-warning", text: "text-white" },
    "2": { name: "Shipped", color: "bg-success", text: "text-white" },
    "3": { name: "Arrived", color: "bg-white", text: "text-dark" },
    "4": { name: "Received", color: "bg-white", text: "text-dark" },
    "5": { name: "Returning", color: "bg-warning", text: "text-white" },
    "6": { name: "Returned", color: "bg-white", text: "text-dark" }
};

// 配送方式
let deliveryMethod = {
    "1": "Store Pickup",
    "2": "Store to Store",
    "3": "Home Delivery"
};


let selectedOrderId;
let deliveryStatusValue;

$(document).ready(function () {
    //初始化
    SearchAllOrder();
    $("#labSearchOrder").hide();

    // 修改訂單按鈕
    $('#tableBody').on('click', '#btnEditOrder', function () {
        let selectedOrderStatus = $("#orderStatusSelect").val();
        let selectedDeliveryStatus = $("#deliveryStatusSelect").val();
        let selectedDeliveryMethod = $("#deliveryMethodSelect").val();
        EditOrderData(selectedOrderId, selectedOrderStatus, selectedDeliveryStatus, selectedDeliveryMethod);
    });

    // 詳情按鈕
    $('#tableBody').on('click', '#btnShowOrderDetail', function () {
        ShowOrderDetail(selectedOrderId);
    });

    // 關閉訂單詳情按鈕
    $(document).on('click', '#btnCloseOrderDetail', function () {
        $("#box").empty();
        $("#overlay").fadeOut(300);
    });

    // 訂單狀態改變時顯示的配送狀態
    $(document).on('change', '#orderStatusSelect', function () {
        // 獲取選中的訂單狀態值
        let selectedOrderStatus = $(this).val();

        let deliveryStatusSelect = $("#deliveryStatusSelect");
        deliveryStatusSelect.empty();

        let selectHtml = "";

        if (selectedOrderStatus == 1) {
            selectHtml += '<option value="1">Shipping</option>' +
                '<option value="2">Shipped</option>' +
                '<option value="3">Arrived</option>' +
                '<option value="4">Received</option>';
        } else if (selectedOrderStatus == 2) {
            selectHtml += '<option value="4">Received</option>';
        } else if (selectedOrderStatus == 3) {
            selectHtml += '<option value="5">Returning</option>' +
                '<option value="6">已退貨</option>';
        } else if (selectedOrderStatus == 4) {
            selectHtml += '<option value="6">Returned</option>';
        } else {
            $.each(deliveryStatus, function (key, value) {
                selectHtml += '<option value="' + key + '">' + value.name + '</option>';
            });
        }

        deliveryStatusSelect.append(selectHtml);

    });

});

//全部訂單資料
function SearchAllOrder() {
    $.ajax({
        url: '/Ajax/OrderHandler.aspx/GetAllOrderData',
        type: 'POST',
        contentType: 'application/json',
        success: function (response) {
            if (response.d === 0) {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else if (response.d === 1) {
                alert("權限不足");
                parent.location.reload();
            } else {
                deliveryStatusValue = 0;
                $("#orderSure").remove();
                let orderData = JSON.parse(response.d[0]);
                let deliveryStatusCountData = JSON.parse(response.d[1]);
                OrderHtml(orderData, deliveryStatusCountData);
            }

            TranslateLanguage("en");

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

// 點選訂單表格時，顯示更改狀態的下拉選單
function ShowEditOrder(element, orderId, orderStatusNum, deliveryStatusNum, deliveryMethodNum) {
    selectedOrderId = orderId;

    let trIndex = $(element).index() / 2;
    let detailElement = $('#orderDetail_' + trIndex);
    detailElement.empty();

    $(".collapse > td > div").empty();

    let selectHtml = '<div class="row d-flex justify-content-center my-3">';
    // 訂單狀態
    selectHtml += '<div class="col"><label for="orderStatusSelect" class="form-label">Order Status</label><select id="orderStatusSelect" class="form-select">';

    if (orderStatusNum == 1) {
        selectHtml += '<option value="1">Paid</option>' +
            '<option value="2">Return</option>';
    } else if (orderStatusNum == 2) {
        selectHtml += '<option value="2">Return</option>';
    } else if (orderStatusNum == 3) {
        selectHtml += '<option value="3">Refunding</option>' +
            '<option value="4">Refunded</option>';
    } else if (orderStatusNum == 4) {
        selectHtml += '<option value="4">Refunded</option>';
    } else {
        $.each(orderStatus, function (key, value) {
            selectHtml += (key == orderStatusNum) ? '<option value="' + key + '" selected >' + value.name + '</option>' : '<option value="' + key + '">' + value.name + '</option>';
        });
    }

    selectHtml += '</select></div>';

    // 配送狀態
    selectHtml += '<div class="col"><label for="deliveryStatusSelect" class="form-label">Delivery Status</label><select id="deliveryStatusSelect" class="form-select">';

    // 根據訂單狀態決定配送狀態的選項
    if (orderStatusNum == 1) {
        // 訂單狀態為已付款時
        selectHtml += '<option value="1">Shipping</option>' +
            '<option value="2">Shipped</option>' +
            '<option value="3">Arrived</option>' +
            '<option value="4">Received</option>';
    } else if (orderStatusNum == 2) {
        // 訂單狀態為申請退貨時
        selectHtml += '<option value="4">Received</option>';
    } else if (orderStatusNum == 3) {
        // 訂單狀態為退款中或已退款時
        selectHtml += '<option value="5">Returning</option>' +
            '<option value="6">Returned</option>';
    } else if (orderStatusNum == 4) {
        selectHtml += '<option value="6">Returned</option>';
    } else {
        // 其他情況下顯示所有配送狀態的選項
        $.each(deliveryStatus, function (key, value) {
            selectHtml += (key == deliveryStatusNum) ? '<option value="' + key + '" selected >' + value.name + '</option>' : '<option value="' + key + '">' + value.name + '</option>';
        });
    }

    selectHtml += '</select></div>';

    // 配送方式
    selectHtml += '<div class="col"><label for="deliveryMethodSelect" class="form-label">Delivery Method</label><select id="deliveryMethodSelect" class="form-select">';
    $.each(deliveryMethod, function (key, value) {
        selectHtml += (key == deliveryMethodNum) ? '<option value="' + key + '" selected>' + value + '</option>' : '<option value="' + key + '">' + value + '</option>';
    });
    selectHtml += '</select></div>';

    // 按鈕
    if (orderStatusNum != 4) {
        selectHtml += '<div class="col-1 d-flex align-items-end px-0"><button id="btnEditOrder" type="submit" class="btn btn-outline-primary px-2 i18n" data-key="btnEdit">修改</button></div>';
    }

    selectHtml += '<div class="col-1 d-flex align-items-end px-0"><button id="btnShowOrderDetail" type="submit" class="btn btn-outline-primary px-2 i18n" data-key="btnDetails">詳情</button></div>' +
        '</select></div>';

    detailElement.append(selectHtml);
    TranslateLanguage("en");
}

// 上方狀態按鈕點擊觸發事件
function ShowOrder(deliveryStatusNum) {
    $.ajax({
        url: '/Ajax/OrderHandler.aspx/GetOrderData',
        data: JSON.stringify({ deliveryStatusNum: deliveryStatusNum }),
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d === 0) {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else if (response.d === 1) {
                alert("權限不足");
                parent.location.reload();
            } else {
                deliveryStatusValue = deliveryStatusNum;
                $("#orderSure").remove();
                let orderData = JSON.parse(response.d[0]);
                let deliveryStatusCountData = JSON.parse(response.d[1]);
                OrderHtml(orderData, deliveryStatusCountData);
            }

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

// 訂單表格內容
function OrderHtml(orderData, deliveryStatusCountData) {
    let tableBody = $('#tableBody');

    tableBody.empty();
    let row = "";
    $.each(orderData, function (index, item) {
        row += '<tr class="px-3" data-bs-toggle="collapse" data-bs-target="#collapse_' + index + '" onclick="ShowEditOrder(this, \'' + item.f_id + '\', \'' + item.f_orderStatus + '\', \'' + item.f_deliveryStatus + '\', \'' + item.f_deliveryMethod + '\')">' +
            '<td>' + item.f_id + '</td>' +
            '<td>' + item.f_account + '</td>' +
            '<td>' + item.f_createdTime + '</td>' +
            '<td>' +
            '<span class="px-3 py-1 rounded ' + orderStatus[item.f_orderStatus].color + ' ' + orderStatus[item.f_orderStatus].text + '">' + orderStatus[item.f_orderStatus].name + '</span>' +
            '</td>' +
            '<td>' +
            '<span class="px-3 py-1 rounded ' + deliveryStatus[item.f_deliveryStatus].color + ' ' + deliveryStatus[item.f_deliveryStatus].text + '">' + deliveryStatus[item.f_deliveryStatus].name + '</span>' +
            '</td>' +
            '<td>' + deliveryMethod[item.f_deliveryMethod] + '</td>' +
            '<td>NT$' + item.f_total + '</td>';

        if (deliveryStatusValue === 7) {
            row += '<td><div class="d-flex justify-content-between">' +
                '<button type="button" class="btn btn-outline-primary btn-sm" onclick="EditReturnOrder(' + item.f_id + ', true)">accept</button>' +
                '<button type="button" class="btn btn-outline-danger btn-sm" onclick="EditReturnOrder(' + item.f_id + ', false)">reject</button>' +
                '</div></td>' +
                '</tr>';
        } else {
            row += '</tr>' +
                '<tr id="collapse_' + index + '" class="collapse">' +
                '<td class="p-0" colspan="8"><div id="orderDetail_' + index + '"></div></td>' +
                '</tr>';
        }
    });

    tableBody.append(row);

    $.each(deliveryStatusCountData, function (index, item) {
        $("#btnDeliveryStatus_0 > span").text(item.statusAll);
        $("#btnDeliveryStatus_1 > span").text(item.status1);
        $("#btnDeliveryStatus_2 > span").text(item.status2);
        $("#btnDeliveryStatus_3 > span").text(item.status3);
        $("#btnDeliveryStatus_4 > span").text(item.status4);
        $("#btnDeliveryStatus_5 > span").text(item.status5);
        $("#btnDeliveryStatus_6 > span").text(item.status6);
        $("#btnOrderStatus_2 > span").text(item.orderStatus2);
    });
}

// 顯示訂單詳細內容
function ShowOrderDetail(orderId) {
    $.ajax({
        url: '/Ajax/OrderHandler.aspx/GetOrderDetailsData',
        data: JSON.stringify({ orderId: orderId }),
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d === 0) {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else if (response.d === 1) {
                alert("權限不足");
                parent.location.reload();
            } else {
                selectedOrderId = orderId;

                let data = JSON.parse(response.d);
                let detailElement = $("#box");

                //明細
                let detailHtml = '<table id="orderDetailTable" class="table table-striped table-hover table-bordered my-4">' +
                    '<thead>' +
                    '<tr>' +
                    '<th>Product name</th>' +
                    '<th>Price</th>' +
                    '<th>Category</th>' +
                    '<th>Quantity</th>' +
                    '<th>Subtotal</th>' +
                    '</tr>' +
                    '</thead>' +
                    '<tbody id="orderDetailTableBody">';

                $.each(data, function (index, item) {
                    detailHtml += '<tr>' +
                        '<td>' + item.f_productName + '</td>' +
                        '<td>' + item.f_productPrice + '</td>' +
                        '<td>' + CategoryCodeToText(item.f_productCategory.toString()) + '</td>' +
                        '<td>' + item.f_quantity + '</td>' +
                        '<td>' + item.f_subtotal + '</td>' +
                        '</tr>';
                });
                detailHtml += '</tbody></table><div class="w-100 d-flex justify-content-center"><button id="btnCloseOrderDetail" class="btn btn-outline-primary">closure</button></div>';
                detailElement.prepend(detailHtml);
                $("#overlay").fadeIn(300);
            }

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

// 更改訂單
function EditOrderData(orderId, orderStatusNum, deliveryStatusNum, deliveryMethodNum) {

    if (!IsSpecialChar(orderId, orderStatusNum, deliveryStatusNum, deliveryMethodNum)) {
        return;
    }

    $.ajax({
        type: "POST",
        url: "/Ajax/OrderHandler.aspx/EditOrder",
        data: JSON.stringify({ orderId: orderId, orderStatusNum: orderStatusNum, deliveryStatusNum: deliveryStatusNum, deliveryMethodNum: deliveryMethodNum }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            switch (response.d) {
                case 0:
                    alert("重複登入，已被登出");
                    window.parent.location.href = "Login.aspx";
                    break;
                case 1:
                    alert("權限不足");
                    parent.location.reload();
                    break;
                case 2:
                    alert("輸入值錯誤");
                    break;
                case 100:
                case 101:
                    let temp = (response.d === 100) ? "更新成功" : "庫存不足或更新時發生錯誤";
                    $("#labSearchOrder").text(temp).show().delay(3000).fadeOut();
                    if (deliveryStatusValue === 0) {
                        SearchAllOrder();
                    } else if (deliveryStatusValue === 7) {
                        ShowReturnOrder();
                    } else {
                        ShowOrder(deliveryStatusValue);
                    }
                    break;
                default:
                    $("#labSearchOrder").text("發生發生內部錯誤，請看日誌").show().delay(3000).fadeOut();
            }
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

// 點擊上方退貨申請按鈕事件
function ShowReturnOrder() {
    $.ajax({
        url: '/Ajax/OrderHandler.aspx/GetReturnOrderData',
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d === 0) {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else if (response.d === 1) {
                alert("權限不足");
                parent.location.reload();
            } else {
                deliveryStatusValue = 7;
                let orderData = JSON.parse(response.d[0]);
                let deliveryStatusCountData = JSON.parse(response.d[1]);

                $("#orderSure").remove();
                $("#myTable > thead > tr").append("<th id='orderSure'>Return</th>");
                OrderHtml(orderData, deliveryStatusCountData);
            }

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

// 同意或拒絕退款申請後，更改訂單狀態和配送狀態
function EditReturnOrder(orderId, boolReturn) {
    $.ajax({
        url: '/Ajax/OrderHandler.aspx/EditReturnOrder',
        data: JSON.stringify({ orderId: orderId, boolReturn: boolReturn }),
        type: 'POST',
        contentType: 'application/json',
        success: function (response) {
            switch (response.d) {
                case 0:
                    alert("重複登入，已被登出");
                    window.parent.location.href = "Login.aspx";
                    break;
                case 1:
                    alert("權限不足");
                    parent.location.reload();
                    break;
                case 2:
                    alert("輸入值錯誤");
                    break;
                case 100:
                case 101:
                    let temp = (response.d === 100) ? "更改成功" : "更改失敗";
                    $("#labSearchOrder").text(temp).show().delay(3000).fadeOut();
                    ShowReturnOrder();
                    break;
                default:
                    $("#labSearchOrder").text("發生發生內部錯誤，請看日誌").show().delay(3000).fadeOut();

            }
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

// 判斷輸入值
function IsSpecialChar(orderId, orderStatusNum, deliveryStatusNum, deliveryMethodNum) {
    if (typeof orderId === 'undefined' || typeof orderStatusNum === 'undefined' || typeof deliveryStatusNum === 'undefined' || typeof deliveryMethodNum === 'undefined') {
        $("#labSearchOrder").text("undefined").show().delay(3000).fadeOut();
        return false;
    }

    let orderIdRegex = /^[0-9]{1,10}$/;
    let orderStatusNumRegex = /^[1-4]{1}$/;
    let deliveryStatusNumRegex = /^[1-6]{1}$/;
    let deliveryMethodNumRegex = /^[1-3]{1}$/;

    if (!orderIdRegex.test(orderId) || !orderStatusNumRegex.test(orderStatusNum) || !deliveryStatusNumRegex.test(deliveryStatusNum) || !deliveryMethodNumRegex.test(deliveryMethodNum)) {
        $("#labSearchOrder").text("輸入值錯誤").show().delay(3000).fadeOut();
        return false;
    }

    return true;
}