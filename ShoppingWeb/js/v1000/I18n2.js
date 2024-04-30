function TranslateLanguage() {
    $.ajax({
        type: "POST",
        url: "/Ajax/UserHandler.aspx/GetLanguage",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let language = response.d;
            $('.i18n').each(function () {
                let key = $(this).data('key');
                let placeholderKey = $(this).data('placeholder-key');

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
}