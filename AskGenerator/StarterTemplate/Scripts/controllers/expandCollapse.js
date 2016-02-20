app.directive('dExpandCollapse', function () {
    return {
        'restrict': 'EA',
        link: function (scope, element, attrs) {
            $(element).find('.option').click(function (e) {
                e.preventDefault();
                $(element).find(".answer").slideToggle('200', function () {
                    $(element).find(".answer").toggleClass('faqPlus faqMinus');
                });
            });
        }
    };
});