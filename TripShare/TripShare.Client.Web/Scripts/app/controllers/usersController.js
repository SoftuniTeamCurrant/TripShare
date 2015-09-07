myApp.controller('UsersController', function ($scope, $location, $routeParams, usersService, tripsService) {

    var isLocationPathHome = $location.path() === "/";
    var isUserLoggedIn = usersService.isLoggedIn();

    if (!isUserLoggedIn) {
        $location.path('/');
    }

    if (isUserLoggedIn && isLocationPathHome) {
        $location.path('/home');
    }

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
                $location.path('/home');
            },
            function (serverError) {
                console.log('Unsuccessful login! <br/>' + serverError.error_description);
                console.log(serverError);

            });
    };

    $scope.searchRedirect = function () {
        var data = $scope.searchData;
        $location.path('/trips/search');
        if (data) {
            $location.search({ fromCity: data.fromCity, toCity: data.toCity, date: data.date ? data.date.yyyymmdd() : null });
        }
    }

    $scope.displaySearchData = function() {
        tripsService.getTripsSearchData($routeParams, function(data) {
            if (data.length) {
                $scope.searchTripData = data;
            }
        },
        function(err) {
            console.log(err);
        });
    }

    $scope.createTrip = function () {
        var data = $scope.createTripData;
        tripsService.postTrip(data, function(data) {
                console.log(data);
            },
            function(error) {
                console.log(error);
            });
        $scope.createTripData = '';
    }

    Date.prototype.yyyymmdd = function () {
        var yyyy = this.getFullYear().toString();
        var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
        var dd = this.getDate().toString();
        return yyyy + '.' + (mm[1] ? mm : "0" + mm[0]) + '.' + (dd[1] ? dd : "0" + dd[0]); // padding
    };
});