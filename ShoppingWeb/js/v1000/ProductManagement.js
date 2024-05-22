let productName = null;
let productCategory = null;
let productMinorCategory = null;
let productBrand = null;
let checkAllMinorCategories = null;
let checkAllBrand = null;
let newCategory = null;
let paginationInitialized = false;
let pageSize = 3;
let pagesTotal = null;
let page = null;
let beforePagesTotal = 1;

$(document).ready(function () {
    // 初始化
    ProductDataReady();
    SearchAllData(1, pageSize);
    $("#labSearchProduct").hide();

    // 搜尋按鈕點擊事件
    $("#btnSearchProduct").click(function () {
        paginationInitialized = false;
        beforePagesTotal = 1;
        productName = $("#txbProductSearch").val();
        productCategory = $("#productCategory").val();  // 獲取大分類值
        productMinorCategory = $("#minorCategory").val(); // 獲取小分類值
        productBrand = $("#brandCategory").val(); // 獲取品牌值
        checkAllMinorCategories = (productMinorCategory == "00");  //是否為全部小分類
        checkAllBrand = (productBrand == "00");  //是否為全部品牌

        newCategory = productCategory + productMinorCategory + productBrand;  //類別編號組合
        SearchProduct(newCategory, productName, checkAllMinorCategories, checkAllBrand, 1, pageSize);
    });

    // 商品是否開放開關
    $(document).on("change", ".toggle-switch", function () {
        let productId = $(this).data('id');
        ToggleProductStatus(productId);
    });

    // 新增商品按鈕點擊事件
    $("#btnAddProduct").click(function () {
        window.location.href = "AddProduct.aspx";
    })
});

//全部商品資料
function SearchAllData(pageNumber, pageSize) {
    $.ajax({
        url: '/Ajax/ProductHandler.aspx/GetAllProductData',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ pageNumber: pageNumber, pageSize: pageSize, beforePagesTotal: beforePagesTotal }),
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
                case 102:
                    $("#labSearchProduct").text(langFont["errorLog"]).show().delay(3000).fadeOut();
                    break;
                default:
                    // 處理成功取得資料的情況
                    let data = JSON.parse(response.d.Data); // 解析 JSON 資料為 JavaScript 物件
                    let tableBody = $('#tableBody');
                    pagesTotal = response.d.TotalPages;

                    // 清空表格內容
                    tableBody.empty();

                    // 動態生成表格內容
                    $.each(data, function (index, item) {
                        let row = '<tr>' +
                            '<td>' + item.f_id + '</td>' +
                            '<td>' + item.f_name + '</td>' +
                            '<td>' + CategoryCodeToText(item.f_category.toString()) + '</td>' +
                            '<td>' + item.f_price + '</td>' +
                            '<td>' + item.f_stock + '</td>' +
                            '<td><div class="form-check form-switch"><input type="checkbox" id="toggle' + item.f_id + '" class="toggle-switch form-check-input" ' + (item.f_isOpen ? 'checked' : '') + ' data-id="' + item.f_id + '"></div></td>' +
                            '<td>' + item.f_introduce + '</td>' +
                            '<td><img src="/ProductImg/' + item.f_img + '" class="img-fluid img-thumbnail" width="80px" height="80px" alt="' + langFont["img"] + '"></td>' +
                            '<td><button class="btn btn-primary" onclick="EditProduct(' + item.f_id + ')">' + langFont["editOne"] + '</button></td>' +
                            '<td><button class="btn btn-danger" onclick="DeleteProduct(' + item.f_id + ')">' + langFont["delOne"] + '</button></td>' +
                            '</tr>';

                        tableBody.append(row);
                    });

                    if (!paginationInitialized) {
                        page = new Pagination({
                            id: 'pagination',
                            total: pagesTotal,
                            showButtons: 5,
                            showFirstLastButtons: true,
                            showGoInput: true,
                            showPagesTotal: true,
                            callback: function (pageIndex) {
                                SearchAllData(pageIndex + 1, pageSize);
                            }
                        });
                        paginationInitialized = true;
                    } else if (beforePagesTotal !== pagesTotal) {
                        alert("資料頁數變動");
                        page.Update(pagesTotal);
                    }

                    beforePagesTotal = pagesTotal;
            }
        },
        error: function (error) {
            $("#labSearchProduct").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
            AddToErrorQueue("HTTP狀態碼: " + error.status + "'\n'HTTP狀態碼文本描述: " + error.statusText + "'\n'詳細訊息: " + error.responseText);
        }
    });
}

