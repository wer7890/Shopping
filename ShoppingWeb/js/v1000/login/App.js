var vm = new Vue({
    el: '#app',
    template: `
        <div class="container">
            <div class="row">
                <h1 class="text-center mt-3">${langFont['titleLogin']}</h1>
            </div>
            <hr />
            <div class="row mx-auto col-12 col-md-5">
                <div class="form-group">
                    <label class="control-label">${langFont['account']}:</label>
                    <div>
                        <input type="text" class="form-control mt-2" v-model="account" placeholder="${langFont['txbAccount']}" />
                    </div>
                </div>
                <br />
                <div class="form-group mt-3">
                    <label class="control-label">${langFont['pwd']}:</label>
                    <div>
                        <input type="password" class="form-control mt-2" v-model="pwd" placeholder="${langFont['txbPassword']}" />
                    </div>
                </div>
                <br />
                <div class="row justify-content-center align-self-center mt-3">
                    <div class="form-check ms-4 mb-2">
                        <input class="form-check-input" type="checkbox" v-model="rememberAccount" />
                        <label class="form-check-label">
                            ${langFont['rememberAccount']}
                        </label>
                    </div>
                    <button @click="Login" class="btn btn-outline-primary btn-lg col-md-offset-3 col-md-6">${langFont['btnLogin']}</button>
                </div>
                <div class="row justify-content-center align-self-center mt-5">
                    <button @click="ChangeLanguage('TW')" class="btn btn-outline-secondary btn-lg col-md-offset-3 col-md-2 fs-6 btn-sm">中文</button>
                    <button @click="ChangeLanguage('EN')" class="btn btn-outline-secondary btn-lg col-md-offset-3 col-md-2 fs-6 btn-sm">English</button>
                </div>
            </div>
            <br />
            <div class="row">
                <label v-text="message" class="col-12 col-sm-12 text-center text-success"></label>
            </div>
        </div>
    `,
    data: function () {
        return {
            account: '',
            pwd: '',
            rememberAccount: false,  //是否勾選記住帳號
            message: '',
        };
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
        //登入
        Login: function () {  
            if (!this.IsSpecialChar(this.account, this.pwd)) {
                return;
            }

            var self = this;

            $.ajax({
                type: "POST",
                url: "/api/Controller/login/LoginUser",
                data: JSON.stringify({ account: this.account, pwd: this.pwd }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    switch (response.Status) {
                        case 2:
                            self.message = langFont['loginFormat'];
                            break;
                        case 100:
                            //如果有勾選記住帳號，就紀錄cookie["account"]，反之則清除
                            if (self.rememberAccount) {
                                document.cookie = 'account=' + self.account + '; max-age=2592000; path=/';  //30天到期，path=/該cookie整個網站都是可看見的
                            } else {
                                document.cookie = 'account=0; max-age=0; path=/';  //馬上過期   
                            }
                            window.location.href = "Frame.aspx";
                            break;
                        case 101:
                            self.message = langFont['loginFailed'];
                            break;
                        default:
                            self.message = langFont['errorLog'];
                    }
                },
                error: function (error) {
                    self.message = langFont['ajaxError'];
                }
            });
        },

        //輸入值判斷
        IsSpecialChar: function (account, pwd) {  
            if (typeof account === 'undefined' || typeof pwd === 'undefined') {
                this.message = "undefined";
                return false;
            }

            var regex = /^[A-Za-z0-9]{6,16}$/;
            var accountValid = regex.test(account);
            var pwdValid = regex.test(pwd);

            if (!accountValid || !pwdValid) {
                this.message = langFont['loginFormat'];
            }

            return accountValid && pwdValid;
        },

        //切換語言
        ChangeLanguage: function (language) {  
            if (typeof language === 'undefined') {
                this.message = "undefined";
                return;
            }
            document.cookie = 'language=' + language + '; max-age=2592000; path=/';
            parent.location.reload();
        },

        //取的cookie 
        GetCookie: function (name) {  
            var cookies = document.cookie.split(';');

            for (var i = 0; i < cookies.length; i++) {
                var cookie = cookies[i].trim();
                if (cookie.startsWith(name + '=')) {
                    return cookie.substring(name.length + 1);
                }
            }
            return null;
        }
    },
    mounted: function () {  //掛載後
        var accountValue = this.GetCookie("account");
        if (accountValue) {
            this.account = accountValue;
            this.rememberAccount = true;
        }
    },
});