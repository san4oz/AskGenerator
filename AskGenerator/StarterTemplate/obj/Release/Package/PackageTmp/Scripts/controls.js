$(function () {
    var token = $('input[name="__RequestVerificationToken"]').val();
    $('.delete').click(function (e) {
        e.preventDefault();
        var target = $(this);
        var id = target.data('id'),
            href = this.href;
        var data;
        data = token ? { '__RequestVerificationToken': token } : {};
        data.id = id;
        $.ajax(href, {
            method: 'post', data: data,
            success: function (d) {
                if (d) {
                    var line = target.parents('tr, .raw, li');
                    if (line.length > 0)
                        line = $(line[0]);
                    line.animate({ height: 0, opacity: 0 }, 250, function () { line.remove() });
                }
            }, error: function (data) {
                if (data.url)
                    location.assign(data.url);
                return data;
            }
        });
    });
});