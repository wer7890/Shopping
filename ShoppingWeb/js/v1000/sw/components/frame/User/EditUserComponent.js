var EditUserComponent = {
    template: `
        <div class="container mt-4">
            <h2 class="text-center">${langFont['titleEditUser']}</h2>
            <br />
            <div class="row">
                <div class="mx-auto col-12 col-md-7 mt-2">
                    <span class="text-dark fs-6">${langFont['userId']} : </span>
                    <span class="fs-6" v-text="userId"></span>
                </div>
                <div class="mx-auto col-12 col-md-7 mt-3">
                    <span class="text-dark fs-6">${langFont['account']} : </span>
                    <span class="fs-6" v-text="account"></span>
                </div>
                <div class="mx-auto mt-3 col-12 col-md-7 mt-3">
                    <span class="text-dark fs-6">${langFont['roles']} : </span>
                    <span class="fs-6" v-text="roles"></span>
                </div>
                <div class="mx-auto col-12 col-md-7 mt-3">
                    <label class="form-label">${langFont['pwd']}</label>
                    <input v-model="pwd" type="password" class="form-control" />
                </div>

            </div>

            <div class="row">
                <button @click="EditUser" class="btn btn-outline-primary mx-auto mt-5 col-12 col-md-6">${langFont['edit']}</button>
            </div>

            <br />
            <div class="row">
                <span v-text="message" class="col-12 col-sm-12 text-center text-success"></span>
            </div>
        </div>
    `,
    data: function () {
        return {
            message: '',
            userId: '',
            account: '',
            roles: '',
            pwd:'',
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
        //預設所選擇管理員的資料
        GetEditUserData: function () {
            var self = this;

            $.ajax({
                type: "POST",
                url: "/api/Controller/user/GetUserDataForEdit",
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
                        case 100:
                            // 直接設定 input 元素的值
                            self.userId = response.UserDataList[0].Id;
                            self.account = response.UserDataList[0].Account;

                            switch (response.UserDataList[0].Roles) {
                                case 1:
                                    self.roles = langFont["superAdmin"];
                                    break;
                                case 2:
                                    self.roles = langFont["memberAdmin"];
                                    break;
                                case 3:
                                    self.roles = langFont["productAdmin"];
                                    break;
                                default:
                                    self.roles = langFont["mistake"];
                                    break;
                            }
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

        //更改管理員
        EditUser: function () {
            if (!this.IsSpecialChar()) {
                return;
            }

            var self = this;

            $.ajax({
                type: "POST",
                url: "/api/Controller/user/EditUser",
                data: JSON.stringify({ pwd: this.pwd }),
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
                            self.message = langFont["editFormat"];
                            break;
                        case 100:
                            alert(langFont["editSuccessful"]);
                            break;
                        case 101:
                            self.message = langFont["editFail"];
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

        //判斷輸入值
        IsSpecialChar: function () {
            if (!this.pwd) {
                this.message = langFont["inputError"];
                return false;
            }

            var regex = /^[A-Za-z0-9]{6,16}$/;

            var pwdValid = regex.test(this.pwd);

            if (!pwdValid) {
                this.message = langFont["editFormat"];
            }

            return pwdValid;
        },
    },
    mounted: function () {  //掛載後
        this.GetEditUserData();
    },
}