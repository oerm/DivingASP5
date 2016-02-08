///<reference path="../../typings/angularjs/angular.d.ts" /> 

module Diving.Services {
    export class DataService implements IPaspDataService {

        private http;

        constructor($http: ng.IHttpService) {
            this.http = $http;            
        }

        GetGeoPoints(email: string, callback: Function) {
            this.http.get('/api/getdiveswithcoordinates/' + email).success((data, status) => {
                callback(data);
            }).error(error => {
                callback(error);
            });
        }

        GetPhotosIds(email: string, diveId: string, minPhoto: number, callback: Function) {
            this.http.get('/api/getuserphotoidsbydiveids/' + email + '/' + diveId + '/' + minPhoto).success((data, status) => {
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
            this.http.get('/api/getuserdives').success((data, status) => {
                callback(data);
            }).error(error => {
                callback(error);
            });            
        }

        GetAuthorizedUserDiveById(diveId: string, callback: Function) {
            this.http.get('/api/getuserdivebyid/' + diveId).success((data, status) => {
                callback(data);
            }).error(error => {
                callback(error);
            });
        }

        GetDiveDictionaries(callback: Function) {
            this.http.get('/api/getdivedictionaries').success((data, status) => {
                callback(data);
            }).error(error => {
                callback(error);
            });
        }   

        SaveDive(dive: Object, callback: Function) {
            var req = {
                method: 'POST',
                url: '/api/dives/saveDive',
                headers: {
                    'Content-Type': undefined
                },
                data: dive
            }
            this.http(req).success((data, status) => {
                callback(data);
            }).error(error => {
                callback(error);
            });
        }   
    }

    export interface IPaspDataService {
        GetGeoPoints(email: string, callback: Function);
        GetAuthorizedUserDives(callback: Function);
        GetAuthorizedUserDiveById(diveId: string, callback: Function);
        GetDiveDictionaries(callback: Function);
        SaveDive(dive: Object, callback: Function);
    }
}

