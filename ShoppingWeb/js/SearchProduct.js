$(document).ready(function () {
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
            "01": "其他",
            "02": "棒球帽",
            "03": "漁夫帽",
            "04": "遮陽帽"
        },
        "11": {
            "01": "其他",
            "02": "襯衫",
            "03": "毛衣",
            "04": "帽T"
        },
        "12": {
            "01": "其他",
            "02": "皮外套",
            "03": "風衣",
            "04": "牛仔外套"
        },
        "13": {
            "01": "其他",
            "02": "運動褲",
            "03": "休閒褲",
            "04": "西褲"
        }
    };

    // 品牌分類
    let brand = {
        "00": "所有",
        "01": "其他",
        "02": "NIKE",
        "03": "FILA",
        "04": "ADIDAS",
        "05": "PUMA"
    }

    // 創建 select 元素並初始化大分類選項
    var categorySelect = $("<select>").attr("id", "productCategory").addClass("form-select");
    categorySelect.prepend($("<option>").attr("value", "0").text("請選擇商品類型"));
    for (let key in majorCategories) {

        if (Object.prototype.hasOwnProperty.call(majorCategories, key)) {
            categorySelect.append($("<option>").attr("value", key).text(majorCategories[key]));
        }

    }
    $("#divCategories").append(categorySelect);

    // 根據所選的大分類更新小分類選項
    $("#productCategory").change(function () {
        var selectedMajorCategory = $(this).val(); // 獲取所選的大分類
        var minorCategorySelect = $("<select>").attr("id", "minorCategory").addClass("form-select");

        // 根據所選的大分類更新小分類選項
        for (let key in minorCategories[selectedMajorCategory]) {

            if (Object.prototype.hasOwnProperty.call(minorCategories[selectedMajorCategory], key)) {
                minorCategorySelect.append($("<option>").attr("value", key).text(minorCategories[selectedMajorCategory][key]));
            }

        }

        // 替換原有的小分類 select 元素
        $("#divMinorCategory").empty().append(minorCategorySelect);
    });

    // 創建品牌 select 元素
    var brandSelect = $("<select>").attr("id", "brandCategory").addClass("form-select");
    for (let key in brand) {

        if (Object.prototype.hasOwnProperty.call(brand, key)) {
            brandSelect.append($("<option>").attr("value", key).text(brand[key]));
        }

    }
    $("#divBrand").append(brandSelect);

    //顯示全部商品
    SearchAllProduct();
    
    $("#btnSearchProduct").click(function () {
        $("#labSearchProduct").text("");
        $('#tableBody').empty();
        let productName = $("#txbProductSearch").val();
        let productCategory = $("#productCategory").val();  // 獲取大分類值
        let productMinorCategory = $("#minorCategory").val(); // 獲取小分類值
        let productBrand = $("#brandCategory").val(); // 獲取品牌值
        let checkAllBrand = false;  //是否為全部品牌

        if (productBrand == "00") {
            checkAllBrand = true;
        }

        let newCategory = productCategory + productMinorCategory + productBrand;
        SearchProduct(newCategory, productName, checkAllBrand);
    });


    // 使用事件代理監聽開關的改變事件
    $(document).on("change", ".toggle-switch", function () {
        $("#labSearchProduct").text("");
        let productId = $(this).data('id');
        ToggleProductStatus(productId);
    });
    

    $("#btnAddProduct").click(function () {
        window.location.href = "AddProduct.aspx";
    })
})

