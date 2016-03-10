"use strict";
var Pages = Pages || {};
Pages.Students = (function () {
    var self = {};
    var token = $('input[name="__RequestVerificationToken"]').val();

    var initRegenerating = function () {
        $('#regenerateKeys').click(function (e) {
            e.preventDefault();
            if (confirm('This will reset all login keys! Continue?')) {
                var url = this.href, btn = $(this);
                btn.attr('disabled', 'disabled');
                $.post(url, { '__RequestVerificationToken': token }).fail(function (data) {
                    btn.attr('title', JSON.stringify(data));
                    btn.removeAttr('disabled');
                });
            }
        });
    };

    self.init = function () {
        initRegenerating();
    };

    return self;
})();