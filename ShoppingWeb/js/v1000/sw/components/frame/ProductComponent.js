var ProductComponent = {
    template: `
        <div class="w-auto mx-3" id="allProductDiv">
            <div class="col-10 d-flex justify-content-center">
                    <h2 class="text-center"><%= Resources.Resource.titleUser %></h2>
             </div>
            <h2 class="text-center">${langFont['product']}<%= Resources.Resource.product %></h2>
            <br />
            <div class="row mx-auto col-12 mb-3">
                <div class="col-2 px-0" id="divCategories">
                </div>
                <div class="col-2" id="divMinorCategory">
                    <label for="minorCategory" class="form-label"><%= Resources.Resource.minorCategories %></label>
                    <select id="minorCategory" class="form-select">
                        <option value=""><%= Resources.Resource.chooseType %></option>
                    </select>
                </div>
                <div class="col-2 ps-0" id="divBrand">
                </div>
                <div class="col-2 px-0">
                    <label for="txbProductSearch" class="form-label"><%= Resources.Resource.productNameSelect %></label>
                    <input type="text" class="form-control" id="txbProductSearch" placeholder="<%= Resources.Resource.productNameSelect %>" />
                </div>
                <div class="col px-0 d-flex justify-content-center align-items-end">
                    <button id="btnSearchProduct" type="submit" class="btn btn-outline-primary"><%= Resources.Resource.select %></button>
                </div>
                <div class="col px-0 d-flex justify-content-center align-items-end">
                    <button id="btnAddProduct" type="submit" class="btn btn-outline-primary"><%= Resources.Resource.addProduct %></button>
                </div>
                <div class="col px-0 d-flex justify-content-center align-items-end">
                    <button id="btnLowProduct" type="submit" class="btn btn-outline-primary"><%= Resources.Resource.lowStock %></button>
                </div>
            </div>

            <div class="row" id="productTableDiv">
                <table id="myTable" class="table table-striped table-hover table-bordered">
                    <thead id="tableHead">
                        <tr>
                            <th>
                                <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.id %></button></th>
                            <th>
                                <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.name %></button></th>
                            <th>
                                <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.categories %></button></th>
                            <th>
                                <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.price %></button></th>
                            <th>
                                <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.stock %></button></th>
                            <th>
                                <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.warningValue %></button></th>
                            <th>
                                <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.open %></button></th>
                            <th>
                                <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.introduce %></button></th>
                            <th>
                                <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.img %></button></th>
                            <th>
                                <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.edit %></button></th>
                            <th>
                                <button type="button" class="btn btn-light btn-sm"><%= Resources.Resource.del %></button></th>

                        </tr>
                    </thead>
                    <tbody id="tableBody">
                        <!-- 內容 -->
                    </tbody>
                </table>
            </div>

            <div id="pagination">
                <!-- 分頁內容 -->
            </div>

            <div class="row">
                <span id="labSearchProduct" class="col-12 col-sm-12 text-center text-success"></span>
            </div>
        </div>
    `,
    data: function () {
        return {
            message: '',
        }
    },
    methods: {

    },
    created: function () {  //創建後
        
    },
    mounted: function () {  //掛載後
        this.GetAllUserData(1, this.pageSize);
    },
    beforeDestroy: function () {  //銷毀前
        
    },
    components: {
        'pagination-component': PaginationComponent,
        'table-component': TableComponent,
    }
};