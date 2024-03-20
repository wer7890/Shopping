$(document).ready(function () {
    SearchAllMember();

    // 使用事件代理監聽開關的改變事件
    $(document).on("change", ".toggle-switch", function () {
        let memberId = $(this).data('id');
        ToggleProductStatus(memberId);
    });

})

//全部商品資料
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
                        '<td>' + item.f_level + '</td>' +
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

//按下是否停權，更改資料庫
function ToggleProductStatus(memberId) {
    $.ajax({
        type: "POST",
        url: "/Ajax/SearchMemberHandler.aspx/ToggleProductStatus",
        data: JSON.stringify({ memberId: memberId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d === "更改成功") {
                $("#labSearchMember").text("更改成功");
            } else if (response.d === "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else {
                $("#labSearchMember").text(response.d);
            }
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}