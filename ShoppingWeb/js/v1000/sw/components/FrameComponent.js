var FrameComponent = {
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
        this.$bus.$on('Frame:Change', this.ShowPage);
    },
    beforeDestroy: function () {  //組件銷毀前，移除該監聽
        this.$bus.$off('Frame:Change', this.ShowPage);
    },
    components: {
        'default-component': DefaultComponent,
        'user-component': UserComponent,
        'member-component': MemberComponent,
        'order-component': OrderComponent,
        'product-component': ProductComponent,
        'add-user-component': AddUserComponent,
        'edit-user-component': EditUserComponent,
    }
};