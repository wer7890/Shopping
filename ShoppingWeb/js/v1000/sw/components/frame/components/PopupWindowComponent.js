var PopupWindowComponent = {
    template: `
        <div class="outerMask" v-if="showMask">
            <div class="innerMask">
                <slot name="content" :pageName="pageName"></slot>
                <div class="container">
                    <div class="row">
                        <button @click="Closure" class="btn btn-outline-secondary mx-auto mt-4 col-12 col-md-6">${langFont['closure']}</button>
                    </div>
                </div>
            </div>
        </div>
    `,
    data: function () {
        return {
            showMask: false,
            pageName: '',
        }
    },
    methods: {
        Show: function (val) {
            this.pageName = val;
            this.showMask = true;
        },
        Closure: function () {
            this.pageName = '';
            this.showMask = false;
        }
    },
    created: function () {  
        this.$bus.$on('PopupWindow:Show', this.Show);
    },
    beforeDestroy: function () {  
        this.$bus.$off('PopupWindow:Show', this.Show);
    },
    components: {
        'add-user-component': AddUserComponent,
        'edit-user-component': EditUserComponent,
    }
}