var PopWindowComponent = {
    name: 'PopWindowComponent',
    template: `
        <div class="outerMask" v-if="show">
            <div class="innerMask">
                <button @click="Clear" class="btn btn-outline-secondary mx-auto mt-4 position-absolute top-0 end-0">${langFont['closure']}</button>
                <slot name="content" :page="page"></slot>
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