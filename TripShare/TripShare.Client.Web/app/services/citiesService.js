'use strict';

myApp.factory('citiesService', function ($http, baseServiceUrl, usersService) {
    var service = {};

    var serviceUrl = baseServiceUrl + '/cities';

    service.getAllCities = function (success, error) {
            $http.get(serviceUrl, { headers: usersService.GetHeaders() })
                .success(function (data) {
                    success(data);
                })
                .error(error);
    }

    return service;
});