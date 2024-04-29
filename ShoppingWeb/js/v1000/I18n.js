function translateElements(language) {
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
}