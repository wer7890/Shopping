let productName = null;
let productCategory = null;
let productMinorCategory = null;
let productBrand = null;
let checkAllMinorCategories = null;
let checkAllBrand = null;
let newCategory = null;

$(document).ready(function () {
    let currentPage = 1; // 初始頁碼為 1
    let pageSize = 5; // 每頁顯示的資料筆數

    // 初始化
    ProductDataReady();
    SearchAllProduct(currentPage, pageSize);

    // 上一頁按鈕點擊事件
    $("#ulPagination").on("click", "#previousPage", function () {
        if (currentPage > 1) {
            currentPage--;
            SearchAllProduct(currentPage, pageSize);
        }
        $("#labSearchProduct").text("");
    });

    // 下一頁按鈕點擊事件
    $("#ulPagination").on("click", "#nextPage", function () {
        if (currentPage < $('#ulPagination').children('li').length - 2) {  // 獲取id="ulPagination"下的li元素個數，-2是因為要扣掉上跟下一頁
            currentPage++;
            SearchAllProduct(currentPage, pageSize);
        }
        $("#labSearchProduct").text("");
    });

    // 數字頁數點擊事件
    $("#pagination").on('click', 'a.pageNumber', function () {
        currentPage = parseInt($(this).text());
        SearchAllProduct(currentPage, pageSize);
        $("#labSearchProduct").text("");
    });

    // 搜尋按鈕點擊事件
    $("#btnSearchProduct").click(function () {
        currentPage = 1;
        pageSize = 5;
        $("#labSearchProduct").text("");
        productName = $("#txbProductSearch").val();
        productCategory = $("#productCategory").val();  // 獲取大分類值
        productMinorCategory = $("#minorCategory").val(); // 獲取小分類值
        productBrand = $("#brandCategory").val(); // 獲取品牌值
        checkAllMinorCategories = (productMinorCategory == "00");  //是否為全部小分類
        checkAllBrand = (productBrand == "00");  //是否為全部品牌
        
        newCategory = productCategory + productMinorCategory + productBrand;  //類別編號組合
        SearchProduct(newCategory, productName, checkAllMinorCategories, checkAllBrand, currentPage, pageSize);
    });

    // 搜尋後上一頁按鈕點擊事件
    $("#ulPagination").on("click", "#searchPreviousPage", function () {
        if (currentPage > 1) {
            currentPage--;
            SearchProduct(newCategory, productName, checkAllMinorCategories, checkAllBrand, currentPage, pageSize);
        }
        $("#labSearchProduct").text("");
    });

    // 搜尋後下一頁按鈕點擊事件
    $("#ulPagination").on("click", "#searchNextPage", function () {
        if (currentPage < $('#ulPagination').children('li').length - 2) {  // 獲取id="ulPagination"下的li元素個數，-2是因為要扣掉上跟下一頁
            currentPage++;
            SearchProduct(newCategory, productName, checkAllMinorCategories, checkAllBrand, currentPage, pageSize);
        }
        $("#labSearchProduct").text("");
    });

    // 搜尋後數字頁數點擊事件
    $("#pagination").on('click', 'a.searchPageNumber', function () {
        currentPage = parseInt($(this).text());
        SearchProduct(newCategory, productName, checkAllMinorCategories, checkAllBrand, currentPage, pageSize);
        $("#labSearchProduct").text("");
    });

    // 開關改變事件
    $(document).on("change", ".toggle-switch", function () {
        $("#labSearchProduct").text("");
        let productId = $(this).data('id');
        ToggleProductStatus(productId);
    });

    // 新增商品按鈕點擊事件
    $("#btnAddProduct").click(function () {
        window.location.href = "AddProduct.aspx";
    })
});

