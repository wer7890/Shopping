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
                    <input type="text" class="form-control" placeholder="${langFont['productNameSelect']}" />
                </div>
                <div class="col px-0 d-flex justify-content-center align-items-end">
                    <button type="submit" class="btn btn-outline-primary">${langFont['select']}</button>
                </div>
                <div class="col px-0 d-flex justify-content-center align-items-end">
                    <button type="submit" class="btn btn-outline-primary">${langFont['addProduct']}</button>
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
                        <td v-text="data.IsOpen"></td>
                        <td v-text="data.Introduce"></td>
                        <td v-text="data.Img"></td>
                        <td><button class="btn btn-primary">${langFont['editOne']}</button></td>
                        <td><button class="btn btn-danger">${langFont['delOne']}</button></td>
                    </template>
                 </table-component>
            </div>

            <pagination-component @Choose="GetAllProductData"></pagination-component>
        </div>
    `,
    data: function () {
        return {
            message: '',
            mainCategoryNum: '00',
            smallCategoryNum: '00',
            brandNum: '00',
            mainCategories: [
                { id: 1, value: '00', name: langFont['selectMajorCategories'] },
                { id: 2, value: '10', name: langFont['hats'] },
                { id: 3, value: '11', name: langFont['tops'] },
                { id: 4, value: '12', name: langFont['outerwear'] },
                { id: 5, value: '13', name: langFont['bottoms'] },
            ],
            smallCategories: {
                '00': [
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
            createPage: false,
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
                            $("#labSearchProduct").text(langFont["inputError"]).show().delay(3000).fadeOut();
                            break;
                        case 100:
                            self.dataArray = response.ProductDataList;
                            self.pagesTotal = response.TotalPages;

                            if (!self.createPage) {
                                self.$bus.$emit('Pagination:Set', self.pageSize, self.pagesTotal);
                                self.createPage = true;
                            } else if (self.beforePagesTotal !== self.pagesTotal) {
                                alert(langFont['pageUpdata']);
                                self.$bus.$emit('Pagination:Updata', self.pagesTotal);
                            }

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

        //點選分按按鈕
        ChoosePagination: function (pageIndex, pageSize) {
            this.message = '';
            this.GetAllProductData(pageIndex, pageSize);
        },

    },
    created: function () {  //創建後
    },
    mounted: function () {  //掛載後
        this.GetAllProductData(1, this.pageSize);
    },
    beforeDestroy: function () {  //銷毀前
        
    },
    components: {
        'pagination-component': PaginationComponent,
        'table-component': TableComponent,
    }
};