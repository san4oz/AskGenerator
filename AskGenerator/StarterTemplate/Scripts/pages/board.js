﻿"use strict";
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
                }
            });
        }
    };

    self.init = function () {
        self.loadBoard();
        Controls.Popover('[data-toggle="popover"]');
    };
    return self;
})();