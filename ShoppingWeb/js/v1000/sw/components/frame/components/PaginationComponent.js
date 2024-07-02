var PaginationComponent = {
    template: `
        <div v-if="total > 0">
            <div :class="directType ? 'pagination-container-direct' : 'pagination-container'">
                
                <div v-if="showPagesTotal" class="pagination-info">
                    <span class="pagination-total" v-text="'Pages: ' + currentPage + '/' + totalPages"></span>
                </div>

                <ul :class="directType ? 'pagination-direct' : 'pagination'">
                    <li v-if="showFirstLastButtons && (total > showButtons) && (currentPage > 1)" @click="First" :class="directType ? 'page-item-direct' : 'page-item'">
                        <span class="page-link">|<</span>
                    </li>

                    <li v-if="showPrevNext && (total > showButtons) && (currentPage > 1)" @click="Prev" :class="directType ? 'page-item-direct' : 'page-item'">
                        <span class="page-link"><</span>
                    </li>

                    <li v-for="page in pagesToShow" :key="page" @click="Page(page)" :class="[directType ? 'page-item-direct' : 'page-item', { active: currentPage === page }]">
                        <a class="page-link" href="javascript:;" v-text="page"></a>
                    </li>

                    <li v-if="showPrevNext && (total > showButtons) && (currentPage < total)" @click="Next" :class="directType ? 'page-item-direct' : 'page-item'">
                        <span class="page-link">></span>
                    </li>

                    <li v-if="showFirstLastButtons && (total > showButtons) && (currentPage < total)" @click="Last" :class="directType ? 'page-item-direct' : 'page-item'">
                        <span class="page-link">>|</span>
                    </li>
                </ul>

                <div v-if="showGoInput" :class="directType ? 'pagination-go-direct' : 'pagination-go'">
                    <input type="text" v-model.number="inputPage" class="page-input" @keyup.enter="Page(inputPage)" />
                    <button class="go-btn" @click="Page(inputPage)">GO</button>
                </div>

            </div>
        </div>
  `,
    props: {
        size: Number,
        total: Number
    },
    data: function () {
        return {
            currentPage: 1,
            showButtons: 5,  //需要顯示的按鈕數量
            showPrevNext: true,  //是否顯示上下頁按鈕
            showFirstLastButtons: true,  //是否顯示首頁和末頁按鈕
            showGoInput: true,  //是否顯示跳轉頁面輸入框和按鈕
            showPagesTotal: true,  //是否顯示總頁數
            directType: false,  //是否為直式 
            inputPage: ''
        };
    },
    computed: {
        totalPages: function () {
            this.currentPage = 1;
            return this.total;
        },
        pagesToShow: function () {
            var pages = [];
            var startPage = Math.max(1, this.currentPage - Math.floor(this.showButtons / 2));
            var endPage = Math.min(this.totalPages, startPage + this.showButtons - 1);
            for (var i = startPage; i <= endPage; i++) {
                pages.push(i);
            }
            return pages;
        }
    },
    watch: {
        total: function (newTotal) {
            if (this.currentPage > newTotal) {
                this.currentPage = newTotal;
            }
        }
    },
    methods: {
        Page: function (pageIndex) {
            if (pageIndex >= 1 && pageIndex <= this.totalPages) {
                this.currentPage = pageIndex;
                this.$emit('Choose', pageIndex, this.size);
            }
        },
        First: function () {
            this.Page(1);
        },
        Prev: function () {
            if (this.currentPage > 1) {
                this.Page(this.currentPage - 1);
            }
        },
        Next: function () {
            if (this.currentPage < this.totalPages) {
                this.Page(this.currentPage + 1);
            }
        },
        Last: function () {
            this.Page(this.totalPages);
        }
    }
}