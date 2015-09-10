'use strict';

myApp.factory('usersService', function ($http, baseServiceUrl) {
    var service = {};

    service.Login = function (loginData, success, error) {
        $http.post(baseServiceUrl.replace('/api', '/token'), 'username=' + loginData.username +
            '&password=' + loginData.password + '&grant_type=password',
            { headers: { "Content-Type": "application/x-www-form-urlencoded" } })
            .success(function (data, status, headers, config) {
                success(data);
            }).error(error);
    };

    service.Logout = function (success, error) {
        $http.post(baseServiceUrl + "/account/logout", {}, {
                headers: {
                    Authorization: "bearer " + localStorage['accessToken'],
                    "Content-Type": "application/x-www-form-urlencoded"
                }
            }).success(success)
            .error(error);
    }

    service.Register = function (registerData, success, error) {
        $http.post(baseServiceUrl + '/account/register', 'Username=' + registerData.username +
            '&email=' + registerData.email +
            '&password=' + registerData.password +
            '&confirmPassword=' + registerData.confirm_pass,
            { headers: { "Content-Type": "application/x-www-form-urlencoded" } })
            .success(function(data, status, headers, config) {
                success(data);
            }).error(error);
    }

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