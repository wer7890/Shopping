let langFont = {
    'loginFormat': 'The account number and password cannot contain non-English and numbers and should be between 6 and 16 in length',
    'loginFailed': 'Account password is wrong',

    'adminSystem': 'Administrator system',
    'memberSystem': 'Member system',
    'productSystem': 'Product systemc',
    'orderSystem': 'Order system',
    'defaultAccount': 'Account: ',
    'signOut': 'Sign out',

    'title1Default': 'Welcome to the backend system',

    'titleUser': 'Administrator Account',
    'addUser': 'Add Administrator',
    'userId': 'Administrator ID',
    'account': 'Account',
    'roles': 'Roles',
    'edit': 'Edit',
    'del': 'Delete',
    'superAdmin': 'Super Administrator',
    'memberAdmin': 'Member Administrator',
    'productAdmin': 'Product Administrator',
    'duplicateLogin': 'Duplicate login detected, logged out',
    'accessDenied': 'Insufficient permissions',
    'delConfirmation': 'Are you sure you want to delete this user?',
    'delSuccessful': 'Deletion successful',
    'delFailed': 'Deletion failed',
    'editFailed': 'Edit failed',
    'errorLog': 'An internal error occurred, please see the log',
    'changeSuccessful': 'Identity changed successfully',
    'changeFailed': 'Identity change failed',
    'ajaxError': 'AJAX Error',

    'titleAddUser': 'Add User',
    'pwd': 'Password',
    'addFormat': 'The account number and password cannot contain non-English and numbers and the length should be between 6 and 16 and the role cannot be empty.',
    'addSuccessful': 'Addition successful',
    'duplicateAccount': 'Duplicate account',
    'addSpecialChar': 'The account number and password cannot contain non-English and numbers and should be between 6 and 16 in length.',

    'titleEditUser': 'Edit User',
    'mistake': 'Mistake',
    'editFormat': 'The password cannot contain non-English and numbers and should be between 6 and 16 in length.',
    'editSuccessful': 'Successfully modified',
    'editFail': 'fail to edit',

    'titleMember': 'Member Account',
    'addMember': 'Add Member',
    'name': 'Name',
    'level': 'Level',
    'phoneNumber': 'Phone Number',
    'accountStatus': 'Status',
    'wallet': 'Wallet',
    'totalSpent': 'Total Spent',
    'inputError': 'Input value format incorrect',
    'addFailed': 'Addition failed',
    'level0': 'Level 0',
    'level1': 'Level 1',
    'level2': 'Level 2',
    'level3': 'Level 3',
    'editStateSuccessful': 'Account status changed successfully',
    'editStateFailed': 'Failed to change account status',
    'editLevelSuccessful': 'Level changed successfully',
    'editLevelFailed': 'Failed to change level',

    'titleOrder': 'Orders',
    'all': 'All',
    'orderId': 'Order ID',
    'serialNumber': 'Serial Number',
    'total': 'Total Amount',
    'shipping': 'Shipping',
    'shipped': 'Shipped',
    'arrived': 'Arrived',
    'received': 'Received',
    'returning': 'Returning',
    'returned': 'Returned',
    'orderStatus': 'Order Status',
    'paid': 'Paid',
    'return': 'Return Requested',
    'refunding': 'Refunding',
    'refunded': 'Refunded',
    'deliveryStatus': 'Delivery Status',
    'deliveryMethod': 'Delivery Method',
    'details': 'Details',
    'accept': 'Accept',
    'reject': 'Reject',
    'productName': 'Product Name',
    'productPrice': 'Product Price',
    'productType': 'Product Type',
    'quantity': 'Quantity',
    'subtotal': 'Subtotal',
    'closure': 'Close',
    'editOrderFailed': 'Insufficient stock or error occurred during update',
    'orderSure': 'Are you sure',

    'titleAddProduct': 'Add Product',
    'productNameTW': 'Product Chinese Name',
    'productNameEN': 'Product English Name',
    'minorCategories': 'Subtype',
    'chooseType': 'Select a type first',
    'productImg': 'Product Image',
    'productStock': 'Stock',
    'isOpen': 'Is Open',
    'no': 'No',
    'yes': 'Yes',
    'productIntroduceTW': 'Product Chinese Description',
    'productIntroduceEN': 'Product English Description',
    'addProduct': 'Add Product',
    'confirmAdd': 'Are you sure you want to add this product?',
    'productNameIimit': 'Product name length must be between 1 and 40 characters',
    'productNameENIimit': 'Product English name length must be between 1 and 100 characters and cannot contain Chinese characters',
    'productImgIimit': 'Please select an image file between 1 and 100 files and cannot contain Chinese characters',
    'productCategoryIimit': 'Product category and openness must be filled',
    'productIntroduceIimit': 'Product description length must be between 1 and 500 characters',
    'productIntroduceENIimit': 'Product English description length must be between 1 and 500 characters and cannot contain Chinese characters',
    'productPriceIimit': 'Price, inventory and warning value can only be numbers and must be filled in',
    'imgSize': 'Image size exceeds limit (maximum 500KB)',
    'brand': 'Brand',
    'selectMajorCategories': 'Select a product type',

    'editProduct': 'Edit Product',
    'productId': 'Product ID',
    'createdTime': 'Creation Time',
    'createdUser': 'Creator ID',
    'addStock': 'Increase Stock',
    'reduceStock': 'Decrease Stock',
    'stockIimit': 'Stock cannot be less than 0',

    'product': 'Product',
    'productNameSelect': 'Product Name Search',
    'select': 'Search',
    'addProduct': 'Add Product',
    'id': 'ID',
    'categories': 'Categories',
    'price': 'Price',
    'stock': 'Stock',
    'open': 'Open',
    'introduce': 'Description',
    'img': 'Image',
    'editOne': 'Edit',
    'delOne': 'Delete',
    'noData': 'No data',
    'confirmEditProduct': 'Are you sure you want to delete this product?',

    'firstPage': 'first',
    'lastPage': 'last',
    'totalPageFirst': 'total',
    'totalPageLast': '',
    'pageNumFirst': 'page',
    'pageNumLast': '',
    'recordNum': 'record',
    'page': 'page',

    'validationException': 'Permission verification exception',
    'lowStock': 'Stock quantity is less than 100',
    'pageUpdata': 'Changes in the number of data pages',

    'titleLogin': 'Login Page',
    'txbAccount': 'Please enter account',
    'txbPassword': 'Please enter password',
    'rememberAccount': 'Remember account',
    'btnLogin': 'Login',
};

