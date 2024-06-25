var TableComponent = {
    template: `
        <table class="table table-striped table-hover ">
            <thead>
                <tr>
                    <th @click="TableDataSort" v-for="data in tableTheadData" :key="data.id" v-text="data.name"></th>
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
        tableTheadData: {
            type: Array,
            required: true
        },
        dataArray: {
            type: [Array, String],
            required: true
        }
    },
    methods: {
        TableDataSort: function () {
            this.$bus.$emit('Table:Sort');
        }
    },
}