"use strict";
var Pages = Pages || {};
Pages.Info = (function () {
    var self = {};

    var initTabs = function () {
        var page = $('#infoPage');
        var tabs = $('.infoPageWrapper .nav-tabs li');

        var loadHtml = function (url, target) {
            tabs.removeClass('active');
            target.addClass('active');

            page.empty();
            page.addClass('loading');
            $.post(url, null, function (data) {
                page.removeClass('loading');
                page.html(data);
            });
        };

        tabs.click(function (e) {
            var target = $(this);
            var url = target.find('a').attr('href');
            if (window.history.pushState) {
                loadHtml(url, target);

                var title = $('title').text();
                window.history.pushState({ url: location.pathname }, title, url);
            } else {
                location.href = url;
            }
        });

        window.onpopstate = function (event) {
            if (event.state.url) {
                var tab = tabs.find('a[href="' + event.state.url + '"').parent();
                if (tab.length) {
                    loadHtml(event.state.url, tab);
                    return;
                }
            }
            location.href = document.location;
        };
    };

    self.init = function () {
        initTabs();
    };
    return self;
})();