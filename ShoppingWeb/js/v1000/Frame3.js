Vue.component('frame-component', {
    data: function () {
        return {
            userAccount: null,
            message: ''
        };
    },
    mounted: function () {
        this.getUserPermission();
    },
    methods: {
        getUserPermission: function () {
            $.ajax({
                type: "POST",
                url: "/api/Controller/login/GetUserPermission",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: (response) => {
                    this.userAccount = response.Account;
                    this.$refs.labUserAccount.textContent += this.userAccount;
                    switch (response.Roles) {
                        case 1:
                            break;
                        case 2:
                            this.$refs.adminPanel.remove();
                            this.$refs.productPanel.remove();
                            break;
                        case 3:
                            this.$refs.adminPanel.remove();
                            this.$refs.memberPanel.remove();
                            this.$refs.orderPanel.remove();
                            break;
                        default:
                            this.$refs.adminPanel.remove();
                            this.$refs.memberPanel.remove();
                            this.$refs.productPanel.remove();
                            this.$refs.orderPanel.remove();
                            this.$refs.labUserAccount.textContent = this.message = langFont["mistake"];
                            break;
                    }
                },
                error: (error) => {
                    this.$refs.labUserAccount.textContent = langFont["ajaxError"];
                }
            });
        },
        signOut: function () {
            $.ajax({
                type: "POST",
                url: "/api/Controller/login/DeleteSession",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: (response) => {
                    window.location.href = "Login.aspx";
                },
                error: (error) => {
                    this.$refs.labUserAccount.textContent = langFont["ajaxError"];
                }
            });
        },
        changeLanguage: function (language) {
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
                    this.$refs.labUserAccount.textContent += this.userAccount;
                },
                error: (error) => {
                    this.$refs.labUserAccount.textContent = langFont["ajaxError"];
                }
            });
        },
        setIframeSrc: function (src) {
            this.$refs.iframeContent.src = src;
        }
    },
    template: `
        <div class="container mt-5">
            <div class="row mt-2">
                <!--左欄-->
                <div class="col-12 col-md-2">
                    <div class="list-group">
                        <a href="javascript:void(0);" class="list-group-item list-group-item-action" ref="adminPanel" @click="setIframeSrc('UserManagement.aspx')">{{ $t('resource.adminSystem') }}</a>
                        <a href="javascript:void(0);" class="list-group-item list-group-item-action" ref="memberPanel" @click="setIframeSrc('MemberManagement.aspx')">{{ $t('resource.memberSystem') }}</a>
                        <a href="javascript:void(0);" class="list-group-item list-group-item-action" ref="productPanel" @click="setIframeSrc('ProductManagement.aspx')">{{ $t('resource.productSystem') }}</a>
                        <a href="javascript:void(0);" class="list-group-item list-group-item-action" ref="orderPanel" @click="setIframeSrc('OrderManagement.aspx')">{{ $t('resource.orderSystem') }}</a>
                    </div>
                    <div class="row mt-2">
                        <label ref="labUserAccount" class="fs-5 text-center align-middle mt-2">{{ $t('resource.account') }} : </label>
                        <br />
                        <button @click="signOut" class="btn btn-outline-dark mt-3">{{ $t('resource.signOut') }}</button>
                    </div>

                    <div class="row justify-content-center align-self-center mt-5">
                        <button class="btn btn-outline-secondary btn-lg col fs-6 btn-sm" @click="changeLanguage('TW')">中文</button>
                        <button class="btn btn-outline-secondary btn-lg col fs-6 btn-sm" @click="changeLanguage('EN')">English</button>
                    </div>

                </div>

                <!--右欄-->
                <div class="col-12 col-md-10">
                    <iframe ref="iframeContent" src="Default.aspx" style="width: 100%; height: 90vh; border: none;"></iframe>
                </div>

            </div>
        </div>
    `
});

new Vue({
    el: '#app'
});

















Vue.component('frame-component', {
    data: function () {
        return {
            userAccount: null,
            message: '',
            panels: [
                { id: 'adminPanel', label: 'adminSystem', href: 'UserManagement.aspx' },
                { id: 'memberPanel', label: 'memberSystem', href: 'MemberManagement.aspx' },
                { id: 'productPanel', label: 'productSystem', href: 'ProductManagement.aspx' },
                { id: 'orderPanel', label: 'orderSystem', href: 'OrderManagement.aspx' }
            ]
        };
    },
    mounted: function () {
        this.getUserPermission();
    },
    methods: {
        getUserPermission: function () {
            $.ajax({
                type: "POST",
                url: "/api/Controller/login/GetUserPermission",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: (response) => {
                    this.userAccount = response.Account;
                    this.$refs.labUserAccount.textContent += this.userAccount;
                    switch (response.Roles) {
                        case 1:
                            break;
                        case 2:
                            this.panels = this.panels.filter(panel => panel.id !== 'adminPanel' && panel.id !== 'productPanel');
                            break;
                        case 3:
                            this.panels = this.panels.filter(panel => panel.id !== 'adminPanel' && panel.id !== 'memberPanel' && panel.id !== 'orderPanel');
                            break;
                        default:
                            this.panels = [];
                            this.$refs.labUserAccount.textContent = this.message = langFont["mistake"];
                            break;
                    }
                },
                error: (error) => {
                    this.$refs.labUserAccount.textContent = langFont["ajaxError"];
                }
            });
        },
        signOut: function () {
            $.ajax({
                type: "POST",
                url: "/api/Controller/login/DeleteSession",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: (response) => {
                    window.location.href = "Login.aspx";
                },
                error: (error) => {
                    this.$refs.labUserAccount.textContent = langFont["ajaxError"];
                }
            });
        },
        changeLanguage: function (language) {
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
                    this.$refs.labUserAccount.textContent += this.userAccount;
                },
                error: (error) => {
                    this.$refs.labUserAccount.textContent = langFont["ajaxError"];
                }
            });
        },
        setIframeSrc: function (href) {
            this.$refs.iframeContent.src = href;
        }
    },
    template: `
        <div class="container mt-5">
            <div class="row mt-2">
                <!--左欄-->
                <div class="col-12 col-md-2">
                    <div class="list-group">
                        <a v-for="panel in panels" :key="panel.id" :href="'javascript:void(0);'" class="list-group-item list-group-item-action" :id="panel.id" @click="setIframeSrc(panel.href)">{{ $t('resource.' + panel.label) }}</a>
                    </div>
                    <div class="row mt-2">
                        <label ref="labUserAccount" class="fs-5 text-center align-middle mt-2">{{ $t('resource.account') }} : </label>
                        <br />
                        <button @click="signOut" class="btn btn-outline-dark mt-3">{{ $t('resource.signOut') }}</button>
                    </div>

                    <div class="row justify-content-center align-self-center mt-5">
                        <button class="btn btn-outline-secondary btn-lg col fs-6 btn-sm" @click="changeLanguage('TW')">中文</button>
                        <button class="btn btn-outline-secondary btn-lg col fs-6 btn-sm" @click="changeLanguage('EN')">English</button>
                    </div>

                </div>

                <!--右欄-->
                <div class="col-12 col-md-10">
                    <iframe ref="iframeContent" src="Default.aspx" style="width: 100%; height: 90vh; border: none;"></iframe>
                </div>

            </div>
        </div>
    `
});

new Vue({
    el: '#app'
});

