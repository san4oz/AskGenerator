"use strict";
var Pages = Pages || {};
Pages.Team = (function () {
    var self = {};

    var initMeters = function () {
        $('.meter').each(function () {
            var $this = $(this);
            $this.jQMeter({
                goal: $this.data('goal').toString(),
                raised: $this.data('raised').toString(),
                meterOrientation: 'vertical',
                width: '20px',
                height: '100px',
                displayTotal: false,
                bgColor: 'none'
            });
        });
    };

    self.init = function () {
        initMeters();
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