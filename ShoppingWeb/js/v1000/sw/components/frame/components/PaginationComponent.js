var PaginationComponent = {
    template: `
        <div id="pagination"></div>
    `,
    data: function () {
        return {
            page: '',
        }
    },
    methods: {
        SetPagination: function (pageSize, pagesTotal) {
            var self = this;
            
            this.page = new Pagination({
                id: 'pagination',
                total: pagesTotal,
                showButtons: 5,
                showFirstLastButtons: true,
                showGoInput: true,
                showPagesTotal: true,
                callback: function (pageIndex) {
                    self.$bus.$emit("Pagination:Choose", pageIndex + 1, pageSize);
                }
            });
        },

        UpdataPagination: function (pagesTotal){
            this.page.Update(pagesTotal);
        }
    },
    created: function () {
        this.$bus.$on('User:PaginationSet', this.SetPagination);
        this.$bus.$on('User:PaginationUpdata', this.UpdataPagination);
        this.$bus.$on('Member:PaginationSet', this.SetPagination);
        this.$bus.$on('Member:PaginationUpdata', this.UpdataPagination);
        this.$bus.$on('Product:PaginationSet', this.SetPagination);
        this.$bus.$on('Product:PaginationUpdata', this.UpdataPagination);
    },
    beforeDestroy: function () {  //銷毀前
        this.$bus.$off('User:PaginationSet', this.SetPagination);
        this.$bus.$off('User:PaginationUpdata', this.UpdataPagination);
        this.$bus.$off('Member:PaginationSet', this.SetPagination);
        this.$bus.$off('Member:PaginationUpdata', this.UpdataPagination);
        this.$bus.$off('Product:PaginationSet', this.SetPagination);
        this.$bus.$off('Product:PaginationUpdata', this.UpdataPagination);
    },
}