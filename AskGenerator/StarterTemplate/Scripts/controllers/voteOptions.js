app.factory('VoteOptions', ['$http', function ($http) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    return $.post('/home/NgData', { '__RequestVerificationToken': token }, function (data) {
        setTimeout(function () { $('#view').removeClass('loading popover'); }, 500);
        return data;
    }).error(function (data) {
        var response = JSON.parse(data.responseText);
        if (response.url)
            location.assign(response.url);
        else
            console.error(data.responseText);
    });
}]);