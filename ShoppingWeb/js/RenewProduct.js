﻿$(document).ready(function () {
    //一開始input預設值
    $.ajax({
        type: "POST",
        url: "/Ajax/RenewProductHandler.aspx/GetProductDataForEdit",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            // 直接設定 input 元素的值
            $("#labProductId").text(data.d.ProductId);
            $("#labProductCreatedOn").text(data.d.ProductCreatedOn);
            $("#labProductOwner").text(data.d.ProductOwner);
            $("#labProductName").text(data.d.ProductName);
            $("#labProductCategory").text(data.d.ProductCategory);
            $("#imgProduct").attr("src", "/ProductImg/" + data.d.ProductImg);
            $("#txbProductPrice").val(data.d.ProductPrice);
            $("#txbProductStock").val(data.d.ProductStock);
            //$("#productIsOpen").val(data.d.ProductOwner);
            $("#txbProductIntroduce").val(data.d.ProductIntroduce);
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });

    $("#btnRenewProduct").click(function () {
        let productPrice = $("#txbProductPrice").val();
        let productStock = $("#txbProductStock").val();
        let productIntroduce = $("#txbProductIntroduce").val(); 
        $("#labRenewUser").text("");

        if (!IsSpecialChar(productIntroduce, productPrice, productStock)) {
            return;
        }

        $.ajax({
            type: "POST",
            url: "/Ajax/RenewProductHandler.aspx/EditProduct",
            data: JSON.stringify({ productPrice: productPrice, productStock: productStock, productIntroduce: productIntroduce }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d === "修改成功") {
                    alert("修改成功");
                    window.location.href = "SearchProduct.aspx"
                } else {
                    $("#labRenewUser").text(response.d);
                }
            },
            error: function (error) {
                console.error('Error:', error);
            }
        });
    })
});

//判斷文字長度 
function IsSpecialChar(productIntroduce, productPrice, productStock) {

    if (typeof productIntroduce === 'undefined' || typeof productPrice === 'undefined' || typeof productStock === 'undefined') {
        $("#labRenewProduct").text("undefined");
        return false;
    }

    if (!/^.{1,500}$/.test(productIntroduce)) {
        $("#labRenewProduct").text("商品描述長度需在1到500之間");
        return false;
    }

    if (!/^[0-9]{1,7}$/.test(productPrice) || !/^[0-9]{1,7}$/.test(productStock)) {
        $("#labRenewProduct").text("價格和庫存量只能是數字且都要填寫");
        return false;
    }

    return true;
}