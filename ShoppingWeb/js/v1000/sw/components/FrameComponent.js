var frameComponent = {
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
        showPage: function (val) {
            this.pageName = val;
        }
    },
    created: function () {
        this.$bus.$on("change-page-name", this.showPage)
    },
    components: {
        'default-component': defaultComponent,
        'user-component': userComponent,
        'member-component': memberComponent,
        'order-component': orderComponent,
        'product-component': productComponent,
    }
};