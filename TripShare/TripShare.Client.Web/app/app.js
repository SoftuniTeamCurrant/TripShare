var myApp = angular.module('TripShare', ['ngRoute']);

myApp.constant('baseServiceUrl', 'http://localhost:54118/api');

myApp.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/views/login-form.html',
            controller: 'UsersController'
        })
        .when('/home', {
            templateUrl: '/views/home.html',
            controller: 'UsersController'
        })
        .when('/trips/search', {
            templateUrl: '/views/search.html',
            controller: 'UsersController'
        })
        .when('/trips/create', {
            templateUrl: '/views/create.html',
            controller: 'UsersController'
        }).when('/trips/my-trips', {

            templateUrl: 'views/my-trips.html',
            controller: 'UsersController'
        });
    
    $routeProvider.otherwise({ redirectTo: '/' });
});
