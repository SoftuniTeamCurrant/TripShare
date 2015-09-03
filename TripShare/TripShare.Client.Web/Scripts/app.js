var myApp = angular.module('myApp', []);

myApp.controller('mainController', function ($scope, $http) {
    $http.get('http://localhost:54118/api/trips')
        .success(function (result) {
            $scope.cities = result;
        });
});