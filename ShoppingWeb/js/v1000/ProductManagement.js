let productName = null;
let productCategory = null;
let productMinorCategory = null;
let productBrand = null;
let checkAllMinorCategories = null;
let checkAllBrand = null;
let newCategory = null;
let paginationInitialized = false;
let pageSize = 4;
let pagesTotal = null;
let page = null;
let beforePagesTotal = 1;

$(document).ready(function () {
    // 初始化
    $("#labSearchProduct").hide();
    $("#lowStockProductsDiv").hide();
    ProductDataReady();
    SearchAllData(1, pageSize);
    SetBtnLowProduct();
    setInterval(function () {
        SetBtnLowProduct();
    }, 30000); 
    
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
        EditProductStatus(productId);
    });

    // 新增商品按鈕點擊事件
    $("#btnAddProduct").click(function () {
        window.location.href = "AddProduct.aspx";
    })

    $("#btnLowProduct").click(function () {
        $("#allProductDiv").empty();
        GetDefaultLowStock();
    });
});

//全部商品資料
function SearchAllData(pageNumber, pageSize) {

    if (typeof pageNumber === 'undefined' || typeof pageSize === 'undefined' || typeof beforePagesTotal === 'undefined') {
        $("#labSearchProduct").text("undefined").show().delay(3000).fadeOut();
        return;
    }

    $.ajax({
        url: '/api/Controller/product/GetAllProductData',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ pageNumber: pageNumber, pageSize: pageSize, beforePagesTotal: beforePagesTotal }),
        success: function (response) {
            switch (response.Status) {
                case 0:
                    alert(langFont["duplicateLogin"]);
                    window.parent.location.href = "Login.aspx";
                    break;
                case 1:
                    alert(langFont["accessDenied"]);
                    parent.location.reload();
                    break;
                case 2:
                    $("#labSearchProduct").text(langFont["inputError"]).show().delay(3000).fadeOut();
                    break;
                case 100:
                    // 處理成功取得資料的情況
                    let data = response.ProductDataList
                    let tableBody = $('#tableBody');
                    pagesTotal = response.TotalPages;

                    // 清空表格內容
                    tableBody.empty();

                    // 動態生成表格內容
                    $.each(data, function (index, item) {
                        let row = '<tr>' +
                            '<td>' + item.Id + '</td>' +
                            '<td>' + item.Name + '</td>' +
                            '<td>' + CategoryCodeToText(item.Category.toString()) + '</td>' +
                            '<td>' + item.Price + '</td>' +
                            '<td>' + item.Stock + '</td>' +
                            '<td>' + item.WarningValue + '</td>' +
                            '<td><div class="form-check form-switch"><input type="checkbox" id="toggle' + item.Id + '" class="toggle-switch form-check-input" ' + (item.IsOpen ? 'checked' : '') + ' ' + (item.Stock === 0 ? 'disabled' : '') + ' data-id="' + item.Id + '"></div></td>' +
                            '<td>' + item.Introduce + '</td>' +
                            '<td><img src="/ProductImg/' + item.Img + '" class="img-fluid img-thumbnail" width="80px" height="80px" alt="' + langFont["img"] + '"></td>' +
                            '<td><button class="btn btn-primary" onclick="EditProduct(' + item.Id + ')">' + langFont["editOne"] + '</button></td>' +
                            '<td><button class="btn btn-danger" onclick="DeleteProduct(' + item.Id + ')">' + langFont["delOne"] + '</button></td>' +
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
                    break;
                default:
                    $("#labSearchProduct").text(langFont["errorLog"]).show().delay(3000).fadeOut();
            }
        },
        error: function (error) {
            $("#labSearchProduct").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
        }
    });
}

