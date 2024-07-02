var WarnComponent = {
    name: 'WarnComponent',
    template: `
        <div id="lowStockProductsDiv" class="container my-4">
            <h3 class="text-center text-danger mb-4">${langFont['lowStock']}</h3>

            <table-component :theadData="theadData" :dataArray="dataArray" v-if="show">
                <template v-slot:table-row="{ data }">
                    <td v-text="data.f_id"></td>
                    <td v-text="data.f_nameTW"></td>
                    <td v-text="data.f_nameEN"></td>
                    <td v-text="data.f_stock"></td>
                    <td v-text="data.f_warningValue"></td>
                    <td>
                        <div class="form-check form-switch">
                            <input v-model="data.f_isOpen" @change="Edit(data.f_id)" type="checkbox" class="toggle-switch form-check-input">
                        </div>
                    </td>
                    <td><button @click="Delete(data.f_id)" class="btn btn-danger">${langFont['delOne']}</button></td>
                </template>
            </table-component>

            <div class="row">
                <span v-text="message" class="col-12 col-sm-12 text-center text-success"></span>
            </div>
        </div>
    `,
    data: function () {
        return {
            message: '',
            theadData: [
                { id: 1, name: langFont['id'] },
                { id: 2, name: langFont['productNameTW'] },
                { id: 3, name: langFont['productNameEN'] },
                { id: 4, name: langFont['stock'] },
                { id: 5, name: langFont['warningValue'] },
                { id: 6, name: langFont['open'] },
                { id: 7, name: langFont['edit'] },
            ],
            dataArray: '',
            show: false,
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
        //顯示預警商品
        GetDefaultLowStock: function () {
            var self = this;
            $.ajax({
                type: "POST",
                url: "/api/Controller/product/GetDefaultLowStock",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var stockInsufficient = JSON.parse(response.StockInsufficient);

                    if (stockInsufficient.length > 0) {
                        self.show = true;
                        self.dataArray = stockInsufficient;
                        
                    } else {
                        self.show = false;
                        self.message = langFont["noData"];
                    }
                },
                error: function (error) {
                    self.message = langFont["ajaxError"];
                }
            });
        },

        //更改商品狀態
        Edit: function (productId) {
            if (!productId) {
                this.message = langFont["inputError"];
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
                            self.$emit('Updata');
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
        Delete: function (productId) {
            if (!productId) {
                this.message = langFont["inputError"];
                return;
            }

            var self = this;
            var yes = confirm(langFont["confirmEditProduct"]);
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
                                self.message = langFont["delSuccessful"];
                                self.$emit('Updata');
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
    },
    mounted: function () {
        this.GetDefaultLowStock();
    },
    components: {
        'table-component': TableComponent,
    }
}