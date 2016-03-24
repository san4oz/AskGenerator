"use strict";
var Pages = Pages || {};
Pages.Team = (function () {
    var self = {};

    self.init = function () {
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
    return self;
})();