'use strict';

myApp.factory('tripsService', function ($http, baseServiceUrl, usersService) {
    var service = {};

    var serviceUrl = baseServiceUrl + '/trips';

    service.getTripsSearchData = function (routeParams, success, error) {
        if (routeParams.fromCity && routeParams.toCity) {
            var date = routeParams.date ? '&DepartureDate=' + routeParams.date : '';
            $http.get(serviceUrl + '/search' +
                    '?DepartureCity=' + routeParams.fromCity +
                    '&ArrivalCity=' + routeParams.toCity
                    + date, { headers: usersService.GetHeaders() })
                .success(function(data, status, headers, config) {
                    success(data);
                }).error(error);
        } else {
            $http.get(serviceUrl, { headers: usersService.GetHeaders() })
                .success(function(data, status, headers, config) {
                    success(data);
                })
                .error(error);
        }
    }

    service.getMyTrips = function (success, error) {
        $http.get(serviceUrl + '/my-trips', {
            headers: usersService.GetHeaders()
        }).success(function (data, headers, config, status) {
            success(data);
        }).error(error);
    }

    service.postTrip = function(data, success, error) {
        $http.post(serviceUrl, data, { headers: usersService.GetHeaders() })
            .success(function(data, status, headers, config) {
                success(data);
            })
            .error(error);
    }


    service.joinTrip = function(id, success, error) {
        $http.put(serviceUrl + "/" + id + "/join", null, { headers: usersService.GetHeaders() })
            .success(function (data, status, headers, config) {
                success(data);
            })
            .error(error);
    }

    service.leaveTrip = function (id, success, error) {
        $http.put(serviceUrl + "/" + id + "/leave", null, { headers: usersService.GetHeaders() })
            .success(function (data, status, headers, config) {
                success(data);
            })
            .error(error);
    }

    service.kick = function(id, userId, success, error) {
        $http.put(serviceUrl + "/" + id + "/kick/" + userId, null, { headers: usersService.GetHeaders() })
            .success(function (data, status, headers, config) {
                success(data);
            })
            .error(error);
    }

    service.addComment = function(id, data, success, error) {
        $http.post(serviceUrl + "/" + id + "/comments", data, { headers: usersService.GetHeaders() })
            .success(function (data, status, headers, config) {
                success(data);
            })
            .error(error);
    }

    return service;
});