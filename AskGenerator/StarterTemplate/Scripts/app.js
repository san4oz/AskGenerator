var app = angular.module('TeachersVote', [
    'ngRoute'
]);

app.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'home/view',
                controller: 'VoteController'
            }).otherwise({
                redirectTo: '/'
            });
    }
]);