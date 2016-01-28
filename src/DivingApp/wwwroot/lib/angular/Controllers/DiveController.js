///<reference path="../../typings/angularjs/angular.d.ts" /> 
///<reference path="../../typings/google.maps.d.ts" /> 
var Diving;
(function (Diving) {
    var Controllers;
    (function (Controllers) {
        var diveController = (function () {
            function diveController($scope, dataService) {
                this.dataService = dataService;
                this.selectedPhotoIndex = -1;
                this.selectedDiveId = -1;
                this.showDivesTab = true;
                this.showMapsTab = false;
                this.showPhotosTab = false;
                this.map = undefined;
                this.scope = $scope;
                var that = this;
                dataService.GetAuthorizedUserDives(function (data) {
                    that.dives = data;
                    if (data.length > 0) {
                        that.GetDive(data[0].DiveID);
                    }
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
            };
            diveController.prototype.GetDive = function (diveId) {
                var that = this;
                that.selectedDiveId = diveId;
                this.dataService.GetAuthorizedUserDiveById(diveId, function (data) {
                    that.selectedDive = data;
                    that.scope.$apply();
                });
            };
            return diveController;
        })();
        Controllers.diveController = diveController;
    })(Controllers = Diving.Controllers || (Diving.Controllers = {}));
})(Diving || (Diving = {}));
var MarkerClusterer;
var moment;
