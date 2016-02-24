app.factory('VoteOptions', ['$http', function ($http) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    return $http.post('/home/NgData', { '__RequestVerificationToken': token })
        .success(function (data) {
            $('#view').removeClass('loading popover');
            return data;
        })
        .error(function (data) {
            if (data.url)
                location.assign(data.url);
            return data;
        })
}]);