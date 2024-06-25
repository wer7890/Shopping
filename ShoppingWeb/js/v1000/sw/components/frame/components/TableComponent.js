﻿var tableComponent = {
    template: `
        <table class="table table-striped table-hover ">
            <thead>
                <tr>
                    <th @click="TableDataSort" v-for="data in tableTheadData" :key="data.id">{{ data.name }}</th>
                </tr>
            </thead>
            <tbody id="tableBody">
                <tr v-for="data in dataArray" :key="data.Id">
                    <slot name="table-row" :data="data"></slot>
                </tr>
            </tbody>
        </table>
    `,
    props: ['tableTheadData', 'dataArray'],
    methods: {
        TableDataSort: function () {
            this.$bus.$emit('table-data-sort');
        }
    },
}