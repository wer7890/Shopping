Vue.component('login-form', {
    data: function() {
        return {
            account: '',
            pwd: '',
            rememberAccount: false,  //是否勾選記住帳號
            message: '',
            lang: 'TW'
        };
    },
    mounted() {
        let accountValue = this.getAccountCookie("account");
        if (accountValue) {
            this.account = accountValue;
            this.rememberAccount = true;
        }
    },
    methods: {
        login() {
            if (!this.isSpecialChar(this.account, this.pwd)) {
                return;
            }

            $.ajax({
                type: "POST",
                url: "/api/Controller/login/LoginUser",
                data: JSON.stringify({ account: this.account, pwd: this.pwd }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: (response) => {
                    switch (response.Status) {
                        case 2:
                            this.message = langFont["loginFormat"];
                            break;
                        case 100:
                            if (this.rememberAccount) {
                                document.cookie = 'account=' + this.account + '; max-age=2592000; path=/';
                            } else {
                                document.cookie = 'account=0; max-age=0; path=/';
                            }
                            window.location.href = "Frame.aspx";
                            break;
                        case 101:
                            this.message = langFont["loginFailed"];
                            break;
                        default:
                            this.message = langFont["errorLog"];
                    }
                },
                error: function (error) {
                    this.message = langFont["ajaxError"];
                }
            });
        },
        isSpecialChar(account, pwd) {
            if (typeof account === 'undefined' || typeof pwd === 'undefined') {
                this.message = "undefined";
                return false;
            }

            let regex = /^[A-Za-z0-9]{6,16}$/;
            let accountValid = regex.test(account);
            let pwdValid = regex.test(pwd);

            if (!accountValid || !pwdValid) {
                this.message = langFont["loginFormat"];
            }

            return accountValid && pwdValid;
        },
        changeLanguage(language) {
            if (typeof language === 'undefined') {
                this.message = "undefined";
                return;
            }

            $.ajax({
                type: "POST",
                url: "/api/Controller/login/SetLanguage",
                data: JSON.stringify({ language: language }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: (response) => {
                    parent.location.reload();
                },
                error: (error) => {
                    this.message = langFont["ajaxError"];
                }
            });
        },
        getAccountCookie(name) {
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
    template: `
        <div class="container">
            <div class="row">
                <h1 class="text-center mt-3">登入頁面</h1>
            </div>
            <hr />
            <div class="row mx-auto col-12 col-md-5">
                <div class="form-group">
                    <label for="txbAccount" class="control-label">帳號:</label>
                    <div>
                        <input type="text" id="txbAccount" class="form-control mt-2" v-model="account" placeholder="請輸入帳號" />
                    </div>
                </div>
                <br />
                <div class="form-group mt-3">
                    <label for="txbPassword" class="control-label">密碼:</label>
                    <div>
                        <input type="password" id="txbPassword" class="form-control mt-2" v-model="pwd" placeholder="請輸入密碼" />
                    </div>
                </div>
                <br />
                <div class="row justify-content-center align-self-center mt-3">
                    <div class="form-check ms-4 mb-2">
                        <input class="form-check-input" type="checkbox" v-model="rememberAccount" id="flexCheckDefault" />
                        <label class="form-check-label" for="flexCheckDefault">
                            記住帳號
                        </label>
                    </div>
                    <button @click="login" class="btn btn-outline-primary btn-lg col-md-offset-3 col-md-6">登入</button>
                </div>
                <div class="row justify-content-center align-self-center mt-5">
                    <button @click="changeLanguage('TW')" class="btn btn-outline-secondary btn-lg col-md-offset-3 col-md-2 fs-6 btn-sm">中文</button>
                    <button @click="changeLanguage('EN')" class="btn btn-outline-secondary btn-lg col-md-offset-3 col-md-2 fs-6 btn-sm">English</button>
                </div>
            </div>
            <br />
            <div class="row">
                <label id="labLogin" class="col-12 col-sm-12 text-center text-success">{{ message }}</label>
            </div>
        </div>
    `
});

let vm = new Vue({
    el: '#app',
    template: `
        <div>
            <login-form></login-form>    
        </div>
    `
});
