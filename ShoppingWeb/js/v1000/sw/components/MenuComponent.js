var MenuComponent = {
    template: `
            <div class="col-12 col-md-2">
                <div class="list-group">
                    <a v-for="item in menuArr" :key="item.id" @click="ChangePageName(item.name)" v-text="item.title" href="javascript:void(0);" class="list-group-item list-group-item-action"></a>
                </div>
                <div class="row mt-2">
                    <label v-text="fullMessage" class="fs-5 text-center align-middle mt-2"></label>
                    <br />
                    <button @click="SignOut" class="btn btn-outline-dark mt-3">${langFont['signOut']}</button>
                </div>

                <div class="row justify-content-center align-self-center mt-5">
                    <button @click="ChangeLanguage('TW')" class="btn btn-outline-secondary btn-lg col fs-6 btn-sm">中文</button>
                    <button @click="ChangeLanguage('EN')" class="btn btn-outline-secondary btn-lg col fs-6 btn-sm">English</button>
                </div>
            </div>    `,
    data: function () {
        return {
            message: '',
            accountStr: langFont['account'],
            isShow: true,
            menuArr: [
                { id: 0, title: langFont['adminSystem'], name: 'user-component' },
                { id: 1, title: langFont['memberSystem'], name: 'member-component' },
                { id: 2, title: langFont['orderSystem'], name: 'order-component' },
                { id: 3, title: langFont['productSystem'], name: 'product-component' },
            ],
        }
    },
    methods: {
        //變更頁面
        ChangePageName: function (name) {  
            this.$bus.$emit("change-page-name", name);
        },

        //登出
        SignOut: function () {  
            var self = this;

            $.ajax({
                type: "POST",
                url: "/api/Controller/login/DeleteSession",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    window.location.href = "Login.aspx";
                },
                error: function (error) {
                    self.message = langFont["ajaxError"];
                }
            });
        },

        //取得帳號和身分
        GetUserPermission: function () {  
            var self = this;

            $.ajax({
                type: "POST",
                url: "/api/Controller/login/GetUserPermission",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    self.message = response.Account
                    switch (response.Roles) {
                        case 1:  //超級管理員
                            break;
                        case 2:  //會員管理員
                            self.menuArr.pop();  //移除最後一個元素
                            self.menuArr.shift();  //移除第一個元素
                            break;
                        case 3:
                            self.menuArr.splice(0, 3);  //移除從索引值0開始的後3個元素(含0)
                            break;
                        default:
                            self.menuArr = [];
                            self.message = langFont["mistake"];
                            break;
                    }
                },
                error: function (error) {
                    self.message = langFont["ajaxError"];
                }
            });
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
    },
    computed: {
        fullMessage: function () {
            return this.accountStr + ':  ' + this.message;
        }
    },
    mounted: function () {  //掛載後
        this.GetUserPermission();
    },
};