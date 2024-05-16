; (function () {
	function Pagination(users) {
		this.setting = {
			id: null,  //新增分頁元素的id
			total: 21,  //總頁數
			showButtons: 5, //需要顯示的按鈕數量
			showFirstLastButtons: false, //是否顯示首頁和末頁按鈕
			showGoInput: false,  //是否顯示跳轉頁面輸入框和按鈕
			callback: null  //回呼函示
		}

		this.cur = 1; //當前頁碼
		for (var attr in users) {
			this.setting[attr] = users[attr];
		}
		
		document.getElementById(this.setting.id).innerHTML = '<div id="paginationBtn" class="text-center d-flex justify-content-center col-12 col-sm-12"><ul class="pagination d-flex justify-content-center" id="ulPagination"></ul></div>' +
			'<div id="paginationInfo" class="text-center text-center d-flex justify-content-center col-5 mx-auto"></div>';
		this.setting.id = document.getElementById("ulPagination");

		this.render();
		
	}

	// 初始dom
	Pagination.prototype.doInit = function (index, cur) {
		index = index || 0;  //分頁按鈕起始索引
		cur = cur || 0;  //當前頁碼
		var html = '';
		var showButtons = this.setting.showButtons;  //顯示的按鈕數量
		var total = this.setting.total;  //總頁數
		var pages = showButtons >= total ? total : showButtons; // 頁數，假如要顯示的按鈕數量大於等於總頁數，那頁數等於總頁數

		//新增數字按鈕
		for (var i = index, lens = pages + index; i < lens; i++) {
			if (i == cur) {
				html += '<li class="page-item active"><a class="page-link paginationA" href="javascript:;">' + (i + 1) + '</a></li>';
			} else {
				html += '<li class="page-item"><a class="page-link paginationA" href="javascript:;">' + (i + 1) + '</a></li>';
			}
		}

		//新增上下頁和首末按鈕
		if (cur == 0 && total > showButtons) {  //當前頁數1且總頁數大於顯示頁數
			html += '<li class="page-item"><span id="next" class="page-link paginationSpan"> > </span></li>';

			if (this.setting.showFirstLastButtons) {
				html += '<li class="page-item"><span id="last" class="page-link paginationSpan"> >| </span></li>';
			}

		} else if (cur == this.setting.total - 1 && total > showButtons) {  //當前在末頁且總頁數大於顯示頁數
			html = '</li><li class="page-item"><span id="prev" class="page-link paginationSpan"> < </span></li>' + html;

			if (this.setting.showFirstLastButtons) {
				html = '<li class="page-item"><span id="first" class="page-link paginationSpan"> |< </span>' + html;
			}

		} else if (showButtons >= total) {  //只顯示數字按鈕

        } else {
			html = '<li class="page-item"><span id="prev" class="page-link paginationSpan"> < </span></li>' + html + '<li class="page-item"><span id="next" class="page-link paginationSpan"> > </span></li>';

			if (this.setting.showFirstLastButtons) {
				html = '<li class="page-item"><span id="first" class="page-link paginationSpan"> |< </span>' + html + '<li class="page-item"><span id="last" class="page-link paginationSpan"> >| </span></li>';
			}

        }

		//新增輸入頁數功能
		if (this.setting.showGoInput) {
			html += '<div class="input-group mb-3 ps-4"><input type="text" id="pageInput" class="form-control" aria-label="GoBtn" aria-describedby="paginationGo" /><button class="btn btn-outline-secondary btn-sm" type="button" id="paginationGo">GO</button></div>'
        }

		return html;
	}

	// 渲染
	Pagination.prototype.render = function () {
		var self = this;

		this.setting.id.innerHTML = this.doInit();
		this.setting.id.onclick = function (e) {
			e = e || window.event;  //處理跨瀏覽器的兼容性
			self.handle(e)
		};
	}

	// click
	Pagination.prototype.handle = function (e) {
		var target = e.target || e.srcElement;  //處理跨瀏覽器的兼容性，e.target如果你点击了一个按钮，那么 e.target 就会指向这个按钮元素。e.srcElement適用老版本瀏覽器
		if (target.parentNode.className === 'page-item active') {  //如果是點選有變色的a標籤，那就直接return
			return false;
		}

		var pageList = this.setting.id;  //ul標籤
		var items = pageList.querySelectorAll('a');  //ul標籤下的a標籤，會返回陣列
		var len = items.length;  //items陣列的長度，就是showButtons顯示的按鈕數量
		var end = items[len - 1].innerHTML;  // 最後一個按鈕的頁碼
		var num = Number(target.innerHTML);  //轉成數字
		this.cur = num ? num : this.cur;
		var cur = this.cur;  //當前頁面
		var total = this.setting.total;  //總頁數
		var pages = this.setting.showButtons;  //顯示的按鈕數量

		// 點擊頁數按鈕 
		if (target.nodeName === 'A') {  //節點名稱，英文大寫呈現
			switch (true) {
				case (cur == end - 1 && cur != total - 1) || (cur == end && cur == total - 1): // 倒二  每次1頁
					pageList.innerHTML = this.doInit(end - (len - 1), cur - 1);
					break;
				case cur == end && cur != total: // 倒一 每次2頁
					pageList.innerHTML = this.doInit(end - (len - 2), cur - 1);
					break;
				case cur == end - (len - 1) && cur > 2: // 左1 每次2頁
					pageList.innerHTML = this.doInit(end - (len + 2), cur - 1);
					break;
				case (cur == end - (len - 2) && cur != 2) || (cur == end - (len - 1) && cur == 2): // 左2 每次1頁
					pageList.innerHTML = this.doInit(end - (len + 1), cur - 1);
					break;
				default: // 最左2個 最右2個 中間
					if (total > pages) {
						pageList.innerHTML = this.doInit(end - pages, cur - 1);
					} else {
						for (var i = 0; i < len; i++) {
							items[i].parentNode.classList.remove('active');
						}
						e.target.parentNode.classList.add('active');
					}
			}
		}

		//其他按鈕
		switch (target.id) {
			case "prev":  // 上一頁
				this.cur--;
				if (this.cur < end - (len - 3) && this.cur > 2) {
					end--;
				}
				pageList.innerHTML = this.doInit(end - pages, this.cur - 1);
				break;
			case "next":  //下一頁
				this.cur++;
				if (this.cur > end - 2 && this.cur < total - 1) {  //前兩頁和後兩頁不用變
					end++;
				}
				console.log(end + "," + pages + "," + this.cur);
				pageList.innerHTML = this.doInit(end - pages, this.cur - 1);
				break;
			case "first":  //首頁
				this.cur = 1;
				end = pages;
				pageList.innerHTML = this.doInit(end - pages, this.cur - 1);
				break;
			case "last":  // 末頁
				this.cur = total;
				end = total;
				pageList.innerHTML = this.doInit(end - pages, this.cur - 1);
				break;
			case "paginationGo":  //跳轉
				var inputPage = parseInt(document.getElementById("pageInput").value); 

				if (inputPage > 0 && inputPage <= total) { 
					this.cur = inputPage;
					var startIndex = Math.max(0, Math.min(this.cur - Math.ceil(this.setting.showButtons / 2), total - this.setting.showButtons)); 
					if (total <= this.setting.showButtons) {
						startIndex = 0; 
					}
					console.log(startIndex + "," + this.cur);
					pageList.innerHTML = this.doInit(startIndex, this.cur - 1); 
					
				}
				
				break;
			default:
				break;
		}

		this.setting.callback && this.setting.callback(this.cur - 1);  //&& 是從左邊到右邊，回傳第一個是falsy的值，若全部皆為truthy，則回傳最後一個值。 || 是從左邊到右邊，回傳第一個是truthy的值，若全部皆為falsy，則回傳最後一個值。
	}

	window.Pagination = Pagination;
})();