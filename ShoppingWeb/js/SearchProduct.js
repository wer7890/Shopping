$(document).ready(function () {
    
    SearchAllProduct();
    
    $("#btnSearchProduct").click(function () {
        $("#labSearchProduct").text("");
        let productName = $("#txbProductSearch").val();
        $('#tableBody').empty();
        SearchProduct("getSearchProductNameData", "@productName", productName);
    });

    $("#productCategory").change(function () {
        $("#labSearchProduct").text("");
        $('#tableBody').empty();
        let selectedValue = $(this).val();
        SearchProduct("getSearchProductCategoryData", "@productCategory", selectedValue);
    });
})

//全部商品資料
function SearchAllProduct() {
    $.ajax({
        url: '/Ajax/SearchProductHandler.aspx/GetAllProductData',
        type: 'POST',
        contentType: 'application/json',
        success: function (response) {
            // 處理成功取得資料的情況
            var data = JSON.parse(response.d); // 解析 JSON 資料為 JavaScript 物件
            var tableBody = $('#tableBody');

            // 清空表格內容
            tableBody.empty();

            // 動態生成表格內容
            $.each(data, function (index, item) {
                var row = '<tr>' +
                    '<td>' + item.f_id + '</td>' +
                    '<td>' + item.f_productName + '</td>' +
                    '<td>' + item.f_productCategory + '</td>' +
                    '<td>' + item.f_productPrice + '</td>' +
                    '<td>' + item.f_productStock + '</td>' +
                    '<td>' + item.f_productIsOpen + '</td>' +
                    //'<td>' + item.f_productOwner + '</td>' +
                    //'<td>' + item.f_productCreatedOn + '</td>' +
                    '<td>' + item.f_productIntroduce + '</td>' +
                    '<td><img src="/Images/' + item.f_productImg + '" class="img-fluid img-thumbnail" width="80px" height="80px" alt="商品圖片"></td>' +
                    '<td><button class="btn btn-primary" onclick="editUser(' + item.f_id + ')">編輯</button></td>' +
                    '<td><button class="btn btn-danger" onclick="deleteUser(' + item.f_id + ')">刪除</button></td>' +
                    '</tr>';

                tableBody.append(row);
            });
            
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}

//部分商品資料
function SearchProduct(sqlName, sqlAdd, searchName) {
    $.ajax({
        url: '/Ajax/SearchProductHandler.aspx/GetProductData',
        data: JSON.stringify({ sqlName: sqlName, sqlAdd: sqlAdd, searchName: searchName }),
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d == "null") {
                $("#productTableDiv").css('display', 'none'); 
                $("#labSearchProduct").text("沒有資料");
            } else {
                $("#productTableDiv").css('display', 'block');
                var data = JSON.parse(response.d);
                var tableBody = $('#tableBody');

                tableBody.empty();
                $.each(data, function (index, item) {
                    var row = '<tr>' +
                        '<td>' + item.f_id + '</td>' +
                        '<td>' + item.f_productName + '</td>' +
                        '<td>' + item.f_productCategory + '</td>' +
                        '<td>' + item.f_productPrice + '</td>' +
                        '<td>' + item.f_productStock + '</td>' +
                        '<td>' + item.f_productIsOpen + '</td>' +
                        //'<td>' + item.f_productOwner + '</td>' +
                        //'<td>' + item.f_productCreatedOn + '</td>' +
                        '<td>' + item.f_productIntroduce + '</td>' +
                        '<td><img src="/Images/' + item.f_productImg + '" class="img-fluid img-thumbnail" width="80px" height="80px" alt="商品圖片"></td>' +
                        '<td><button class="btn btn-primary" onclick="editUser(' + item.f_id + ')">編輯</button></td>' +
                        '<td><button class="btn btn-danger" onclick="deleteUser(' + item.f_id + ')">刪除</button></td>' +
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