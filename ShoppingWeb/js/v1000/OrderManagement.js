let selectedOrderId;
let deliveryStatusValue;
let isReturn;
let paginationInitialized = false;
let pageSize = 10;
let pagesTotal = null;
let page = null;
let beforePagesTotal = null;

$(document).ready(function () {
    //初始化
    SearchAllData(1, pageSize);
    $("#labSearchOrder").hide();

    //點選上方按鈕時，該按鈕變色
    $(".btnHand").click(function () {
        paginationInitialized = false;
        $(".btnHand").each(function (index) {
            $(this).removeClass("btn-secondary").addClass("btn-outline-secondary");
        });

        $(this).removeClass("btn-outline-secondary").addClass("btn-secondary");

        let buttonId = $(this).attr("id"); // 獲取按鈕的 id
        let deliveryStatus = buttonId.split("_")[1]; // 從 id 中提取出狀態碼

        // 根據狀態碼執行相應的函數
        if (deliveryStatus === "0") {
            SearchAllData(1, pageSize);
        } else if (deliveryStatus === "7") {
            ShowReturnOrder(1, pageSize);
        } else {
            ShowOrder(deliveryStatus, 1, pageSize);
        }
    });

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
            selectHtml += '<option value="1">' + langFont["shipping"] + '</option>' +
                '<option value="2">' + langFont["shipped"] + '</option>' +
                '<option value="3">' + langFont["arrived"] + '</option>' +
                '<option value="4">' + langFont["received"] + '</option>';
        } else if (selectedOrderStatus == 2) {
            selectHtml += '<option value="4">' + langFont["received"] + '</option>';
        } else if (selectedOrderStatus == 3) {
            selectHtml += '<option value="5">' + langFont["returning"] + '</option>' +
                '<option value="6">' + langFont["returned"] + '</option>';
        } else if (selectedOrderStatus == 4) {
            selectHtml += '<option value="6">' + langFont["returned"] + '</option>';
        } else {
            $.each(deliveryStatus, function (key, value) {
                selectHtml += '<option value="' + key + '">' + value.name + '</option>';
            });
        }

        deliveryStatusSelect.append(selectHtml);

    });
});