//搜尋商品資料
function SearchProduct(productCategory, productName, checkAllMinorCategories, checkAllBrand, pageNumber, pageSize) {
    $.ajax({
        url: '/Ajax/ProductHandler.aspx/GetProductData',
        data: JSON.stringify({ productCategory: productCategory, productName: productName, checkAllMinorCategories: checkAllMinorCategories, checkAllBrand: checkAllBrand, pageNumber: pageNumber, pageSize: pageSize, beforePagesTotal: beforePagesTotal }),
        type: 'POST',
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
                case 101:
                    $("#productTableDiv").css('display', 'none');
                    $("#labSearchProduct").text(langFont["noData"]).show().delay(3000).fadeOut();
                    $('#ulPagination').empty();
                    break;
                case 102:
                    $("#labSearchProduct").text(langFont["errorLog"]).show().delay(3000).fadeOut();
                    break;
                default:
                    $("#productTableDiv").css('display', 'block');
                    let data = JSON.parse(response.d.Data);
                    let tableBody = $('#tableBody');
                    pagesTotal = response.d.TotalPages;

                    tableBody.empty();

                    $.each(data, function (index, item) {
                        let row = '<tr>' +
                            '<td>' + item.f_id + '</td>' +
                            '<td>' + item.f_name + '</td>' +
                            '<td>' + CategoryCodeToText(item.f_category.toString()) + '</td>' +
                            '<td>' + item.f_price + '</td>' +
                            '<td>' + item.f_stock + '</td>' +
                            '<td><div class="form-check form-switch"><input type="checkbox" id="toggle' + item.f_id + '" class="toggle-switch form-check-input" ' + (item.f_isOpen ? 'checked' : '') + ' data-id="' + item.f_id + '"></div></td>' +
                            '<td>' + item.f_introduce + '</td>' +
                            '<td><img src="/ProductImg/' + item.f_img + '" class="img-fluid img-thumbnail" width="80px" height="80px" alt="商品圖片"></td>' +
                            '<td><button class="btn btn-primary" onclick="EditProduct(' + item.f_id + ')">' + langFont["editOne"] + '</button></td>' +
                            '<td><button class="btn btn-danger" onclick="DeleteProduct(' + item.f_id + ')">' + langFont["delOne"] + '</button></td>' +
                            '</tr>';
                        tableBody.append(row);
                    });

                    if (!paginationInitialized) {
                        page.Update(pagesTotal, function (pageIndex) {
                            SearchProduct(newCategory, productName, checkAllMinorCategories, checkAllBrand, pageIndex + 1, pageSize);
                        });
                        paginationInitialized = true;
                    } else if (beforePagesTotal !== pagesTotal) {
                        alert("資料頁數變動");
                        page.Update(pagesTotal);
                    }

                    beforePagesTotal = pagesTotal;
            }
        },
        error: function (error) {
            $("#labSearchProduct").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
            AddToErrorQueue("HTTP狀態碼: " + error.status + "'\n'HTTP狀態碼文本描述: " + error.statusText + "'\n'詳細訊息: " + error.responseText);
        }
    });
}


//按下是否開放開關，更改資料庫
function ToggleProductStatus(productId) {
    $.ajax({
        type: "POST",
        url: "/Ajax/ProductHandler.aspx/ToggleProductStatus",
        data: JSON.stringify({ productId: productId }),
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
                case 100:
                    $("#labSearchProduct").text(langFont["editSuccessful"]).show().delay(3000).fadeOut();
                    break;
                case 101:
                    $("#labSearchProduct").text(langFont["editFail"]).show().delay(3000).fadeOut();
                    break;
                default:
                    $("#labSearchProduct").text(langFont["errorLog"]).show().delay(3000).fadeOut();
            }
        },
        error: function (error) {
            $("#labSearchProduct").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
            AddToErrorQueue("HTTP狀態碼: " + error.status + "'\n'HTTP狀態碼文本描述: " + error.statusText + "'\n'詳細訊息: " + error.responseText);
        }
    });
}

//刪除
function DeleteProduct(productId) {
    let yes = confirm(langFont["confirmEditProduct"]);
    if (yes == true) {
        $.ajax({
            type: "POST",
            url: "/Ajax/ProductHandler.aspx/RemoveProduct",
            data: JSON.stringify({ productId: productId }),
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
                    case 100:
                        window.location.reload();
                        break;
                    case 101:
                        $("#labSearchProduct").text(langFont["delFailed"]).show().delay(3000).fadeOut();
                        break;
                    default:
                        $("#labSearchProduct").text(langFont["errorLog"]).show().delay(3000).fadeOut();
                }
            },
            error: function (error) {
                $("#labSearchProduct").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
                AddToErrorQueue("HTTP狀態碼: " + error.status + "'\n'HTTP狀態碼文本描述: " + error.statusText + "'\n'詳細訊息: " + error.responseText);
            }
        });
    }
}

//編輯
function EditProduct(productId) {
    $.ajax({
        type: "POST",
        url: "/Ajax/ProductHandler.aspx/SetSessionProductId",
        data: JSON.stringify({ productId: productId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d === true) {
                window.location.href = "EditProduct.aspx";
            }
        },
        error: function (error) {
            $("#labSearchProduct").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
            AddToErrorQueue("HTTP狀態碼: " + error.status + "'\n'HTTP狀態碼文本描述: " + error.statusText + "'\n'詳細訊息: " + error.responseText);
        }
    });
}
