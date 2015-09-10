myApp.controller('UsersController', function ($scope, $location, $routeParams, $route, usersService, tripsService, citiesService) {

    var isLocationPathHome = $location.path() === "/";
    var isUserLoggedIn = usersService.isLoggedIn();
    $scope.isLoggedIn = isUserLoggedIn;
    $scope.cities = {};

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
                $scope.UserName = serverData["userName"];
                usersService.SetCredentials(serverData);
                ClearData();
                $location.path('/home');
            },
            function (serverError) {
                console.log('Unsuccessful login! <br/>' + serverError.error_description);
                console.log(serverError);

            });
    };

    $scope.logout = function () {
        usersService.Logout(
            function () {
                console.log("Successfully logged out!");
                usersService.ClearCredentials();
                $location.path('/#')
            },
            function (serverError) {
                console.log('Unsuccessful logout!');
            });
    };

    $scope.searchRedirect = function () {
        var data = $scope.searchData;
        $location.path('/trips/search');
        if (data) {
            $location.search({ fromCity: data.fromCity, toCity: data.toCity, date: data.date ? data.date.yyyymmdd() : null });
        }
    }
    //TODO createRedirect

    $scope.displaySearchData = function () {
        tripsService.getTripsSearchData($routeParams, function (data) {
            data.forEach(function (trip) {
                trip["isJoined"] = false;
                trip["isOwner"] = false;
                if (trip["DriverName"] === localStorage["userName"]) {
                    trip["isOwner"] = true;
                }
                trip["Passengers"].forEach(function (passanger) {
                    if (passanger["UserName"] == localStorage["userName"]) {
                        trip["isJoined"] = true;
                    }
                });
            });
            if (data.length) {
                $scope.searchTripData = data;
            }
            console.log(data);
        },
        function (err) {
            console.log(err);
        });
    }

    $scope.displayMyTrips = function () {
        tripsService.getMyTrips(function (data) {
            data.forEach(function (trip) {
                trip["isJoined"] = false;
                trip["isOwner"] = false;
                if (trip["DriverName"] === localStorage["userName"]) {
                    trip["isOwner"] = true;
                }
                trip["Passengers"].forEach(function (passanger) {
                    if (passanger["UserName"] == localStorage["userName"]) {
                        trip["isJoined"] = true;
                    }
                });
            });
            if (data) {
                $scope.myTripsData = data;
            }
            console.log(data);
        },
        function (err) {
            console.log(err);
        });
    }

    $scope.joinTrip = function (id) {
        tripsService.joinTrip(id,
            function (data) {
                console.log("You have joined successfully");
                $route.reload();
            },
            function (err) {
                console.log(err.responseText);
            });
    }

    $scope.deleteTrip = function(id)
    {
        tripsService.deleteTrip(id,
            function(data) {
                console.log("You have deleted trip with id=" + id + " successfully");
                $route.reload();
            },
            function(err) {
                console.log(err.responseText);
            });
    }

    $scope.leaveTrip = function (id) {
        tripsService.leaveTrip(id,
            function (data) {
                console.log("You have left successfully");
                $route.reload();
            },
            function (err) {
                console.log(err.responseText);
            });
    }

    $scope.kick = function(id, userId) {
        tripsService.kick(id, userId, function(data) {
            console.log(data);
            console.log("successfull kick");
            $route.reload();
        },
        function(err) {
            console.log(err);
        })
    }

    var getAllCities = function () {
        citiesService.getAllCities(function (data) {
            $scope.cities = data;
        }, function (err) {
            console.log(err);
        });
    }

    if ($location.path() != '/') {
        $(document).ready(getAllCities());
    }

    $scope.addComment = function (id, content) {
        var data = {
            Content: content
        }
        tripsService.addComment(id, data, function(data) {
                console.log("commented!");
                $route.reload();
            },
            function(err) {
                console.log(err);
            });
    }

    $scope.createTrip = function () {
        var data = {};

        var title = $scope.createTripData.Title;
        var departureCity = $scope.createTripData.fromCity;
        var arrivalCity = $scope.createTripData.toCity;
        var seats = $scope.createTripData.avaiableSeats;
        var description = $scope.createTripData.Description;
        var date = new Date($scope.createTripData.date);

        if (date == 'Invalid Date') {
            data = {
                Title: title,
                ArrivalCityId: arrivalCity,
                DepartureCityId: departureCity,
                AvaibleSeats: seats,
                Description: description
            }
        }
        else {
            data = {
                Title: title,
                ArrivalCityId: arrivalCity,
                DepartureCityId: departureCity,
                AvaibleSeats: seats,
                Description: description,
                DepartureDate: date.yyyymmdd()
            }
        };

        tripsService.postTrip(data, function (data) {
            console.log(data);
        },
        function (error) {
            console.log(date);
            console.log(error);
        });
    };


    $scope.navTemplate = '/views/nav.html';
    $scope.tripsTemplate = '/views/trips-view-tpl.html';

    Date.prototype.yyyymmdd = function () {
        var yyyy = this.getFullYear().toString();
        var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
        var dd = this.getDate().toString();
        return yyyy + '.' + (mm[1] ? mm : "0" + mm[0]) + '.' + (dd[1] ? dd : "0" + dd[0]); // padding
    };
});