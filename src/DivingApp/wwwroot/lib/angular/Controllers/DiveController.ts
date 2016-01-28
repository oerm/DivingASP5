///<reference path="../../typings/angularjs/angular.d.ts" /> 
///<reference path="../../typings/google.maps.d.ts" /> 

module Diving.Controllers {

    import DataService = Diving.Services;

    export class diveController {

        public name: string;
        public map: any;    
        public selectedPhotoIndex: number;
        public selectedDiveId: number;
        public selectedPhotoInfo: photoDetailes;
        public dives;
        public selectedDive;
        public selectedDivePhotos;     

        public showDivesTab: boolean; 
        public showMapsTab: boolean;      
        public showPhotosTab: boolean;           

        private dataService;  
        private options: any;
        private currentUserEmail: string;
        private scope: any;

      

        constructor($scope, dataService: DataService.IPaspDataService) {          
            this.dataService = dataService;
            this.selectedPhotoIndex = -1;
            this.selectedDiveId = -1;         
            this.showDivesTab = true; 
            this.showMapsTab = false;
            this.showPhotosTab = false;
            this.map = undefined;
            this.scope = $scope;
            var that = this;
            dataService.GetAuthorizedUserDives(function (data)
            {
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
                    }
                });                
            }

            if (tabIndex == 3) {
                this.showDivesTab = false;
                this.showMapsTab = false;
                this.showPhotosTab = true;
            }
        }

        public GetDive(diveId: number) {
            var that = this;
            that.selectedDiveId = diveId;
            this.dataService.GetAuthorizedUserDiveById(diveId, function (data) {
                that.selectedDive = data;
                that.scope.$apply();
            });
        }
    }
}

var MarkerClusterer: any;
var moment: any;









