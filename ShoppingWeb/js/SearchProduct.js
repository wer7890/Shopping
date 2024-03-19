$(document).ready(function () {
    
    SearchAllProduct();
    
    $("#btnSearchProduct").click(function () {
        $("#labSearchProduct").text("");
        $('#tableBody').empty();
        let productName = $("#txbProductSearch").val();
        let productCategory = $("#productCategory").val();
        SearchProduct(productCategory, productName);
    });


    // 使用事件代理監聽開關的改變事件
    $(document).on("change", ".toggle-switch", function () {
        let productId = $(this).data('id');
        ToggleProductStatus(productId);
    });
    

    $("#btnAddProduct").click(function () {
        window.location.href = "AddProduct.aspx"
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
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}

//搜尋商品資料
function SearchProduct(productCategory, productName) {
    $.ajax({
        url: '/Ajax/SearchProductHandler.aspx/GetProductData',
        data: JSON.stringify({ productCategory: productCategory, productName: productName }),
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
        error: function (xhr, status, error) {
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
            if (response.d === "更改成功") {
                $("#labSearchProduct").text("更改成功");
            } else if (response.d === "重複登入"){
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
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
                if (response.d === "刪除成功") {
                    window.location.reload();
                } else if (response.d === "重複登入"){
                    alert("重複登入，已被登出");
                    window.parent.location.href = "Login.aspx";
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
