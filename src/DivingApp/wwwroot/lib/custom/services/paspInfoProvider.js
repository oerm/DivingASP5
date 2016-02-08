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
                this.http.get('/api/getdiveswithcoordinates/' + email).success(function (data, status) {
                    callback(data);
                }).error(function (error) {
                    callback(error);
                });
            };
            DataService.prototype.GetPhotosIds = function (email, diveId, minPhoto, callback) {
                this.http.get('/api/getuserphotoidsbydiveids/' + email + '/' + diveId + '/' + minPhoto).success(function (data, status) {
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
                this.http.get('/api/getuserdives').success(function (data, status) {
                    callback(data);
                }).error(function (error) {
                    callback(error);
                });
            };
            DataService.prototype.GetAuthorizedUserDiveById = function (diveId, callback) {
                this.http.get('/api/getuserdivebyid/' + diveId).success(function (data, status) {
                    callback(data);
                }).error(function (error) {
                    callback(error);
                });
            };
            DataService.prototype.GetDiveDictionaries = function (callback) {
                this.http.get('/api/getdivedictionaries').success(function (data, status) {
                    callback(data);
                }).error(function (error) {
                    callback(error);
                });
            };
            DataService.prototype.SaveDive = function (dive, callback) {
                var req = {
                    method: 'POST',
                    url: '/api/dives/saveDive',
                    headers: {
                        'Content-Type': undefined
                    },
                    data: dive
                };
                this.http(req).success(function (data, status) {
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
