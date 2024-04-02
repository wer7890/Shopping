$(document).ready(function () {
    window.parent.getUserPermission();

    var dbStock = null;
    //一開始input預設值
    $.ajax({
        type: "POST",
        url: "/Ajax/ProductHandler.aspx/GetProductDataForEdit",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            // 直接設定 input 元素的值
            $("#labProductId").text(data.d.ProductId);
            $("#labProductCreatedOn").text(data.d.ProductCreatedOn);
            $("#labProductOwner").text(data.d.ProductOwner);
            $("#labProductName").text(data.d.ProductName);
            $("#labProductCategory").text(data.d.ProductCategory);
            $("#labProductStock").text(data.d.ProductStock);
            $("#imgProduct").attr("src", "/ProductImg/" + data.d.ProductImg);
            $("#txbProductPrice").val(data.d.ProductPrice);
            $("#txbProductIntroduce").val(data.d.ProductIntroduce);
            dbStock = data.d.ProductStock;
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });

    $("#btnRenewProduct").click(function () {
        let productPrice = $("#txbProductPrice").val();
        let productStock = $("#txbProductStock").val(); 
        let productIntroduce = $("#txbProductIntroduce").val();
        let productCheckStock = $("input[name='flexRadioDefault']:checked").val();
        $("#labRenewUser").text("");

        if (!IsSpecialChar(productIntroduce, productPrice, productStock)) {
            return;
        }

        if (productCheckStock == 0 && productStock > dbStock) {
            $("#labRenewProduct").text("庫存量不能小於0");
            return;
        }

        $.ajax({
            type: "POST",
            url: "/Ajax/ProductHandler.aspx/EditProduct",
            data: JSON.stringify({ productPrice: productPrice, productStock: productStock, productIntroduce: productIntroduce, productCheckStock: productCheckStock }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d === "重複登入") {
                    alert("重複登入，已被登出");
                    window.parent.location.href = "Login.aspx";
                }else if (response.d === "修改成功") {
                    alert("修改成功");
                    window.location.href = "ProductManagement.aspx";
                } else {
                    $("#labRenewProduct").text(response.d);
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