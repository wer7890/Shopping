var OrderComponent = {
    template: `
        <div class="container">
            <h2 class="text-center">${langFont['titleOrder']}</h2>
            <br />
            <div class="row">
                <div class="btn-group me-2" role="group" aria-label="First group">
                    <button @click="GetOrderData(data.id)" v-for="data in btnArrayData" :key="data.id" v-text="data.name" type="button" class="btn btn-outline-secondary btnHand"></button>
                </div>
            </div>
            <br />

            <div class="row" id="orderTableDiv">
                <order-table-component :theadData="theadData" :dataArray="dataArray">
                    <template v-slot:table-row="{ data }">
                        <td v-text="data.Id"></td>
                        <td v-text="data.Account"></td>
                        <td v-text="data.CreatedTime"></td>
                        <td>
                            <span :class="spanClass + orderStatus[data.OrderStatus].color + ' ' + orderStatus[data.OrderStatus].text" v-text="orderStatus[data.OrderStatus].name"></span>
                        </td>
                        <td>
                            <span :class="spanClass + deliveryStatus[data.DeliveryStatus].color + ' ' + deliveryStatus[data.DeliveryStatus].text" v-text="deliveryStatus[data.DeliveryStatus].name"></span>
                        </td>
                        <td v-text="deliveryMethod[data.DeliveryMethod]"></td>
                        <td v-text="data.Total"></td>
                    </template>
                </order-table-component>
            </div>
            
            <pagination-component @Choose="GetAllOrderData" :size="pageSize" :total="pagesTotal"></pagination-component>
        
            <div class="row">
                <span v-text="message" class="col-12 col-sm-12 text-center text-success"></span>
            </div>

        </div>
    `,
    data: function () {
        return {
            message: '',
            spanClass: 'px-3 py-1 rounded ',
            btnArrayData: [
                { id: 1, name: langFont['shipping'],},
                { id: 2, name: langFont['shipped'] },
                { id: 3, name: langFont['arrived'] },
                { id: 4, name: langFont['received'] },
                { id: 5, name: langFont['returning'] },
                { id: 6, name: langFont['returned'] },
                { id: 7, name: langFont['return'] },
            ],
            //table的thead中的資料
            theadData: [
                { id: 1, name: langFont['orderId'] },
                { id: 2, name: langFont['serialNumber'] },
                { id: 3, name: langFont['createdTime'] },
                { id: 4, name: langFont['orderStatus'] },
                { id: 5, name: langFont['deliveryStatus'] },
                { id: 6, name: langFont['deliveryMethod'] },
                { id: 7, name: langFont['total'] },
            ],
            dataArray: '',
            deliveryStatusCountData: '',
            orderStatus: {
                "1": { name: langFont['paid'], color: "bg-white", text: "text-dark" },
                "2": { name: langFont['return'], color: "bg-success", text: "text-white" },
                "3": { name: langFont['refunding'], color: "bg-warning", text: "text-white" },
                "4": { name: langFont['refunded'], color: "bg-white", text: "text-dark" }
            },
            deliveryStatus: {
                "1": { name: langFont['shipping'], color: "bg-warning", text: "text-white" },
                "2": { name: langFont['shipped'], color: "bg-success", text: "text-white" },
                "3": { name: langFont['arrived'], color: "bg-white", text: "text-dark" },
                "4": { name: langFont['received'], color: "bg-white", text: "text-dark" },
                "5": { name: langFont['returning'], color: "bg-warning", text: "text-white" },
                "6": { name: langFont['returned'], color: "bg-white", text: "text-dark" }
            },
            deliveryMethod: {
                "1": langFont['supermarket'],
                "2": langFont['store'],
                "3": langFont['home']
            },

            pageSize: 5,
            pagesTotal: null,
            beforePagesTotal: 1,
            createPage: false,
        }
    },
    watch: {
        dataArray: function () {

        }
    },
    methods: {
        //全部訂單資料
        GetAllOrderData: function (pageNumber, pageSize) {
            if (!pageNumber || !pageSize || !this.beforePagesTotal) {
                this.message = langFont["inputError"];
                return;
            }

            var self = this;

            $.ajax({
                url: '/api/Controller/order/GetAllOrderData',
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
                            self.dataArray = response.OrderList;
                            self.deliveryStatusCountData = response.StatusList;
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

        //所選擇狀態的訂單資料
        GetOrderData: function (deliveryStatusNum, pageNumber = 1, pageSize = this.pageSize) {
            var self = this;

            $.ajax({
                url: '/api/Controller/order/GetOrderData',
                data: JSON.stringify({ deliveryStatusNum: deliveryStatusNum, pageNumber: pageNumber, pageSize: pageSize, beforePagesTotal: this.beforePagesTotal }),
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
                            self.dataArray = response.OrderList;
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

    },
    mounted: function () {  //掛載後
        this.GetAllOrderData(1, this.pageSize);
    },
    components: {
        'pagination-component': PaginationComponent,
        'order-table-component': OrderTableComponent,
    }
};