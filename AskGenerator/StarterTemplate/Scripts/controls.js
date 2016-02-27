﻿var Controls = (function () {
    var self = {};
    var token = $('input[name="__RequestVerificationToken"]').val();

    self.TTips = function (selector, title) {
        if (title)
            $(selector).tooltip({ title: title });
        else
            $(selector).tooltip();
    };

    self.Popover = function (selector, shown, hidden) {
        selector = $(selector);
        selector.popover({ animation: false, placement: 'top', delay: 100, container: 'body', trigger: 'focus' });
        selector.on('click', function (e) {
            e.preventDefault();
        });
        
        if (shown && typeof (shown) === 'function')
            selector.on('shown.bs.popover', shown);
        if (hidden && typeof (hidden) === 'function')
            selector.on('hidden.bs.popover', hidden);
    };

    self.initDelete = function () {
        $('.delete').click(function (e) {
            e.preventDefault();
            var target = $(this);
            var id = target.data('id'),
                href = this.href;
            var data;
            data = token ? { '__RequestVerificationToken': token } : {};
            data.id = id;
            $.ajax(href, {
                method: 'post', data: data,
                success: function (d) {
                    if (d) {
                        var line = target.parents('tr, .raw, li');
                        if (line.length > 0)
                            line = $(line[0]);
                        line.animate({ height: 0, opacity: 0 }, 250, function () { line.remove() });
                    }
                }, error: function (data) {
                    if (data.url)
                        location.assign(data.url);
                    return data;
                }
            });
        });
    };
    return self;
})();

$(function () {
    Controls.initDelete();
});