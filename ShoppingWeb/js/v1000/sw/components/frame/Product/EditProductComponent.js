var EditProductComponent = {
    template: `
        <div class="container">
            <h2 class="text-center">${langFont['editProduct']}</h2>
            <br />
            <div class="row">
                <div class="mx-auto col-12 col-md-7">
                    <span class="text-dark fs-6">${langFont['productId']} : </span>
                    <span v-text="dbData.Id" class="fs-6"></span>
                </div>
                <div class="mx-auto col-12 col-md-7 mt-2">
                    <span class="text-dark fs-6">${langFont['createdTime']} : </span>
                    <span v-text="dbData.CreatedTime" class="fs-6"></span>
                </div>
                <div class="mx-auto col-12 col-md-7 mt-2">
                    <span class="text-dark fs-6">${langFont['createdUser']} : </span>
                    <span v-text="dbData.CreatedUser" class="fs-6"></span>
                </div>
                <div class="mx-auto col-12 col-md-7 mt-2">
                    <span class="text-dark fs-6">${langFont['productNameTW']} : </span>
                    <span v-text="dbData.NameTW" class="fs-6"></span>
                </div>
                <div class="mx-auto col-12 col-md-7 mt-2">
                    <span class="text-dark fs-6">${langFont['productNameEN']} : </span>
                    <span v-text="dbData.NameEN" class="fs-6"></span>
                </div>
                <div class="mx-auto col-12 col-md-7 mt-2">
                    <span class="text-dark fs-6">${langFont['productType']} : </span>
                    <span v-text="dbData.Category" class="fs-6"></span>
                </div>
                <div class="mx-auto col-12 col-md-7 mt-2">
                    <span class="text-dark fs-6">${langFont['productStock']} : </span>
                    <span v-text="dbData.Stock" class="fs-6"></span>
                </div>
                <div class="mx-auto col-12 col-md-7 mt-3">
                    <img :src="imgSrc + dbData.Img" class="img-fluid img-thumbnail w-25 i18n" alt="img" />
                </div>
                <div class="mx-auto col-12 col-md-7 mt-3">
                    <label class="form-label">${langFont['productPrice']}</label>
                    <input v-model="price" type="number" class="form-control" />
                </div>
                
                <div class="row mx-auto col-12 col-md-7 mt-3">
                    <div class="col-12 col-md-3 mt-3">
                        <div class="form-check">
                            <input class="form-check-input" type="radio" value="true" v-model="stockChange" />
                            <label class="form-check-label">
                                ${langFont['addStock']}
                            </label>
                        </div>
                        <div class="form-check">
                            <input class="form-check-input" type="radio" value="false" v-model="stockChange" />
                            <label class="form-check-label">
                                ${langFont['reduceStock']}
                            </label>
                        </div>
                    </div>
                    <div class="col-12 col-md-9">
                        <label class="form-label"></label>
                        <input v-model="stock" type="number" class="form-control" />
                    </div>
                </div>


            
                <div class="mx-auto col-12 col-md-7 mt-3">
                    <label for="txbProductStockWarning" class="form-label i18n" data-key="productPrice">${langFont['warningValue']}</label>
                    <input v-model="warningValue" type="number" class="form-control" />
                </div>
                <div class="mx-auto col-12 col-md-7 mt-4">
                    <label for="txbProductIntroduce" class="form-label">${langFont['productIntroduceTW']}</label>
                    <textarea v-model="introduceTW" rows="3" class="form-control"></textarea>
                </div>
                <div class="mx-auto col-12 col-md-7 mt-4">
                    <label for="txbProductIntroduceEN" class="form-label">${langFont['productIntroduceEN']}</label>
                    <textarea v-model="introduceEN" rows="3" class="form-control"></textarea>
                </div>

                <button @click="Edit" class="btn btn-outline-primary mx-auto mt-4 col-12 col-md-6">${langFont['edit']}</button>
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
            imgSrc: '/ProductImg/',
            dbData: '',
            stockChange: true,
            stock: '',
            price: '',
            warningValue: '',
            introduceTW: '',
            introduceEN: '',
        }
    },
    watch: {
        message: function () {
            var self = this;
            setTimeout(function () {
                self.message = '';
            }, 3000);
        },
        dbData: function () {
            this.price = this.dbData.Price;
            this.warningValue = this.dbData.WarningValue;
            this.introduceTW = this.dbData.IntroduceTW;
            this.introduceEN = this.dbData.IntroduceEN;
        }
    },
    methods: {
        //預設所選擇商品的資料
        GetData: function () {
            var self = this;
            $.ajax({
                type: "POST",
                url: "/api/Controller/product/GetProductDataForEdit",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    switch (response.Status) {
                        case 0:
                            alert(langFont["duplicateLogin"]);
                            window.parent.location.href = "Login.aspx";
                            break;
                        case 1:
                            alert(langFont["accessDenied"]);
                            parent.location.reload();
                            break;
                        case 2:
                            self.message = langFont["inputError"];
                            break;
                        case 100:
                            self.dbData = response.ProductDataList[0];
                            console.log(self.dbData);
                            break;
                        default:
                            self.message = langFont["errorLog"];
                            break;
                    }
                },
                error: function (error) {
                    self.message = langFont["ajaxError"];
                }
            });
        },

        //更改商品
        Edit: function () {
            if (!this.IsSpecialChar()) {
                return;
            }

            if (!this.stockChange && this.stock > this.dbData.Stock) {
                this.message = langFont["stockIimit"];
                return;
            }

            var self = this;

            $.ajax({
                type: "POST",
                url: "/api/Controller/product/EditProduct",
                data: JSON.stringify({ productPrice: this.price, productStock: this.stock, productIntroduce: this.introduceTW, productIntroduceEN: this.introduceEN, productCheckStock: this.stockChange, productStockWarning: this.warningValue }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    switch (response.Status) {
                        case 0:
                            alert(langFont["duplicateLogin"]);
                            window.parent.location.href = "Login.aspx";
                            break;
                        case 1:
                            alert(langFont["accessDenied"]);
                            parent.location.reload();
                            break;
                        case 2:
                            self.message = langFont["inputError"];
                            break;
                        case 100:
                            alert(langFont["editSuccessful"]);
                            break;
                        case 101:
                            self.message = langFont["editFail"] + langFont["stockIimit"];
                            break;
                        default:
                            self.message = langFont["errorLog"];
                    }
                },
                error: function (error) {
                    self.message = langFont["ajaxError"];
                }
            });
        },

        //判斷輸入值
        IsSpecialChar: function () {
            if (!this.introduceTW || !this.introduceEN || !this.price || !this.stock || !this.warningValue) {
                this.message = 'undefined';
                return false;
            }

            if (!/^.{1,500}$/.test(this.introduceTW)) {
                this.message = langFont["productIntroduceIimit"];
                return false;
            }

            if (!/^[^\u4e00-\u9fa5]{1,1000}$/.test(this.introduceEN)) {
                this.message = langFont["productIntroduceENIimit"];
                return false;
            }

            if (!/^[0-9]{1,7}$/.test(this.price) || !/^[0-9]{1,7}$/.test(this.stock) || !/^[0-9]{1,7}$/.test(this.warningValue)) {
                this.message = langFont["productPriceIimit"];
                return false;
            }

            return true;
        }
    },
    mounted: function () {  //掛載後
        this.GetData();
    },
}