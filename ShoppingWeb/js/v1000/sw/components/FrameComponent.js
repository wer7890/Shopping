var frameComponent = {
    template: `
        <div class="col-12 col-md-10">
            <keep-alive>
                <component :is='pageName'></component>
            </keep-alive>
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
        this.$bus.$on('change-page-name', this.ShowPage)
    },
    beforeDestroy: function () {  //組件銷毀前，移除該監聽
        this.$bus.$off('change-page-name', this.ShowPage);
    },
    components: {
        'default-component': defaultComponent,
        'user-component': userComponent,
        'member-component': memberComponent,
        'order-component': orderComponent,
        'product-component': productComponent,
        'add-user-component': addUserComponent,
        'edit-user-component': editUserComponent,
    }
};