var UserComponent = {
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
                        <td><button @click="SetEditUser(data.Id)" class="btn btn-primary">${langFont['edit']}</button></td>
                        <td><button @click="DeleteUser(data.Id)" class="btn btn-danger">${langFont['del']}</button></td>
                    </template>
                 </table-component>
            </div>
            
            <pagination-component @Choose="GetAllUserData"></pagination-component>

            <div class="row">
                <span v-text="message" class="col-12 col-sm-12 text-center text-success"></span>
            </div>
            
            <popup-window-component>
                <template v-slot:content="{ page }">
                    <component :is="page"></component>
                </template>
            </popup-window-component>
            
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
            sortRise: false,  //升序

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
        }
    },
    methods: {
        //全部管理員資料
        GetAllUserData: function (pageNumber, pageSize) {
            if (typeof pageNumber === 'undefined' || typeof pageSize === 'undefined' || typeof this.beforePagesTotal === 'undefined') {
                this.message = "undefined";
                return;
            }

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

        //刪除刪除管理員
        DeleteUser: function (userId) {
            if (typeof userId === 'undefined') {
                this.message = "undefined";
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
                                self.GetAllUserData(1, self.pageSize);
                                self.$bus.$emit('Pagination:Updata', self.pagesTotal);
                                self.message = langFont["delSuccessful"];
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
            if (typeof userId === 'undefined' || typeof roles === 'undefined') {
                this.message = "undefined";
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
        SetEditUser: function (userId) {
            if (typeof userId === 'undefined') {
                this.message = "undefined";
                return;
            }

            var self = this;

            $.ajax({
                type: "POST",
                url: "/api/Controller/user/SetSessionSelectUserId",
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
                            self.$bus.$emit('PopupWindow:Set', 'edit-user-component');
                            break;
                        default:
                            alert(langFont["editFailed"]);
                            break;
                    }
                },
                error: function (error) {
                    self.message = langFont["ajaxError"];
                }
            });
        },

        //跳轉至新增管理員組件
        AddUser: function () {
            this.$bus.$emit('PopupWindow:Set', 'add-user-component');
        },

        //排序
        TableDataSort: function () {
            if (this.sortRise) {
                this.dataArray.sort(function (a, b) {
                    return a.Id - b.Id;
                });

                this.sortRise = !this.sortRise;
            } else {
                this.dataArray.sort(function (a, b) {
                    return b.Id - a.Id;
                });

                this.sortRise = !this.sortRise;
            }
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
        'popup-window-component': PopupWindowComponent,
    }
};