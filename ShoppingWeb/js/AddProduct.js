$(document).ready(function () {
    $("#btnAddProduct").click(function () {
        let productName = $("#txbProductName").val();
        let productCategory = $("#productCategory").val();
        //let productImg = $("#txbProductImg").val();
        let productPrice = $("#txbProductPrice").val();
        let productStock = $("#txbProductStock").val();
        let productIsOpen = $("#productIsOpen").val();
        let productIntroduce = $("#txbProductIntroduce").val();


        // 取得檔案輸入元素
        var fileInput = document.getElementById("txbProductImg");
        // 取得使用者選擇的檔案
        var file = fileInput.files[0];
        // 建立 FormData 物件來儲存檔案資料
        var formData = new FormData();
        // 將檔案加入到 FormData 物件中
        formData.append("file", file);

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
                    $("#labAddProduct").text("上傳成功" + response);
                }

            },
            error: function () {
                console.log("Error: " + error);
                $("#labAddProduct").text("上傳失敗: " + error);
            }
        });



        //$.ajax({
        //    type: "POST",
        //    url: "/Ajax/AddProductHandler.aspx/AddProduct",  
        //    data: JSON.stringify({
        //        productName: productName,
        //        productCategory: productCategory,
        //        productImg: productImg,
        //        productPrice: productPrice,
        //        productStock: productStock,
        //        productIsOpen: productIsOpen,
        //        productIntroduce: productIntroduce
        //    }),
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (response) {
        //        if (response.d === "1") {
        //            alert("新增成功");
        //        } else {
        //            $("#labAddProduct").text("新增錯誤");
        //        }
        //    },
        //    error: function (error) {
        //        console.error('AJAX Error:', error);
        //        $("#labAddProduct").text("發生錯誤，請查看控制台");
        //    }
        //});

    });
});