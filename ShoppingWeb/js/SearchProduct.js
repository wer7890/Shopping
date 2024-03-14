﻿$(document).ready(function () {
    // 使用 AJAX 從後端取得資料
    $.ajax({
        url: '/Ajax/SearchProductHandler.aspx/GetUserData',
        type: 'POST',
        contentType: 'application/json',
        success: function (response) {
            // 處理成功取得資料的情況
            var data = JSON.parse(response.d); // 解析 JSON 資料為 JavaScript 物件
            var tableBody = $('#tableBody');

            // 清空表格內容
            tableBody.empty();

            // 動態生成表格內容
            $.each(data, function (index, item) {
                var row = '<tr>' +
                    '<td>' + item.f_id + '</td>' +
                    '<td>' + item.f_productName + '</td>' +
                    '<td>' + item.f_productCategory + '</td>' +
                    '<td>' + item.f_productPrice + '</td>' +
                    '<td>' + item.f_productStock + '</td>' +
                    '<td>' + item.f_productIsOpen + '</td>' +
                    '<td>' + item.f_productOwner + '</td>' +
                    '<td>' + item.f_productCreatedOn + '</td>' +
                    '<td>' + item.f_productIntroduce + '</td>' +
                    '<td>' + item.f_productImg + '</td>' +
                    '<td><button class="btn btn-primary" onclick="editUser(' + item.f_id + ')">編輯</button></td>' +
                    '<td><button class="btn btn-danger" onclick="deleteUser(' + item.f_id + ')">刪除</button></td>' +
                    '</tr>';

                tableBody.append(row);
            });
        },
        error: function (xhr, status, error) {
            // 處理發生錯誤的情況
            console.error('Error:', error);
        }
    });

})