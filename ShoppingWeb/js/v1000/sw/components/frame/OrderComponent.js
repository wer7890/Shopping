var OrderComponent = {
    template: `
        <div class="container">
            <h2 class="text-center">${langFont['titleOrder']}</h2>
            <br />
            <div class="row">
                <div class="btn-group me-2" role="group" aria-label="First group">
                    <button @click="ShowOrder" v-for="data in btnArrayData" :key="data.id" v-text="data.name" type="button" class="btn btn-outline-secondary btnHand"></button>
                </div>
            </div>
            <br />

            <div class="row" id="orderTableDiv">
                  <table-component :theadData="theadData" :dataArray="dataArray">
                    
                 </table-component>
            </div>

            <div class="row">
                <span v-text="message" class="col-12 col-sm-12 text-center text-success"></span>
            </div>

            <div id="overlay">
                <div id="box"></div>
            </div>
        </div>
    `,
    data: function () {
        return {
            message: '',
            btnArrayData: [
                { id: 0, name: langFont['all'] },
                { id: 1, name: langFont['shipping'] },
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

            pageSize: 5,
            pagesTotal: null,
            beforePagesTotal: 1,
            createPage: false,
        }
    },
    methods: {
        GetAllOrderData: function (pageNumber, pageSize) {
            console.log("GetAllOrderData");
        },
        ShowOrder: function () {
            console.log("ShowOrder");
        },
    },
    created: function () {  //創建後

    },
    mounted: function () {  //掛載後
        this.GetAllOrderData(1, this.pageSize);
    },
    beforeDestroy: function () {  //銷毀前
        
    },
    components: {
        'pagination-component': PaginationComponent,
        'table-component': TableComponent,
    }
};