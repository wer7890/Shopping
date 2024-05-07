let currentPage = 1; // 初始頁碼為 1
let pageSize = 5; // 每頁顯示的資料筆數

$(document).ready(function () {
    
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
        if (currentPage < $('#ulPagination').children('li').length - 2) {  // 獲取id="ulPagination"下的li元素個數，-2是因為要扣掉上跟下一頁
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

})

//依資料筆數來開分頁頁數
function AddPages(totalPages) { 
    if (totalPages > 0) {
        let ulPagination = $('#ulPagination');
        ulPagination.empty();
        ulPagination.append('<li class="page-item" id="previousPage"><a class="page-link" href="#"> << </a></li>');
        for (let i = 1; i <= totalPages; i++) {
            ulPagination.append('<li class="page-item" id="page' + i + '"><a class="page-link pageNumber" href="#">' + i + '</a></li>');
        }
        ulPagination.append('<li class="page-item" id="nextPage"><a class="page-link" href="#"> >> </a></li>');

    }
}

// 當切換到哪個頁面時，就把該頁面的按鈕變色
function UpdatePaginationControls(currentPage) {
    $('#pagination .page-item').removeClass('active');
    $('#page' + currentPage).addClass('active');
}