//全部訂單資料
function SearchAllData(pageNumber, pageSize) {
    $.ajax({
        url: '/Ajax/OrderHandler.aspx/GetAllOrderData',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ pageNumber: pageNumber, pageSize: pageSize }),
        success: function (response) {
            if (response.d === 0) {
                alert(langFont["duplicateLogin"]);
                window.parent.location.href = "Login.aspx";
            } else if (response.d === 1) {
                alert(langFont["accessDenied"]);
                parent.location.reload();
            } else if (response.d === 102) {
                $("#labSearchOrder").text(langFont["errorLog"]).show().delay(3000).fadeOut();
            } else {
                $("#orderTableDiv").css('display', 'block');
                deliveryStatusValue = 0;
                $("#orderSure").remove();
                let orderData = JSON.parse(response.d.Data[0]);
                let deliveryStatusCountData = JSON.parse(response.d.Data[1]);
                pagesTotal = response.d.TotalPages;
                OrderHtml(orderData, deliveryStatusCountData);

                if (!paginationInitialized) {
                    page = new Pagination({
                        id: 'pagination',
                        total: pagesTotal,
                        showButtons: 5,
                        showFirstLastButtons: true,
                        callback: function (pageIndex) {
                            SearchAllData(pageIndex + 1, pageSize);
                        }
                    });
                    paginationInitialized = true;
                } else {

                    if (beforePagesTotal !== pagesTotal) {
                        alert("資料頁數變動");
                        SearchAllData(1, pageSize);
                        page.Update(pagesTotal);
                    }

                }

                beforePagesTotal = pagesTotal;
            }
        },
        error: function (error) {
            $("#labSearchOrder").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
            AddToErrorQueue("HTTP狀態碼: " + error.status + "'\n'HTTP狀態碼文本描述: " + error.statusText + "'\n'詳細訊息: " + error.responseText);
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
    selectHtml += '<div class="col"><label for="orderStatusSelect" class="form-label">' + langFont["orderStatus"] + '</label><select id="orderStatusSelect" class="form-select">';

    if (orderStatusNum == 1) {
        selectHtml += '<option value="1">' + langFont["paid"] + '</option>' +
            '<option value="2">' + langFont["return"] + '</option>';
    } else if (orderStatusNum == 2) {
        selectHtml += '<option value="2">' + langFont["return"] + '</option>';
    } else if (orderStatusNum == 3) {
        selectHtml += '<option value="3">' + langFont["refunding"] + '</option>' +
            '<option value="4">' + langFont["refunded"] + '</option>';
    } else if (orderStatusNum == 4) {
        selectHtml += '<option value="4">' + langFont["refunded"] + '</option>';
    } else {
        $.each(orderStatus, function (key, value) {
            selectHtml += (key == orderStatusNum) ? '<option value="' + key + '" selected >' + value.name + '</option>' : '<option value="' + key + '">' + value.name + '</option>';
        });
    }

    selectHtml += '</select></div>';

    // 配送狀態
    selectHtml += '<div class="col"><label for="deliveryStatusSelect" class="form-label">' + langFont["deliveryStatus"] + '</label><select id="deliveryStatusSelect" class="form-select">';

    // 根據訂單狀態決定配送狀態的選項
    if (orderStatusNum == 1) {
        // 訂單狀態為已付款時
        selectHtml += '<option value="1">' + langFont["shipping"] + '</option>' +
            '<option value="2">' + langFont["shipped"] + '</option>' +
            '<option value="3">' + langFont["arrived"] + '</option>' +
            '<option value="4">' + langFont["received"] + '</option>';
    } else if (orderStatusNum == 2) {
        // 訂單狀態為申請退貨時
        selectHtml += '<option value="4">' + langFont["received"] + '</option>';
    } else if (orderStatusNum == 3) {
        // 訂單狀態為退款中或已退款時
        selectHtml += '<option value="5">' + langFont["returning"] + '</option>' +
            '<option value="6">' + langFont["returned"] + '</option>';
    } else if (orderStatusNum == 4) {
        selectHtml += '<option value="6">' + langFont["returned"] + '</option>';
    } else {
        // 其他情況下顯示所有配送狀態的選項
        $.each(deliveryStatus, function (key, value) {
            selectHtml += (key == deliveryStatusNum) ? '<option value="' + key + '" selected >' + value.name + '</option>' : '<option value="' + key + '">' + value.name + '</option>';
        });
    }

    selectHtml += '</select></div>';

    // 配送方式
    selectHtml += '<div class="col"><label for="deliveryMethodSelect" class="form-label">' + langFont["deliveryMethod"] + '</label><select id="deliveryMethodSelect" class="form-select">';
    $.each(deliveryMethod, function (key, value) {
        selectHtml += (key == deliveryMethodNum) ? '<option value="' + key + '" selected>' + value + '</option>' : '<option value="' + key + '">' + value + '</option>';
    });
    selectHtml += '</select></div>';

    // 按鈕
    if (orderStatusNum != 4) {
        selectHtml += '<div class="col-1 d-flex align-items-end px-0"><button id="btnEditOrder" type="submit" class="btn btn-outline-primary px-2">' + langFont["edit"] + '</button></div>';
    }

    selectHtml += '<div class="col-1 d-flex align-items-end px-0"><button id="btnShowOrderDetail" type="submit" class="btn btn-outline-primary px-2">' + langFont["details"] + '</button></div>' +
        '</select></div>';

    detailElement.append(selectHtml);
}

// 上方狀態按鈕點擊觸發事件
function ShowOrder(deliveryStatusNum, pageNumber, pageSize) {
    $.ajax({
        url: '/Ajax/OrderHandler.aspx/GetOrderData',
        data: JSON.stringify({ deliveryStatusNum: deliveryStatusNum, pageNumber: pageNumber, pageSize: pageSize }),
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d === 0) {
                alert(langFont["duplicateLogin"]);
                window.parent.location.href = "Login.aspx";
            } else if (response.d === 1) {
                alert(langFont["accessDenied"]);
                parent.location.reload();
            } else if (response.d === 101) {
                $('#ulPagination, #paginationInfo').empty();
                $("#orderTableDiv").css('display', 'none');
                $("#labSearchOrder").text(langFont["noData"]).show().delay(3000).fadeOut();
                
            } else if (response.d === 102) {
                $("#labSearchOrder").text(langFont["errorLog"]).show().delay(3000).fadeOut();
            } else {
                $("#orderTableDiv").css('display', 'block');
                deliveryStatusValue = deliveryStatusNum;
                $("#orderSure").remove();
                let orderData = JSON.parse(response.d.Data[0]);
                let deliveryStatusCountData = JSON.parse(response.d.Data[1]);
                pagesTotal = response.d.TotalPages;
                isReturn = false;

                OrderHtml(orderData, deliveryStatusCountData);


                if (!paginationInitialized) {
                    page.Update(pagesTotal, function (pageIndex) {
                        ShowOrder(deliveryStatusValue, pageIndex + 1, pageSize);
                    });
                    paginationInitialized = true;
                } else {

                    if (beforePagesTotal !== pagesTotal) {
                        alert("資料頁數變動");
                        ShowOrder(deliveryStatusValue, 1, pageSize);
                        page.Update(pagesTotal);
                    }

                }

                beforePagesTotal = pagesTotal;
            }

        },
        error: function (error) {
            $("#labSearchOrder").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
            AddToErrorQueue("HTTP狀態碼: " + error.status + "'\n'HTTP狀態碼文本描述: " + error.statusText + "'\n'詳細訊息: " + error.responseText);
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
                '<button type="button" class="btn btn-outline-primary btn-sm" onclick="EditReturnOrder(' + item.f_id + ', true)">' + langFont["yes"] + '</button>' +
                '<button type="button" class="btn btn-outline-danger btn-sm" onclick="EditReturnOrder(' + item.f_id + ', false)">' + langFont["no"] + '</button>' +
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
    $(".btnSpan").remove();
    $.each(deliveryStatusCountData, function (index, item) {
        $("#btnDeliveryStatus_0").append('<span class="btnSpan">(' + item.statusAll + ')</span>');
        $("#btnDeliveryStatus_1").append('<span class="btnSpan">(' + item.status1 + ')</span>');
        $("#btnDeliveryStatus_2").append('<span class="btnSpan">(' + item.status2 + ')</span>');
        $("#btnDeliveryStatus_3").append('<span class="btnSpan">(' + item.status3 + ')</span>');
        $("#btnDeliveryStatus_4").append('<span class="btnSpan">(' + item.status4 + ')</span>');
        $("#btnDeliveryStatus_5").append('<span class="btnSpan">(' + item.status5 + ')</span>');
        $("#btnDeliveryStatus_6").append('<span class="btnSpan">(' + item.status6 + ')</span>');
        $("#btnDeliveryStatus_7").append('<span class="btnSpan">(' + item.orderStatus2 + ')</span>');
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
                alert(langFont["duplicateLogin"]);
                window.parent.location.href = "Login.aspx";
            } else if (response.d === 1) {
                alert(langFont["accessDenied"]);
                parent.location.reload();
            } else if (response.d === 102) {
                $("#labSearchOrder").text(langFont["errorLog"]).show().delay(3000).fadeOut();
            } else {
                selectedOrderId = orderId;

                let data = JSON.parse(response.d);
                let detailElement = $("#box");

                //明細
                let detailHtml = '<table id="orderDetailTable" class="table table-striped table-hover table-bordered my-4">' +
                    '<thead>' +
                    '<tr>' +
                    '<th>' + langFont["productName"] + '</th>' +
                    '<th>' + langFont["productPrice"] + '</th>' +
                    '<th>' + langFont["productType"] + '</th>' +
                    '<th>' + langFont["quantity"] + '</th>' +
                    '<th>' + langFont["subtotal"] + '</th>' +
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
                detailHtml += '</tbody></table><div class="w-100 d-flex justify-content-center"><button id="btnCloseOrderDetail" class="btn btn-outline-primary">' + langFont["closure"] + '</button></div>';
                detailElement.prepend(detailHtml);
                $("#overlay").fadeIn(300);
            }

        },
        error: function (error) {
            $("#labSearchOrder").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
            AddToErrorQueue("HTTP狀態碼: " + error.status + "'\n'HTTP狀態碼文本描述: " + error.statusText + "'\n'詳細訊息: " + error.responseText);
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
                    alert(langFont["duplicateLogin"]);
                    window.parent.location.href = "Login.aspx";
                    break;
                case 1:
                    alert(langFont["accessDenied"]);
                    parent.location.reload();
                    break;
                case 2:
                    alert(langFont["inputError"]);
                    break;
                case 100:
                case 101:
                    let temp = (response.d === 100) ? langFont["editSuccessful"] : langFont["editOrderFailed"];
                    $("#labSearchOrder").text(temp).show().delay(3000).fadeOut();
                    if (deliveryStatusValue === 0) {
                        SearchAllData(1, pageSize);
                    } else if (deliveryStatusValue === 7) {
                        ShowReturnOrder(1, pageSize);
                    } else {
                        ShowOrder(deliveryStatusValue, 1, pageSize);
                    }
                    break;
                default:
                    $("#labSearchOrder").text(langFont["errorLog"]).show().delay(3000).fadeOut();
            }
        },
        error: function (error) {
            $("#labSearchOrder").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
            AddToErrorQueue("HTTP狀態碼: " + error.status + "'\n'HTTP狀態碼文本描述: " + error.statusText + "'\n'詳細訊息: " + error.responseText);
        }
    });
}

