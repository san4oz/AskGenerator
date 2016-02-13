app.controller('VoteController', ['$scope', 'VoteOptions', function ($scope, $request) {
    $request.success(function (data) {
        $scope.options = [];
        for (var option in data.options) {
            $scope.options.push(new VoteOption(data.options[option], data.teachers));
        }
    });
}]);