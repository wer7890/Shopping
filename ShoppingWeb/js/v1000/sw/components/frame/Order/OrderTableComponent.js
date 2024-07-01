var OrderTableComponent = {
    template: `
        <table class="table table-striped table-hover">
            <thead>
                <tr>
                    <th v-for="data in theadData" :key="data.id" v-text="data.name"></th>
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
        theadData: Array,
        dataArray: [Array, String],
    },
    data: function () {
        return {

        }
    },
}