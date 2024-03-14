$(document).ready(function () {
    $("#btnAddProduct").click(function () {
        let productName = $("#txbProductName").val();
        let productCategory = $("#productCategory").val();
        let productImg = $("#txbProductImg").val().split('\\').pop(); // 提取文件名
        let productPrice = $("#txbProductPrice").val();
        let productStock = $("#txbProductStock").val();
        let productIsOpen = $("#productIsOpen").val();
        let productIntroduce = $("#txbProductIntroduce").val();

        if (productName === "" || productCategory === "" || productImg === "" || productPrice === "" || productStock === "" || productIsOpen === "" || productIntroduce === "") {
            $("#labAddProduct").text("請填寫全部");
            return;
        }

        let fileInput = $("#txbProductImg")[0];
        // 取得使用者選擇的檔案
        let file = fileInput.files[0];

        if (file && file.type.startsWith('image/')) {
            // 建立 FormData 物件來儲存檔案資料
            let formData = new FormData();
            // 將檔案加入到 FormData 物件中
            formData.append("file", file);

            // 處理文件上傳
            $.ajax({
                url: "/Ajax/AddProductHandler.aspx",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response === "未選擇要上傳的檔案") {
                        $("#labAddProduct").text("上傳失敗");
                    } else {
                        if (response === "圖片上傳成功") {
                            UploadFile(productName, productCategory, productPrice, productStock, productIsOpen, productIntroduce);
                        } else {
                            $("#labAddProduct").text("上傳失敗: " + response);
                        }
                    }
                },
                error: function () {
                    console.log("Error: " + error);
                    $("#labAddProduct").text("上傳失敗: " + error);
                }
            });
        } else {
            $("#labAddProduct").text("請選擇圖片檔案");
        }

    });
});

function UploadFile(productName, productCategory, productPrice, productStock, productIsOpen, productIntroduce) {
    $.ajax({
        type: "POST",
        url: "/Ajax/AddProductHandler.aspx/AddProduct",
        data: JSON.stringify({
            productName: productName,
            productCategory: productCategory,
            productPrice: productPrice,
            productStock: productStock,
            productIsOpen: productIsOpen,
            productIntroduce: productIntroduce
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d === "1") {
                alert("新增成功");
            } else {
                $("#labAddProduct").text("資料庫圖片名稱重複");
            }
        },
        error: function (error) {
            console.error('AJAX Error:', error);
            $("#labAddProduct").text("發生錯誤，請查看控制台");
        }
    });
}