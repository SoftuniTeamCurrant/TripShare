var myApp = angular.module('myApp', []);

myApp.controller('mainController', function ($scope, $http) {
    $scope.function = $http.get('http://localhost:54118/api/trips')
        .success(function (result) {
            $scope.cities = result;
        });

    $scope.login = function () {
        var loginData = $scope.LoginData;
        loginData['grant_type'] = 'password';
        $http.post('http://localhost:54118/token', loginData, { headers: { "Content-Type": "application/x-www-form-urlencoded" } })
            .success(function(result) {
                $scope.UserData = result;
            })
            .error(function(err) {
                console.log(err);
            });
    }
});