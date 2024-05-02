﻿let langFont = {
    'adminSystem': '管理員系統',
    'memberSystem': '會員系統',
    'productSystem': '商品系統',
    'orderSystem': '訂單系統',
    'defaultAccount': '帳號: ',
    'signOut': '登出',

    'title1Default': '歡迎登入後台系統',

    'titleUser': '管理員帳號',
    'addUser': '新增管理員',
    'userId': '管理員ID',
    'account': '帳號',
    'roles': '角色',
    'edit': '更改',
    'del': '刪除',
    'superAdmin': '超級管理員',
    'memberAdmin': '會員管理員',
    'productAdmin': '商品管理員',
    'duplicateLogin': '重複登入，已被登出',
    'accessDenied': '權限不足',
    'delConfirmation': '確定要刪除該用戶嗎',
    'delFailed': '刪除失敗',
    'editFailed': '編輯失敗',
    'errorLog': '發生發生內部錯誤，請看日誌',
    'changeSuccessful': '身分更改成功',
    'changeFailed': '身分更改失敗',
    'ajaxError': 'AJAX發生錯誤',

    'titleAddUser': '新增帳號',
    'pwd': '密碼',
    'addFormat': '帳號和密碼不能含有非英文和數字且長度應在6到16之間且腳色不能為空',
    'addSuccessful': '新增成功',
    'duplicateAccount': '帳號重複',
    'addSpecialChar': '帳號和密碼不能含有非英文和數字且長度應在6到16之間',

    'titleEditUser': '修改帳號',
    'mistake': '錯誤',
    'editFormat': '密碼不能含有非英文和數字且長度應在6到16之間',
    'editSuccessful': '修改成功',
    'editFail': '修改失敗',

    'titleMember': '會員帳號',
    'addMember': '新增會員',
    'name': '名稱',
    'level': '等級',
    'phoneNumber': '電話',
    'accountStatus': '狀態',
    'wallet': '錢包',
    'totalSpent': '總花費',
    'inputError': '輸入值格式錯誤',
    'addFailed': '新增失敗',
    'level0': '等級0',
    'level1': '等級1',
    'level2': '等級2',
    'level3': '等級3',
    'editStateSuccessful': '帳號狀態更改成功',
    'editStateFailed': '帳號狀態更改失敗',
    'editLevelSuccessful': '等級更改成功',
    'editLevelFailed': '等級更改失敗',

    'shipping': '發貨中',
    'Shipped': '已發貨',
    'Arrived': '已到貨',
    'Received': '已取貨',
    'Returning': '退貨中',
    'Returned': '已退貨',
    'orderStatus': '訂單狀態',
    'paid': '已付款',
    'return': '申請退貨',
    'refunding': '退款中',
    'refunded': '已退款',
    'deliveryStatus': '配送狀態',
    'deliveryMethod': '配送方式',
    'details': '詳情',
    'accept': '接受',
    'reject': '拒絕',
    'productName': '商品名稱',
    'productPrice': '商品價格',
    'productType': '商品類型',
    'quantity': '數量',
    'subtotal': '小記',
    'closure': '關閉',
    'editOrderFailed': '庫存不足或更新時發生錯誤',
    'orderSure': '是否同意',

};

// 商品大分類
let majorCategories = {
    "10": "帽子",
    "11": "上衣",
    "12": "外套",
    "13": "褲子"
};

// 商品小分類
let minorCategories = {
    "0": {
        "0": "請先選擇類型"
    },
    "10": {
        "00": "全部",
        "01": "其他",
        "02": "棒球帽",
        "03": "漁夫帽",
        "04": "遮陽帽"
    },
    "11": {
        "00": "全部",
        "01": "其他",
        "02": "襯衫",
        "03": "毛衣",
        "04": "帽T"
    },
    "12": {
        "00": "全部",
        "01": "其他",
        "02": "皮外套",
        "03": "風衣",
        "04": "牛仔外套"
    },
    "13": {
        "00": "全部",
        "01": "其他",
        "02": "運動褲",
        "03": "休閒褲",
        "04": "西褲"
    }
};

// 商品品牌分類
let brand = {
    "00": "全部",
    "01": "其他",
    "02": "NIKE",
    "03": "FILA",
    "04": "ADIDAS",
    "05": "PUMA"
}


// 訂單狀態
let orderStatus = {
    "1": { name: "已付款", color: "bg-white", text: "text-dark" },
    "2": { name: "申請退貨", color: "bg-success", text: "text-white" },
    "3": { name: "退款中", color: "bg-warning", text: "text-white" },
    "4": { name: "已退款", color: "bg-white", text: "text-dark" }
};

// 配送狀態
let deliveryStatus = {
    "1": { name: "發貨中", color: "bg-warning", text: "text-white" },
    "2": { name: "已發貨", color: "bg-success", text: "text-white" },
    "3": { name: "已到貨", color: "bg-white", text: "text-dark" },
    "4": { name: "已取貨", color: "bg-white", text: "text-dark" },
    "5": { name: "退貨中", color: "bg-warning", text: "text-white" },
    "6": { name: "已退貨", color: "bg-white", text: "text-dark" }
};

// 配送方式
let deliveryMethod = {
    "1": "超商取貨",
    "2": "店到店",
    "3": "宅配"
};