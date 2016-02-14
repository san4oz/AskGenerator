var onloadCallback = function () {
    var form = $('#form');
    form.submit(false);
    grecaptcha.render('g-recaptcha', {
        'sitekey': '6LdmoRcTAAAAAIDuxf0IQd8jNceT0hWsjXPysHiv',
        'callback': function () {
            form.unbind('submit', false);
            $('#sendBtn').attr('disabled', false);
        }
    });
};