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
                    self.$emit("Choose", pageIndex + 1, pageSize);
                }
            });
        },

        UpdataPagination: function (pagesTotal){
            this.page.Update(pagesTotal);
        }
    },
    created: function () {
        this.$bus.$on('Pagination:Set', this.SetPagination);
        this.$bus.$on('Pagination:Updata', this.UpdataPagination);
    },
    beforeDestroy: function () {  //銷毀前
        this.$bus.$off('Pagination:Set', this.SetPagination);
        this.$bus.$off('Pagination:Updata', this.UpdataPagination);
    },
}