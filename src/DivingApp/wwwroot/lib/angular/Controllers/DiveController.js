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
                this.showDives = true;
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
            return diveController;
        })();
        Controllers.diveController = diveController;
    })(Controllers = Diving.Controllers || (Diving.Controllers = {}));
})(Diving || (Diving = {}));
var MarkerClusterer;
var moment;
