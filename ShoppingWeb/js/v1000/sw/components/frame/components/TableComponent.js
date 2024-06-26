var TableComponent = {
    template: `
        <table class="table table-striped table-hover table-bordered ">
            <thead>
                <tr>
                    <th @click="Sort" v-for="data in theadData" :key="data.id" v-text="data.name"></th>
                </tr>
            </thead>
            <tbody id="tableBody">
                <tr v-for="data in dataArray" :key="data.Id">
                    <slot name="table-row" :data="data"></slot>
                </tr>
            </tbody>
        </table>
    `,
    props: {
        theadData: {
            type: Array,
            required: true
        },
        dataArray: {
            type: [Array, String],
            required: true
        }
    },
    methods: {
        Sort: function () {
            this.$bus.$emit('Table:Sort');
        }
    },
}