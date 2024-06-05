$(document).ready(function () {
    let dbStock = null;
    //一開始input預設值
    $.ajax({
        type: "POST",
        url: "/api/Controller/product/GetProductDataForEdit",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            switch (data) {
                case 0:
                    alert(langFont["duplicateLogin"]);
                    window.parent.location.href = "Login.aspx";
                    break;
                case 1:
                    alert(langFont["accessDenied"]);
                    parent.location.reload();
                    break;
                case 102:
                    $("#labRenewProduct").text(langFont["errorLog"]);
                    break;
                default:
                    // 直接設定 input 元素的值
                    $("#labProductId").text(data.ProductId);
                    $("#labProductCreatedOn").text(data.ProductCreatedOn);
                    $("#labProductOwner").text(data.ProductOwner);
                    $("#labProductName").text(data.ProductName);
                    $("#labProductNameEN").text(data.ProductNameEN);
                    $("#labProductCategory").text(CategoryCodeToText(data.ProductCategory.toString()));
                    $("#labProductStock").text(data.ProductStock);
                    $("#imgProduct").attr("src", "/ProductImg/" + data.ProductImg);
                    $("#txbProductPrice").val(data.ProductPrice);
                    $("#txbProductIntroduce").val(data.ProductIntroduce);
                    $("#txbProductIntroduceEN").val(data.ProductIntroduceEN);
                    $("#txbProductStockWarning").val(data.ProductStockWarning);
                    dbStock = data.ProductStock;
            }
        },
        error: function (error) {
            $("#labRenewProduct").text(langFont["ajaxError"]);
        }
    });

    //按下修改按鈕
    $("#btnRenewProduct").click(function () {
        let productPrice = $("#txbProductPrice").val();
        let productStock = $("#txbProductStock").val(); 
        let productIntroduce = $("#txbProductIntroduce").val();
        let productIntroduceEN = $("#txbProductIntroduceEN").val();
        let productStockWarning = $("#txbProductStockWarning").val();
        let productCheckStock = $("input[name='flexRadioDefault']:checked").val();
        $("#labRenewUser").text("");

        if (!IsSpecialChar(productIntroduce, productIntroduceEN, productPrice, productStock, productStockWarning)) {
            return;
        }

        if (productCheckStock == 0 && productStock > dbStock) {
            $("#labRenewProduct").text(langFont["stockIimit"]);
            return;
        }

        $.ajax({
            type: "POST",
            url: "/api/Controller/product/EditProduct",
            data: JSON.stringify({ productPrice: productPrice, productStock: productStock, productIntroduce: productIntroduce, productIntroduceEN: productIntroduceEN, productCheckStock: productCheckStock, productStockWarning: productStockWarning }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                switch (response) {
                    case 0:
                        alert(langFont["duplicateLogin"]);
                        window.parent.location.href = "Login.aspx";
                        break;
                    case 1:
                        alert(langFont["accessDenied"]);
                        parent.location.reload();
                        break;
                    case 2:
                        $("#labRenewProduct").text(langFont["inputError"]);
                        break;
                    case 100:
                        alert(langFont["editSuccessful"]);
                        window.location.href = "ProductManagement.aspx";
                        break;
                    case 101:
                        $("#labRenewProduct").text(langFont["editFail"] + langFont["stockIimit"]);
                        break;
                    default:
                        $("#labRenewProduct").text(langFont["errorLog"]);
                }
            },
            error: function (error) {
                $("#labRenewProduct").text(langFont["ajaxError"]);
            }
        });
    })
});

//判斷文字長度 
function IsSpecialChar(productIntroduce, productIntroduceEN, productPrice, productStock, productStockWarning) {

    if (typeof productIntroduce === 'undefined' || typeof productIntroduceEN === 'undefined' || typeof productPrice === 'undefined' || typeof productStock === 'undefined' || typeof productStockWarning === 'undefined') {
        $("#labRenewProduct").text("undefined");
        return false;
    }

    if (!/^.{1,500}$/.test(productIntroduce)) {
        $("#labRenewProduct").text(langFont["productIntroduceIimit"]);
        return false;
    }

    if (!/^[^\u4e00-\u9fa5]{1,1000}$/.test(productIntroduceEN)) {
        $("#labAddProduct").text(langFont["productIntroduceENIimit"]);
        return false;
    }

    if (!/^[0-9]{1,7}$/.test(productPrice) || !/^[0-9]{1,7}$/.test(productStock) || !/^[0-9]{1,7}$/.test(productStockWarning)) {
        $("#labRenewProduct").text(langFont["productPriceIimit"]);
        return false;
    }

    return true;
}