"use strict";
var Pages = Pages || {};
Pages.Team = (function () {
    var self = {};

    var initSelection = function () {
        var teamBox = $("#team");
        teamBox.select2({
            placeholder: "Виберіть кафедру",
            width: 400
        });
        teamBox.on('change', function () {
            var value = teamBox.val();
            if (value) {
                var index = location.href.indexOf('=');
                location.href = location.href.substring(teamBox, index + 1) + value;
            }
        });
    };

    self.init = function () {
        initSelection();
        Controls.TTipAndPopover('a[data-toggle]');
    };
    return self;
})();