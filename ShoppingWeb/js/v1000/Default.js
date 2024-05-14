var page = new Pagination({
	id: 'ulPagination', //頁面元素的id
	total: 30, //總頁數
	showButtons: 5,  //需要顯示的按鈕數量
	callback: function (pageIndex) {  //點擊分頁後觸發的回調，pageIndex就是當前選擇的頁面的索引，從0開始
		console.log(pageIndex);
	}
});

