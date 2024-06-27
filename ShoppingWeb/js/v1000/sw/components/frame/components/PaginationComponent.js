var PaginationComponent = {
    template: `
        <div id="pagination"></div>
    `,
    props: {
        size: Number,
        total: Number
    },
    data: function () {
        return {
            page: '',
            initial: true
        }
    },
    watch: {
        total: function () {
            this.initial ? this.initial = false : alert(langFont['pageUpdata']);
            this.page.Update(this.total);
        }
    },
    mounted: function () {
        var self = this;

        this.page = new Pagination({
            id: 'pagination',
            total: this.total,
            showButtons: 5,
            showFirstLastButtons: true,
            showGoInput: true,
            showPagesTotal: true,
            callback: function (pageIndex) {
                self.$emit("Choose", pageIndex + 1, self.size);
            }
        });
    },
}