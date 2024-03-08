$(document).ready(function () {
    $("#btnAddProduct").click(function () {
        let productName = $("#txbProductName").val();
        let productType = $("#productType").val();
        let productImg = $("#txbProductImg").val();
        let productPrice = $("#txbProductPrice").val();
        let productSum = $("#txbProductSum").val();
        let productOpen = $("#productOpen").val();
        let productIntroduce = $("#txbProductIntroduce").val();

        console.log("name: " + productName);
        console.log("Type: " + productType);
        console.log("Img: " + productImg);
        console.log("Price: " + productPrice);
        console.log("Sum: " + productSum);
        console.log("Open: " + productOpen);
        console.log("Introduce: " + productIntroduce);
    });
});