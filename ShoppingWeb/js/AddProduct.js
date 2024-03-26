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
        "01": "其他",
        "02": "NIKE",
        "03": "FILA",
        "04": "ADIDAS",
        "05": "PUMA"
    }

    // 創建 select 元素並初始化大分類選項
    let categorySelect = $("<select>").attr("id", "productCategory").addClass("form-select");
    categorySelect.prepend($("<option>").attr("value", "0").text("請選擇商品類型"));
    for (let key in majorCategories) {
        
        if (Object.prototype.hasOwnProperty.call(majorCategories, key)) {
            categorySelect.append($("<option>").attr("value", key).text(majorCategories[key]));
        }

    }
    $("#divCategories").append(categorySelect);

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
    $("#divBrand").append(brandSelect);

    $("#btnAddProduct").click(function () {
        let productName = $("#txbProductName").val();
        let productCategory = $("#productCategory").val();  // 獲取大分類值
        let productMinorCategory = $("#minorCategory").val(); // 獲取小分類值
        let productBrand = $("#brandCategory").val(); // 獲取品牌值
        let productImg = $("#txbProductImg").val().split('\\').pop(); // 提取文件名
        let productPrice = $("#txbProductPrice").val();
        let productStock = $("#txbProductStock").val();
        let productIsOpen = $("#productIsOpen").val();
        let productIntroduce = $("#txbProductIntroduce").val();
        $("#labAddProduct").text("");
        let newCategory = productCategory + productMinorCategory + productBrand;

        if (!IsSpecialChar(productName, productCategory, productMinorCategory, productBrand, productImg, productIsOpen, productIntroduce, productPrice, productStock)) {
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
            formData.append("productCategory", newCategory);
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
function IsSpecialChar(productName, productCategory, productMinorCategory, productBrand, productImg, productIsOpen, productIntroduce, productPrice, productStock) {

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

    if (!/.{2,}/.test(productCategory) || !/.{2,}/.test(productMinorCategory) || !/.{2,}/.test(productBrand) || !/.{1,}/.test(productIsOpen)) {
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

