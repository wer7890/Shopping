var paginationComponent = {
    template: `
        <div id="pagination">
            <!-- 分頁內容 -->
        </div>
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
                    self.$bus.$emit("choose-pagination", pageIndex + 1, pageSize);
                }
            });
        },

        UpdataPagination: function (pagesTotal){
            this.page.Update(pagesTotal);
        }
    },
    created: function () {
        this.$bus.$on("set-pagination", this.SetPagination);
        this.$bus.$on("updata-pagination", this.UpdataPagination);
    },
}