let translations = {
    'titleUser': '管理員帳號',
    'addUser': '新增管理員',
    'userId': '管理員ID',
    'account': '帳號',
    'roles': '角色',
    'edit': '編輯',
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
    'changeFailed': '身分更改失敗'
    
};


//$(document).ready(function () {
//    let currentPage = 1; // 初始頁碼為 1
//    let pageSize = 5; // 每頁顯示的資料筆數

//    SearchAllUserInfo(currentPage, pageSize);

//    //上一頁
//    $("#ulPagination").on("click", "#previousPage", function () {
//        if (currentPage > 1) {
//            currentPage--;
//            SearchAllUserInfo(currentPage, pageSize);
//        }
//        $("#labSearchUser").text("");
//    });

//    //下一頁
//    $("#ulPagination").on("click", "#nextPage", function () {
//        if (currentPage < $('#ulPagination').children('li').length - 2) {  // 獲取id="ulPagination"下的li元素個數，-2是因為要扣掉上跟下一頁
//            currentPage++;
//            SearchAllUserInfo(currentPage, pageSize);
//        }
//        $("#labSearchUser").text("");
//    });

//    //數字頁數
//    $("#pagination").on('click', 'a.pageNumber', function () {
//        currentPage = parseInt($(this).text());
//        SearchAllUserInfo(currentPage, pageSize);
//        $("#labSearchUser").text("");
//    });

//    //新增管理員
//    $("#btnAddUser").click(function () {
//        window.location.href = "AddUser.aspx";
//    })

//    // 監聽表格標題的點擊事件，排序
//    $("#myTable th").click(function () {
//        let table = $(this).parents('table').eq(0);  //獲取被點擊標題所屬的表格。
//        let rows = table.find('tr:gt(0)').toArray().sort(CompareValues($(this).index()));  //獲取表格中的所有行 (除了表頭行)，然後使用 sort 方法根據標題的索引值進行排序。CompareValues 函數用於定義排序的規則。
//        this.asc = !this.asc;  // 切換排序的方向。首次點擊設置為升序，再次點擊設置為降序。
//        if (!this.asc) {  // 如果排序方向為降序，反轉行的順序。
//            rows = rows.reverse();
//        }
//        for (let i = 0; i < rows.length; i++) {  //根據排序結果，將行重新附加到表格，從而達到排序的效果。
//            table.append(rows[i]);
//        }
//    });

//    //身分下拉選單
//    $(document).on("change", ".f_roles", function () {
//        $("#labSearchMember").text("");
//        let userId = $(this).data('id');
//        let roles = $(this).val();
//        ToggleUserRoles(userId, roles);
//    });

//})

////全部管理員資料
//function SearchAllUserInfo(pageNumber, pageSize) {
//    $.ajax({
//        url: '/Ajax/UserHandler.aspx/GetAllUserData',
//        type: 'POST',
//        contentType: 'application/json',
//        data: JSON.stringify({ pageNumber: pageNumber, pageSize: pageSize }),
//        success: function (response) {
//            if (response.d === 0) {
//                alert("重複登入，已被登出");
//                window.parent.location.href = "Login.aspx";
//            } else if (response.d === 1) {
//                alert("權限不足");
//                parent.location.reload();
//            } else {
//                // 處理成功取得資料的情況
//                let data = JSON.parse(response.d.Data); // 解析 JSON 資料為 JavaScript 物件
//                let tableBody = $('#tableBody');

//                // 清空表格內容
//                tableBody.empty();

//                // 動態生成表格內容
//                $.each(data, function (index, item) {
//                    let row = '<tr>' +
//                        '<td>' + item.f_id + '</td>' +
//                        '<td>' + item.f_account + '</td>' +
//                        '<td>' +
//                        '<select class="form-select form-select-sm f_roles" data-id="' + item.f_id + '">' +
//                        '<option value="1"' + (item.f_roles == '1' ? ' selected' : '') + '>超級管理員</option>' +
//                        '<option value="2"' + (item.f_roles == '2' ? ' selected' : '') + '>會員管理員</option>' +
//                        '<option value="3"' + (item.f_roles == '3' ? ' selected' : '') + '>商品管理員</option>' +
//                        '</select>' +
//                        '</td>' +
//                        '<td><button class="btn btn-primary" onclick="EditUser(' + item.f_id + ')">編輯</button></td>' +
//                        '<td><button class="btn btn-danger" onclick="DeleteUser(' + item.f_id + ')">刪除</button></td>' +
//                        '</tr>';

