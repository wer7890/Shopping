//商品分類，選擇框設定
function ProductDataReady() {

    // 創建 select 元素並初始化大分類選項
    let categorySelect = $("<select>").attr("id", "productCategory").addClass("form-select");
    categorySelect.prepend($("<option>").attr("value", "0").text(langFont["selectMajorCategories"]));
    for (let key in majorCategories) {

        if (Object.prototype.hasOwnProperty.call(majorCategories, key)) {
            categorySelect.append($("<option>").attr("value", key).text(majorCategories[key]));
        }

    }
    $("#divCategories").append('<label for="productCategory" class="form-label">' + langFont["productType"] + '</label>').append(categorySelect);

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
        $("#divMinorCategory").empty().append('<label for="minorCategory" class="form-label">' + langFont["minorCategories"] + '</label>').append(minorCategorySelect);
    });

    // 創建品牌 select 元素
    let brandSelect = $("<select>").attr("id", "brandCategory").addClass("form-select");
    for (let key in brand) {

        if (Object.prototype.hasOwnProperty.call(brand, key)) {
            brandSelect.append($("<option>").attr("value", key).text(brand[key]));
        }

    }
    $("#divBrand").append('<label for="brandCategory" class="form-label">' + langFont["brand"] + '</label>').append(brandSelect);
}

//把類型代號轉成文字
function CategoryCodeToText(category) {
    let dbMajorCategories = category.substring(0, 2);
    let dbMinorCategories = category.substring(2, 4);
    let dbBrand = category.substring(4, 6);

    let result = majorCategories[dbMajorCategories] + "-" + minorCategories[dbMajorCategories][dbMinorCategories] + "-" + brand[dbBrand];
    return result;
}