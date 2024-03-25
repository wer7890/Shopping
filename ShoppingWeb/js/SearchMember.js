$(document).ready(function () {
    SearchAllMember();

    $(document).on("change", ".toggle-switch", function () {
        $("#labSearchMember").text("");
        let memberId = $(this).data('id');
        ToggleMemberStatus(memberId);
    });

    //等級下拉選單
    $(document).on("change", ".f_level", function () {
        $("#labSearchMember").text("");
        let memberId = $(this).data('id');
        let level = $(this).val();
        ToggleMemberLevel(memberId, level);
    });

})

//全部會員資料
function SearchAllMember() {
    $.ajax({
        url: '/Ajax/SearchMemberHandler.aspx/GetAllMemberData',
        type: 'POST',
        contentType: 'application/json',
        success: function (response) {
            if (response.d == "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else {
                let data = JSON.parse(response.d); 
                let tableBody = $('#tableBody');

                tableBody.empty();

                $.each(data, function (index, item) {
                    let row = '<tr>' +
                        '<td>' + item.f_id + '</td>' +
                        '<td>' + item.f_name + '</td>' +
                        '<td>' + item.f_pwd + '</td>' +
                        '<td>' +
                        '<select class="form-select form-select-sm f_level" data-id="' + item.f_id + '">' +
                        '<option value="1"' + (item.f_level == '1' ? ' selected' : '') + '>等級1</option>' +
                        '<option value="2"' + (item.f_level == '2' ? ' selected' : '') + '>等級2</option>' +
                        '<option value="3"' + (item.f_level == '3' ? ' selected' : '') + '>等級3</option>' +
                        '</select>' +
                        '</td>' +
                        '<td>' + item.f_wallet + '</td>' +
                        '<td>' + item.f_phoneNumber + '</td>' +
                        '<td>' + item.f_email + '</td>' +
                        '<td>' + item.f_createdTime + '</td>' +
                        '<td><div class="form-check form-switch"><input type="checkbox" id="toggle' + item.f_id + '" class="toggle-switch form-check-input" ' + (item.f_accountStatus ? 'checked' : '') + ' data-id="' + item.f_id + '"></div></td>' +
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
        url: "/Ajax/SearchMemberHandler.aspx/ToggleProductStatus",
        data: JSON.stringify({ memberId: memberId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d === "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            }else if (response.d === "更改成功") {
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
        url: "/Ajax/SearchMemberHandler.aspx/ToggleMemberLevel",
        data: JSON.stringify({ memberId: memberId, level: level }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d === "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
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