//全部商品資料
function SearchAllProduct(pageNumber, pageSize) {
    $.ajax({
        url: '/Ajax/ProductHandler.aspx/GetAllProductData',
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
            } else {
                // 處理成功取得資料的情況
                let data = JSON.parse(response.d.Data); // 解析 JSON 資料為 JavaScript 物件
                let tableBody = $('#tableBody');

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
                        '<td><img src="/ProductImg/' + item.f_img + '" class="img-fluid img-thumbnail" width="80px" height="80px" alt="商品圖片"></td>' +
                        '<td><button class="btn btn-primary" onclick="EditProduct(' + item.f_id + ')">' + langFont["editOne"] + '</button></td>' +
                        '<td><button class="btn btn-danger" onclick="DeleteProduct(' + item.f_id + ')">' + langFont["delOne"] + '</button></td>' +
                        '</tr>';

                    tableBody.append(row);
                });

                //依資料筆數來開分頁頁數
                if (response.d.TotalPages > 0) {
                    let ulPagination = $('#ulPagination');
                    ulPagination.empty();
                    ulPagination.append('<li class="page-item" id="previousPage"><a class="page-link" href="#"> << </a></li>');
                    for (let i = 1; i <= response.d.TotalPages; i++) {
                        ulPagination.append('<li class="page-item" id="page' + i + '"><a class="page-link pageNumber" href="#">' + i + '</a></li>');
                    }
                    ulPagination.append('<li class="page-item" id="nextPage"><a class="page-link" href="#"> >> </a></li>');

                }
            }
            UpdatePaginationControls(pageNumber);
        },
        error: function (error) {
            console.error('Error:', error);
            $("#labSearchProduct").text(langFont["ajaxError"]);
        }
    });
}

//搜尋商品資料
function SearchProduct(productCategory, productName, checkAllMinorCategories, checkAllBrand, pageNumber, pageSize) {
    $.ajax({
        url: '/Ajax/ProductHandler.aspx/GetProductData',
        data: JSON.stringify({ productCategory: productCategory, productName: productName, checkAllMinorCategories: checkAllMinorCategories, checkAllBrand: checkAllBrand, pageNumber: pageNumber, pageSize: pageSize }),
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
                $("#productTableDiv").css('display', 'none');
                $("#labSearchProduct").text(langFont["noData"]);
                $('#ulPagination').empty();
            } else {
                $("#productTableDiv").css('display', 'block');
                let data = JSON.parse(response.d.Data);
                let tableBody = $('#tableBody');

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

                if (response.d.TotalPages > 0) {
                    let ulPagination = $('#ulPagination');
                    ulPagination.empty();
                    ulPagination.append('<li class="page-item" id="searchPreviousPage"><a class="page-link" href="#"> << </a></li>');
                    for (let i = 1; i <= response.d.TotalPages; i++) {
                        ulPagination.append('<li class="page-item" id="page' + i + '"><a class="page-link searchPageNumber" href="#">' + i + '</a></li>');
                    }
                    ulPagination.append('<li class="page-item" id="searchNextPage"><a class="page-link" href="#"> >> </a></li>');

                }
            }
            UpdatePaginationControls(pageNumber);
        },
        error: function (error) {
            console.error('Error:', error);
            $("#labSearchProduct").text(langFont["ajaxError"]);
        }
    });
}

//當切換到哪個頁面時，就把該頁面的按鈕變色
function UpdatePaginationControls(currentPage) {
    $('#pagination .page-item').removeClass('active');
    $('#page' + currentPage).addClass('active');
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
                    $("#labSearchProduct").text(langFont["editSuccessful"]);
                    break;
                case 101:
                    $("#labSearchProduct").text(langFont["editFail"]);
                    break;
                default:
                    $("#labSearchProduct").text(langFont["errorLog"]);
            }
        },
        error: function (error) {
            console.error('Error:', error);
            $("#labSearchProduct").text(langFont["ajaxError"]);
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
                        $("#labSearchProduct").text(langFont["delFailed"]);
                        break;
                    default:
                        $("#labSearchProduct").text(langFont["errorLog"]);
                }
            },
            error: function (error) {
                console.error('Error:', error);
                $("#labSearchProduct").text(langFont["ajaxError"]);
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
            console.error('Error:', error);
            $("#labSearchProduct").text(langFont["ajaxError"]);
        }
    });


}