// 點擊上方退貨申請按鈕事件
function ShowReturnOrder(pageNumber, pageSize) {
    $.ajax({
        url: '/Ajax/OrderHandler.aspx/GetReturnOrderData',
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ pageNumber: pageNumber, pageSize: pageSize }),
        success: function (response) {
            if (response.d === 0) {
                alert(langFont["duplicateLogin"]);
                window.parent.location.href = "Login.aspx";
            } else if (response.d === 1) {
                alert(langFont["accessDenied"]);
                parent.location.reload();
            } else if (response.d === 101) {
                $("#orderTableDiv").css('display', 'none');
                $("#labSearchOrder").text(langFont["noData"]).show().delay(3000).fadeOut();
                $('#ulPagination, #paginationInfo').empty();
            } else if (response.d === 102) {
                $("#labSearchOrder").text(langFont["errorLog"]).show().delay(3000).fadeOut();
            } else {
                $("#orderTableDiv").css('display', 'block');
                deliveryStatusValue = 7;
                let orderData = JSON.parse(response.d.Data[0]);
                let deliveryStatusCountData = JSON.parse(response.d.Data[1]);
                pagesTotal = response.d.TotalPages;
                isReturn = true;

                $("#orderSure").remove();
                $("#myTable > thead > tr").append("<th id='orderSure'>" + langFont['orderSure'] + "</th>");
                OrderHtml(orderData, deliveryStatusCountData);

                if (!paginationInitialized) {
                    page.Update(pagesTotal, function (pageIndex) {
                        ShowReturnOrder(pageIndex + 1, pageSize);
                    });
                    paginationInitialized = true;
                } else {

                    if (beforePagesTotal !== pagesTotal) {
                        alert("資料頁數變動");
                        ShowReturnOrder(1, pageSize);
                        page.Update(pagesTotal);
                    }

                }

                beforePagesTotal = pagesTotal;
            }

        },
        error: function (error) {
            $("#labSearchOrder").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
            AddToErrorQueue("HTTP狀態碼: " + error.status + "'\n'HTTP狀態碼文本描述: " + error.statusText + "'\n'詳細訊息: " + error.responseText);
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
                    alert(langFont["duplicateLogin"]);
                    window.parent.location.href = "Login.aspx";
                    break;
                case 1:
                    alert(langFont["accessDenied"]);
                    parent.location.reload();
                    break;
                case 2:
                    alert(langFont["inputError"]);
                    break;
                case 100:
                case 101:
                    let temp = (response.d === 100) ? langFont["editSuccessful"] : langFont["editFail"];
                    $("#labSearchOrder").text(temp).show().delay(3000).fadeOut();
                    paginationInitialized = false;
                    ShowReturnOrder(1, pageSize);
                    break;
                default:
                    $("#labSearchOrder").text(langFont["errorLog"]).show().delay(3000).fadeOut();

            }
        },
        error: function (error) {
            $("#labSearchOrder").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
            AddToErrorQueue("HTTP狀態碼: " + error.status + "'\n'HTTP狀態碼文本描述: " + error.statusText + "'\n'詳細訊息: " + error.responseText);
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
        $("#labSearchOrder").text(langFont["inputError"]).show().delay(3000).fadeOut();
        return false;
    }

    return true;
}