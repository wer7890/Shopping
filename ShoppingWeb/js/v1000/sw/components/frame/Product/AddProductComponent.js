var AddProductComponent = {
    template: `
        <div class="container">
            <h2 class="text-center">${langFont['addProduct']}</h2>
            <br />
            <div class="row">
                <div class="mx-auto col-12 col-md-7">
                    <label class="form-label">${langFont['productNameTW']}</label>
                    <input v-model="nameTW" type="text" class="form-control" />
                </div>
                <div class="mx-auto col-12 col-md-7 mt-1">
                    <label class="form-label">${langFont['productNameEN']}</label>
                    <input v-model="nameEN" type="text" class="form-control" />
                </div>
                            
                <div class="row mx-auto col-12 col-md-7 mt-3">
                    <div class="col px-0">
                        <label class="form-label">${langFont['productType']}</label>
                        <select v-model="mainCategoryNum" class="form-select">
                            <option v-for="data in mainCategories" :key="data.id" :value="data.value" v-text="data.name"></option>
                        </select>
                    </div>
                    <div class="col">
                        <label class="form-label">${langFont['minorCategories']}</label>
                        <select v-model="smallCategoryNum" class="form-select">
                            <option v-for="data in smallCategories[mainCategoryNum]" :key="data.id" :value="data.value" v-text="data.name"></option>
                        </select>
                    </div>
                    <div class="col px-0">
                        <label class="form-label">${langFont['brand']}</label>
                        <select v-model="brandNum" class="form-select">
                            <option v-for="data in brand" :key="data.id" :value="data.value" v-text="data.name"></option>
                        </select>
                    </div>
                </div>

                <div class="mx-auto col-12 col-md-7 mt-1">
                    <label class="form-label">${langFont['productImg']}</label>
                    <input @change="HandleFiles" type="file" class="form-control" />
                </div>
                <div class="mx-auto col-12 col-md-7 mt-1">
                    <label class="form-label">${langFont['price']}</label>
                    <input v-model="price" type="number" class="form-control" />
                </div>
                <div class="mx-auto col-12 col-md-7 mt-1">
                    <label class="form-label">${langFont['stock']}</label>
                    <input v-model="stock" type="number" class="form-control" />
                </div>
                <div class="mx-auto col-12 col-md-7 mt-1">
                    <label class="form-label">${langFont['warningValue']}</label>
                    <input v-model="warning" type="number" class="form-control" />
                </div>
                <div class="mx-auto col-12 col-md-7 mt-1">
                    <label class="form-label">${langFont['isOpen']}</label>
                    <select v-model="isOpen" class="form-select">
                        <option value="0">${langFont['no']}</option>
                        <option value="1">${langFont['yes']}</option>
                    </select>
                </div>

                <div class="mx-auto col-12 col-md-7 mt-1">
                    <label class="form-label">${langFont['productIntroduceTW']}</label>
                    <textarea v-model="introduceTW" rows="3" class="form-control"></textarea>
                </div>
                <div class="mx-auto col-12 col-md-7 mt-1">
                    <label class="form-label">${langFont['productIntroduceEN']}</label>
                    <textarea v-model="introduceEN" rows="3" class="form-control"></textarea>
                </div>

                <button @click="AddProduct" class="btn btn-outline-primary mx-auto mt-4 col-12 col-md-6">${langFont['addProduct']}</button>
            </div>
            <br />
            <div class="row">
                <label v-text="message" class="col-12 col-sm-12 text-center text-success"></label>
            </div>
        </div>
    `,
    data: function () {
        return {
            message: '',
            mainCategories: [
                { id: 1, value: '0', name: langFont['selectMajorCategories'] },
                { id: 2, value: '10', name: langFont['hats'] },
                { id: 3, value: '11', name: langFont['tops'] },
                { id: 4, value: '12', name: langFont['outerwear'] },
                { id: 5, value: '13', name: langFont['bottoms'] },
            ],
            smallCategories: {
                '0': [
                    { id: 1, value: '0', name: langFont['chooseType'] },
                ],
                '10': [
                    { id: 2, value: '01', name: langFont['other'] },
                    { id: 3, value: '02', name: langFont['baseballCaps'] },
                    { id: 4, value: '03', name: langFont['fishermanHats'] },
                    { id: 5, value: '04', name: langFont['sunHats'] },
                ],
                '11': [
                    { id: 2, value: '01', name: langFont['other'] },
                    { id: 3, value: '02', name: langFont['shirts'] },
                    { id: 4, value: '03', name: langFont['sweaters'] },
                    { id: 5, value: '04', name: langFont['tShirts'] },
                ],
                '12': [
                    { id: 2, value: '01', name: langFont['other'] },
                    { id: 3, value: '02', name: langFont['leatherJackets'] },
                    { id: 4, value: '03', name: langFont['windbreakers'] },
                    { id: 5, value: '04', name: langFont['denimJackets'] },
                ],
                '13': [
                    { id: 2, value: '01', name: langFont['other'] },
                    { id: 3, value: '02', name: langFont['athleticPants'] },
                    { id: 4, value: '03', name: langFont['casualPants'] },
                    { id: 5, value: '04', name: langFont['dressPants'] },
                ]
            },
            brand: [
                { id: 2, value: '01', name: langFont['other'] },
                { id: 3, value: '02', name: langFont['nike'] },
                { id: 4, value: '03', name: langFont['fila'] },
                { id: 5, value: '04', name: langFont['adidas'] },
                { id: 6, value: '04', name: langFont['puma'] },
            ],
            mainCategoryNum: '0',  //所選取的大分類
            smallCategoryNum: '0', //所選取的小分類
            brandNum: '01',         //所選取的品牌
            nameTW: '',
            nameEN: '',
            img: '',
            price: '',
            stock: '',
            warning: '',
            isOpen: '0',
            introduceTW: '',
            introduceEN: '',
            file: ''
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
    methods: {
        //新增商品
        AddProduct: function () {

            if (!this.IsSpecialChar()) {
                return;
            }
            
            var yes = confirm(langFont["confirmAdd"]);
            if (yes == true) {
                var productNum = this.mainCategoryNum + this.smallCategoryNum + this.brandNum;

                if (!this.CheckFileSize()) {
                    this.message = langFont["inputError"];
                    return;
                }

                // 建立 FormData 物件來儲存檔案資料 
                var formData = new FormData();
                // 將檔案加入到 FormData 物件中
                formData.append("file", this.file);
                formData.append("productName", this.nameTW);
                formData.append("productNameEN", this.nameEN);
                formData.append("productCategory", productNum);
                formData.append("productPrice", this.price);
                formData.append("productStock", this.stock);
                formData.append("productIsOpen", this.isOpen);
                formData.append("productIntroduce", this.introduceTW);
                formData.append("productIntroduceEN", this.introduceEN);
                formData.append("productStockWarning", this.warning);

                var self = this;
                $.ajax({
                    url: "/api/Controller/product/UploadProduct",
                    type: "POST",
                    data: formData,
                    contentType: false,
                    processData: false,
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
                                alert(langFont["addSuccessful"]);
                                self.$emit('Updata');
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
            }
        },

        // 提取圖片名稱名
        HandleFiles: function (element) {
            this.img = element.target.files[0].name;
            this.file = element.target.files[0];
        },

        //判斷文字長度 
        IsSpecialChar: function () {
            if (!this.mainCategoryNum || !this.smallCategoryNum || !this.brandNum || !this.nameTW || !this.nameEN || !this.img || !this.price || !this.stock || !this.warning || !this.isOpen || !this.introduceTW || !this.introduceEN) {
                this.message = langFont["inputError"];
                return false;
            }

            if (!/^.{1,40}$/.test(this.nameTW)) {
                this.message = langFont["productNameIimit"];
                return false;
            }

            if (!/^[^\u4e00-\u9fa5]{1,100}$/.test(this.nameEN)) {
                this.message = langFont["productNameENIimit"];
                return false;
            }

            if (!/(\.jpg|\.png)$/i.test(this.img)) {
                this.message = langFont["productImgIimit"];
                $("#labAddProduct").text(langFont["productImgIimit"]);
                return false;
            }

            if (!/.{2,}/.test(this.mainCategoryNum) || !/.{2,}/.test(this.smallCategoryNum) || !/.{2,}/.test(this.brandNum) || !/.{1,}/.test(this.isOpen)) {
                this.message = langFont["productCategoryIimit"];
                return false;
            }

            if (!/^.{1,500}$/.test(this.introduceTW)) {
                this.message = langFont["productIntroduceIimit"];
                return false;
            }

            if (!/^[^\u4e00-\u9fa5]{1,1000}$/.test(this.introduceEN)) {
                this.message = langFont["productIntroduceENIimit"];
                return false;
            }

            if (!/^[0-9]{1,7}$/.test(this.price) || !/^[0-9]{1,7}$/.test(this.stock) || !/^[0-9]{1,7}$/.test(this.warning)) {
                this.message = langFont["productPriceIimit"];
                return false;
            }

            return true;
        },

        //判斷圖片大小
        CheckFileSize: function () {
            if (!this.file) {
                return false;
            }

            // 檢查圖片大小
            var maxSizeInBytes = 500 * 1024; // 500KB
            if (this.file.size > maxSizeInBytes) {
                this.message = langFont["imgSize"];
                return false;
            }
            return true;
        }
    },
}