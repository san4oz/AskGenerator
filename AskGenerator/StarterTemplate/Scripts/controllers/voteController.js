app.controller('VoteController', ['$scope', '$http', 'VoteOptions', function ($scope, $http, $request) {
    $request.success(function (data) {
        $scope.options = [];
        if (!data.teachers || !data.teachers.length)
        {
            $('#view').remove();
            $('#TeachersVote .no-teachers').show(100);
        }
        $scope.$apply(function () {
            for (var option in data.options) {
                $scope.options.push(new VoteOption(data.options[option], data.teachers, $http));
            }
        });
    });
}]);