//搜尋商品資料
function SearchProduct(productCategory, productName, checkAllMinorCategories, checkAllBrand, pageNumber, pageSize) {

    if (typeof productCategory === 'undefined' || typeof productName === 'undefined' || typeof checkAllMinorCategories === 'undefined' || typeof checkAllBrand === 'undefined' || typeof pageNumber === 'undefined' || typeof pageSize === 'undefined' || typeof beforePagesTotal === 'undefined') {
        $("#labSearchProduct").text("undefined").show().delay(3000).fadeOut();
        return;
    }

    $.ajax({
        url: '/api/Controller/product/GetProductData',
        data: JSON.stringify({ productCategory: productCategory, productName: productName, checkAllMinorCategories: checkAllMinorCategories, checkAllBrand: checkAllBrand, pageNumber: pageNumber, pageSize: pageSize, beforePagesTotal: beforePagesTotal }),
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            switch (response.Status) {
                case 0:
                    alert(langFont["duplicateLogin"]);
                    window.parent.location.href = "Login.aspx";
                    break;
                case 1:
                    alert(langFont["accessDenied"]);
                    parent.location.reload();
                    break;
                case 2:
                    $("#labSearchProduct").text(langFont["inputError"]).show().delay(3000).fadeOut();
                    break;
                case 100:
                    $("#productTableDiv").css('display', 'block');
                    let data = response.ProductDataList;
                    let tableBody = $('#tableBody');
                    pagesTotal = response.TotalPages;

                    tableBody.empty();

                    $.each(data, function (index, item) {
                        let row = '<tr>' +
                            '<td>' + item.Id + '</td>' +
                            '<td>' + item.Name + '</td>' +
                            '<td>' + CategoryCodeToText(item.Category.toString()) + '</td>' +
                            '<td>' + item.Price + '</td>' +
                            '<td>' + item.Stock + '</td>' +
                            '<td>' + item.WarningValue + '</td>' +
                            '<td><div class="form-check form-switch"><input type="checkbox" id="toggle' + item.Id + '" class="toggle-switch form-check-input" ' + (item.IsOpen ? 'checked' : '') + ' ' + (item.Stock === 0 ? 'disabled' : '') + ' data-id="' + item.Id + '"></div></td>' +
                            '<td>' + item.Introduce + '</td>' +
                            '<td><img src="/ProductImg/' + item.Img + '" class="img-fluid img-thumbnail" width="80px" height="80px" alt="商品圖片"></td>' +
                            '<td><button class="btn btn-primary" onclick="EditProduct(' + item.Id + ')">' + langFont["editOne"] + '</button></td>' +
                            '<td><button class="btn btn-danger" onclick="DeleteProduct(' + item.Id + ')">' + langFont["delOne"] + '</button></td>' +
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
                    break;
                case 101:
                    $("#productTableDiv").css('display', 'none');
                    $("#labSearchProduct").text(langFont["noData"]).show().delay(3000).fadeOut();
                    $('#ulPagination').empty();
                    break;
                default:
                    $("#labSearchProduct").text(langFont["errorLog"]).show().delay(3000).fadeOut();
            }
        },
        error: function (error) {
            $("#labSearchProduct").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
        }
    });
}

//按下是否開放開關，更改資料庫
function EditProductStatus(productId) {

    if (typeof productId === 'undefined') {
        $("#labSearchProduct").text("undefined").show().delay(3000).fadeOut();
        return;
    }

    $.ajax({
        type: "POST",
        url: "/api/Controller/product/EditProductStatus",
        data: JSON.stringify({ productId: productId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            switch (response.Status) {
                case 0:
                    alert(langFont["duplicateLogin"]);
                    window.parent.location.href = "Login.aspx";
                    break;
                case 1:
                    alert(langFont["accessDenied"]);
                    parent.location.reload();
                    break;
                case 2:
                    $("#labSearchProduct").text(langFont["inputError"]).show().delay(3000).fadeOut();
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
        }
    });
}

//刪除
function DeleteProduct(productId) {

    if (typeof productId === 'undefined') {
        $("#labSearchProduct").text("undefined").show().delay(3000).fadeOut();
        return;
    }

    let yes = confirm(langFont["confirmEditProduct"]);
    if (yes == true) {
        $.ajax({
            type: "POST",
            url: "/api/Controller/product/DelProduct",
            data: JSON.stringify({ productId: productId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                switch (response.Status) {
                    case 0:
                        alert(langFont["duplicateLogin"]);
                        window.parent.location.href = "Login.aspx";
                        break;
                    case 1:
                        alert(langFont["accessDenied"]);
                        parent.location.reload();
                        break;
                    case 2:
                        $("#labSearchProduct").text(langFont["inputError"]).show().delay(3000).fadeOut();
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
            }
        });
    }
}

//編輯
function EditProduct(productId) {

    if (typeof productId === 'undefined') {
        $("#labSearchProduct").text("undefined").show().delay(3000).fadeOut();
        return;
    }

    $.ajax({
        type: "POST",
        url: "/api/Controller/product/SetSessionProductId",
        data: JSON.stringify({ productId: productId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            switch (response.Status) {
                case 0:
                    alert(langFont["duplicateLogin"]);
                    window.parent.location.href = "Login.aspx";
                    break;
                case 1:
                    alert(langFont["accessDenied"]);
                    parent.location.reload();
                    break;
                case 2:
                    $("#labSearchProduct").text(langFont["inputError"]).show().delay(3000).fadeOut();
                    break;
                case 100:
                    window.location.href = "EditProduct.aspx";
                    break;
                default:
                    alert(langFont["editFailed"]);
                    break;
            }
        },
        error: function (error) {
            $("#labSearchProduct").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
        }
    });
}

//按下庫存預警按鈕
function GetDefaultLowStock() {
    $.ajax({
        type: "POST",
        url: "/api/Controller/product/GetDefaultLowStock",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let stockInsufficient = JSON.parse(response.StockInsufficient);
            language = response.Language;
            let lowStockTableBody = $('#lowStockTableBody');
            lowStockTableBody.empty(); // 清空表格內容
            $("#lowStockProductsDiv").show(); // 顯示庫存不足的商品區域

            if (stockInsufficient.length > 0) {
                $("#lowStockTable").show();

                $.each(stockInsufficient, function (index, item) {
                    let name = (language === "TW") ? item.f_nameTW : item.f_nameEN;
                    let row = '<tr>' +
                        '<td>' + item.f_id + '</td>' +
                        '<td>' + name + '</td>' +
                        '<td>' + item.f_stock + '</td>' +
                        '<td>' + item.f_warningValue + '</td>' +
                        '<td><div class="form-check form-switch"><input type="checkbox" id="toggle' + item.f_id + '" class="toggle-switch form-check-input" ' + (item.f_isOpen ? 'checked' : '') + ' ' + (item.f_stock === 0 ? 'disabled' : '') + ' data-id="' + item.f_id + '"></div></td>' +
                        '<td><button class="btn btn-primary" onclick="EditProduct(' + item.f_id + ')">' + langFont["editOne"] + '</button></td>' +
                        '</tr>';
                    lowStockTableBody.append(row);
                });
            } else {
                $("#lowStockTable").hide();
                $("#labSearchStork").text(langFont["noData"]).show().delay(3000).fadeOut();
            }
        },
        error: function (error) {
            $("#labSearchStork").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
        }
    });
}

//設定庫存預警按鈕顏色
function SetBtnLowProduct() {
    $.ajax({
        type: "POST",
        url: "/api/Controller/product/GetDefaultLowStock",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let stockInsufficient = JSON.parse(response.StockInsufficient);

            if (stockInsufficient.length > 0) {
                $("#btnLowProduct").removeClass("btn-outline-primary").addClass("btn-danger");
            } else {
                $("#btnLowProduct").removeClass("btn-danger").addClass("btn-outline-primary");
            }
        },
        error: function (error) {
            $("#labSearchStork").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
        }
    });
}
