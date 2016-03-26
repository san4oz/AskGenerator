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
                if (index == -1) location.href = location.href + '?id=' + value;
                else location.href = location.href.substring(teamBox, index + 1) + value;
            }
        });
    };

    self.init = function () {
        initSelection();
        Controls.TTipAndPopover('a[data-toggle]');
        Controls.TTips('.badges [data-toggle="tooltip"]');
        var popover = $('#details-popover');
        $('.teacher-tile').each(function () {
            Controls.HtmlPopover(this, function () {
                var data = $(this).data('popover');
                popover.find('td[id]').html(' <span class="h4"> - </span>');

                for (var i = 0; i < data.length; i++) {
                    var badge = data[i];
                    $('#' + badge.Id + 'm').html(badge.Mark);
                }

                var votesCount = $(this).data('votes-count');
                if (votesCount !== undefined) $('#votesCount').html(votesCount);

                return popover.html();
            });
        });
    };
    return self;
})();