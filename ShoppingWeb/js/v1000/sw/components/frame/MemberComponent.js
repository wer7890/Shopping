var memberComponent = {
    template: `
        <div class="w-auto mx-3">
            <div class="row">
                <div class="col-10 d-flex justify-content-center">
                    <h2 class="text-center">${langFont['titleMember']}</h2>
                </div>
                <div class="col-2 d-flex justify-content-center">
                    <button id="btnAddMember" type="submit" class="btn btn-outline-primary">${langFont['addMember']}</button>
                </div>
            </div>
            <br />
            <div class="row">
                <table id="myTable" class="table table-striped table-hover table-bordered">
                    <thead>
                        <tr>
                            <th v-for="data in tableTheadData" :key="data.id">{{ data.name }}</th>
                        </tr>
                    </thead>
                    <tbody id="tableBody">
                        <tr v-for="data in dataArray" :key="data.Id">
                            <td>{{ data.Id }}</td>
                            <td>{{ data.Account }}</td>
                            <td>{{ data.Pwd }}</td>
                            <td>{{ data.Name }}</td>
                            <td>{{ data.Level }}</td>
                            <td>{{ data.PhoneNumber }}</td>
                            <td>{{ data.AccountStatus }}</td>
                            <td>{{ data.Amount }}</td>
                            <td>{{ data.TotalSpent }}</td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div id="pagination">
                <!-- 分頁內容 -->
            </div>

            <div class="row">
                <span id="labSearchMember" class="col-12 col-sm-12 text-center text-success"></span>
            </div>
        </div>
    `,
    data: function () {
        return {
            message: '',
            //table的thead中的資料
            tableTheadData: [
                { id: 1, name: langFont['id'] },
                { id: 2, name: langFont['account'] },
                { id: 3, name: langFont['pwd'] },
                { id: 4, name: langFont['name'] },
                { id: 5, name: langFont['level'] },
                { id: 6, name: langFont['phoneNumber'] },
                { id: 7, name: langFont['accountStatus'] },
                { id: 8, name: langFont['wallet'] },
                { id: 9, name: langFont['totalSpent'] },
            ],
            //table的tbody中的資料下拉選單
            rolesArray: [
                { value: 1, name: langFont['superAdmin'] },
                { value: 2, name: langFont['memberAdmin'] },
                { value: 3, name: langFont['productAdmin'] },
            ],
            dataArray: '',
            sortRise: false,  //升序
            pageSize: 5,
            pagesTotal: null,
            beforePagesTotal: 1,
            createPage: false,
        }
    },
    methods: {
        GetAllMemberData: function (pageNumber, pageSize) {
            if (typeof pageNumber === 'undefined' || typeof pageSize === 'undefined' || typeof this.beforePagesTotal === 'undefined') {
                $("#labSearchMember").text("undefined").show().delay(3000).fadeOut();
                return;
            }

            var self = this;

            $.ajax({
                url: '/api/Controller/member/GetAllMemberData',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ pageNumber: pageNumber, pageSize: pageSize, beforePagesTotal: self.beforePagesTotal }),
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
                            self.dataArray = response.MemberDataList;
                            self.pagesTotal = response.TotalPages;

                            if (!self.createPage) {
                                self.$bus.$emit('set-pagination', self.pageSize, self.pagesTotal);
                                self.createPage = true;
                            } else if (self.beforePagesTotal !== self.pagesTotal) {
                                alert(langFont['pageUpdata']);
                                self.$bus.$emit('updata-pagination', self.pagesTotal);
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
    },
    mounted: function () {  //掛載後
        this.GetAllMemberData(1, this.pageSize);
    },
};