$(document).ready(function () {
    window.parent.getUserPermission();
    SearchAllMember();

    //按下新增會員按鈕
    $("#btnAddMember").click(function () {
        $.ajax({
            type: "POST",
            url: "/Ajax/MemberHandler.aspx/AddMember",  // 這裡指定後端方法的位置
            data: JSON.stringify({ account: GetRandomStr(), pwd: GetRandomStr(), name: GetRandomName(), birthday: GetRandomDate(), phone: GetRandomPhone(), email: GetRandomEmail(), address: "台中市" }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d === "重複登入") {
                    alert("重複登入，已被登出");
                    window.parent.location.href = "Login.aspx";
                } else if (response.d === "權限不足") {
                    alert("權限不足");
                    parent.location.reload();
                } else if (response.d === "1") {
                    window.location.reload();
                } else if (response.d === "0") {
                    $("#labSearchMember").text("新增失敗");
                } else {
                    $("#labSearchMember").text(response.d);
                }
            },
            error: function (error) {
                console.error('AJAX Error:', error);
                $("#labSearchMember").text("發生錯誤，請查看控制台");
            }
        });      
    })

    //是否停權
    $(document).on("change", ".toggle-switch", function () {
        $("#labSearchMember").text("");
        let memberId = $(this).data('id');
        ToggleMemberStatus(memberId);
    });

    //更改等級
    $(document).on("change", ".f_level", function () {
        $("#labSearchMember").text("");
        let memberId = $(this).data('id');
        let level = $(this).val();
        ToggleMemberLevel(memberId, level);
    });

})


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
        name += String.fromCharCode(randomAccess(0x4E00, 0x9FFF))
    }
    return name
}

//隨機生成日期
function GetRandomDate() { 
    min = new Date(0);  //1970-01-01
    max = new Date()
    var res = new Date(Math.random() * (max.getTime() - min.getTime()))
    return res.getFullYear() + "-" + (res.getMonth() + 1) + "-" + res.getDate();
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


//全部會員資料
function SearchAllMember() {
    $.ajax({
        url: '/Ajax/MemberHandler.aspx/GetAllMemberData',
        type: 'POST',
        contentType: 'application/json',
        success: function (response) {
            if (response.d == "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else if (response.d === "權限不足") {
                alert("權限不足");
                parent.location.reload();
            } else {
                let data = JSON.parse(response.d); 
                let tableBody = $('#tableBody');

                tableBody.empty();

                $.each(data, function (index, item) {
                    let row = '<tr>' +
                        '<td>' + item.f_id + '</td>' +
                        '<td>' + item.f_account + '</td>' +
                        '<td>' + item.f_pwd + '</td>' +
                        '<td>' + item.f_name + '</td>' +
                        '<td>' +
                        '<select class="form-select form-select-sm f_level" data-id="' + item.f_id + '">' +
                        '<option value="0"' + (item.f_level == '0' ? ' selected' : '') + '>等級0</option>' +
                        '<option value="1"' + (item.f_level == '1' ? ' selected' : '') + '>等級1</option>' +
                        '<option value="2"' + (item.f_level == '2' ? ' selected' : '') + '>等級2</option>' +
                        '<option value="3"' + (item.f_level == '3' ? ' selected' : '') + '>等級3</option>' +
                        '</select>' +
                        '</td>' +
                        '<td>' + item.f_phoneNumber + '</td>' +
                        '<td>' + item.f_email + '</td>' +
                        '<td><div class="form-check form-switch"><input type="checkbox" id="toggle' + item.f_id + '" class="toggle-switch form-check-input" ' + (item.f_accountStatus ? 'checked' : '') + ' data-id="' + item.f_id + '"></div></td>' +
                        '<td>' + item.f_amount + '</td>' +
                        '<td>' + item.f_totalSpent + '</td>' +
                        '</tr>';

                    tableBody.append(row);
                });
            }

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

//按下是否啟用，更改資料庫
function ToggleMemberStatus(memberId) {
    $.ajax({
        type: "POST",
        url: "/Ajax/MemberHandler.aspx/ToggleProductStatus",
        data: JSON.stringify({ memberId: memberId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d === "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else if (response.d === "權限不足") {
                alert("權限不足");
                parent.location.reload();
            } else if (response.d === "更改成功") {
                $("#labSearchMember").text("帳號狀態更改成功");
            } else {
                $("#labSearchMember").text(response.d);
            }
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

//更改會員等級
function ToggleMemberLevel(memberId, level) {
    $.ajax({
        type: "POST",
        url: "/Ajax/MemberHandler.aspx/ToggleMemberLevel",
        data: JSON.stringify({ memberId: memberId, level: level }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d === "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else if (response.d === "權限不足") {
                alert("權限不足");
                parent.location.reload();
            }else if (response.d === "更改成功") {
                $("#labSearchMember").text("等級更改成功");
            } else {
                $("#labSearchMember").text(response.d);
            }
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}