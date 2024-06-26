var FrameComponent = {
    template: `
        <div class="col-12 col-md-10">
            <component :is='name'></component>
        </div>
    `,
    data: function () {
        return {
            name: 'default-component',
        }
    },
    methods: {
        //顯示選擇的頁面
        Show: function (val) {
            this.name = val;
        }
    },
    created: function () {
        this.$bus.$on('Frame:Change', this.Show);
    },
    beforeDestroy: function () {  //組件銷毀前，移除該監聽
        this.$bus.$off('Frame:Change', this.Show);
    },
    components: {
        'default-component': DefaultComponent,
        'user-component': UserComponent,
        'member-component': MemberComponent,
        'order-component': OrderComponent,
        'product-component': ProductComponent,
    }
};