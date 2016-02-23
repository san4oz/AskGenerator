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
                if (data)
                    root.html(data);
            });
        }
    };

    self.init = function () {
        self.loadBoard();
    };
    return self;
})();