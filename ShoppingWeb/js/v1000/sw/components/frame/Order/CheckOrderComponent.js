var CheckOrderComponent = {
    template: `
        <div class="container mt-5">
            <h2 class="text-center">${langFont['titleEditOrder']}</h2>
            <div class="row d-flex justify-content-center my-3">
                <div class="col">
                    <label class="form-label">${langFont['orderStatus']}</label>
                    <select v-model="order" class="form-select">
                        <option v-for="data in orderStatus" :key="data.id" :value="data.value" v-text="data.name"></option>
                    </select>
                </div>

                <div class="col">
                    <label class="form-label">${langFont['deliveryStatus']}</label>
                    <select v-model="delivery" class="form-select">
                        <option v-for="data in deliveryStatus" :key="data.id" :value="data.value" v-text="data.name"></option>
                    </select>
                </div>

                <div class="col">
                    <label class="form-label">${langFont['deliveryMethod']}</label>
                    <select v-model="method" class="form-select">
                        <option v-for="data in deliveryMethod" :key="data.id" :value="data.value" v-text="data.name"></option>
                    </select>
                </div>

                <div class="col-1 d-flex align-items-end px-0">
                    <button @click="Edit" type="submit" class="btn btn-outline-primary px-2">${langFont["edit"]}</button>
                </div>
            </div>

            <div class="row">
                <label v-text="message" class="col-12 col-sm-12 text-center text-success"></label>
            </div>

            <h2 class="text-center mt-5">${langFont['titleOrderDetail']}</h2>
            <div class="row mt-3">
                <table-component :theadData="theadData" :dataArray="dataArray">
                    <template v-slot:table-row="{ data }">
                        <td v-text="data.ProductName"></td>
                        <td v-text="data.ProductPrice"></td>
                        <td v-text="data.ProductCategory"></td>
                        <td v-text="data.Quantity"></td>
                        <td v-text="data.Subtotal"></td>
                    </template>
                </table-component>
            </div>
        </div>
    `,
    props: {
        orderId: Number,
        orderStatusNum: Number,
        deliveryStatusNum: Number,
        deliveryMethodNum: Number,
    },
    data: function () {
        return {
            message: '',
            theadData: [
                { id: 1, name: langFont["productName"] },
                { id: 2, name: langFont["productPrice"] },
                { id: 3, name: langFont['productType'] },
                { id: 4, name: langFont['quantity'] },
                { id: 5, name: langFont['subtotal'] },
            ],
            dataArray: '',
            orderStatus: [
                { value: 1, name: langFont['paid'] },
                { value: 2, name: langFont['return'] },
                { value: 3, name: langFont['refunding'] },
                { value: 4, name: langFont['refunded'] },
            ],
            deliveryStatus: [
                { value: 1, name: langFont['shipping'] },
                { value: 2, name: langFont['shipped'] },
                { value: 3, name: langFont['arrived'] },
                { value: 4, name: langFont['received'] },
                { value: 5, name: langFont['returning'] },
                { value: 6, name: langFont['returned'] },
            ],
            deliveryMethod: [
                { value: 1, name: langFont['supermarket'] },
                { value: 2, name: langFont['store'] },
                { value: 3, name: langFont['home'] },
            ],
            order: this.orderStatusNum,
            delivery: this.deliveryStatusNum,
            method: this.deliveryMethodNum,
        }
    },
    watch: {
        message: function () {
            var self = this;
            setTimeout(function () {
                self.message = '';
            }, 3000);
        },
    },
    methods: {
        //更改訂單
        Edit: function () {
            if (!this.orderId || !this.order || !this.delivery || !this.method) {
                this.message = langFont["inputError"];
                return;
            }

            var self = this;

            $.ajax({
                type: "POST",
                url: "/api/Controller/order/EditOrder",
                data: JSON.stringify({ orderId: this.orderId, orderStatusNum: this.order, deliveryStatusNum: this.delivery, deliveryMethodNum: this.method }),
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
                        case 101:
                            var temp = (response.Status === 100) ? langFont["editSuccessful"] : langFont["editOrderFailed"];
                            self.message = temp;
                            self.$emit('Updata');
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

        //一開始下拉選單的預設值
        GetOrderDetail: function () {
            if (!this.orderId) {
                this.message = langFont["inputError"];
                return;
            }

            var self = this;

            $.ajax({
                url: '/api/Controller/order/GetOrderDetailsData',
                data: JSON.stringify({ orderId: this.orderId }),
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
                            self.dataArray = response.OrderDetailList;
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
    mounted: function () {  //掛載後
        this.GetOrderDetail();
    },
    components: {
        'table-component': TableComponent,
    }
}