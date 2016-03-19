"use strict";
var Pages = Pages || {};
Pages.Board = (function () {
    var self = {};
    Object.defineProperty(self, 'rootSelector', { value: '#teacherBoard' });

    self.loadBoard = function (callback) {
        var root = $(self.rootSelector);
        var url = root.data('src');
        if (url) {
            $.post(url, undefined, function (data) {
                if (data) {
                    root.html(data);

                    var dataFromContainer = $('#data-from');
                    var dataFrom = dataFromContainer.html();
                    dataFromContainer.remove();
                    if (dataFrom)
                        $('#data-from-wrapper').html(dataFrom);
                    Controls.TTips('[data-toggle="tooltip"]');

                    if (callback)
                        callback();
                }
            });
        }
    };

    self.init = function () {
        self.loadBoard(function () {
            var popover = $('#details-popover');
            $('.teacher-tile').each(function () {
                Controls.HtmlPopover(this, function () {
                    var data = $(this).data('popover');
                    popover.find('td[id]').html(' <span class="h4"> - </span>');
                    for (var i = 0; i < data.length; i++) {
                        var badge = data[i];
                        $('#' + badge.Id + 'm').html(badge.Mark);
                    }
                    return popover.html();
                });
            });
        });
        Controls.TTipAndPopover('.help');
    };

    var getTimeRemaining = function getTimeRemaining(endtime) {
        var t = endtime - Date.parse(new Date());
        return {
            'total': t,
            'days': Math.floor(t / (1000 * 60 * 60 * 24)),
            'hours': Math.floor((t / (1000 * 60 * 60)) % 24),
            'minutes': Math.floor((t / 1000 / 60) % 60),
            'seconds': Math.floor((t / 1000) % 60)
        };
    };

    self.initRemainingClock = function (selector, endtimeString) {
        var clock = $(selector);
        var endtime = Date.parse(endtimeString);
        var timeinterval
        var alingn = function (u) { return ('0' + u).slice(-2); };
        var update = function () {
            var t = getTimeRemaining(endtime);
            clock.find('.days').html(alingn(t.days));
            clock.find('.hours').html(alingn(t.hours));
            clock.find('.minutes').html(alingn(t.minutes));
            clock.find('.seconds').html(alingn(t.seconds));
            if (t.total <= 0) {
                clearInterval(timeinterval);
            }
        };
        update();
        timeinterval = setInterval(update, 1000);
    };

    self.initSubscibe = function () {
        var form = $('.subscribe');
        form.validate();
        form.removeAttr('novalidate').removeProp('novalidate');
        form.on('submit', function (e) {
            e.preventDefault();
            var url = form.attr('action');
            if (!url)
                url = 'home/subscribe';
            
            var data = form.serialize();
            form.prop('disabled', true).attr('disabled', 'disabled');
            $.post(url, data, function (resp) {
                if (resp) {
                    form.find('.form-group div').html('<span style="color:green;"><i style="color:black;">' + resp + '</i> додано </span>');
                }
            }).error(function () {
                form.find('input').removeClass('valid').addClass('input-validation-error');
            }).complete(function () {
                form.removeProp('disabled').removeAttr('disabled');
            });
        });
    };

    return self;
})();