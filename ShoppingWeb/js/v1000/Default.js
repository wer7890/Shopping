var page = new Pagination({
	id: 'ulPagination', //頁面元素的id
	total: 30, //總頁數
	showButtons: 5,  //需要顯示的按鈕數量
	callback: function (pageIndex) {  //点击分页后触发的回调，pageIndex就是当前选择的页面的索引，从0开始
		console.log(pageIndex);
	}
});


