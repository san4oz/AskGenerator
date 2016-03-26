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
                msg.html(err.status);
                msg.css('color', 'red');
            }).complete(function () {
                msg.fadeIn(250);
            });
        });
    };

    self.init = function () {
        initRecalculate('#recalculate');
        initRecalculate('#sendresult');
    };

    return self;
})();