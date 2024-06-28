var PopWindowComponent = {
    template: `
        <div class="outerMask" v-if="show">
            <div class="innerMask">
                <slot name="content" :page="page"></slot>
                <div class="container">
                    <div class="row">
                        <button @click="Clear" class="btn btn-outline-secondary mx-auto mt-4 col-12 col-md-6">${langFont['closure']}</button>
                    </div>
                </div>
            </div>
        </div>
    `,
    data: function () {
        return {
            show: false,
            page: '',
        }
    },
    methods: {
        Set: function (val) {
            if (val) {
                this.page = val;
                this.show = true;
            } 
        },
        Clear: function () {
            this.page = '';
            this.show = false;
        },
    },
    created: function () {  
        this.$bus.$on('PopWindow:Set', this.Set);
    },
    beforeDestroy: function () {  
        this.$bus.$off('PopWindow:Set', this.Set);
    },
}