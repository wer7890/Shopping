let currentPage = 1; // 當前頁數，初始頁數為 1
let pageSize = 5; // 每頁顯示的資料筆數
let pagesTotal = null; //總頁數

$(document).ready(function () {

    SearchAllUserInfo(currentPage, pageSize);

    //上一頁
    $("#ulPagination").on("click", "#previousPage", function () {
        if (currentPage > 1) {
            currentPage--;
            SearchAllUserInfo(currentPage, pageSize);
        }
        $("#labSearchUser").text("");
    });

    //下一頁
    $("#ulPagination").on("click", "#nextPage", function () {
        if (currentPage < pagesTotal) { 
            currentPage++;
            SearchAllUserInfo(currentPage, pageSize);
        }
        $("#labSearchUser").text("");
    });

    //數字頁數
    $("#pagination").on('click', 'a.pageNumber', function () {
        currentPage = parseInt($(this).text());
        SearchAllUserInfo(currentPage, pageSize);
        $("#labSearchUser").text("");
    });

    //首頁
    $("#ulPagination").on("click", "#firstPage", function () {
        currentPage = 1;
        SearchAllUserInfo(currentPage, pageSize);
        $("#labSearchUser").text("");
    });

    //末頁
    $("#ulPagination").on("click", "#lastPage", function () {
        currentPage = pagesTotal;
        SearchAllUserInfo(currentPage, pageSize);
        $("#labSearchUser").text("");
    });

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
        $("#labSearchMember").text("");
        let userId = $(this).data('id');
        let roles = $(this).val();
        ToggleUserRoles(userId, roles);
    });

});

//全部管理員資料
function SearchAllUserInfo(pageNumber, pageSize) {
    $.ajax({
        url: '/Ajax/UserHandler.aspx/GetAllUserData',
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

                //依資料筆數來開分頁頁數
                if (response.d.TotalPages > 0) {
                    let ulPagination = $('#ulPagination');
                    ulPagination.empty();

                    paginationBtnHtml = '<li class="page-item" id="firstPage"><a class="page-link" href="#">' + langFont["firstPage"] + '</a></li>' +
                        '<li class="page-item" id="previousPage"><a class="page-link" href="#"> << </a></li>';
                    for (let i = 1; i <= response.d.TotalPages; i++) {
                        paginationBtnHtml += '<li class="page-item" id="page' + i + '"><a class="page-link pageNumber" href="#">' + i + '</a></li>';
                    }
                    paginationBtnHtml += '<li class="page-item" id="nextPage"><a class="page-link" href="#"> >> </a></li>' +
                        '<li class="page-item" id="lastPage"><a class="page-link" href="#"> ' + langFont["lastPage"] + ' </a></li>';
                    ulPagination.append(paginationBtnHtml);
                }
            }
            UpdatePaginationControls(pageNumber);
        },
        error: function (xhr, status, error) {
            // 處理發生錯誤的情況
            console.error('Error:', error);
            $("#labSearchUser").text(langFont["ajaxError"]);
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
                        $("#labSearchUser").text(langFont["errorLog"]);
                }
            },
            error: function (error) {
                console.error('Error:', error);
                $("#labSearchUser").text(langFont["ajaxError"]);
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
            console.error('Error:', error);
            $("#labSearchUser").text(langFont["ajaxError"]);
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
                    $("#labSearchUser").text(langFont["changeSuccessful"]);
                    break;
                case 101:
                    $("#labSearchUser").text(langFont["changeFailed"]);
                    break;
                default:
                    $("#labSearchUser").text(langFont["errorLog"]);
            }
        },
        error: function (error) {
            console.error('Error:', error);
            $("#labSearchUser").text(langFont["ajaxError"]);
        }
    });
}

// 當切換到哪個頁面時，就把該頁面的按鈕變色
function UpdatePaginationControls(currentPage) {
    $('#pagination .page-item').removeClass('active');
    $('#page' + currentPage).addClass('active');
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
