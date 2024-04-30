// 大分類
majorCategories = {
    "10": "帽子",
    "11": "上衣",
    "12": "外套",
    "13": "褲子"
};

// 小分類
minorCategories = {
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
brand = {
    "01": "其他",
    "02": "NIKE",
    "03": "FILA",
    "04": "ADIDAS",
    "05": "PUMA"
}

$(document).ready(function () {
    ProductDataReady();
    
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
        let productIsOpen = $("#productIsOpen").val();
        let productIntroduce = $("#txbProductIntroduce").val();
        let productIntroduceEN = $("#txbProductIntroduceEN").val();
        $("#labAddProduct").text("");
        let newCategory = productCategory + productMinorCategory + productBrand;

        if (!IsSpecialChar(productName, productNameEN, productCategory, productMinorCategory, productBrand, productImg, productIsOpen, productIntroduce, productIntroduceEN, productPrice, productStock)) {
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
            formData.append("productNameEN", productNameEN);
            formData.append("productCategory", newCategory);
            formData.append("productPrice", productPrice);
            formData.append("productStock", productStock);
            formData.append("productIsOpen", productIsOpen);
            formData.append("productIntroduce", productIntroduce);
            formData.append("productIntroduceEN", productIntroduceEN);

            // 圖片上傳
            $.ajax({
                url: "/Ajax/ProductHandler.aspx",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response === "重複登入") {
                        alert("重複登入，已被登出");
                        window.parent.location.href = "Login.aspx";
                    } else if (response === "權限不足") {
                        alert("權限不足");
                        parent.location.reload();
                    } else if (response === "1") {
                        alert("新增成功");
                        window.location.href = "ProductManagement.aspx"
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
function IsSpecialChar(productName, productNameEN, productCategory, productMinorCategory, productBrand, productImg, productIsOpen, productIntroduce, productIntroduceEN, productPrice, productStock) {

    if (typeof productName === 'undefined' || typeof productNameEN === 'undefined' || typeof productCategory === 'undefined' || typeof productImg === 'undefined' || typeof productIsOpen === 'undefined' || typeof productIntroduce === 'undefined' || typeof productIntroduceEN === 'undefined' || typeof productPrice === 'undefined' || typeof productStock === 'undefined') {
        $("#labAddProduct").text("undefined");
        return false;
    }

    if (!/^.{1,40}$/.test(productName)) {
        $("#labAddProduct").text("商品名稱長度需在1到40之間");
        return false;
    }

    if (!/^[^\u4e00-\u9fa5]{1,100}$/.test(productNameEN)) {
        $("#labAddProduct").text("商品英文名稱長度需在1到100之間且不能包含中文");
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

    if (!/^[^\u4e00-\u9fa5]{1,1000}$/.test(productIntroduceEN)) {
        $("#labAddProduct").text("商品英文描述長度需在1到500之間且不能包含中文");
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
