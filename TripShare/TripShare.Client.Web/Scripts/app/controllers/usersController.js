myApp.controller('UsersController', function ($scope, $location, usersService) {

    var ClearData = function () {
        $scope.loginData = "";
        $scope.registerData = "";
        $scope.userData = "";
        $scope.passwordData = "";
    };

    // Tested and working
    $scope.login = function () {
        usersService.Login($scope.loginData,
            function (serverData) {
                console.log('Successfully logged in!');
                usersService.SetCredentials(serverData);
                ClearData();
            },
            function (serverError) {
                console.log('Unsuccessful login! <br/>' + serverError.error_description);
                console.log(serverError);
            });
    };

});