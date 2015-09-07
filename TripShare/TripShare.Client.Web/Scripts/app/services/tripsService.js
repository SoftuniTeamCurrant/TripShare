'use strict';

myApp.factory('tripsService', function ($http, baseServiceUrl, usersService) {
    var service = {};

    var serviceUrl = baseServiceUrl + '/trips';

    service.getTripsSearchData = function (routeParams, success, error) {
        var date = routeParams.date ? '&DepartureDate='+routeParams.date : '';
        $http.get(serviceUrl + '/search' + 
            '?DepartureCity=' + routeParams.fromCity +
            '&ArrivalCity=' + routeParams.toCity
             + date, { headers: usersService.GetHeaders() })
            .success(function (data, status, headers, config) {
                success(data);
            }).error(error);
    }

    return service;
});