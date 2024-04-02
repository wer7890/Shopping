let productName = null;
let productCategory = null;
let productMinorCategory = null;
let productBrand = null;
let checkAllMinorCategories = null;
let checkAllBrand = null;
let newCategory = null;

// 大分類
let majorCategories = {
    "10": "帽子",
    "11": "上衣",
    "12": "外套",
    "13": "褲子"
};

// 小分類
let minorCategories = {
    "0": {
        "0": "請先選擇類型"
    },
    "10": {
        "00": "全部",
        "01": "其他",
        "02": "棒球帽",
        "03": "漁夫帽",
        "04": "遮陽帽"
    },
    "11": {
        "00": "全部",
        "01": "其他",
        "02": "襯衫",
        "03": "毛衣",
        "04": "帽T"
    },
    "12": {
        "00": "全部",
        "01": "其他",
        "02": "皮外套",
        "03": "風衣",
        "04": "牛仔外套"
    },
    "13": {
        "00": "全部",
        "01": "其他",
        "02": "運動褲",
        "03": "休閒褲",
        "04": "西褲"
    }
};

// 品牌分類
let brand = {
    "00": "全部",
    "01": "其他",
    "02": "NIKE",
    "03": "FILA",
    "04": "ADIDAS",
    "05": "PUMA"
}

$(document).ready(function () {
    let currentPage = 1; // 初始頁碼為 1
    let pageSize = 5; // 每頁顯示的資料筆數

    // 初始化
    window.parent.getUserPermission();
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
            if (response.d == "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
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
                        '<td><button class="btn btn-primary" onclick="EditProduct(' + item.f_id + ')">改</button></td>' +
                        '<td><button class="btn btn-danger" onclick="DeleteProduct(' + item.f_id + ')">刪</button></td>' +
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

            if (response.d == "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else if (response.d == "null") {
                $("#productTableDiv").css('display', 'none');
                $("#labSearchProduct").text("沒有資料");
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
                        '<td><button class="btn btn-primary" onclick="EditProduct(' + item.f_id + ')">改</button></td>' +
                        '<td><button class="btn btn-danger" onclick="DeleteProduct(' + item.f_id + ')">刪</button></td>' +
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

            if (response.d === "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else if (response.d === "更改成功") {
                $("#labSearchProduct").text("更改成功");
            } else {
                $("#labSearchProduct").text(response.d);
            }
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

//刪除
function DeleteProduct(productId) {
    let yes = confirm('確定要刪除該商品嗎');
    if (yes == true) {
        $.ajax({
            type: "POST",
            url: "/Ajax/ProductHandler.aspx/RemoveProduct",
            data: JSON.stringify({ productId: productId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d === "重複登入") {
                    alert("重複登入，已被登出");
                    window.parent.location.href = "Login.aspx";
                } else if (response.d === "刪除成功") {
                    window.location.reload();
                } else {
                    $("#labSearchProduct").text(response.d);
                }
            },
            error: function (error) {
                console.error('Error:', error);
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
                window.location.href = "RenewProduct.aspx";
            }
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });


}

//商品分類，選擇框設定
function ProductDataReady() {

    // 創建 select 元素並初始化大分類選項
    let categorySelect = $("<select>").attr("id", "productCategory").addClass("form-select");
    categorySelect.prepend($("<option>").attr("value", "0").text("請選擇商品類型"));
    for (let key in majorCategories) {

        if (Object.prototype.hasOwnProperty.call(majorCategories, key)) {
            categorySelect.append($("<option>").attr("value", key).text(majorCategories[key]));
        }

    }
    $("#divCategories").append('<label for="productCategory" class="form-label">商品類型</label>').append(categorySelect);

    // 根據所選的大分類更新小分類選項
    $("#productCategory").change(function () {
        let selectedMajorCategory = $(this).val(); // 獲取所選的大分類
        let minorCategorySelect = $("<select>").attr("id", "minorCategory").addClass("form-select");

        // 根據所選的大分類更新小分類選項
        for (let key in minorCategories[selectedMajorCategory]) {

            if (Object.prototype.hasOwnProperty.call(minorCategories[selectedMajorCategory], key)) {
                minorCategorySelect.append($("<option>").attr("value", key).text(minorCategories[selectedMajorCategory][key]));
            }

        }

        // 替換原有的小分類 select 元素
        $("#divMinorCategory").empty().append('<label for="minorCategory" class="form-label">子類型</label>').append(minorCategorySelect);
    });

    // 創建品牌 select 元素
    let brandSelect = $("<select>").attr("id", "brandCategory").addClass("form-select");
    for (let key in brand) {

        if (Object.prototype.hasOwnProperty.call(brand, key)) {
            brandSelect.append($("<option>").attr("value", key).text(brand[key]));
        }

    }
    $("#divBrand").append('<label for="brandCategory" class="form-label">品牌</label>').append(brandSelect);
}

//把類型代號轉成文字
function CategoryCodeToText(category) {
    let dbMajorCategories = category.substring(0, 2);
    let dbMinorCategories = category.substring(2, 4);
    let dbBrand = category.substring(4, 6);

    let result = majorCategories[dbMajorCategories] + "-" + minorCategories[dbMajorCategories][dbMinorCategories] + "-" + brand[dbBrand];
    return result;
}
