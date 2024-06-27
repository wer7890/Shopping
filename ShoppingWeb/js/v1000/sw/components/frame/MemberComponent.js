var MemberComponent = {
    template: `
        <div class="w-auto mx-3">
            <div class="row">
                <div class="col-10 d-flex justify-content-center">
                    <h2 class="text-center">${langFont['titleMember']}</h2>
                </div>
                <div class="col-2 d-flex justify-content-center">
                    <button @click="AddMember" type="submit" class="btn btn-outline-primary">${langFont['addMember']}</button>
                </div>
            </div>
            <br />

            <div class="row">
                <table-component :theadData="theadData" :dataArray="dataArray">
                    <template v-slot:table-row="{ data }">
                        <td v-text="data.Id"></td>
                        <td v-text="data.Account"></td>
                        <td v-text="data.Pwd"></td>
                        <td v-text="data.Name"></td>
                        <td>
                            <select v-model="data.Level" @change="EditMemberLevel(data.Id, data.Level)" class="form-select form-select-sm f_roles">
                                <option v-for="data in levelArray" :key="data.value" :value="data.value" v-text="data.name"></option>
                            </select>
                        </td>
                        <td v-text="data.PhoneNumber"></td>
                        <td>
                            <div class="form-check form-switch">
                                <input v-model="data.AccountStatus" @change="EditMemberStatus(data.Id)" type="checkbox" class="toggle-switch form-check-input">
                            </div>
                        </td>
                        <td v-text="data.Amount"></td>
                        <td v-text="data.TotalSpent"></td>
                    </template>
                 </table-component>
            </div>

            <pagination-component @Choose="GetAllMemberData" :size="pageSize" :total="pagesTotal"></pagination-component>

            <div class="row">
                <span v-text="message" class="col-12 col-sm-12 text-center text-success"></span>
            </div>
        </div>
    `,
    data: function () {
        return {
            message: '',
            //table的thead中的資料
            theadData: [
                { id: 1, name: langFont['id'] },
                { id: 2, name: langFont['account'] },
                { id: 3, name: langFont['pwd'] },
                { id: 4, name: langFont['name'] },
                { id: 5, name: langFont['level'] },
                { id: 6, name: langFont['phoneNumber'] },
                { id: 7, name: langFont['accountStatus'] },
                { id: 8, name: langFont['wallet'] },
                { id: 9, name: langFont['totalSpent'] },
            ],
            //table的tbody中的資料下拉選單
            levelArray: [
                { value: 0, name: langFont["level0"] },
                { value: 1, name: langFont['level1'] },
                { value: 2, name: langFont['level2'] },
                { value: 3, name: langFont['level3'] },
            ],
            dataArray: '',
            pageSize: 5,
            pagesTotal: null,
            beforePagesTotal: 1,
            numeric: "0123456789",
            lowerCase: "abcdefghijklmnopqrstuvwxyz",
            upperCase: "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            emailSuffix: ["@hotmail.com", "@yahoo.com", "@gmail.com", "@live.com", "@qq.com"],
        }
    },
    methods: {
        //搜尋全部管理員資料
        GetAllMemberData: function (pageNumber, pageSize) {
            if (!pageNumber || !pageSize || !this.beforePagesTotal) {
                this.message = "undefined";
                return;
            }

            var self = this;

            $.ajax({
                url: '/api/Controller/member/GetAllMemberData',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ pageNumber: pageNumber, pageSize: pageSize, beforePagesTotal: self.beforePagesTotal }),
                success: function (response) {
                    switch (response.Status) {
                        case 0:
                            alert(langFont["duplicateLogin"]);
                            window.parent.location.href = "Login.aspx";
                            break;
                        case 1:
                            alert(langFont["accessDenied"]);
                            parent.location.reload();
                            break;
                        case 2:
                            self.message = langFont["inputError"];
                            break;
                        case 100:
                            self.dataArray = response.MemberDataList;
                            self.pagesTotal = response.TotalPages;
                            self.beforePagesTotal = self.pagesTotal;
                            break;
                        case 101:
                            self.message = langFont["noData"];
                            break;
                        default:
                            self.message = langFont["errorLog"];
                    }
                },
                error: function (error) {
                    self.message = langFont["ajaxError"];
                }
            });
        },

        //新增會員
        AddMember: function () {
            var self = this;

            $.ajax({
                type: "POST",
                url: "/api/Controller/member/AddMember",  // 這裡指定後端方法的位置 
                data: JSON.stringify({ account: this.GetRandomStr(), pwd: this.GetRandomStr(), name: this.GetRandomName(), birthday: this.GetRandomDate(), phone: this.GetRandomPhone(), email: this.GetRandomEmail(), address: "台中市" }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    switch (response.Status) {
                        case 0:
                            alert(langFont["duplicateLogin"]);
                            window.parent.location.href = "Login.aspx";
                            break;
                        case 1:
                            alert(langFont["accessDenied"]);
                            parent.location.reload();
                            break;
                        case 2:
                            self.message = langFont["inputError"];
                            break;
                        case 100:
                            self.message = langFont["addSuccessful"];
                            break;
                        case 101:
                            self.message = langFont["addFailed"];
                            break;
                        default:
                            self.message = langFont["errorLog"];
                    }
                },
                error: function (error) {
                    self.message = langFont["ajaxError"];
                }
            });
        },

        //更改會員等級
        EditMemberLevel: function (memberId, level) {
            if (!memberId || !level) {
                this.message = "undefined";
                return;
            }

            var self = this;

            $.ajax({
                type: "POST",
                url: "/api/Controller/member/EditMemberLevel",
                data: JSON.stringify({ memberId: memberId, level: level }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    switch (response.Status) {
                        case 0:
                            alert(langFont["duplicateLogin"]);
                            window.parent.location.href = "Login.aspx";
                            break;
                        case 1:
                            alert(langFont["accessDenied"]);
                            parent.location.reload();
                            break;
                        case 2:
                            self.message = langFont["inputError"];
                            break;
                        case 100:
                            self.message = langFont["editLevelSuccessful"];
                            break;
                        case 101:
                            self.message = langFont["editLevelFailed"];
                            break;
                        default:
                            self.message = langFont["errorLog"];
                    }
                },
                error: function (error) {
                    self.message = langFont["ajaxError"];
                }
            });
        },

        //更改會員狀態
        EditMemberStatus: function (memberId) {
            if (!memberId) {
                this.message = "undefined";
                return;
            }

            var self = this;

            $.ajax({
                type: "POST",
                url: "/api/Controller/member/EditMemberStatus",
                data: JSON.stringify({ memberId: memberId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    switch (response.Status) {
                        case 0:
                            alert(langFont["duplicateLogin"]);
                            window.parent.location.href = "Login.aspx";
                            break;
                        case 1:
                            alert(langFont["accessDenied"]);
                            parent.location.reload();
                            break;
                        case 2:
                            self.message = langFont["inputError"];
                            break;
                        case 100:
                            self.message = langFont["editStateSuccessful"];
                            break;
                        case 101:
                            self.message = langFont["editStateFailed"];
                            break;
                        default:
                            self.message = langFont["errorLog"];
                    }
                },
                error: function (error) {
                    self.message = langFont["ajaxError"];
                }
            });
        },


        //隨機生成(min,max)範圍的數字
        RandomInt: function (min, max) {
            return min + Math.floor(Math.random() * (max - min + 1));
        },

        //隨機從list取值生成一段長度為len字符序列
        RandomSequence: function (len, list) {
            if (len <= 1) {
                len = 1;
            }
            var s = "";
            var n = list.length;
            if (typeof list === "string") {
                while (len-- > 0) {
                    s += list.charAt(Math.random() * n)
                }
            } else if (list instanceof Array) {
                while (len-- > 0) {
                    s += list[Math.floor(Math.random() * n)]
                }
            }
            return s;
        },

        //隨機生成帳號密碼
        GetRandomStr: function () {
            var opt = this.numeric + this.lowerCase + this.upperCase;
            return this.RandomSequence(this.RandomInt(6, 16), opt);
        },

        //隨機生成中文姓名，使用Unicode(萬國碼)編碼
        GetRandomName: function () {
            function RandomAccess(min, max) {
                return Math.floor(Math.random() * (min - max) + max)
            }
            var name = ""
            for (let i = 0; i < 3; i++) {
                name += String.fromCharCode(RandomAccess(0x4E00, 0x9FFF))
            }
            return name
        },

        //隨機生成日期
        GetRandomDate: function () {
            var min = new Date(0);  //1970-01-01
            var max = new Date()
            var res = new Date(Math.random() * (max.getTime() - min.getTime()));
            return res.getFullYear() + "-" + (res.getMonth() + 1) + "-" + res.getDate();
        },

        //隨機生成手機號碼
        GetRandomPhone: function () {
            return "09" + this.RandomSequence(8, this.numeric);
        },

        //隨機生成信箱
        GetRandomEmail: function () {
            var opt = this.numeric + this.lowerCase + this.upperCase
            return this.RandomSequence(this.RandomInt(4, 10), opt) + this.RandomSequence(1, this.emailSuffix);
        }
    },
    watch: {
        message: function () {
            var self = this;
            setTimeout(function () {
                self.message = '';
            }, 3000);
        }
    },
    mounted: function () {  //掛載後
        this.GetAllMemberData(1, this.pageSize);
    },
    components: {
        'pagination-component': PaginationComponent,
        'table-component': TableComponent,
    }
};