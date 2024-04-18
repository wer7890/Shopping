// 訂單狀態
let orderStatus = {
    "1": { name: "未付款", color: "bg-warning", text: "text-white" },
    "2": { name: "付款失敗", color: "bg-danger", text: "text-white" },
    "3": { name: "已付款", color: "bg-white", text: "text-dark" },
    "4": { name: "申請退貨", color: "bg-success", text: "text-white" },
    "5": { name: "退款中", color: "bg-warning", text: "text-white" },
    "6": { name: "已退款", color: "bg-white", text: "text-dark" }
};

// 配送狀態
let deliveryStatus = {
    "1": { name: "發貨中", color: "bg-warning", text: "text-white" },
    "2": { name: "已發貨", color: "bg-success", text: "text-white" },
    "3": { name: "已到貨", color: "bg-white", text: "text-dark" },
    "4": { name: "已取貨", color: "bg-white", text: "text-dark" },
    "5": { name: "退貨中", color: "bg-warning", text: "text-white" },
    "6": { name: "已退貨", color: "bg-white", text: "text-dark" }
};

// 配送方式
let deliveryMethod = {
    "1": "超商取貨",
    "2": "店到店",
    "3": "宅配"
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
        $("#box").empty().fadeOut(300);
        $("#overlay").fadeOut(300);
    });

    // 使用事件代理監聽訂單狀態下拉選單值的變化
    $(document).on('change', '#orderStatusSelect', function () {
        // 獲取選中的訂單狀態值
        let selectedOrderStatus = $(this).val();
        console.log(selectedOrderStatus);

        let deliveryStatusSelect = $("#deliveryStatusSelect");
        deliveryStatusSelect.empty();

        if (selectedOrderStatus == 5 || selectedOrderStatus == 6) {
            deliveryStatusSelect.append('<option value="5">退貨中</option>');
            deliveryStatusSelect.append('<option value="6">已退貨</option>');
        } else if (selectedOrderStatus == 3 || selectedOrderStatus == 4) {
            deliveryStatusSelect.append('<option value="2">已發貨</option>');
            deliveryStatusSelect.append('<option value="3">已到貨</option>');
            deliveryStatusSelect.append('<option value="4">已取貨</option>');
        } else if (selectedOrderStatus == 1 || selectedOrderStatus == 2) {
            deliveryStatusSelect.append('<option value="1" selected>發貨中</option>');
        } else {
            $.each(deliveryStatus, function (key, value) {
                deliveryStatusSelect.append('<option value="' + key + '">' + value.name + '</option>');
            });
        }
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
                deliveryStatusValue = 0;
                $("#orderSure").css('display', 'none');;
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

// 顯示更改狀態的下拉選單
function ShowEditOrder(element, orderId, orderStatusNum, deliveryStatusNum, deliveryMethodNum) {
    selectedOrderId = orderId;

    let trIndex = $(element).index() / 2;
    let detailElement = $('#orderDetail_' + trIndex);
    detailElement.empty();

    $(".collapse > td > div").empty();

    let selectHtml = '<div class="row d-flex justify-content-center my-3">';
    // 訂單狀態
    selectHtml += '<div class="col"><label for="orderStatusSelect" class="form-label">訂單狀態</label><select id="orderStatusSelect" class="form-select">';
    $.each(orderStatus, function (key, value) {
        selectHtml += (key == orderStatusNum) ? '<option value="' + key + '" selected >' + value.name + '</option>' : '<option value="' + key + '">' + value.name + '</option>';
    });
    selectHtml += '</select></div>';

    // 配送狀態
    selectHtml += '<div class="col"><label for="deliveryStatusSelect" class="form-label">配送狀態</label><select id="deliveryStatusSelect" class="form-select">';

    // 根據訂單狀態決定配送狀態的選項
    if (orderStatusNum == 5 || orderStatusNum == 6) {
        // 訂單狀態為退款中或已退款時，只能選擇退貨中或已退回
        selectHtml += '<option value="5">退貨中</option>';
        selectHtml += '<option value="6">已退貨</option>';
    } else if (orderStatusNum == 3 || orderStatusNum == 4) {
        // 訂單狀態為已付款時，只能選擇已到貨或已取貨
        selectHtml += '<option value="2">已發貨</option>';
        selectHtml += '<option value="3">已到貨</option>';
        selectHtml += '<option value="4">已取貨</option>';
    } else if (orderStatusNum == 1 || orderStatusNum == 2) {
        // 訂單狀態為未付款或付款失敗時，只能選擇發貨中
        selectHtml += '<option value="1" selected>發貨中</option>';
    } else {
        // 其他情況下顯示所有配送狀態的選項
        $.each(deliveryStatus, function (key, value) {
            selectHtml += (key == deliveryStatusNum) ? '<option value="' + key + '" selected >' + value.name + '</option>' : '<option value="' + key + '">' + value.name + '</option>';
        });
    }

    selectHtml += '</select></div>';

    // 配送方式
    selectHtml += '<div class="col"><label for="deliveryMethodSelect" class="form-label">配送方式</label><select id="deliveryMethodSelect" class="form-select">';
    $.each(deliveryMethod, function (key, value) {
        selectHtml += (key == deliveryMethodNum) ? '<option value="' + key + '" selected>' + value + '</option>' : '<option value="' + key + '">' + value + '</option>';
    });
    selectHtml += '</select></div>';
    selectHtml += '<div class="col-1 d-flex align-items-end"><button id="btnEditOrder" type="submit" class="btn btn-outline-primary">修改</button></div>' +
        '<div class="col-1 d-flex align-items-end"><button id="btnShowOrderDetail" type="submit" class="btn btn-outline-primary">詳情</button></div>' +
        '</select></div>';

    detailElement.append(selectHtml);
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
            if (response.d == "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else if (response.d === "權限不足") {
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
                        '<td>' + item.f_productName + '</td>' +
                        '<td>' + item.f_productPrice + '</td>' +
                        '<td>' + CategoryCodeToText(item.f_productCategory.toString()) + '</td>' +
                        '<td>' + item.f_quantity + '</td>' +
                        '<td>' + item.f_subtotal + '</td>' +
                        '</tr>';
                });
                detailHtml += '</tbody></table><div class="w-100 d-flex justify-content-center"><button id="btnCloseOrderDetail" class="btn btn-outline-primary">關閉</button></div>';                
                $("#overlay").fadeIn(300);
                detailElement.prepend(detailHtml).fadeIn(300);
            }

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

// 更改訂單
function EditOrderData(orderId, orderStatusNum, deliveryStatusNum, deliveryMethodNum) {
    $.ajax({
        type: "POST",
        url: "/Ajax/OrderHandler.aspx/EditOrder",
        data: JSON.stringify({ orderId: orderId, orderStatusNum: orderStatusNum, deliveryStatusNum: deliveryStatusNum, deliveryMethodNum: deliveryMethodNum }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d === "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else if (response.d === "權限不足") {
                alert("權限不足");
                parent.location.reload();
            } else if (response.d === "更改成功") {
                $("#labSearchOrder").text(response.d).show().delay(3000).fadeOut();
                if (deliveryStatusValue === 0) {
                    SearchAllOrder();
                } else if (deliveryStatusValue === 7) {
                    ShowReturnOrder();
                } else {
                    ShowOrder(deliveryStatusValue);
                }
                
            } else {
                $("#labSearchOrder").text(response.d).show().delay(3000).fadeOut();
            }
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
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
            if (response.d == "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else if (response.d === "權限不足") {
                alert("權限不足");
                parent.location.reload();
            } else {
                deliveryStatusValue = deliveryStatusNum;
                $("#orderSure").css('display', 'none');;
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

//訂單表格內容
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
            '<td>NT$' + item.f_total + '</td>' +
            '</tr>' +
            '<tr id="collapse_' + index + '" class="collapse">' +
            '<td class="p-0" colspan="8"><div id="orderDetail_' + index + '"></div></td>' +
            '</tr>';
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
        $("#btnOrderStatus_4 > span").text(item.orderStatus4);
    });
}

//退款申請
function ShowReturnOrder() {
    $.ajax({
        url: '/Ajax/OrderHandler.aspx/GetReturnOrderData',
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
                deliveryStatusValue = 7;
                let orderData = JSON.parse(response.d[0]);
                let deliveryStatusCountData = JSON.parse(response.d[1]);

                $("#orderSure").css('display', 'block');

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
                        '<td>NT$' + item.f_total + '</td>' +
                        '<td class="p-1"><div class="d-flex justify-content-between">' +
                        '<button type="button" class="btn btn-outline-primary btn-sm" onclick="EditReturnOrder(' + item.f_id + ', true)">接受</button>' +
                        '<button type="button" class="btn btn-outline-danger btn-sm" onclick="EditReturnOrder(' + item.f_id + ', false)">拒絕</button>' +
                        '</div></td>' +
                        '</tr>';
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
                    $("#btnOrderStatus_4 > span").text(item.orderStatus4);
                });
            }

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

//同意退款申請後，更改訂單狀態和配送狀態
function EditReturnOrder(orderId, boolReturn) {
    $.ajax({
        url: '/Ajax/OrderHandler.aspx/EditReturnOrder',
        data: JSON.stringify({ orderId: orderId, boolReturn: boolReturn }),
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
                $("#labSearchOrder").text(response.d).show().delay(3000).fadeOut();
                ShowReturnOrder();
            }

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}
