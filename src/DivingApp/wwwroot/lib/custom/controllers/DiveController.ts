﻿///<reference path="../../typings/angularjs/angular.d.ts" /> 
///<reference path="../../typings/google.maps.d.ts" /> 

module Diving.Controllers {

    import DataService = Diving.Services;

    export class diveController {
               
        private dataService; 

        private currentUserEmail: string;  
        public dives;       
        public countries;
        public suits;
        public weights;
        public tanks;   
        public time;         
            
        public selectedDive;
        public selectedDiveId: number;          
        public selectedCountryId;
        public selectedPhotoIndex: number;
        public selectedPhotoInfo: photoDetailes;

        public showDivesList: boolean; 
        public showDivesTab: boolean; 
        public showMapsTab: boolean;      
        public showPhotosTab: boolean;
        public showLoadingTab: boolean;
        public showSearchingGeoStatus: boolean;
        public showPhoto: boolean;
        public showPhotoDelete: boolean;
        public showUpdateMessage: boolean;

        public errors: any;
        public location: string;
        public map: any;
        private options: any;       
        private scope: any;
        private marker: any;  

        constructor($scope, dataService: DataService.IDataService) {     
            var scope = $scope.$parent;
            scope.DiveChild = this;  
            this.dataService = dataService;          
            this.selectedPhotoIndex = -1;
            this.selectedDiveId = -1; 
            this.showLoadingTab = true; 
            this.showDivesList = true;        
            this.showDivesTab = false; 
            this.showMapsTab = false;
            this.showPhotosTab = false;
            this.showPhoto = false;
            this.showPhotoDelete = false;
            this.showSearchingGeoStatus = false;    
            this.showUpdateMessage = false;       
            this.map = undefined;
            this.scope = $scope;
            var that = this;
            dataService.GetDiveDictionaries(function (data) {
                that.countries = data.Countries;               
                that.suits = data.Suits;
                that.weights = data.Weights
                that.tanks = data.Tanks;   
                that.time = data.Time;
                that.refreshDives(that, -1, false);                                  
            });    
             
        }

        public Init(userEmail) {            
            this.currentUserEmail = userEmail;
        }       

        public ShowSelectedDiveTab(tabIndex: number) {           
            if (tabIndex == 0) this.showLoadingTab = true;
            else {
                this.showLoadingTab = false;
                if (tabIndex == 1) {
                    this.showDivesTab = true;
                    this.showMapsTab = false;
                    this.showPhotosTab = false;
                }

                if (tabIndex == 2) {
                    this.showUpdateMessage = false;  
                    this.showDivesTab = false;
                    this.showMapsTab = true;
                    this.showPhotosTab = false;
                    var that = this;
                    setTimeout(function () {

                        var lat = that.selectedDive && that.selectedDive.Latitude ? that.selectedDive.Latitude : 1;
                        var lan = that.selectedDive && that.selectedDive.Longitude ? that.selectedDive.Longitude : 1;
                        this.options = {
                            zoom: that.selectedDive && that.selectedDive.Latitude && that.selectedDive.Longitude? 6 :2,
                            center: new google.maps.LatLng(lat, lan),
                            mapTypeId: google.maps.MapTypeId.ROADMAP
                        };

                        that.map = new google.maps.Map(document.getElementById('map'), this.options);
                        that.map.addListener('zoom_changed', function () {
                            that.scope.$apply();
                        });
                        that.map.addListener('click', function () {
                            that.scope.$apply();
                        });
                        if (that.selectedDive && that.selectedDive.Latitude && that.selectedDive.Longitude) {
                            that.marker = new google.maps.Marker({
                                map: that.map,
                                draggable: true,
                                title: that.selectedDive.Location,
                                position: new google.maps.LatLng(that.selectedDive.Latitude, that.selectedDive.Longitude)
                            });

                            google.maps.event.addListener(that.marker, 'dragend', function () {
                                that.selectedDive.Latitude = that.marker.getPosition().lat().toFixed(6).replace(".", ",");
                                that.selectedDive.Longitude = that.marker.getPosition().lng().toFixed(6).replace(".", ",");
                            });
                        }
                    });
                }

                if (tabIndex == 3) {
                    this.showUpdateMessage = false;  
                    this.showDivesTab = false;
                    this.showMapsTab = false;
                    this.showPhotosTab = true;
                }
            }
        }

