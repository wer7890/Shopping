let currentPage = 1; // 當前頁數，初始頁數為 1
let pageSize = 5; // 每頁顯示的資料筆數
let pagesTotal = null; //總頁數

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

})

//依資料筆數來開分頁頁數
function AddPages(totalPages) {
    if (totalPages > 0) {
        let ulPagination = $('#ulPagination');
        ulPagination.empty();
        paginationBtnHtml = '<li class="page-item" id="firstPage"><a class="page-link" href="#">' + langFont["firstPage"] + '</a></li>' +
            '<li class="page-item" id="previousPage"><a class="page-link" href="#"> << </a></li>';
        for (let i = 1; i <= totalPages; i++) {
            paginationBtnHtml += '<li class="page-item" id="page' + i + '"><a class="page-link pageNumber" href="#">' + i + '</a></li>';
        }
        paginationBtnHtml += '<li class="page-item" id="nextPage"><a class="page-link" href="#"> >> </a></li>' +
            '<li class="page-item" id="lastPage"><a class="page-link" href="#"> ' + langFont["lastPage"] + ' </a></li>';
        ulPagination.append(paginationBtnHtml);
    }
}

// 當切換到哪個頁面時，就把該頁面的按鈕變色
function UpdatePaginationControls(currentPage) {
    $('#pagination .page-item').removeClass('active');
    $('#page' + currentPage).addClass('active');
}