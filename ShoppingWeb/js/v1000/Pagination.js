; (function () {
	function Pagination(users) {
		this.setting = {
			id: null,
			total: 21,
			showButtons: 6,
			callback: null
		}

		this.cur = 1; //當前頁碼
		for (var attr in users) {
			this.setting[attr] = users[attr];
		}
		this.setting.id = document.getElementById(this.setting.id);
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

		for (var i = index, lens = pages + index; i < lens; i++) {
			if (i == cur) {
				html += '<li class="page-item active"><a class="page-link" href="javascript:;">' + (i + 1) + '</a></li>';
			} else {
				html += '<li class="page-item"><a class="page-link" href="javascript:;">' + (i + 1) + '</a></li>';
			}
		}

		if (cur == 0 && total > showButtons) {  //顯示末頁和下一頁
			return html + '<li class="page-item"><span id="next" class="page-link"> > </span></li>' + '<li class="page-item"><span id="last" class="page-link"> >| </span></li>';
		} else if (cur == this.setting.total - 1 && total > showButtons) {  //顯示首頁和上一頁
			return '<li class="page-item"><span id="first" class="page-link"> |< </span>' + '</li><li class="page-item"><span id="prev" class="page-link"> < </span></li>' + html;
		} else if (showButtons >= total) {  //只顯示數字按鈕
			return html;
		}

		return '<li class="page-item"><span id="first" class="page-link"> |< </span>' + '<li class="page-item"><span id="prev" class="page-link"> < </span></li>' + html + '<li class="page-item"><span id="next" class="page-link"> > </span></li>' + '<li class="page-item"><span id="last" class="page-link"> >| </span></li>';
	}

	// 渲染
	Pagination.prototype.render = function () {
		var self = this;
		this.setting.id.innerHTML = this.doInit();
		this.setting.id.onclick = function (e) {
			e = e || window.event;
			self.handle(e)
		};
	}

	// click
	Pagination.prototype.handle = function (e) {
		var target = e.target || e.srcElement;
		if (target.parentNode.className === 'page-item active') {  //如果是點選有變色的a標籤，那就直接return
			return false;
		}

		var pageList = this.setting.id;  //ul標籤
		var items = pageList.querySelectorAll('a');  //ul標籤下的a標籤，會返回陣列
		var len = items.length;  //items陣列的長度，就是showButtons顯示的按鈕數量
		var end = items[len - 1].innerHTML;  // 最後一個按鈕的頁碼
		var num = Number(target.innerHTML);  //轉成數字
		this.cur = num ? num : this.cur;
		var cur = this.cur;
		var total = this.setting.total;  //總頁數
		var pages = this.setting.showButtons;  //顯示的按鈕數量

		// 點擊分頁 
		if (target.nodeName === 'A') {  //節點名稱，英文大寫呈現
			// 往右 
			if ((cur == end - 1 && cur != total - 1) || (cur == end && cur == total - 1)) { // 倒二  每次1頁
				pageList.innerHTML = this.doInit(end - (len - 1), cur - 1);
			} else if (cur == end && cur != total) { // 倒一 每次2頁
				pageList.innerHTML = this.doInit(end - (len - 2), cur - 1);
			} else if (cur == end - (len - 1) && cur > 2) { // 左1 每次2頁
				pageList.innerHTML = this.doInit(end - (len + 2), cur - 1);
			} else if ((cur == end - (len - 2) && cur != 2) || (cur == end - (len - 1) && cur == 2)) { // 左2 每次1頁
				pageList.innerHTML = this.doInit(end - (len + 1), cur - 1);
			} else {  // 最左2個 最右2個 中間
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
			default:
				break;
		}

		this.setting.callback && this.setting.callback(this.cur - 1);  //&& 是從左邊到右邊，回傳第一個是falsy的值，若全部皆為truthy，則回傳最後一個值。 || 是從左邊到右邊，回傳第一個是truthy的值，若全部皆為falsy，則回傳最後一個值。
	}

	window.Pagination = Pagination;
})();