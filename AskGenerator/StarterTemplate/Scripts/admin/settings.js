"use strict";
var Pages = Pages || {};
Pages.Settings = (function () {
    var self = {};

    self.initGeneral = function () {
        $('.date').datetimepicker({
            format: 'd/m/Y H:i'
        });
    };

    return self;
})();