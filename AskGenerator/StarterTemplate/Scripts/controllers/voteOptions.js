app.factory('VoteOptions', ['$http', function ($http) {
    return $http.post('/home/NgData')
        .success(function (data) {
            return data;
        })
        .error(function (data) {
            return data;
        })
}]);