//                    tableBody.append(row);
//                });

//                //依資料筆數來開分頁頁數
//                if (response.d.TotalPages > 0) {
//                    let ulPagination = $('#ulPagination');
//                    ulPagination.empty();
//                    ulPagination.append('<li class="page-item" id="previousPage"><a class="page-link" href="#"> << </a></li>');
//                    for (let i = 1; i <= response.d.TotalPages; i++) {
//                        ulPagination.append('<li class="page-item" id="page' + i + '"><a class="page-link pageNumber" href="#">' + i + '</a></li>');
//                    }
//                    ulPagination.append('<li class="page-item" id="nextPage"><a class="page-link" href="#"> >> </a></li>');

//                }
//            }
//            UpdatePaginationControls(pageNumber);
//        },
//        error: function (xhr, status, error) {
//            // 處理發生錯誤的情況
//            console.error('Error:', error);
//        }
//    });
//}

////刪除
//function DeleteUser(userId) {
//    let yes = confirm('確定要刪除該用戶嗎');
//    if (yes == true) {
//        $.ajax({
//            type: "POST",
//            url: "/Ajax/UserHandler.aspx/RemoveUserInfo",
//            data: JSON.stringify({ userId: userId }),
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (response) {
//                switch (response.d) {
//                    case 0:
//                        alert("重複登入，已被登出");
//                        window.parent.location.href = "Login.aspx";
//                        break;
//                    case 1:
//                        alert("權限不足");
//                        parent.location.reload();
//                        break;
//                    case 100:
//                        // 刪除成功後，刷新當前頁面並刷新表格
//                        window.location.reload();
//                        break;
//                    case 101:
//                        alert("刪除失敗");
//                        break;
//                    default:
//                        $("#labSearchUser").text("發生發生內部錯誤，請看日誌");
//                }
//            },
//            error: function (error) {
//                console.error('Error:', error);
//            }
//        });
//    }
//}

////編輯
//function EditUser(userId) {
//    $.ajax({
//        type: "POST",
//        url: "/Ajax/UserHandler.aspx/SetSessionSelectUserId",
//        data: JSON.stringify({ userId: userId }),
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response) {
//            if (response.d === true) {
//                window.location.href = "EditUser.aspx";
//            } else {
//                alert("失敗");
//            }
//        },
//        error: function (error) {
//            console.error('Error:', error);
//        }
//    });
//}

////更改管理員身分
//function ToggleUserRoles(userId, roles) {
//    $.ajax({
//        type: "POST",
//        url: "/Ajax/UserHandler.aspx/ToggleUserRoles",
//        data: JSON.stringify({ userId: userId, roles: roles }),
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response) {
//            switch (response.d) {
//                case 0:
//                    alert("重複登入，已被登出");
//                    window.parent.location.href = "Login.aspx";
//                    break;
//                case 1:
//                    alert("權限不足");
//                    parent.location.reload();
//                    break;
//                case 100:
//                    $("#labSearchUser").text("身分更改成功");
//                    break;
//                case 101:
//                    $("#labSearchUser").text("身分更改失敗");
//                    break;
//                default:
//                    $("#labSearchUser").text("發生發生內部錯誤，請看日誌");
//            }
//        },
//        error: function (error) {
//            console.error('Error:', error);
//        }
//    });
//}

//// 當切換到哪個頁面時，就把該頁面的按鈕變色
//function UpdatePaginationControls(currentPage) {
//    $('#pagination .page-item').removeClass('active');
//    $('#page' + currentPage).addClass('active');
//}

//// 比較函數，根據列的索引進行比較，根據給定索引值比較兩個行的值。如果值是數字，則使用數字比較，否則使用字典順序比較。
//function CompareValues(index) {
//    return function (a, b) {
//        let valA = GetCellValue(a, index);
//        let valB = GetCellValue(b, index);
//        return $.isNumeric(valA) && $.isNumeric(valB) ? valA - valB : valA.localeCompare(valB);
//    };
//}

//// 獲取單元格的值
//function GetCellValue(row, index) {
//    return $(row).children('td').eq(index).text();
//}
