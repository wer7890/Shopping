var userComponent = {
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
                <table id="myTable" class="table table-striped table-hover ">
                    <thead>
                        <tr>
                            <th>
                                <button type="button" class="btn btn-light btn-sm">${langFont['userId']}</button></th>
                            <th>
                                <button type="button" class="btn btn-light btn-sm">${langFont['account']}</button></th>
                            <th>
                                <button type="button" class="btn btn-light btn-sm">${langFont['roles']}</button></th>
                            <th>
                                <button type="button" class="btn btn-light btn-sm" disabled>${langFont['edit']}</button></th>
                            <th>
                                <button type="button" class="btn btn-light btn-sm" disabled>${langFont['del']}</button></th>
                        </tr>
                    </thead>
                    <tbody id="tableBody">
                        <tr v-for="data in dataArray" :key="data.Id">
                            <td>{{ data.Id }}</td>
                            <td>{{ data.Account }}</td>
                            <td>
                                <select v-model="data.Roles" @change="EditUserRoles(data.Id, data.Roles)" class="form-select form-select-sm f_roles">
                                    <option v-for="data in rolesArray" :ket="data.value" :value="data.value">{{ data.name }}</option>
                                </select>
                            </td>
                            <td><button @click="SetEditUser(data.Id)" class="btn btn-primary">${langFont['edit']}</button></td>
                            <td><button @click="DeleteUser(data.Id)" class="btn btn-danger">${langFont['del']}</button></td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div id="pagination">
                <!-- 分頁內容 -->
            </div>

            <div class="row">
                <span class="col-12 col-sm-12 text-center text-success">{{ message }}</span>
            </div>
        </div>
    `,
    data: function () {
        return {
            rolesArray: [
                { value: 1, name: langFont['superAdmin'] },
                { value: 2, name: langFont['memberAdmin'] },
                { value: 3, name: langFont['productAdmin'] },
            ],
            message: '',
            pageSize: 5,
            pagesTotal: null,
            page: null,
            beforePagesTotal: 1,
            dataArray: '',
        }
    },
    methods: {
        //搜尋資料
        SearchAllData: function (pageNumber, pageSize) {
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

        SetPagination: function () {
            
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
                                self.SearchAllData(1, self.pageSize);
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

        //更改至管理員(先去拿該會員資料，在把該會員資料帶進去)
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
                            self.$bus.$emit('change-page-name', 'edit-user-component');
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

        //跳轉至管理員
        AddUser: function () {
            this.$bus.$emit('change-page-name', 'add-user-component');
        }
    },
    mounted: function () {
        this.SearchAllData(1, this.pageSize);
    }
};