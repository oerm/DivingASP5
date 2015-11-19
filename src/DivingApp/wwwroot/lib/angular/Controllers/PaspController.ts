///<reference path="../../typings/angularjs/angular.d.ts" /> 
///<reference path="../../typings/google.maps.d.ts" /> 

module Diving.Controllers {

    import Pasp = Diving.Services;

    export class paspController {

        public name: string;
        public map: any;
        public selectedDiveId: number;
        public selectedPhotoIndex: number;
        public selectedPhotoInfo: photoDetailes;
        public markers;
        public photos;

        public showDives: boolean;
        public showMaps: boolean;
        public showPhoto: boolean;

        private paspService;  
        private options: any;
        private currentUserEmail: string;
        private scope: any;

        public selectedDive;

        constructor($scope, PaspService: Pasp.IPaspDataService) {          
            this.paspService = PaspService;
            this.selectedDiveId = -1;
            this.selectedPhotoIndex = -1;
            this.showDives = true;
            this.showMaps = false;
            this.showPhoto = false;
            this.map = undefined;
            this.scope = $scope;
        }

        public init(userEmail) {            
            this.currentUserEmail = userEmail;
        }

        hidePhoto() {       
            this.showPhoto = false;
        }

        showTab(tabIndex: number) {
            if (tabIndex == 1) {
                this.showDives = true;
                this.showMaps = false;
            }

            if (tabIndex == 2) {
                this.showDives = false;
                this.showMaps = true;
                var that = this;
                setTimeout(function () {
                    this.options = {
                        zoom: 2,
                        center: new google.maps.LatLng(1, 1),
                        mapTypeId: google.maps.MapTypeId.ROADMAP
                    };
                    if (!that.map) {
                        that.map = new google.maps.Map(document.getElementById('map'), this.options);
                    }

                    that.paspService.GetGeoPoints(that.currentUserEmail, function (data) {
                        that.markers = [];
                        var marker;
                        for (var i = 0; i < data.length; i++) {
                            var a = function () {
                                var diveId = data[i].DiveId;
                                marker = new google.maps.Marker({ map: that.map, draggable: false, title: data[i].Location + ": " + data[i].DiveComment, position: new google.maps.LatLng(data[i].CoordinateX, data[i].CoordinateY) });
                                marker.addListener('click', function () {
                                    that.getPhotos(diveId);
                                });

                                that.markers.push(marker);
                            };
                            a();
                        }
                        var marker = new MarkerClusterer(that.map, that.markers);
                    });
                 
                }, 100);
            }
        }

        getPhotos(diveId: number) {      
            this.resetPhoto();
            this.selectedDiveId = diveId;     
            var that = this;
       
            this.paspService.GetPhotosIds(this.currentUserEmail, this.selectedDiveId, function (data) {                 
                that.photos = data.split(',');               
                if (that.photos.length > 0) {                
                    that.selectedPhotoIndex = 0;               
                    that.changeCurrentPhotoIndex(that.selectedPhotoIndex);
                }
            });
        }

        showNext() {
            if (this.selectedPhotoIndex < this.photos.length-1) this.selectedPhotoIndex++;
            else this.selectedPhotoIndex = 0;  
            this.changeCurrentPhotoIndex(this.selectedPhotoIndex);           
        }

        openPasp(id: number) {
            location.href = 'Pasp/PaspShow?login=' + id;
        }

        private changeCurrentPhotoIndex(index: number) {         
            if (index == -1) this.hidePhoto();
            else {             
                var that = this;
                this.paspService.GetPhoto(this.currentUserEmail, this.photos[this.selectedPhotoIndex], function (data) {
                    that.selectedPhotoInfo = new photoDetailes();
                    that.selectedPhotoInfo.photoId = that.photos[that.selectedPhotoIndex];
                    that.selectedPhotoInfo.photoDate = "Photo date: " + moment(data.Date).format('DD/MM/YYYY');
                    that.selectedPhotoInfo.photoInfo = data.Comment; 
                    that.showPhoto = true;
                    that.scope.$apply();                                   
                });
            }
        }

        private resetPhoto() {
            this.selectedPhotoIndex = -1;
            this.changeCurrentPhotoIndex(this.selectedPhotoIndex);
        }
    }

    export class photoDetailes {
        photoId: number;
        photoDate: string;
        photoInfo: string;
    }
}


var MarkerClusterer: any;
var moment: any;

var myApp = angular.module("diving-app", []);
myApp.factory("PaspService", ["$http", ($http) => new Diving.Services.PaspService($http)]);
myApp.controller('paspController', ['$scope', "PaspService", Diving.Controllers.paspController]);

//myApp.controller("paspController", ["$scope", "PaspService", ($scope, PaspService) => new Diving.Controllers.paspController($scope, PaspService)]);








