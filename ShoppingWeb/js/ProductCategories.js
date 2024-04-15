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