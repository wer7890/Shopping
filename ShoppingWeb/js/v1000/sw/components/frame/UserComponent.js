var UserComponent = {
    name: 'UserComponent',
    template: `
        <div class="container">
            <div class="row">
                <div class="col-10 d-flex justify-content-center">
                    <h2 class="text-center">${langFont['titleUser']}</h2>
                </div>
                <div class="col-2 d-flex justify-content-center">
                    <button @click="AddUser" type="submit" class="btn btn-outline-primary btn-sm">${langFont['addUser']}</button>
                </div>
            </div>
            <br />
            
            <div class="row">
                <table-component :theadData="theadData" :dataArray="dataArray" @Sort="TableDataSort">
                    <template v-slot:table-row="{ data }">
                        <td v-text="data.Id"></td>
                        <td v-text="data.Account"></td>
                        <td>
                            <select v-model="data.Roles" @change="EditUserRoles(data.Id, data.Roles)" class="form-select form-select-sm f_roles">
                                <option v-for="role in rolesArray" :key="role.value" :value="role.value" v-text="role.name"></option>
                            </select>
                        </td>
                        <td><button @click="SetEditUser(data.Id, data.Account, data.Roles)" class="btn btn-primary">${langFont['edit']}</button></td>
                        <td><button @click="DeleteUser(data.Id)" class="btn btn-danger">${langFont['del']}</button></td>
                    </template>
                 </table-component>
            </div>
            
            <pagination-component @Choose="GetAllUserData" :size="pageSize" :total="pagesTotal"></pagination-component>

            <div class="row">
                <span v-text="message" class="col-12 col-sm-12 text-center text-success"></span>
            </div>
            
            <pop-window-component>
                <template v-slot:content="{ page }">
                    <component :is="page" @Updata="Updata"></component>
                </template>
            </pop-window-component>
            
        </div>
    `,
    data: function () {
        return {
            message: '',
            //table的thead中的資料
            theadData: [
                { id: 1, name: langFont['userId'] },
                { id: 2, name: langFont['account'] },
                { id: 3, name: langFont['roles'] },
                { id: 4, name: langFont['edit'] },
                { id: 5, name: langFont['del'] },
            ],
            //table的tbody中的資料下拉選單
            rolesArray: [
                { value: 1, name: langFont['superAdmin'] },
                { value: 2, name: langFont['memberAdmin'] },
                { value: 3, name: langFont['productAdmin'] },
            ],
            dataArray: '',

            pageSize: 5,
            pagesTotal: null,
            beforePagesTotal: 1,
            pageIndex: 1,
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
        //全部管理員資料
        GetAllUserData: function (pageNumber, pageSize) {
            if (!pageNumber || !pageSize || !this.beforePagesTotal) {
                this.message = langFont["inputError"];
                return;
            }

            this.pageIndex = pageNumber;
            var self = this;

            $.ajax({
                url: '/api/Controller/user/GetAllUserData',
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
                            // 處理成功取得資料的情況
                            self.dataArray = response.UserDataList;
                            self.pagesTotal = response.TotalPages;
                            self.beforePagesTotal = self.pagesTotal;
                            break;
                        case 101:
                            self.dataArray = '';
                            self.pagesTotal = 0;
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

        //刪除刪除管理員
        DeleteUser: function (userId) {
            if (!userId) {
                this.message = langFont["inputError"];
                return;
            }

            var self = this;
            var yes = confirm(langFont["delConfirmation"]);
            if (yes == true) {
                $.ajax({
                    type: "POST",
                    url: "/api/Controller/user/DelUserInfo",
                    data: JSON.stringify({ userId: userId }),
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
                                // 刪除成功後，重新讀取資料
                                self.message = langFont["delSuccessful"];
                                self.GetAllUserData(self.pageIndex, self.pageSize);
                                break;
                            case 101:
                                alert(langFont["delFailed"]);
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

        //更改管理員身分
        EditUserRoles: function (userId, roles) {
            if (!userId || !roles) {
                this.message = langFont["inputError"];
                return;
            }

            var self = this;

            $.ajax({
                type: "POST",
                url: "/api/Controller/user/EditUserRoles",
                data: JSON.stringify({ userId: userId, roles: roles }),
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
                            self.message = langFont["changeSuccessful"];
                            break;
                        case 101:
                            self.message = langFont["changeFailed"];
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

        //跳轉更改管理員組件
        SetEditUser: function (userId, account, roles) {
            if (!userId) {
                this.message = langFont["inputError"];
                return;
            }

            var newUrl = window.location.protocol + "//" + window.location.host + window.location.pathname +
                '?userId=' + userId + '&account=' + encodeURIComponent(account) + '&roles=' + encodeURIComponent(roles);
            window.history.pushState({ path: newUrl }, '', newUrl);
            this.$bus.$emit('PopWindow:Set', 'edit-user-component');
        },

        //跳轉至新增管理員組件
        AddUser: function () {
            this.$bus.$emit('PopWindow:Set', 'add-user-component');
        },

        //排序
        TableDataSort: function (sortBol) {
            if (sortBol) {
                this.dataArray.sort(function (a, b) {
                    return a.Id - b.Id;
                });
            } else {
                this.dataArray.sort(function (a, b) {
                    return b.Id - a.Id;
                });
            }
        },

        //更新表格
        Updata: function () {
            this.GetAllUserData(this.pageIndex, this.pageSize);
        },

    },
    mounted: function () {  //掛載後
        this.GetAllUserData(1, this.pageSize);
    },
    components: {
        'pagination-component': PaginationComponent,
        'table-component': TableComponent,
        'add-user-component': AddUserComponent,
        'edit-user-component': EditUserComponent,
        'pop-window-component': PopWindowComponent,
    }
};