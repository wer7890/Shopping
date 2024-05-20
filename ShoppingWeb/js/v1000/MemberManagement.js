let pageSize = 5;
let pagesTotal = null;
let page = null;
let beforePagesTotal = null;

$(document).ready(function () {
    SearchAllData(1, pageSize);
    $("#labSearchMember").hide();

    //按下新增會員按鈕
    $("#btnAddMember").click(function () {
        $.ajax({
            type: "POST",
            url: "/Ajax/MemberHandler.aspx/AddMember",  // 這裡指定後端方法的位置
            data: JSON.stringify({ account: GetRandomStr(), pwd: GetRandomStr(), name: GetRandomName(), birthday: GetRandomDate(), phone: GetRandomPhone(), email: GetRandomEmail(), address: "台中市" }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                switch (response.d) {
                    case 0:
                        alert(langFont["duplicateLogin"]);
                        window.parent.location.href = "Login.aspx";
                        break;
                    case 1:
                        alert(langFont["accessDenied"]);
                        parent.location.reload();
                        break;
                    case 2:
                        $("#labSearchMember").text(langFont["inputError"]).show().delay(3000).fadeOut();
                        break;
                    case 100:
                        window.location.reload();
                        break;
                    case 101:
                        $("#labSearchMember").text(langFont["addFailed"]).show().delay(3000).fadeOut();
                        break;
                    default:
                        $("#labSearchMember").text(langFont["errorLog"]).show().delay(3000).fadeOut();

                }
            },
            error: function (error) {
                console.error('AJAX Error:', error);
                $("#labSearchMember").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
            }
        });
    })

    //是否停權
    $(document).on("change", ".toggle-switch", function () {
        let memberId = $(this).data('id');
        ToggleMemberStatus(memberId);
    });

    //更改等級
    $(document).on("change", ".f_level", function () {
        let memberId = $(this).data('id');
        let level = $(this).val();
        ToggleMemberLevel(memberId, level);
    });

})


const numeric = "0123456789";
const lowerCase = "abcdefghijklmnopqrstuvwxyz"
const upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

const emailSuffix = ["@hotmail.com", "@yahoo.com", "@gmail.com", "@live.com", "@qq.com"];

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

//隨機生成中文姓名，使用Unicode(萬國碼)編碼
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
    let res = new Date(Math.random() * (max.getTime() - min.getTime()));
    return res.getFullYear() + "-" + (res.getMonth() + 1) + "-" + res.getDate();
}

//隨機生成手機號碼
function GetRandomPhone() {
    return "09" + randomSequence(8, numeric);
}

//隨機生成信箱
function GetRandomEmail() {
    let opt = numeric + lowerCase + upperCase
    return randomSequence(randomInt(4, 10), opt) + randomSequence(1, emailSuffix);
}


//全部會員資料
function SearchAllData(pageNumber, pageSize) {
    $.ajax({
        url: '/Ajax/MemberHandler.aspx/GetAllMemberData',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ pageNumber: pageNumber, pageSize: pageSize }),
        success: function (response) {
            if (response.d === 0) {
                alert(langFont["duplicateLogin"]);
                window.parent.location.href = "Login.aspx";
            } else if (response.d === 1) {
                alert(langFont["accessDenied"]);
                parent.location.reload();
            } else {
                let data = JSON.parse(response.d.Data);
                let tableBody = $('#tableBody');

                pagesTotal = response.d.TotalPages;

                tableBody.empty();

                $.each(data, function (index, item) {
                    let row = '<tr>' +
                        '<td>' + item.f_id + '</td>' +
                        '<td>' + item.f_account + '</td>' +
                        '<td>' + item.f_pwd + '</td>' +
                        '<td>' + item.f_name + '</td>' +
                        '<td>' +
                        '<select class="form-select form-select-sm f_level" data-id="' + item.f_id + '">' +
                        '<option value="0"' + (item.f_level == '0' ? ' selected' : '') + '>' + langFont["level0"] + '</option>' +
                        '<option value="1"' + (item.f_level == '1' ? ' selected' : '') + '>' + langFont["level1"] + '</option>' +
                        '<option value="2"' + (item.f_level == '2' ? ' selected' : '') + '>' + langFont["level2"] + '</option>' +
                        '<option value="3"' + (item.f_level == '3' ? ' selected' : '') + '>' + langFont["level3"] + '</option>' +
                        '</select>' +
                        '</td>' +
                        '<td>' + item.f_phoneNumber + '</td>' +
                        '<td><div class="form-check form-switch"><input type="checkbox" id="toggle' + item.f_id + '" class="toggle-switch form-check-input" ' + (item.f_accountStatus ? 'checked' : '') + ' data-id="' + item.f_id + '"></div></td>' +
                        '<td>' + item.f_amount + '</td>' +
                        '<td>' + item.f_totalSpent + '</td>' +
                        '</tr>';

                    tableBody.append(row);
                });

                if (page === null) {
                    page = new Pagination({
                        id: 'pagination', 
                        total: pagesTotal, 
                        showButtons: 5, 
                        showFirstLastButtons: true, 
                        showGoInput: true,
                        showPagesTotal: true,
                        callback: function (pageIndex) {  
                            SearchAllData(pageIndex + 1, pageSize); 
                        }
                    });
                } else {

                    if (beforePagesTotal !== pagesTotal) {
                        alert("資料頁數變動");
                        SearchAllData(1, pageSize); 
                        page.Update(pagesTotal);
                    }

                }

                beforePagesTotal = pagesTotal;
            }
        },
        error: function (error) {
            console.error('Error:', error);
            $("#labSearchMember").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
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
            switch (response.d) {
                case 0:
                    alert(langFont["duplicateLogin"]);
                    window.parent.location.href = "Login.aspx";
                    break;
                case 1:
                    alert(langFont["accessDenied"]);
                    parent.location.reload();
                    break;
                case 100:
                    $("#labSearchMember").text(langFont["editStateSuccessful"]).show().delay(3000).fadeOut();
                    break;
                case 101:
                    $("#labSearchMember").text(langFont["editStateFailed"]).show().delay(3000).fadeOut();
                    break;
                default:
                    $("#labSearchMember").text(langFont["errorLog"]).show().delay(3000).fadeOut();
            }
        },
        error: function (error) {
            console.error('Error:', error);
            $("#labSearchMember").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
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
            switch (response.d) {
                case 0:
                    alert(langFont["duplicateLogin"]);
                    window.parent.location.href = "Login.aspx";
                    break;
                case 1:
                    alert(langFont["accessDenied"]);
                    parent.location.reload();
                    break;
                case 100:
                    $("#labSearchMember").text(langFont["editLevelSuccessful"]).show().delay(3000).fadeOut();
                    break;
                case 101:
                    $("#labSearchMember").text(langFont["editLevelFailed"]).show().delay(3000).fadeOut();
                    break;
                default:
                    $("#labSearchMember").text(langFont["errorLog"]).show().delay(3000).fadeOut();
            }
        },
        error: function (error) {
            console.error('Error:', error);
            $("#labSearchMember").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
        }
    });
}