var OrderComponent = {
    template: `
        <div class="container">
            <h2 class="text-center">${langFont['titleOrder']}</h2>
            <br />
            <div class="row">
                <div class="btn-group me-2" role="group" aria-label="First group">
                    <button @click="ShowOrder" v-for="data in btnArrayData" :key="data.id" type="button" class="btn btn-outline-secondary btnHand">{{ data.name }}</button>
                </div>
            </div>
            <br />
            <div class="row" id="orderTableDiv">
                  
            </div>

            <div class="row">
                <span class="col-12 col-sm-12 text-center text-success">{{ message }}</span>
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
            tableTheadData: [
                { id: 1, name: langFont['userId'] },
                { id: 2, name: langFont['account'] },
                { id: 3, name: langFont['roles'] },
                { id: 4, name: langFont['edit'] },
                { id: 5, name: langFont['del'] },
            ],
            dataArray: '',
        }
    },
    methods: {
        ShowOrder: function () {

        }
    },
    created: function () {  //創建後

    },
    mounted: function () {  //掛載後
        
    },
    beforeDestroy: function () {  //銷毀前
        
    },
    components: {
        'pagination-component': PaginationComponent,
        'table-component': TableComponent,
    }
};