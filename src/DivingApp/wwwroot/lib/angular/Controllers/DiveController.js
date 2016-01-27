///<reference path="../../typings/angularjs/angular.d.ts" /> 
///<reference path="../../typings/google.maps.d.ts" /> 
var Diving;
(function (Diving) {
    var Controllers;
    (function (Controllers) {
        var diveController = (function () {
            function diveController($scope, dataService) {
                this.dataService = dataService;
                this.selectedDiveId = -1;
                this.selectedPhotoIndex = -1;
                this.showDivesTab = true;
                this.showMapsTab = false;
                this.showPhotosTab = false;
                this.map = undefined;
                this.scope = $scope;
                var that = this;
                dataService.GetAuthorizedUserDives(function (data) {
                    that.Dives = data;
                });
            }
            diveController.prototype.init = function (userEmail) {
                this.currentUserEmail = userEmail;
            };
            diveController.prototype.showSelectedDiveTab = function (tabIndex) {
                if (tabIndex == 1) {
                    this.showDivesTab = true;
                    this.showMapsTab = false;
                    this.showPhotosTab = false;
                }
                if (tabIndex == 2) {
                    this.showDivesTab = false;
                    this.showMapsTab = true;
                    this.showPhotosTab = false;
                }
                if (tabIndex == 3) {
                    this.showDivesTab = false;
                    this.showMapsTab = false;
                    this.showPhotosTab = true;
                }
            };
            return diveController;
        })();
        Controllers.diveController = diveController;
    })(Controllers = Diving.Controllers || (Diving.Controllers = {}));
})(Diving || (Diving = {}));
var MarkerClusterer;
var moment;
