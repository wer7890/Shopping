var AddProductComponent = {
    template: `
        <div class="container">
            <h2 class="text-center">${langFont['addProduct']}</h2>
            <br />
            <div class="row">
                <div class="mx-auto col-12 col-md-7">
                    <label for="txbProductName" class="form-label">${langFont['productNameTW']}</label>
                    <input type="text" id="txbProductName" class="form-control" />
                </div>
                <div class="mx-auto col-12 col-md-7 mt-1">
                    <label for="txbProductNameEN" class="form-label">${langFont['productNameEN']}</label>
                    <input type="text" id="txbProductNameEN" class="form-control" />
                </div>
                
                <div class="mx-auto col-12 col-md-7 mt-1">
                    <label for="txbProductImg" class="form-label">${langFont['productImg']}</label>
                    <input type="file" id="txbProductImg" class="form-control" />
                </div>
                <div class="mx-auto col-12 col-md-7 mt-1">
                    <label for="txbProductPrice" class="form-label">${langFont['price']}</label>
                    <input type="number" id="txbProductPrice" class="form-control" />
                </div>
                <div class="mx-auto col-12 col-md-7 mt-1">
                    <label for="txbProductStock" class="form-label">${langFont['stock']}</label>
                    <input type="number" id="txbProductStock" class="form-control" />
                </div>
                <div class="mx-auto col-12 col-md-7 mt-1">
                    <label for="txbProductStockWarning" class="form-label">${langFont['warningValue']}</label>
                    <input type="number" id="txbProductStockWarning" class="form-control" />
                </div>
                <div class="mx-auto col-12 col-md-7 mt-1">
                    <label for="productIsOpen" class="form-label">${langFont['isOpen']}</label>
                    <select id="productIsOpen" class="form-select">
                        <option value="0">${langFont['no']}</option>
                        <option value="1">${langFont['yes']}</option>
                    </select>
                </div>

                <div class="mx-auto col-12 col-md-7 mt-1">
                    <label for="txbProductIntroduce" class="form-label">${langFont['productIntroduceTW']}</label>
                    <textarea rows="3" class="form-control" id="txbProductIntroduce"></textarea>
                </div>
                <div class="mx-auto col-12 col-md-7 mt-1">
                    <label for="txbProductIntroduceEN" class="form-label">${langFont['productIntroduceEN']}</label>
                    <textarea rows="3" class="form-control" id="txbProductIntroduceEN"></textarea>
                </div>

                <button id="btnAddProduct" class="btn btn-outline-primary mx-auto mt-4 col-12 col-md-6">${langFont['addProduct']}</button>
            </div>
            <br />
            <div class="row">
                <label v-text="message" class="col-12 col-sm-12 text-center text-success"></label>
            </div>
        </div>
    `,
    data: function () {
        return {
            message: '',

        }
    },
}