// 商品大分類
let majorCategories = {
    "10": "Hats",
    "11": "Tops",
    "12": "Outerwear",
    "13": "Bottoms"
};

// 商品小分類
let minorCategories = {
    "0": {
        "0": "Please select a type first"
    },
    "10": {
        "00": "All",
        "01": "Others",
        "02": "Baseball Caps",
        "03": "Fisherman Hats",
        "04": "Sun Hats"
    },
    "11": {
        "00": "All",
        "01": "Others",
        "02": "Shirts",
        "03": "Sweaters",
        "04": "T-Shirts"
    },
    "12": {
        "00": "All",
        "01": "Others",
        "02": "Leather Jackets",
        "03": "Windbreakers",
        "04": "Denim Jackets"
    },
    "13": {
        "00": "All",
        "01": "Others",
        "02": "Athletic Pants",
        "03": "Casual Pants",
        "04": "Dress Pants"
    }
};


// 商品品牌分類
let brand = {
    "00": "All",
    "01": "Other",
    "02": "NIKE",
    "03": "FILA",
    "04": "ADIDAS",
    "05": "PUMA"
}

// 訂單狀態
let orderStatus = {
    "1": { name: "Paid", color: "bg-white", text: "text-dark" },
    "2": { name: "Return", color: "bg-success", text: "text-white" },
    "3": { name: "Refunding", color: "bg-warning", text: "text-white" },
    "4": { name: "Refunded", color: "bg-white", text: "text-dark" }
};

// 配送狀態
let deliveryStatus = {
    "1": { name: "Shipping", color: "bg-warning", text: "text-white" },
    "2": { name: "Shipped", color: "bg-success", text: "text-white" },
    "3": { name: "Arrived", color: "bg-white", text: "text-dark" },
    "4": { name: "Received", color: "bg-white", text: "text-dark" },
    "5": { name: "Returning", color: "bg-warning", text: "text-white" },
    "6": { name: "Returned", color: "bg-white", text: "text-dark" }
};

// 配送方式
let deliveryMethod = {
    "1": "Store Pickup",
    "2": "Store to Store",
    "3": "Home Delivery"
};