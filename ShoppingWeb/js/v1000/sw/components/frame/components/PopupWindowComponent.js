var PopupWindowComponent = {
    template: `
        <div class="outerMask" v-if="showMask">
            <div class="innerMask">
                <slot></slot>
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
        }
    },
    methods: {
        Show: function () {
            this.showMask = true;
        },
        Closure: function () {
            this.showMask = false;
        }
    },
    created: function () {  
        this.$bus.$on('User:ShowAddUser', this.Show);
        this.$bus.$on('User:ShowEditUser', this.Show);
    },
    beforeDestroy: function () {  
        this.$bus.$off('User:ShowAddUser', this.Show);
        this.$bus.$off('User:ShowEditUser', this.Show);
    },
    components: {
        'add-user-component': AddUserComponent,
        'edit-user-component': EditUserComponent,
    }
}