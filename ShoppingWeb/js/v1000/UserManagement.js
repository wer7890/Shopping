let pageSize = 1;
let pagesTotal = null;
let page = null;
let beforePagesTotal = 1;

$(document).ready(function () {
    SearchAllData(1, pageSize);
       
    $("#labSearchUser").hide();

    //新增管理員
    $("#btnAddUser").click(function () {
        window.location.href = "AddUser.aspx";
    })

    // 監聽表格標題的點擊事件，排序
    $("#myTable th").click(function () {
        let table = $(this).parents('table').eq(0);  //獲取被點擊標題所屬的表格。
        let rows = table.find('tr:gt(0)').toArray().sort(CompareValues($(this).index()));  //獲取表格中的所有行 (除了表頭行)，然後使用 sort 方法根據標題的索引值進行排序。CompareValues 函數用於定義排序的規則。
        this.asc = !this.asc;  // 切換排序的方向。首次點擊設置為升序，再次點擊設置為降序。
        if (!this.asc) {  // 如果排序方向為降序，反轉行的順序。
            rows = rows.reverse();
        }
        for (let i = 0; i < rows.length; i++) {  //根據排序結果，將行重新附加到表格，從而達到排序的效果。
            table.append(rows[i]);
        }
    });

    //身分下拉選單
    $(document).on("change", ".f_roles", function () {
        let userId = $(this).data('id');
        let roles = $(this).val();
        ToggleUserRoles(userId, roles);
    });

});

//全部管理員資料
function SearchAllData(pageNumber, pageSize) {
    $.ajax({
        url: '/Ajax/UserHandler.aspx/GetAllUserData',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ pageNumber: pageNumber, pageSize: pageSize, beforePagesTotal: beforePagesTotal }),
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
                case 102:
                    $("#labSearchUser").text(langFont["errorLog"]).show().delay(3000).fadeOut();
                    break;
                default:
                    // 處理成功取得資料的情況
                    let data = JSON.parse(response.d.Data); // 解析 JSON 資料為 JavaScript 物件
                    let tableBody = $('#tableBody');

                    pagesTotal = response.d.TotalPages;

                    // 清空表格內容
                    tableBody.empty();

                    // 動態生成表格內容
                    $.each(data, function (index, item) {
                        let row = '<tr>' +
                            '<td>' + item.f_id + '</td>' +
                            '<td>' + item.f_account + '</td>' +
                            '<td>' +
                            '<select class="form-select form-select-sm f_roles" data-id="' + item.f_id + '">' +
                            '<option value="1"' + (item.f_roles == '1' ? ' selected' : '') + '>' + langFont["superAdmin"] + '</option>' +
                            '<option value="2"' + (item.f_roles == '2' ? ' selected' : '') + '>' + langFont["memberAdmin"] + '</option>' +
                            '<option value="3"' + (item.f_roles == '3' ? ' selected' : '') + '>' + langFont["productAdmin"] + '</option>' +
                            '</select>' +
                            '</td>' +
                            '<td><button class="btn btn-primary" onclick="EditUser(' + item.f_id + ')">' + langFont["edit"] + '</button></td>' +
                            '<td><button class="btn btn-danger" onclick="DeleteUser(' + item.f_id + ')">' + langFont["del"] + '</button></td>' +
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
                    } else if (beforePagesTotal !== pagesTotal) {
                        alert("資料頁數變動");
                        page.Update(pagesTotal);
                    }

                    beforePagesTotal = pagesTotal;
            }
        },
        error: function (error) {
            $("#labSearchUser").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
        }
    });
}

//刪除
function DeleteUser(userId) {
    let yes = confirm(langFont["delConfirmation"]);
    if (yes == true) {
        $.ajax({
            type: "POST",
            url: "/Ajax/UserHandler.aspx/RemoveUserInfo",
            data: JSON.stringify({ userId: userId }),
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
                        // 刪除成功後，刷新當前頁面並刷新表格
                        window.location.reload();
                        break;
                    case 101:
                        alert(langFont["delFailed"]);
                        break;
                    default:
                        $("#labSearchUser").text(langFont["errorLog"]).show().delay(3000).fadeOut();
                }
            },
            error: function (error) {
                $("#labSearchUser").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
            }
        });
    }
}

//編輯
function EditUser(userId) {
    $.ajax({
        type: "POST",
        url: "/Ajax/UserHandler.aspx/SetSessionSelectUserId",
        data: JSON.stringify({ userId: userId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d === true) {
                window.location.href = "EditUser.aspx";
            } else {
                alert(langFont["editFailed"]);
            }
        },
        error: function (error) {
            $("#labSearchUser").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
        }
    });
}

//更改管理員身分
function ToggleUserRoles(userId, roles) {
    $.ajax({
        type: "POST",
        url: "/Ajax/UserHandler.aspx/ToggleUserRoles",
        data: JSON.stringify({ userId: userId, roles: roles }),
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
                    $("#labSearchUser").text(langFont["changeSuccessful"]).show().delay(3000).fadeOut();
                    break;
                case 101:
                    $("#labSearchUser").text(langFont["changeFailed"]).show().delay(3000).fadeOut();
                    break;
                default:
                    $("#labSearchUser").text(langFont["errorLog"]).show().delay(3000).fadeOut();
            }
        },
        error: function (error) {
            $("#labSearchUser").text(langFont["ajaxError"]).show().delay(3000).fadeOut();
        }
    });
}

// 比較函數，根據列的索引進行比較，根據給定索引值比較兩個行的值。如果值是數字，則使用數字比較，否則使用字典順序比較。
function CompareValues(index) {
    return function (a, b) {
        let valA = GetCellValue(a, index);
        let valB = GetCellValue(b, index);
        return $.isNumeric(valA) && $.isNumeric(valB) ? valA - valB : valA.localeCompare(valB);
    };
}

// 獲取單元格的值
function GetCellValue(row, index) {
    return $(row).children('td').eq(index).text();
}
