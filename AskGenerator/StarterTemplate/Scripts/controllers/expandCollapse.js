app.directive('dExpandCollapse', function () {
    return {
        'restrict': 'EA',
        link: function (scope, element, attrs) {
            $(element).find('.option').click(function () {
                $(element).find(".answer").slideToggle('200', function () {
                    $(element).find(".answer").toggleClass('faqPlus faqMinus');
                });
            }
            )
        }
    }
});