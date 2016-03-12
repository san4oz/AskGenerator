var GTM = (function () {
    var self = {};

    Object.defineProperty(self, 'isAvaliable', {
        get: function () {
            return !!dataLayer;
        }
    });

    self.trackVoteClick = function (isSelectedPrev) {
        if (!self.isAvaliable)
            return;
        if (!isSelectedPrev)
            isSelectedPrev = false;
        dataLayer.push({
            'event': 'voteClick',
            'value': 'isSelectedPrev'
        });
    };

    self.trackVote = function (isSelectedPrev) {
        if (!self.isAvaliable)
            return;
        if (!isSelectedPrev)
            isSelectedPrev = false;
        dataLayer.push({
            'event': 'vote',
            'value': 'isSelectedPrev'
        });
    };

    return self;
})();