//全部商品資料
function SearchAllProduct() {
    $.ajax({
        url: '/Ajax/SearchProductHandler.aspx/GetAllProductData',
        type: 'POST',
        contentType: 'application/json',
        success: function (response) {
            if (response.d == "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else {
                // 處理成功取得資料的情況
                let data = JSON.parse(response.d); // 解析 JSON 資料為 JavaScript 物件
                let tableBody = $('#tableBody');

                // 清空表格內容
                tableBody.empty();

                // 動態生成表格內容
                $.each(data, function (index, item) {
                    let row = '<tr>' +
                        '<td>' + item.f_id + '</td>' +
                        '<td>' + item.f_name + '</td>' +
                        '<td>' + item.f_category + '</td>' +
                        '<td>' + item.f_price + '</td>' +
                        '<td>' + item.f_stock + '</td>' +
                        '<td><div class="form-check form-switch"><input type="checkbox" id="toggle' + item.f_id + '" class="toggle-switch form-check-input" ' + (item.f_isOpen ? 'checked' : '') + ' data-id="' + item.f_id + '"></div></td>' +
                        //'<td>' + item.f_productOwner + '</td>' +
                        //'<td>' + item.f_productCreatedOn + '</td>' +
                        '<td>' + item.f_introduce + '</td>' +
                        '<td><img src="/ProductImg/' + item.f_img + '" class="img-fluid img-thumbnail" width="80px" height="80px" alt="商品圖片"></td>' +
                        '<td><button class="btn btn-primary" onclick="editProduct(' + item.f_id + ')">改</button></td>' +
                        '<td><button class="btn btn-danger" onclick="deleteProduct(' + item.f_id + ')">刪</button></td>' +
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

//搜尋商品資料
function SearchProduct(productCategory, productName, checkAllBrand) {
    $.ajax({
        url: '/Ajax/SearchProductHandler.aspx/GetProductData',
        data: JSON.stringify({ productCategory: productCategory, productName: productName, checkAllBrand: checkAllBrand }),
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
            } else {
                $("#productTableDiv").css('display', 'block');
                let data = JSON.parse(response.d);
                let tableBody = $('#tableBody');

                tableBody.empty();
                $.each(data, function (index, item) {
                    let row = '<tr>' +
                        '<td>' + item.f_id + '</td>' +
                        '<td>' + item.f_name + '</td>' +
                        '<td>' + item.f_category + '</td>' +
                        '<td>' + item.f_price + '</td>' +
                        '<td>' + item.f_stock + '</td>' +
                        '<td><div class="form-check form-switch"><input type="checkbox" id="toggle' + item.f_id + '" class="toggle-switch form-check-input" ' + (item.f_isOpen ? 'checked' : '') + ' data-id="' + item.f_id + '"></div></td>' +
                        '<td>' + item.f_introduce + '</td>' +
                        '<td><img src="/ProductImg/' + item.f_img + '" class="img-fluid img-thumbnail" width="80px" height="80px" alt="商品圖片"></td>' +
                        '<td><button class="btn btn-primary" onclick="editProduct(' + item.f_id + ')">改</button></td>' +
                        '<td><button class="btn btn-danger" onclick="deleteProduct(' + item.f_id + ')">刪</button></td>' +
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

//按下是否開放開關，更改資料庫
function ToggleProductStatus(productId) {
    $.ajax({
        type: "POST",
        url: "/Ajax/SearchProductHandler.aspx/ToggleProductStatus",
        data: JSON.stringify({ productId: productId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            if (response.d === "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            }else if (response.d === "更改成功"){
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
function deleteProduct(productId) {
    let yes = confirm('確定要刪除商品嗎');
    if (yes == true) {
        $.ajax({
            type: "POST",
            url: "/Ajax/SearchProductHandler.aspx/RemoveProduct",
            data: JSON.stringify({ productId: productId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d === "重複登入") {
                    alert("重複登入，已被登出");
                    window.parent.location.href = "Login.aspx";
                }else if (response.d === "刪除成功"){
                    window.location.reload();
                }else {
                    $("#labSearch").text(response.d);
                }
            },
            error: function (error) {
                console.error('Error:', error);
            }
        });
    }
}

//編輯
function editProduct(productId) {
    $.ajax({
        type: "POST",
        url: "/Ajax/SearchProductHandler.aspx/SetSessionProductId",
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
