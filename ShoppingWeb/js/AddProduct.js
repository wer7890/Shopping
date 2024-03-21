﻿$(document).ready(function () {
    $("#btnAddProduct").click(function () {
        let productName = $("#txbProductName").val();
        let productCategory = $("#productCategory").val();
        let productImg = $("#txbProductImg").val().split('\\').pop(); // 提取文件名
        let productPrice = $("#txbProductPrice").val();
        let productStock = $("#txbProductStock").val();
        let productIsOpen = $("#productIsOpen").val();
        let productIntroduce = $("#txbProductIntroduce").val();
        $("#labAddProduct").text("");

        if (!IsSpecialChar(productName, productCategory, productImg, productIsOpen, productIntroduce, productPrice, productStock)) {
            return;
        }

        let yes = confirm('確定要新增商品嗎');
        if (yes == true) {
            let fileInput = $("#txbProductImg")[0];
            // 取得使用者選擇的檔案 
            let file = fileInput.files[0];

            if (!CheckFileSize(file)) {
                return;
            }

            // 建立 FormData 物件來儲存檔案資料
            let formData = new FormData();
            // 將檔案加入到 FormData 物件中
            formData.append("file", file);
            formData.append("productName", productName);
            formData.append("productCategory", productCategory);
            formData.append("productPrice", productPrice);
            formData.append("productStock", productStock);
            formData.append("productIsOpen", productIsOpen);
            formData.append("productIntroduce", productIntroduce);

            // 圖片上傳
            $.ajax({
                url: "/Ajax/AddProductHandler.aspx",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response === "重複登入") {
                        alert("重複登入，已被登出");
                        window.parent.location.href = "Login.aspx";
                    }else if (response === "1") {
                        alert("新增成功");
                        window.location.href = "SearchProduct.aspx"
                    } else {
                        $("#labAddProduct").text("新增失敗: " + response);
                    }
                },
                error: function () {
                    console.log("Error: " + error);
                    $("#labAddProduct").text("發生錯誤: " + error);
                }
            });
        }

    });
});


//判斷文字長度 
function IsSpecialChar(productName, productCategory, productImg, productIsOpen, productIntroduce, productPrice, productStock) {

    if (typeof productName === 'undefined' || typeof productCategory === 'undefined' || typeof productImg === 'undefined' || typeof productIsOpen === 'undefined' || typeof productIntroduce === 'undefined' || typeof productPrice === 'undefined' || typeof productStock === 'undefined') {
        $("#labAddProduct").text("undefined");
        return false;
    }

    if (!/^.{1,40}$/.test(productName)) {
        $("#labAddProduct").text("商品名稱長度需在1到40之間");
        return false;
    }

    if (!/(\.jpg|\.png)$/i.test(productImg)) {
        $("#labAddProduct").text("請選擇圖片檔案");
        return false;
    }

    if (!/.{1,}/.test(productCategory) || !/.{1,}/.test(productIsOpen)) {
        $("#labAddProduct").text("商品類別和是否開放必須填寫");
        return false;
    }

    if (!/^.{1,500}$/.test(productIntroduce)) {
        $("#labAddProduct").text("商品描述長度需在1到500之間");
        return false;
    }

    if (!/^[0-9]{1,7}$/.test(productPrice) || !/^[0-9]{1,7}$/.test(productStock)) {
        $("#labAddProduct").text("價格和庫存量只能是數字且都要填寫");
        return false;
    }

    return true;
}

//判斷圖片大小
function CheckFileSize(file) {

    // 檢查圖片大小
    const maxSizeInBytes = 500 * 1024; // 500KB
    if (file.size > maxSizeInBytes) {
        $("#labAddProduct").text("圖片大小超過限制（最大500KB）");
        return false;
    }
    return true;
}
