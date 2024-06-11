$(document).ready(function () {
    ProductDataReady();

    //刪除下拉選單中"全部"的選項
    $("#productCategory").change(function () {
        $("#minorCategory option[value='00']").remove();
    });
    $("#brandCategory option[value='00']").remove();

    //按下新增按鈕
    $("#btnAddProduct").click(function () {
        let productName = $("#txbProductName").val();
        let productNameEN = $("#txbProductNameEN").val();
        let productCategory = $("#productCategory").val();  // 獲取大分類值
        let productMinorCategory = $("#minorCategory").val(); // 獲取小分類值
        let productBrand = $("#brandCategory").val(); // 獲取品牌值
        let productImg = $("#txbProductImg").val().split('\\').pop(); // 提取文件名
        let productPrice = $("#txbProductPrice").val();
        let productStock = $("#txbProductStock").val();
        let productStockWarning = $("#txbProductStockWarning").val();
        let productIsOpen = $("#productIsOpen").val();
        let productIntroduce = $("#txbProductIntroduce").val();
        let productIntroduceEN = $("#txbProductIntroduceEN").val();
        $("#labAddProduct").text("");
        let newCategory = productCategory + productMinorCategory + productBrand;

        if (!IsSpecialChar(productName, productNameEN, productCategory, productMinorCategory, productBrand, productImg, productIsOpen, productIntroduce, productIntroduceEN, productPrice, productStock, productStockWarning)) {
            return;
        }

        let yes = confirm(langFont["confirmAdd"]);
        if (yes == true) {
            let fileInput = $("#txbProductImg")[0];
            // 取得使用者選擇的檔案 
            let file = fileInput.files[0];

            if (!CheckFileSize(file)) {
                $("#labAddProduct").text(langFont["inputError"]);
                return;
            }

            // 建立 FormData 物件來儲存檔案資料 
            let formData = new FormData();
            // 將檔案加入到 FormData 物件中
            formData.append("file", file);
            formData.append("productName", productName);
            formData.append("productNameEN", productNameEN);
            formData.append("productCategory", newCategory);
            formData.append("productPrice", productPrice);
            formData.append("productStock", productStock);
            formData.append("productIsOpen", productIsOpen);
            formData.append("productIntroduce", productIntroduce);
            formData.append("productIntroduceEN", productIntroduceEN);
            formData.append("productStockWarning", productStockWarning);

            // 圖片上傳
            $.ajax({
                url: "/api/Controller/product/UploadProduct",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    switch (response.Msg) {
                        case 0:
                            alert(langFont["duplicateLogin"]);
                            window.parent.location.href = "Login.aspx";
                            break;
                        case 1:
                            alert(langFont["accessDenied"]);
                            parent.location.reload();
                            break;
                        case 2:
                            $("#labAddProduct").text(langFont["inputError"]);
                            break;
                        case 100:
                            alert(langFont["addSuccessful"]);
                            window.location.href = "ProductManagement.aspx"
                            break;
                        case 101:
                            $("#labAddProduct").text(langFont["addFailed"]);
                            break;
                        default:
                            $("#labAddProduct").text(langFont["errorLog"]);
                    }
                },
                error: function (error) {
                    $("#labAddProduct").text(langFont["ajaxError"]);
                }
            });
        }

    });
});


//判斷文字長度 
function IsSpecialChar(productName, productNameEN, productCategory, productMinorCategory, productBrand, productImg, productIsOpen, productIntroduce, productIntroduceEN, productPrice, productStock, productStockWarning) {

    if (typeof productName === 'undefined' || typeof productNameEN === 'undefined' || typeof productCategory === 'undefined' || typeof productImg === 'undefined' || typeof productIsOpen === 'undefined' || typeof productIntroduce === 'undefined' || typeof productIntroduceEN === 'undefined' || typeof productPrice === 'undefined' || typeof productStock === 'undefined' || typeof productStockWarning === 'undefined') {
        $("#labAddProduct").text("undefined");
        return false;
    }

    if (!/^.{1,40}$/.test(productName)) {
        $("#labAddProduct").text(langFont["productNameIimit"]);
        return false;
    }

    if (!/^[^\u4e00-\u9fa5]{1,100}$/.test(productNameEN)) {
        $("#labAddProduct").text(langFont["productNameENIimit"]);
        return false;
    }

    if (!/(\.jpg|\.png)$/i.test(productImg)) {
        $("#labAddProduct").text(langFont["productImgIimit"]);
        return false;
    }

    if (!/.{2,}/.test(productCategory) || !/.{2,}/.test(productMinorCategory) || !/.{2,}/.test(productBrand) || !/.{1,}/.test(productIsOpen)) {
        $("#labAddProduct").text(langFont["productCategoryIimit"]);
        return false;
    }

    if (!/^.{1,500}$/.test(productIntroduce)) {
        $("#labAddProduct").text(langFont["productIntroduceIimit"]);
        return false;
    }

    if (!/^[^\u4e00-\u9fa5]{1,1000}$/.test(productIntroduceEN)) {
        $("#labAddProduct").text(langFont["productIntroduceENIimit"]);
        return false;
    }

    if (!/^[0-9]{1,7}$/.test(productPrice) || !/^[0-9]{1,7}$/.test(productStock) || !/^[0-9]{1,7}$/.test(productStockWarning)) {
        $("#labAddProduct").text(langFont["productPriceIimit"]);
        return false;
    }

    return true;
}

//判斷圖片大小
function CheckFileSize(file) {

    if (typeof file === 'undefined') {
        return false;
    }

    // 檢查圖片大小
    const maxSizeInBytes = 500 * 1024; // 500KB
    if (file.size > maxSizeInBytes) {
        $("#labAddProduct").text(langFont["imgSize"]);
        return false;
    }
    return true;
}
