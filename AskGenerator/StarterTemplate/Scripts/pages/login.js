"use strict";
var Pages = Pages || {};
Pages.Login = (function () {
    var self = {};

    self.init = function () {
        Controls.TTipAndPopover('.help');
    };
    return self;
})();