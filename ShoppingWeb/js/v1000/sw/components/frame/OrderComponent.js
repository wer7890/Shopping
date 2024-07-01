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
                <table-component :theadData="theadData" :dataArray="dataArray">
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
    
                        <td v-if="isReturn">
                            <div class="d-flex justify-content-between">
                                <button type="button" class="btn btn-outline-primary btn-sm" @click="EditReturnOrder(data.Id, true)">${langFont['yes']}</button>
                                <button type="button" class="btn btn-outline-danger btn-sm" @click="EditReturnOrder(data.Id, false)">${langFont['no']}</button>
                            </div>
                        </td>

                        <td v-if="!isReturn">
                            <button @click="EditOrder(data.Id, data.OrderStatus, data.DeliveryStatus, data.DeliveryMethod)" class="btn btn-primary">${langFont['edit']}</button>
                        </td>
                    </template>
                </table-component>
            </div>
            
            <pagination-component @Choose="GetSelectOrderData" :size="pageSize" :total="pagesTotal"></pagination-component>
        
            <div class="row">
                <span v-text="message" class="col-12 col-sm-12 text-center text-success"></span>
            </div>

            <pop-window-component>
                <template v-slot:content="{ page }">
                    <component :is="page" @Updata="Updata" :orderId="orderId" :orderStatusNum="orderStatusNum" :deliveryStatusNum="deliveryStatusNum" :deliveryMethodNum="deliveryMethodNum"></component>
                </template>
            </pop-window-component>
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
                { id: 8, name: langFont['edit'] },
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
            selectedOrderId: '',
            isReturn: false,
            orderId: '',
            orderStatusNum: '',
            deliveryStatusNum: '',
            deliveryMethodNum: '',


            pageSize: 5,
            pagesTotal: null,
            beforePagesTotal: 1,
            createPage: false,
        }
    },
    watch: {
        message: function () {
            var self = this;
            setTimeout(function () {
                self.message = '';
            }, 3000);
        },
        dataArray: function () {
            this.btnArrayData = [
                { id: 1, name: langFont['shipping'] + '(' + this.deliveryStatusCountData[0].Shipping + ')'},
                { id: 2, name: langFont['shipped'] + '(' + this.deliveryStatusCountData[0].Shipped + ')' },
                { id: 3, name: langFont['arrived'] + '(' + this.deliveryStatusCountData[0].Arrived + ')' },
                { id: 4, name: langFont['received'] + '(' + this.deliveryStatusCountData[0].Received + ')' },
                { id: 5, name: langFont['returning'] + '(' + this.deliveryStatusCountData[0].Returning + ')' },
                { id: 6, name: langFont['returned'] + '(' + this.deliveryStatusCountData[0].Returned + ')' },
                { id: 7, name: langFont['return'] + '(' + this.deliveryStatusCountData[0].Return + ')' },
            ]
        },
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
            this.selectedOrderId = deliveryStatusNum;

            if (deliveryStatusNum === 7) {
                this.GetReturnOrderData(pageNumber = 1, pageSize = this.pageSize);
                return;
            }

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
                            if (self.isReturn) {
                                self.theadData.push({ id: 8, name: langFont['edit'] });
                            }
                            self.isReturn = false;
                            self.theadData = self.theadData.filter(function (item) {
                                return item.id !== 9;
                            });
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

        //點擊上方退貨申請按鈕事件
        GetReturnOrderData: function (pageNumber, pageSize) {
            if (!pageNumber || !pageSize || !this.beforePagesTotal) {
                this.message = langFont["inputError"];
                return;
            }

            var self = this;

            $.ajax({
                url: '/api/Controller/order/GetReturnOrderData',
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
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

                            if (!self.isReturn) {
                                self.theadData.push({ id: 9, name: langFont['orderSure'] });
                            }

                            self.theadData = self.theadData.filter(function (item) {
                                return item.id !== 8;
                            });
                            self.isReturn = true;
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

        //分頁的callback function
        GetSelectOrderData: function (pageNumber, pageSize) {
            if (this.selectedOrderId) {
                this.GetOrderData(this.selectedOrderId, pageNumber, pageSize);
            } else {
                this.GetAllOrderData(pageNumber, pageSize);
            }
        },

        //同意或拒絕退款申請後，更改訂單狀態和配送狀態
        EditReturnOrder: function (orderId, boolReturn) {
            if (!orderId) {
                this.message = langFont["inputError"];
                return;
            }

            var self = this;

            $.ajax({
                url: '/api/Controller/order/EditReturnOrder',
                data: JSON.stringify({ orderId: orderId, boolReturn: boolReturn }),
                type: 'POST',
                contentType: 'application/json',
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
                            var temp = (response.Status === 100) ? langFont["editSuccessful"] : langFont["editFail"];
                            self.message = temp;
                            self.GetReturnOrderData(1, self.pageSize);
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

        //跳轉至查看訂單組件
        EditOrder: function (orderId, orderStatusNum, deliveryStatusNum, deliveryMethodNum) {
            this.orderId = orderId;
            this.orderStatusNum = orderStatusNum;
            this.deliveryStatusNum = deliveryStatusNum;
            this.deliveryMethodNum = deliveryMethodNum;
            this.$bus.$emit('PopWindow:Set', 'check-order-component');
        },

        //更新表格
        Updata: function () {
            this.GetAllOrderData(1, this.pageSize);
        }

    },
    mounted: function () {  //掛載後
        this.GetAllOrderData(1, this.pageSize);
    },
    components: {
        'pagination-component': PaginationComponent,
        'table-component': TableComponent,
        'pop-window-component': PopWindowComponent,
        'check-order-component': CheckOrderComponent,
    }
};