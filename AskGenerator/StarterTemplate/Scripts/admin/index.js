"use strict";
var Index = (function () {
    var self = {};

    var initRecalculate = function (selector) {
        var link = $(selector);
        var msg = link.next('.msg');
        link.click(function (e) {
            e.preventDefault();
            msg.fadeOut(400);
            link.attr('disabled', true).prop('disabled', true);
            var url = this.href;
            $.get(url, null, function (data) {
                if (data) {
                    msg.html('Success');
                    msg.css('color', 'green');
                }
            }).error(function (err) {
                msg.html(err.status.toString() + ' ' + err.responseText);
                msg.css('color', 'red');
            }).complete(function () {
                link.removeAttr('disabled').prop('disabled', false);
                msg.fadeIn(250);
            });
        });
    };

    self.init = function () {
        initRecalculate('#recalculate');
        initRecalculate('#sendresult');
        Controls.TTips('#sendresult');
    };

    return self;
})();