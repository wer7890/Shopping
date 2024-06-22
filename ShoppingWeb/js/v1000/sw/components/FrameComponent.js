﻿var frameComponent = {
    template: `
        <div class="col-12 col-md-10">
            <component :is='pageName'></component>
        </div>
    `,
    data: function () {
        return {
            pageName: 'default-component',
        }
    },
    methods: {
        //顯示選擇的頁面
        ShowPage: function (val) {
            this.pageName = val;
        }
    },
    created: function () {
        this.$bus.$on("change-page-name", this.ShowPage)
    },
    components: {
        'default-component': defaultComponent,
        'user-component': userComponent,
        'member-component': memberComponent,
        'order-component': orderComponent,
        'product-component': productComponent,
    }
};