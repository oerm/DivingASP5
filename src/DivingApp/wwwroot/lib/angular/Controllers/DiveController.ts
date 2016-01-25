///<reference path="../../typings/angularjs/angular.d.ts" /> 
///<reference path="../../typings/google.maps.d.ts" /> 

module Diving.Controllers {

    import DataService = Diving.Services;

    export class diveController {

        public name: string;
        public map: any;
        public selectedDiveId: number;
        public selectedPhotoIndex: number;
        public selectedPhotoInfo: photoDetailes;
        public Dives;
        public photos;     

        public showDives: boolean;        

        private dataService;  
        private options: any;
        private currentUserEmail: string;
        private scope: any;

        public selectedDive;

        constructor($scope, dataService: DataService.IPaspDataService) {          
            this.dataService = dataService;
            this.selectedDiveId = -1;
            this.selectedPhotoIndex = -1;
            this.showDives = true;        
            this.map = undefined;
            this.scope = $scope;
            var that = this;
            dataService.GetAuthorizedUserDives(function (data)
            {
                that.Dives = data;
            });
        }

        public init(userEmail) {            
            this.currentUserEmail = userEmail;
        }
    }
}

var MarkerClusterer: any;
var moment: any;









