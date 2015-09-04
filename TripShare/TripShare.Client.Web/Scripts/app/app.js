var myApp = angular.module('TripShare', ['ngRoute']);

myApp.constant('baseServiceUrl', 'http://localhost:54118/api')

myApp.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/content/partials/login-form.html',
            controller: 'UsersController'
        });

    $routeProvider.otherwise({ redirectTo: '/' });
});
