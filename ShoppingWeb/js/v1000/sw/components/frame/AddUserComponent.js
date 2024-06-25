var AddUserComponent = {
    template: `
        <div class="container">
            <h2 class="text-center">${langFont['titleAddUser']}</h2>
            <br />
            <div class="row">
                <div class="mx-auto col-12 col-md-7 mt-2">
                    <label for="txbAccount" class="form-label">${langFont['account']}</label>
                    <input v-model="account" type="text" id="txbAccount" class="form-control" />
                </div>
                <div class="mx-auto col-12 col-md-7 mt-2">
                    <label for="txbPwd" class="form-label">${langFont['pwd']}</label>
                    <input v-model="pwd" type="password" id="txbPwd" class="form-control" />
                </div>
                <div class="mx-auto mt-3 col-12 col-md-7 mt-2">
                    <label for="ddlRoles" class="form-label">${langFont['roles']}</label>
                    <select v-model="roles" class="form-select form-select-sm f_roles">
                        <option v-for="data in rolesArray" :ket="data.value" :value="data.value">{{ data.name }}</option>
                    </select>
                </div>
            
                <button @click="AddUser" class="btn btn-outline-primary mx-auto mt-4 col-12 col-md-6">${langFont['addUser']}</button>
            </div>
            <br />
            <div class="row">
                <label class="col-12 col-sm-12 text-center text-success">{{ message }}</label>
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
            account: '',
            pwd: '',
            roles: 1,
        }
    },
    methods: {
        //新增管理員
        AddUser: function () {
            if (!this.IsSpecialChar()) {
                return;
            }

            var self = this;

            $.ajax({
                type: "POST",
                url: "/api/Controller/user/AddUser",  // 這裡指定後端方法的位置
                data: JSON.stringify({ account: this.account, pwd: this.pwd, roles: this.roles }),
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
                            self.message = langFont["addFormat"];
                            break;
                        case 100:
                            alert(langFont["addSuccessful"]);
                            self.$bus.$emit('change-page-name', 'user-component');
                            break;
                        case 101:
                            self.message = langFont["duplicateAccount"];
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

        //判斷特殊字元和長度
        IsSpecialChar: function () {
            if (typeof this.account === 'undefined' || typeof this.pwd === 'undefined' || typeof this.roles === 'undefined') {
                this.message = 'undefined';
                return false;
            }

            var regex = /^[A-Za-z0-9]{6,16}$/;

            var accountValid = regex.test(this.account);
            var pwdValid = regex.test(this.pwd);

            if (!accountValid || !pwdValid) {
                this.message = langFont["addSpecialChar"];
            }

            return accountValid && pwdValid;
        }
    },

}