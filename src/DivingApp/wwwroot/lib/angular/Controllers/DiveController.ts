///<reference path="../../typings/angularjs/angular.d.ts" /> 
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
            
        public selectedDive;
        public selectedDiveId: number;          
        public selectedCountryId;
        public selectedPhotoIndex: number;

        public showDivesTab: boolean; 
        public showMapsTab: boolean;      
        public showPhotosTab: boolean;
        public showLoadingTab: boolean;

        private options: any;
        public map: any;
        private scope: any;      

        constructor($scope, dataService: DataService.IPaspDataService) {          
            this.dataService = dataService;
            this.selectedPhotoIndex = -1;
            this.selectedDiveId = -1; 
            this.showLoadingTab = true;         
            this.showDivesTab = false; 
            this.showMapsTab = false;
            this.showPhotosTab = false;
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
                            zoom: 2,
                            center: new google.maps.LatLng(1, 1),
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
                                var marker = new google.maps.Marker({
                                    map: that.map,
                                    draggable: true,
                                    title: that.selectedDive.Location ,
                                    position: new google.maps.LatLng(that.selectedDive.Latitude, that.selectedDive.Longitude)
                                });
                                
                                google.maps.event.addListener(marker, 'dragend', function () {
                                    that.selectedDive.Latitude = marker.getPosition().lat().toFixed(6).replace(".", ",");
                                    that.selectedDive.Longitude = marker.getPosition().lng().toFixed(6).replace(".", ",");                                   
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

        public GetDive(diveId: number) {          
            var that = this;
            that.showSelectedDiveTab(0);
            that.selectedDiveId = diveId;
            that.dataService.GetAuthorizedUserDiveById(diveId, function (data) {
                that.selectedDive = data;               
                that.showSelectedDiveTab(1);
            });
        }
    }
}

var MarkerClusterer: any;
var moment: any;









