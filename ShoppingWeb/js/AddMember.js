$(document).ready(function () {
    new TwCitySelector();
    $("#txbAccount").text(GetRandomStr());
    $("#txbPwd").text(GetRandomStr());
    $("#txbName").text(GetRandomName());
    $("#txbBirthday").text(GetRandomDate());
    $("#txbPhoneNumber").text(GetRandomPhone());
    $("#txbEmail").text(GetRandomEmail());
    for (let i = 0; i < 10; i++) {
        console.log(GetRandomStr(), GetRandomName(), GetRandomDate(), GetRandomPhone(), GetRandomEmail());
    }

});


const numeric = "0123456789";
const lowerCase = "abcdefghijklmnopqrstuvwxyz"
const upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

const emailSuffix = ["@gmail.com", "@yahoo.com", "@msn.com", "@hotmail.com",
    "@aol.com", "@ask.com", "@live.com", "@qq.com", "@0355.net", "@163.com",
    "@163.net", "@263.net", "@3721.net", "@yeah.net", "@126.com", "@sina.com",
    "@sohu.com", "@yahoo.com.cn"];

//隨機生成(min,max)範圍的數字
function randomInt(min, max) {
    return min + Math.floor(Math.random() * (max - min + 1))
}

//隨機從list取值生成一段長度為len字符序列
function randomSequence(len, list) {
    if (len <= 1) {
        len = 1;
    }
    let s = "";
    let n = list.length;
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
}

//隨機生成帳號密碼
function GetRandomStr() {
    let opt = numeric + lowerCase + upperCase
    return randomSequence(randomInt(6, 16), opt)
}

//隨機生成中文姓名
function GetRandomName() {
    function randomAccess(min, max) {
        return Math.floor(Math.random() * (min - max) + max)
    }
    let name = ""
    for (let i = 0; i < 3; i++) {
        name += String.fromCharCode(randomAccess(0x4E00, 0x9FA5))
    }
    return name
}

//隨機生成日期
function GetRandomDate(min, max) { // min, max
    min = min === undefined ? new Date(0) : min
    max = max === undefined ? new Date() : max
    var res = new Date(Math.random() * (max.getTime() - min.getTime()))
    return res.getFullYear() + "-" + res.getMonth() + "-" + res.getDay();
}

//隨機生成手機號碼
function GetRandomPhone() {
    return "09" + randomSequence(8, numeric);
}

//隨機生成信箱
function GetRandomEmail() {
    var opt = numeric + lowerCase + upperCase
    return randomSequence(randomInt(4, 10), opt) + randomSequence(1, emailSuffix);
}






