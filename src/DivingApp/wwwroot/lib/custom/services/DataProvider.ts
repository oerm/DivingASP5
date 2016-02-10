///<reference path="../../typings/angularjs/angular.d.ts" /> 

module Diving.Services {
    export class DataService implements IDataService {

        private http;

        constructor($http: ng.IHttpService) {
            this.http = $http;            
        }

        GetGeoPoints(email: string, callback: Function) {
            this.http.get('/api/dives/getdiveswithcoordinates/' + email, {
                cache: false
            }).success((data, status) => {
                callback(data);
            }).error(error => {
                callback(error);
            });
        }

        GetPhotosIds(email: string, diveId: string, minPhoto: number, callback: Function) {
            this.http.get('/api/dives/getuserphotoidsbydiveids/' + email + '/' + diveId + '/' + minPhoto).success((data, status) => {
                callback(data);
            }).error(error => {
                callback(error);
            });
        }

        GetPhoto(email: string, photoId: string, callback: Function) {
            this.http.get('/GetPhoto/' + email + '/' + photoId).success((data, status) => {
                callback(data);
            }).error(error => {
                callback(error);
            });
        }

        GetAuthorizedUserDives(callback: Function) {

            var req = {
                method: 'GET',
                url: '/api/dives/getuserdives/' + Date.now(),
                cache: false,
                headers: { 'Cache-Control': 'no-cache' }
            }

            this.http(req).success((data, status) => {
                callback(data);
            }).error(error => {
                callback(error);
            });            
        }

        GetAuthorizedUserDiveById(diveId: string, callback: Function) {
            this.http.get('/api/dives/getuserdivebyid/' + diveId + '/' + Date.now(), {
                cache: false
            }).success((data, status) => {
                callback(data);
            }).error(error => {
                callback(error);
            });
        }

        GetDiveDictionaries(callback: Function) {
            this.http.get('/api/dives/getdivedictionaries').success((data, status) => {
                callback(data);
            }).error(error => {
                callback(error);
            });
        }   

        SaveDive(dive: Object, callback: Function, errorHandler: Function) {
            var req = {
                method: 'POST',
                url: '/api/dives/saveDive',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                data: $.param(dive)
            }
            this.http(req).success((data, status) => {
                callback(data);
            }).error(error => {
                errorHandler(error);
            });
        }   

        DeleteDive(diveId: number, callback: Function) {
            this.http.delete('/api/dives/deleteDive/' + diveId).success((data, status) => {
                callback(data);
            }).error(error => {
                callback(error);
            });
        }   
    }

    export interface IDataService {
        GetGeoPoints(email: string, callback: Function);
        GetAuthorizedUserDives(callback: Function);
        GetAuthorizedUserDiveById(diveId: string, callback: Function);
        GetDiveDictionaries(callback: Function);
        SaveDive(dive: Object, callback: Function, errorHandler: Function);      
        DeleteDive(diveId: number, callback: Function);
    }
}

