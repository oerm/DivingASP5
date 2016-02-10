///<reference path="../../typings/angularjs/angular.d.ts" /> 
var Diving;
(function (Diving) {
    var Services;
    (function (Services) {
        var DataService = (function () {
            function DataService($http) {
                this.http = $http;
            }
            DataService.prototype.GetGeoPoints = function (email, callback) {
                this.http.get('/api/dives/getdiveswithcoordinates/' + email, {
                    cache: false
                }).success(function (data, status) {
                    callback(data);
                }).error(function (error) {
                    callback(error);
                });
            };
            DataService.prototype.GetPhotosIds = function (email, diveId, minPhoto, callback) {
                this.http.get('/api/dives/getuserphotoidsbydiveids/' + email + '/' + diveId + '/' + minPhoto).success(function (data, status) {
                    callback(data);
                }).error(function (error) {
                    callback(error);
                });
            };
            DataService.prototype.GetPhoto = function (email, photoId, callback) {
                this.http.get('/GetPhoto/' + email + '/' + photoId).success(function (data, status) {
                    callback(data);
                }).error(function (error) {
                    callback(error);
                });
            };
            DataService.prototype.GetAuthorizedUserDives = function (callback) {
                var req = {
                    method: 'GET',
                    url: '/api/dives/getuserdives/' + Date.now(),
                    cache: false,
                    headers: { 'Cache-Control': 'no-cache' }
                };
                this.http(req).success(function (data, status) {
                    callback(data);
                }).error(function (error) {
                    callback(error);
                });
            };
            DataService.prototype.GetAuthorizedUserDiveById = function (diveId, callback) {
                this.http.get('/api/dives/getuserdivebyid/' + diveId + '/' + Date.now(), {
                    cache: false
                }).success(function (data, status) {
                    callback(data);
                }).error(function (error) {
                    callback(error);
                });
            };
            DataService.prototype.GetDiveDictionaries = function (callback) {
                this.http.get('/api/dives/getdivedictionaries').success(function (data, status) {
                    callback(data);
                }).error(function (error) {
                    callback(error);
                });
            };
            DataService.prototype.SaveDive = function (dive, callback, errorHandler) {
                var req = {
                    method: 'POST',
                    url: '/api/dives/saveDive',
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    },
                    data: $.param(dive)
                };
                this.http(req).success(function (data, status) {
                    callback(data);
                }).error(function (error) {
                    errorHandler(error);
                });
            };
            DataService.prototype.DeleteDive = function (diveId, callback) {
                this.http.delete('/api/dives/deleteDive/' + diveId).success(function (data, status) {
                    callback(data);
                }).error(function (error) {
                    callback(error);
                });
            };
            return DataService;
        })();
        Services.DataService = DataService;
    })(Services = Diving.Services || (Diving.Services = {}));
})(Diving || (Diving = {}));
