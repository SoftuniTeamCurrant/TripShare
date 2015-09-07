var myApp = angular.module('TripShare', ['ngRoute']);

myApp.constant('baseServiceUrl', 'http://localhost:54118/api')

myApp.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/content/partials/login-form.html',
            controller: 'UsersController'
        })
        .when('/home', {
            templateUrl: '/content/partials/home.html',
            controller: 'UsersController'
        })
        .when('/trips/search', {
            templateUrl: '/content/partials/search.html',
            controller: 'UsersController'
        })

    $routeProvider.otherwise({ redirectTo: '/' });
});
