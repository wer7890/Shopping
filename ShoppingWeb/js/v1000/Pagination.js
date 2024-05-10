let currentPage = 1; // 當前頁數，初始頁數為 1
let pageSize = 5; // 每頁顯示的資料筆數
let pagesTotal = null; //總頁數

$(document).ready(function () {
    //上一頁
    $("#ulPagination").on("click", "#previousPage", function () {
        if (currentPage > 1) {
            currentPage--;
            SearchAllData(currentPage, pageSize);
        }
    });

    //下一頁
    $("#ulPagination").on("click", "#nextPage", function () {
        if (currentPage < pagesTotal) {
            currentPage++;
            SearchAllData(currentPage, pageSize);
        }
    });

    //數字頁數
    $("#pagination").on('click', 'a.pageNumber', function () {
        currentPage = parseInt($(this).text());
        SearchAllData(currentPage, pageSize);
    });

    //首頁
    $("#ulPagination").on("click", "#firstPage", function () {
        currentPage = 1;
        SearchAllData(currentPage, pageSize);
    });

    //末頁
    $("#ulPagination").on("click", "#lastPage", function () {
        currentPage = pagesTotal;
        SearchAllData(currentPage, pageSize);
    });
})

//依資料筆數來開分頁頁數
function AddPages(pagesTotal, IsSearch) {
    let ulPagination = $('#ulPagination');
    ulPagination.empty();
    let paginationInfoDiv = $('#paginationInfo');
    paginationInfoDiv.empty();

    if (pagesTotal > 0) {
        //依資料筆數來開分頁頁數 
        let paginationBtnHtml = '<li class="page-item" id="' + (IsSearch ? 'searchFirstPage' : 'firstPage') + '"><a class="page-link" href="#">' + langFont["firstPage"] + '</a></li>' +
            '<li class="page-item" id="' + (IsSearch ? 'searchPreviousPage' : 'previousPage') + '"><a class="page-link" href="#"> < </a></li>';

        if (currentPage <= 2) {
            for (let i = 1; i <= Math.min(5, pagesTotal); i++) {
                paginationBtnHtml += '<li class="page-item" id="page' + i + '"><a class="page-link ' + (IsSearch ? 'searchPageNumber' : 'pageNumber') + '" href="#">' + i + '</a></li>';
            }
        } else if (currentPage >= pagesTotal - 1) {
            for (let i = Math.max(1, pagesTotal - 4); i <= pagesTotal; i++) {
                paginationBtnHtml += '<li class="page-item" id="page' + i + '"><a class="page-link ' + (IsSearch ? 'searchPageNumber' : 'pageNumber') + '" href="#">' + i + '</a></li>';
            }
        } else {
            for (let i = currentPage - 2; i <= currentPage + 2; i++) {
                paginationBtnHtml += '<li class="page-item" id="page' + i + '"><a class="page-link ' + (IsSearch ? 'searchPageNumber' : 'pageNumber') + '" href="#">' + i + '</a></li>';
            }
        }

        paginationBtnHtml += '<li class="page-item" id="' + (IsSearch ? 'searchNextPage' : 'nextPage') + '"><a class="page-link" href="#"> > </a></li>' +
            '<li class="page-item" id="' + (IsSearch ? 'searchLastPage' : 'lastPage') + '"><a class="page-link" href="#">' + langFont["lastPage"] + '</a></li>';

        ulPagination.append(paginationBtnHtml);

        let selectElement = $('<select id="pageSelect" class="form-select form-select-sm ms-3" onchange="' + (IsSearch ? 'SearchChangePage(this)' : 'ChangePage(this)') + '"></select>');
        for (let i = 1; i <= pagesTotal; i++) {
            let = optionHtml = '<option value="' + i + '"' + (i === currentPage ? ' selected' : '') + '>第' + i + '頁</option>';
            selectElement.append(optionHtml);
        }
        paginationInfoDiv.append(selectElement);
        paginationInfoDiv.append('<span class="col-3">共 ' + pagesTotal + ' 頁</span>');
    }
}

// 當切換到哪個頁面時，就把該頁面的按鈕變色
function UpdatePaginationControls(currentPage) {
    $('#pagination .page-item').removeClass('active');
    $('#page' + currentPage).addClass('active');
}

//頁數下拉選單
function ChangePage(selectElement) {
    currentPage = parseInt(selectElement.value);
    SearchAllData(currentPage, pageSize);
}
