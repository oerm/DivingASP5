///<reference path="../../typings/angularjs/angular.d.ts" /> 
var Diving;
(function (Diving) {
    var Services;
    (function (Services) {
        var PaspService = (function () {
            function PaspService($http) {
                this.http = $http;
            }
            PaspService.prototype.GetGeoPoints = function (email, callback) {
                this.http.get('/api/GetDivesWithCoordinates/' + email).success(function (data, status) {
                    callback(data);
                }).error(function (error) {
                    callback(error);
                });
            };
            PaspService.prototype.GetPhotosIds = function (email, diveId, callback) {
                this.http.get('GetPhotoIds/' + email + '/' + diveId).success(function (data, status) {
                    callback(data);
                }).error(function (error) {
                    callback(error);
                });
            };
            PaspService.prototype.GetPhoto = function (email, photoId, callback) {
                this.http.get('GetPhoto/' + email + '/' + photoId).success(function (data, status) {
                    callback(data);
                }).error(function (error) {
                    callback(error);
                });
            };
            return PaspService;
        })();
        Services.PaspService = PaspService;
    })(Services = Diving.Services || (Diving.Services = {}));
})(Diving || (Diving = {}));