        public SearchForLocation() {
            if (this.marker) this.marker.setMap(null);
            this.showSearchingGeoStatus = true;
            var geocoder = new google.maps.Geocoder();
            var that = this;

            geocoder.geocode({ 'address': this.location }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    var myOptions = {
                        zoom: 8,
                        center: results[0].geometry.location,
                        mapTypeId: google.maps.MapTypeId.ROADMAP
                    }
                    that.map = new google.maps.Map($("#map")[0], myOptions);
                    that.marker = new google.maps.Marker({
                        map: that.map,
                        draggable: true,
                        position: results[0].geometry.location
                    });
                    that.selectedDive.Latitude = results[0].geometry.location.lat().toFixed(6);
                    that.selectedDive.Longitude = results[0].geometry.location.lng().toFixed(6);
                    var lat = results[0].geometry.location.lat().toFixed(6);
                    var lgn = results[0].geometry.location.lng().toFixed(6);
                  
                    google.maps.event.addListener(that.marker, 'dragend', function () {
                        that.selectedDive.Latitude = that.marker.getPosition().lat().toFixed(6);
                        that.selectedDive.Longitude = that.marker.getPosition().lng().toFixed(6);                      
                    });
                } else {
                    alert("Failed to make request to GEO service: " + status);
                }
                that.showSearchingGeoStatus = false;
                that.scope.$apply();
            });
        }

        public GetDive(diveId: number) {
            this.showUpdateMessage = false;
            this.HidePhotoDelete();
            this.ShowSelectedDiveTab(0);
            this.selectedDiveId = diveId;
            var that = this;
            setTimeout(function () {
                that.dataService.GetAuthorizedUserDiveById(diveId, function (data) {
                    that.location = "";
                    that.resetPhoto();
                    that.selectedDive = data;
                    that.ShowSelectedDiveTab(1);
                    that.showDivesList = true;
                })
            }, 250);
        }      

        public GetPhoto(id: number) {
            this.selectedPhotoIndex = this.selectedDive.Photos.map(function (e) { return e.PhotoId }).indexOf(id);
            this.changeCurrentPhotoIndex(this.selectedPhotoIndex);
        }

        public ShowNext() {
            if (this.selectedPhotoIndex < this.selectedDive.Photos.length - 1) this.selectedPhotoIndex++;
            else this.selectedPhotoIndex = 0;
            this.changeCurrentPhotoIndex(this.selectedPhotoIndex);
        }

        public HidePhoto() {
            this.showPhoto = false;
        }

        public ShowPhotoDelete() {
            this.showPhotoDelete = true;
        }

        public HidePhotoDelete() {
            this.showPhotoDelete = false;          
        }

        public ShowDiveDelete(diveId: string) {
            this.dives[this.dives.map(function (e) { return e.DiveID; }).indexOf(diveId)].ShowDelete = true;
        }

        public HideDiveDelete(diveId: string) {
            this.dives[this.dives.map(function (e) { return e.DiveID; }).indexOf(diveId)].ShowDelete = false;
        }

        public CreateNewDive() {
            this.showDivesList = false;
            this.selectedDive = new Object;
            this.selectedDive.WeightIsOk = this.weights[0].Value;
            this.selectedDive.SuitType = this.suits[0].Value;
            this.selectedDive.Tank = this.tanks[0].Value;
            this.selectedDive.DiveTime = this.time[0].Value;
            this.selectedDive.CountryId = 804;  
            this.selectedDive.DiveDate = this.selectedDive.DiveDateString = moment(Date.now()).format('DD/MM/YYYY');
            if (this.marker) this.marker.setMap(null);  
            this.ShowSelectedDiveTab(1);
            this.showUpdateMessage = false;         
        }

        public SaveDive() {
            this.errors = null;
            this.ShowSelectedDiveTab(0);
            this.selectedDive.DiveDate = this.selectedDive.DiveDateString;
            var selectedId = this.selectedDive && this.selectedDive.DiveID ? this.selectedDive.DiveID : -1;
            var that = this;
            this.dataService.SaveDive(this.selectedDive,
                function (data) {
                    that.refreshDives(that, selectedId, (selectedId > 0));
                },
                function (error) {
                    that.errors = error;
                    that.ShowSelectedDiveTab(1);
                });
        }

        public DeleteDive(diveId: number) {         
            var that = this;
            this.dataService.DeleteDive(diveId,
                function (data) {
                    that.refreshDives(that, -1, false);
                    that.ShowSelectedDiveTab(1);
                },
                function (error) {
                    that.errors = error;
                    that.ShowSelectedDiveTab(1);
                });
        }

        public DeletePhoto(photoId: number) {
            var that = this;
            this.dataService.DeletePhoto(photoId,
                function (data) {                    
                    that.selectedDive.Photos.splice(that.selectedPhotoIndex, 1);
                    that.resetPhoto();
                },
                function (error) {
                    alert('Failed to delete photo');
                });
        }

        public CancelCreateNewDive() {
            if (this.dives.length > 0) this.GetDive(this.dives[0].DiveID);
            this.showDivesList = true;
        }

        private refreshDives(context, diveId, showUpdated) {
            context.dataService.GetAuthorizedUserDives(function (data) {
                context.dives = data;
                if (data.length > 0) {
                    context.GetDive(diveId == -1 ? data[0].DiveID : diveId);
                }
                else {
                    context.CreateNewDive();
                }
                context.showUpdateMessage = showUpdated;
            });
        }

        private changeCurrentPhotoIndex(index: number) {
            this.HidePhotoDelete();
            if (index == -1) this.HidePhoto();
            else {
                var that = this;
                this.dataService.GetPhoto(this.currentUserEmail, this.selectedDive.Photos[this.selectedPhotoIndex], function (data) {
                    that.selectedPhotoInfo = new photoDetailes();
                    that.selectedPhotoInfo.photoId = that.selectedDive.Photos[that.selectedPhotoIndex];
                    that.selectedPhotoInfo.photoDate = moment(data.Date).format('DD/MM/YYYY');
                    that.selectedPhotoInfo.photoInfo = data.Comment;
                    that.showPhoto = true;
                });
            }
        }

        private resetPhoto() {
            this.selectedPhotoIndex = -1;
            this.changeCurrentPhotoIndex(this.selectedPhotoIndex);
        }        
    }
}

var MarkerClusterer: any;
var moment: any;









