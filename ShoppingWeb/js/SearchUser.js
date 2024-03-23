﻿$(document).ready(function () {

    searchAllUserInfo();

    $("#btnAddUser").click(function () {
        window.location.href = "AddUser.aspx";
    })

    // 監聽表格標題的點擊事件
    $('#myTable th').click(function () {
        var table = $(this).parents('table').eq(0);  //獲取被點擊標題所屬的表格。
        var rows = table.find('tr:gt(0)').toArray().sort(compareValues($(this).index()));  //獲取表格中的所有行 (除了表頭行)，然後使用 sort 方法根據標題的索引值進行排序。compareValues 函數用於定義排序的規則。
        this.asc = !this.asc;  // 切換排序的方向。首次點擊設置為升序，再次點擊設置為降序。
        if (!this.asc) {  // 如果排序方向為降序，反轉行的順序。
            rows = rows.reverse();
        }
        for (var i = 0; i < rows.length; i++) {  //根據排序結果，將行重新附加到表格，從而達到排序的效果。
            table.append(rows[i]);
        }
    });

})

//全部管理員資料
function searchAllUserInfo() {
    $.ajax({
        url: '/Ajax/SearchUserHandler.aspx/GetUserData',
        type: 'POST',
        contentType: 'application/json',
        success: function (response) {
            if (response.d == "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            } else {
                // 處理成功取得資料的情況
                var data = JSON.parse(response.d); // 解析 JSON 資料為 JavaScript 物件
                var tableBody = $('#tableBody');

                // 清空表格內容
                tableBody.empty();

                // 動態生成表格內容
                $.each(data, function (index, item) {
                    var row = '<tr>' +
                        '<td>' + item.f_id + '</td>' +
                        '<td>' + item.f_name + '</td>' +
                        '<td>' + item.f_pwd + '</td>' +
                        '<td>' + item.f_roles + '</td>' +
                        '<td><button class="btn btn-primary" onclick="editUser(' + item.f_id + ')">編輯</button></td>' +
                        '<td><button class="btn btn-danger" onclick="deleteUser(' + item.f_id + ')">刪除</button></td>' +
                        '</tr>';

                    tableBody.append(row);
                });
            }
        },
        error: function (xhr, status, error) {
            // 處理發生錯誤的情況
            console.error('Error:', error);
        }
    });
}

//刪除
function deleteUser(userId) {
    $.ajax({
        type: "POST",
        url: "/Ajax/SearchUserHandler.aspx/RemoveUserInfo",  
        data: JSON.stringify({ userId: userId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            if (response.d === "重複登入") {
                alert("重複登入，已被登出");
                window.parent.location.href = "Login.aspx";
            }else if (response.d === "刪除成功") {
                // 刪除成功後，刷新當前頁面並刷新表格
                window.location.reload();
            } else {
                $("#labSearch").text("刪除失敗");
            }
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

//編輯
function editUser(userId) {
    $.ajax({
        type: "POST",
        url: "/Ajax/SearchUserHandler.aspx/SetSessionUserId", 
        data: JSON.stringify({ userId: userId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d === true) {
                window.location.href = "RenewUser.aspx";
            } else {
                alert("失敗");
            }
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

// 比較函數，根據列的索引進行比較，根據給定索引值比較兩個行的值。如果值是數字，則使用數字比較，否則使用字典順序比較。
function compareValues(index) {
    return function (a, b) {
        var valA = getCellValue(a, index);
        var valB = getCellValue(b, index);
        return $.isNumeric(valA) && $.isNumeric(valB) ? valA - valB : valA.localeCompare(valB);
    };
}

// 獲取單元格的值
function getCellValue(row, index) {
    return $(row).children('td').eq(index).text();
}
