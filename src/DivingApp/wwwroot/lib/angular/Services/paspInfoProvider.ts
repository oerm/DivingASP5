///<reference path="../../typings/angularjs/angular.d.ts" /> 

module Diving.Services {
    export class PaspService implements IPaspDataService {

        private http;

        constructor($http: ng.IHttpService) {
            this.http = $http;            
        }

        GetGeoPoints(email: string, callback: Function) {
            this.http.get('/api/GetDivesWithCoordinates/' + email).success((data, status) => {
                callback(data);
            }).error(error => {
                callback(error);
            });
        }

        GetPhotosIds(email: string, diveId: string, callback: Function) {
            this.http.get('GetPhotoIds/' + email + '/' + diveId).success((data, status) => {
                callback(data);
            }).error(error => {
                callback(error);
            });
        }

        GetPhoto(email: string, photoId: string, callback: Function) {
            this.http.get('GetPhoto/' + email + '/' + photoId).success((data, status) => {
                callback(data);
            }).error(error => {
                callback(error);
            });
        }
    }

    export interface IPaspDataService {
        GetGeoPoints(email: string, callback: Function);
    }
}

