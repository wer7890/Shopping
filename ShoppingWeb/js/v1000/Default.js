let translations = {
    'title1Default': {
        'zh': '歡迎登入後台系統',
        'en': 'Welcome to the backend system'
    }
};

$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Ajax/UserHandler.aspx/GetLanguage",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let language = response.d;

            $('.i18n').each(function () {
                var key = $(this).data('key');
                var placeholderKey = $(this).data('placeholder-key');

                if (key) {
                    let translation = translations[key][language];
                    $(this).text(translation);
                }

                if (placeholderKey) {
                    let placeholderTranslation = translations[placeholderKey][language];
                    $(this).attr('placeholder', placeholderTranslation);
                }
            });
        },
        error: function (error) {
            console.error('AJAX Error:', error);
        }
    });
});
