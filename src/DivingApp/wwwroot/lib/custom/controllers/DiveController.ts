///<reference path="../../../typings/angularjs/angular.d.ts" /> 
///<reference path="../../../typings/google.maps.d.ts" /> 

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
            
        public selectedDive;
        public selectedDiveId: number;          
        public selectedCountryId;
        public selectedPhotoIndex: number;
        public selectedPhotoInfo: photoDetailes;

        public showDivesTab: boolean; 
        public showMapsTab: boolean;      
        public showPhotosTab: boolean;
        public showLoadingTab: boolean;
        public showSearchingGeoStatus: boolean;
        public showPhoto: boolean;
        public showPhotoDelete: boolean;

        public location: string;
        public map: any;
        private options: any;       
        private scope: any;
        private marker: any;  

        constructor($scope, dataService: DataService.IPaspDataService) {          
            this.dataService = dataService;
            this.selectedPhotoIndex = -1;
            this.selectedDiveId = -1; 
            this.showLoadingTab = true;         
            this.showDivesTab = false; 
            this.showMapsTab = false;
            this.showPhotosTab = false;
            this.showPhoto = false;
            this.showPhotoDelete = false;
            this.showSearchingGeoStatus = false;
            this.map = undefined;
            this.scope = $scope;
            var that = this;
            dataService.GetDiveDictionaries(function (data) {
                that.countries = data.Countries;               
                that.suits = data.Suits;
                that.weights = data.Weights
                that.tanks = data.Tanks;                   
            });     
            dataService.GetAuthorizedUserDives(function (data) {
                that.dives = data;
                if (data.length > 0) {
                    that.GetDive(data[0].DiveID);
                }              
            });         
        }

        public init(userEmail) {            
            this.currentUserEmail = userEmail;
        }       

        public showSelectedDiveTab(tabIndex: number) {
            if (tabIndex == 0) this.showLoadingTab = true;
            else {
                this.showLoadingTab = false;
                if (tabIndex == 1) {
                    this.showDivesTab = true;
                    this.showMapsTab = false;
                    this.showPhotosTab = false;
                }

                if (tabIndex == 2) {
                    this.showDivesTab = false;
                    this.showMapsTab = true;
                    this.showPhotosTab = false;
                    var that = this;
                    setTimeout(function () {
                        this.options = {
                            zoom: 6,
                            center: new google.maps.LatLng(that.selectedDive.Latitude, that.selectedDive.Longitude),
                            mapTypeId: google.maps.MapTypeId.ROADMAP
                        };
                        if (!that.map) {
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
                                    title: that.selectedDive.Location ,
                                    position: new google.maps.LatLng(that.selectedDive.Latitude, that.selectedDive.Longitude)
                                });
                                
                                google.maps.event.addListener(that.marker, 'dragend', function () {
                                    that.selectedDive.Latitude = that.marker.getPosition().lat().toFixed(6).replace(".", ",");
                                    that.selectedDive.Longitude = that.marker.getPosition().lng().toFixed(6).replace(".", ",");
                                });

                            }
                        }
                    });
                }

                if (tabIndex == 3) {
                    this.showDivesTab = false;
                    this.showMapsTab = false;
                    this.showPhotosTab = true;
                }
            }
        }

        public searchForLocation() {
            this.marker.setMap(null);
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
                    that.selectedDive.Latitude = results[0].geometry.location.lat().toFixed(6).replace(".", ",");
                    that.selectedDive.Longitude = results[0].geometry.location.lng().toFixed(6).replace(".", ",");
                    var lat = results[0].geometry.location.lat().toFixed(6).replace(".", ",");
                    var lgn = results[0].geometry.location.lng().toFixed(6).replace(".", ",");
                  
                    google.maps.event.addListener(that.marker, 'dragend', function () {
                        that.selectedDive.Latitude = that.marker.getPosition().lat().toFixed(6).replace(".", ",");
                        that.selectedDive.Longitude = that.marker.getPosition().lng().toFixed(6).replace(".", ",");                      
                    });
                } else {
                    alert("Failed to make request to GEO service: " + status);
                }
                that.showSearchingGeoStatus = false;
                that.scope.$apply();
            });
        }

        public GetDive(diveId: number) {          
            var that = this;
            that.showSelectedDiveTab(0);
           
            that.selectedDiveId = diveId;
            that.dataService.GetAuthorizedUserDiveById(diveId, function (data) {
                that.location = "";
                that.resetPhoto();
                that.selectedDive = data;               
                that.showSelectedDiveTab(1);            
            });
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

        private changeCurrentPhotoIndex(index: number) {
            if (index == -1) this.HidePhoto();
            else {
                var that = this;
                this.dataService.GetPhoto(this.currentUserEmail, this.selectedDive.Photos[this.selectedPhotoIndex], function (data) {
                    that.selectedPhotoInfo = new photoDetailes();
                    that.selectedPhotoInfo.photoId = that.selectedDive.Photos[that.selectedPhotoIndex];
                    that.selectedPhotoInfo.photoDate = moment(data.Date).format('DD/MM/YYYY');
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
}

var MarkerClusterer: any;
var moment: any;









