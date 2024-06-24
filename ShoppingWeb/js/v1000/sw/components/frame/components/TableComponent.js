var tableComponent = {
    template: `
        <div class="row">
            <table class="table table-striped table-hover ">
                <thead>
                    <tr>
                        <th v-for="data in tableTheadData" :key="data.id">{{ data.name }}</th>
                    </tr>
                </thead>
                <tbody id="tableBody">
                    <tr v-for="data in dataArray" :key="data.Id">
                        <td>{{ data.Id }}</td>
                        <td>{{ data.Account }}</td>
                        <td>
                            <select v-model="data.Roles" class="form-select form-select-sm f_roles">
                                <option v-for="data in rolesArray" :ket="data.value" :value="data.value">{{ data.name }}</option>
                            </select>
                        </td>
                        <td><button class="btn btn-primary">${langFont['edit']}</button></td>
                        <td><button class="btn btn-danger">${langFont['del']}</button></td>
                    </tr>
                </tbody>
            </table>
        </div>
    `,
    props: ['theadData', 'data', 'roles'],
    data: function () {
        return {
            tableTheadData: this.theadData,
            dataArray: this.data,
            rolesArray: this.roles
        }
    },
    methods: {
        SetData: function (data) {
            this.dataArray = data;
            console.log('tableTheadData: ', this.tableTheadData);
            console.log('dataArray: ', this.dataArray);
            console.log('rolesArray: ', this.rolesArray);
        }
    },
    created: function () {  //創建後
        this.$bus.$on('updata-table', this.SetData);
    },
    beforeDestroy: function () {  //銷毀前
        this.$bus.$off('updata-table', this.SetData);
    },
}