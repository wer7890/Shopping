var ProductComponent = {
    template: `
        <div class="w-auto mx-3" id="allProductDiv">
            <h2 class="text-center">${langFont['product']}</h2>
            <br />
            <div class="row mx-auto col-12 mb-3">
                <div class="col-2 px-0">
                    <label class="form-label">${langFont['productType']}</label>
                    <select v-model="mainCategoryNum" class="form-select">
                        <option v-for="data in mainCategories" :key="data.id" :value="data.value" v-text="data.name"></option>
                    </select>
                </div>
                <div class="col-2">
                    <label class="form-label">${langFont['minorCategories']}</label>
                    <select v-model="smallCategoryNum" class="form-select">
                        <option v-for="data in smallCategories[mainCategoryNum]" :key="data.id" :value="data.value" v-text="data.name"></option>
                    </select>
                </div>
                <div class="col-2 ps-0">
                    <label class="form-label">${langFont['brand']}</label>
                    <select v-model="brandNum" class="form-select">
                        <option v-for="data in brand" :key="data.id" :value="data.value" v-text="data.name"></option>
                    </select>
                </div>
                <div class="col-2 px-0">
                    <label class="form-label">${langFont['productNameSelect']}</label>
                    <input v-model="productName" type="text" class="form-control" placeholder="${langFont['productNameSelect']}" />
                </div>
                <div class="col px-0 d-flex justify-content-center align-items-end">
                    <button @click="SearchProduct(1, pageSize)" type="submit" class="btn btn-outline-primary">${langFont['select']}</button>
                </div>
                <div class="col px-0 d-flex justify-content-center align-items-end">
                    <button @click="AddProduct" type="submit" class="btn btn-outline-primary">${langFont['addProduct']}</button>
                </div>
                <div class="col px-0 d-flex justify-content-center align-items-end">
                    <button type="submit" class="btn btn-outline-primary">${langFont['stockWarn']}</button>
                </div>
            </div>

            <div class="row" id="productTableDiv">
                <table-component :theadData="theadData" :dataArray="dataArray">
                    <template v-slot:table-row="{ data }">
                        <td v-text="data.Id"></td>
                        <td v-text="data.Name"></td>
                        <td v-text="data.Category"></td>
                        <td v-text="data.Price"></td>
                        <td v-text="data.Stock"></td>
                        <td v-text="data.WarningValue"></td>
                        <td>
                            <div class="form-check form-switch">
                                <input v-model="data.IsOpen" @change="EditProductStatus(data.Id)" type="checkbox" class="toggle-switch form-check-input">
                            </div>
                        </td>
                        <td v-text="data.Introduce"></td>
                        <td><img :src="imgSrc + data.Img" class="img-fluid img-thumbnail" width="200px" height="200px" alt="${langFont['img']}"></td>
                        <td><button @click="EditProduct(data.Id)" class="btn btn-primary">${langFont['editOne']}</button></td>
                        <td><button @click="DeleteProduct(data.Id)" class="btn btn-danger">${langFont['delOne']}</button></td>
                    </template>
                 </table-component>
            </div>

            <pagination-component @Choose="GetAllProductData" :size="pageSize" :total="pagesTotal"></pagination-component>

            <div class="row">
                <span v-text="message" class="col-12 col-sm-12 text-center text-success"></span>
            </div>



            <pop-window-component>
                <template v-slot:content="{ page }">
                    <component :is="page"></component>
                </template>
            </pop-window-component>
        </div>
    `,
    data: function () {
        return {
            message: '',
            imgSrc: '/ProductImg/',
            mainCategoryNum: '0',  //所選取的大分類
            smallCategoryNum: '00', //所選取的小分類
            brandNum: '00',         //所選取的品牌
            productName: '',        //輸入的商品名稱
            mainCategories: [
                { id: 1, value: '0', name: langFont['selectMajorCategories'] },
                { id: 2, value: '10', name: langFont['hats'] },
                { id: 3, value: '11', name: langFont['tops'] },
                { id: 4, value: '12', name: langFont['outerwear'] },
                { id: 5, value: '13', name: langFont['bottoms'] },
            ],
            smallCategories: {
                '0': [
                    { id: 1, value: '00', name: langFont['chooseType'] },
                ],
                '10': [
                    { id: 1, value: '00', name: langFont['all'] },
                    { id: 2, value: '01', name: langFont['other'] },
                    { id: 3, value: '02', name: langFont['baseballCaps'] },
                    { id: 4, value: '03', name: langFont['fishermanHats'] },
                    { id: 5, value: '04', name: langFont['sunHats'] },
                ],
                '11': [
                    { id: 1, value: '00', name: langFont['all'] },
                    { id: 2, value: '01', name: langFont['other'] },
                    { id: 3, value: '02', name: langFont['shirts'] },
                    { id: 4, value: '03', name: langFont['sweaters'] },
                    { id: 5, value: '04', name: langFont['tShirts'] },
                ],
                '12': [
                    { id: 1, value: '00', name: langFont['all'] },
                    { id: 2, value: '01', name: langFont['other'] },
                    { id: 3, value: '02', name: langFont['leatherJackets'] },
                    { id: 4, value: '03', name: langFont['windbreakers'] },
                    { id: 5, value: '04', name: langFont['denimJackets'] },
                ],
                '13': [
                    { id: 1, value: '00', name: langFont['all'] },
                    { id: 2, value: '01', name: langFont['other'] },
                    { id: 3, value: '02', name: langFont['athleticPants'] },
                    { id: 4, value: '03', name: langFont['casualPants'] },
                    { id: 5, value: '04', name: langFont['dressPants'] },
                ]
            },
            brand: [
                { id: 1, value: '00', name: langFont['all'] },
                { id: 2, value: '01', name: langFont['other'] },
                { id: 3, value: '02', name: langFont['nike'] },
                { id: 4, value: '03', name: langFont['fila'] },
                { id: 5, value: '04', name: langFont['adidas'] },
                { id: 6, value: '04', name: langFont['puma'] },
            ],
            theadData: [
                { id: 1, name: langFont['id'] },
                { id: 2, name: langFont['name'] },
                { id: 3, name: langFont['categories'] },
                { id: 4, name: langFont['price'] },
                { id: 5, name: langFont['stock'] },
                { id: 6, name: langFont['warningValue'] },
                { id: 7, name: langFont['open'] },
                { id: 8, name: langFont['introduce'] },
                { id: 9, name: langFont['img'] },
                { id: 10, name: langFont['edit'] },
                { id: 11, name: langFont['del'] },
            ],
            dataArray: '',

            pageSize: 3,
            pagesTotal: null,
            beforePagesTotal: 1,
        }
    },
    watch: {
        message: function () {
            var self = this;
            setTimeout(function () {
                self.message = '';
            }, 3000);
        }
    },
    methods: {
        //全部商品資料
        GetAllProductData: function (pageNumber, pageSize) {
            if (typeof pageNumber === 'undefined' || typeof pageSize === 'undefined' || typeof this.beforePagesTotal === 'undefined') {
                this.message = 'undefined';
                return;
            }
            
            var self = this;

            $.ajax({
                url: '/api/Controller/product/GetAllProductData',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ pageNumber: pageNumber, pageSize: pageSize, beforePagesTotal: this.beforePagesTotal }),
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
                            self.dataArray = response.ProductDataList;
                            self.pagesTotal = response.TotalPages;
                            self.beforePagesTotal = self.pagesTotal;
                            break;
                        case 101:
                            self.message = langFont["noData"];
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

        //搜尋商品
        SearchProduct: function (pageNumber, pageSize) {
            var productNum = this.mainCategoryNum + this.smallCategoryNum + this.brandNum
            var self = this;

            $.ajax({
                url: '/api/Controller/product/GetProductData',
                data: JSON.stringify({ productCategory: productNum, productName: this.productName, checkAllMinorCategories: (this.smallCategoryNum == '00'), checkAllBrand: (this.brandNum === '00'), pageNumber: pageNumber, pageSize: pageSize, beforePagesTotal: this.beforePagesTotal }),
                type: 'POST',
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
                            self.dataArray = response.ProductDataList;
                            self.pagesTotal = response.TotalPages;
                            self.beforePagesTotal = self.pagesTotal;
                            break;
                        case 101:
                            self.message = langFont["noData"];
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

        //更改商品狀態
        EditProductStatus: function (productId) {
            if (typeof productId === 'undefined') {
                this.message = 'undefined';
                return;
            }

            var self = this;

            $.ajax({
                type: "POST",
                url: "/api/Controller/product/EditProductStatus",
                data: JSON.stringify({ productId: productId }),
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
                            self.message = langFont["editSuccessful"];
                            break;
                        case 101:
                            self.message = langFont["editFail"];
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

        //刪除
        DeleteProduct: function (productId) {
            if (typeof productId === 'undefined') {
                this.message = 'undefined';
                return;
            }

            var self = this;
            let yes = confirm(langFont["confirmEditProduct"]);
            if (yes == true) {
                $.ajax({
                    type: "POST",
                    url: "/api/Controller/product/DelProduct",
                    data: JSON.stringify({ productId: productId }),
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
                                //window.location.reload();
                                self.message = langFont["delSuccessful"];
                                break;
                            case 101:
                                self.message = langFont["delFailed"];
                                break;
                            default:
                                self.message = langFont["errorLog"];
                        }
                    },
                    error: function (error) {
                        self.message = langFont["ajaxError"];
                    }
                });
            }
        },

        //跳轉更改商品組件
        EditProduct: function (productId) {
            if (typeof productId === 'undefined') {
                this.message = 'undefined';
                return;
            }

            var self = this;

            $.ajax({
                type: "POST",
                url: "/api/Controller/product/SetSessionProductId",
                data: JSON.stringify({ productId: productId }),
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
                            self.$bus.$emit('PopWindow:Set', 'edit-product-component');
                            break;
                        default:
                            alert(langFont["editFailed"]);
                            break;
                    }
                },
                error: function (error) {
                    self.message = langFont["ajaxError"];
                }
            });
        },

        //跳轉至新增商品組件
        AddProduct: function () {
            this.$bus.$emit('PopWindow:Set', 'add-product-component');
        }
    },
    mounted: function () {  //掛載後
        this.GetAllProductData(1, this.pageSize);
    },
    components: {
        'pagination-component': PaginationComponent,
        'table-component': TableComponent,
        'add-product-component': AddProductComponent,
        'edit-product-component': EditProductComponent,
        'pop-window-component': PopWindowComponent,
    }
};