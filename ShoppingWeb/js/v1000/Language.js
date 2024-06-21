// 語言包
const messages = {
    TW: {
        message: {
            titleLogin: '登入頁面',
            account: '帳號',
            txbAccount: '請輸入帳號',
            pwd: '密碼',
            txbPassword: '請輸入密碼',
            rememberAccount: '記住帳號',
            btnLogin: '登入',
            loginFormat: '帳號和密碼不能含有非英文和數字且長度應在6到16之間',
            loginFailed: '帳號密碼錯誤',
            errorLog: '發生發生內部錯誤，請看日誌',
            ajaxError: 'AJAX發生錯誤',
        }
    },
    EN: {
        message: {
            titleLogin: 'Login Page',
            account: 'Account',
            txbAccount: 'Please enter account',
            pwd: 'Password',
            txbPassword: 'Please enter password',
            rememberAccount: 'Remember account',
            btnLogin: 'Login',
            loginFormat: 'The account number and password cannot contain non-English and numbers and should be between 6 and 16 in length',
            loginFailed: 'Account password is wrong',
            errorLog: 'An internal error occurred, please see the log',
            ajaxError: 'AJAX Error',
        }
    }
};

//創建 VueI18n 實例
let i18n = new VueI18n({
    locale: GetLanguageCookie("language"), // 默認語言
    messages, // 語言包
});

//取得指定的Cookie
function GetLanguageCookie(name) {
    var cookies = document.cookie.split(';');

    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i].trim();
        if (cookie.startsWith(name + '=')) {
            return cookie.substring(name.length + 1);
        }
    }
    return null;
}