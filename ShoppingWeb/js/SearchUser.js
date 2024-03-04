$(document).ready(function () {
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

})

function deleteUser(userId) {
    $.ajax({
        type: "POST",
        url: "../Web/SearchUser.aspx/deleteUser",  // 這裡指定後端方法的位置
        data: JSON.stringify({ userId: userId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d === true) {
                // 刪除成功後，刷新当前页面並刷新表格
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

function editUser(userId) {
    $.ajax({
        type: "POST",
        url: "../Web/SearchUser.aspx/setRenewSession",  // 這裡指定後端方法的位置
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
