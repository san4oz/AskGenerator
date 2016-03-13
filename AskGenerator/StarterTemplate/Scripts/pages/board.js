"use strict";
var Pages = Pages || {};
Pages.Board = (function () {
    var self = {};
    Object.defineProperty(self, 'rootSelector', { value: '#teacherBoard' });
    
    self.loadBoard = function () {
        var root = $(self.rootSelector);
        var url = root.data('src');
        if (url) {
            $.post(url, undefined, function (data) {
                if (data) {
                    root.html(data);
                    Controls.TTips('[data-toggle="tooltip"]');
                    var popover = $('#details-popover');
                    $('.teacher-tile').each(function () {
                        Controls.HtmlPopover(this, function () {
                            var data = $(this).data('popover');
                            for (var i = 0; i < data.length; i++) {
                                var badge = data[i];
                                $('#' + badge.Id + 'm').html(badge.Mark);
                            }
                            return popover.html();
                        });
                    });
                }
            });
        }
    };

    self.init = function () {
        self.loadBoard();
        var helps = $('.help');
        helps.each(function () {
            var h = $(this);
            Controls.TTips(h, h.data('content'));
            Controls.Popover(h, function (e) {
                h.tooltip('destroy');
            }, function (e) {
                Controls.TTips(h, h.data('content'));
            });
        });
    };
    return self;
})();