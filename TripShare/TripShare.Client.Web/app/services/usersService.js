'use strict';

myApp.factory('usersService', function ($http, baseServiceUrl) {
    var service = {};

    var serviceUrl = baseServiceUrl + '/users';

    service.Login = function (loginData, success, error) {
        $http.post(baseServiceUrl.replace('/api', '/token'), 'username=' + loginData.username +
            '&password=' + loginData.password + '&grant_type=password',
            { headers: { "Content-Type": "application/x-www-form-urlencoded" } })
            .success(function (data, status, headers, config) {
                success(data);
            }).error(error);
    };

    service.SetCredentials = function (serverData) {
        localStorage['accessToken'] = serverData.access_token;
        localStorage['userName'] = serverData.userName;
    };

    service.GetUsername = function () {
        return localStorage['userName'];
    };

    service.ClearCredentials = function () {
        localStorage.clear();
    };

    service.GetHeaders = function () {
        return {
            Authorization: "Bearer " + localStorage['accessToken']
        };
    };

    service.isLoggedIn = function () {
        return localStorage['accessToken'];
    };

    return service;
});