///<reference path="../../typings/angularjs/angular.d.ts" /> 
///<reference path="../../typings/google.maps.d.ts" /> 
var Diving;
(function (Diving) {
    var Controllers;
    (function (Controllers) {
        var paspController = (function () {
            function paspController($scope, PaspService) {
                this.paspService = PaspService;
                this.selectedDiveId = -1;
                this.selectedPhotoIndex = -1;
                this.showDives = true;
                this.showMaps = false;
                this.showPhoto = false;
                this.showGeoDiveInfo = false;
                this.map = undefined;
                this.scope = $scope;
            }
            paspController.prototype.init = function (userEmail) {
                this.currentUserEmail = userEmail;
            };
            paspController.prototype.hidePhoto = function () {
                this.showPhoto = false;
            };
            paspController.prototype.showTab = function (tabIndex) {
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
                            zoom: 3,
                            center: new google.maps.LatLng(1, 1),
                            mapTypeId: google.maps.MapTypeId.ROADMAP
                        };
                        if (!that.map) {
                            that.map = new google.maps.Map(document.getElementById('map'), this.options);
                            that.map.addListener('zoom_changed', function () {
                                that.showGeoDiveInfo = false;
                                that.scope.$apply();
                            });
                            that.map.addListener('click', function () {
                                that.showGeoDiveInfo = false;
                                that.scope.$apply();
                            });
                        }
                        that.paspService.GetGeoPoints(that.currentUserEmail, function (data) {
                            that.markers = [];
                            var marker;
                            for (var i = 0; i < data.length; i++) {
                                if (data[i].CoordinateX && data[i].CoordinateY) {
                                    var clickHandler = function () {
                                        var currentDive = data[i];
                                        var diveId = data[i].DiveId;
                                        marker = new google.maps.Marker({
                                            map: that.map,
                                            draggable: false,
                                            title: data[i].Location + ": " + data[i].DiveComment,
                                            position: new google.maps.LatLng(data[i].CoordinateX, data[i].CoordinateY)
                                        });
                                        marker.data = data[i];
                                        marker.addListener('click', function (e) {
                                            that.selectedGeoDive = this.data;
                                            var overlay = new google.maps.OverlayView();
                                            overlay.draw = function () { };
                                            overlay.setMap(that.map);
                                            var proj = overlay.getProjection();
                                            var pos = this.getPosition();
                                            var p = proj.fromLatLngToContainerPixel(pos);
                                            $('#geoDiveDetails').css({
                                                top: p.y + $('#geoDiveDetails').parent().position().top - $('#geoDiveDetails').height(),
                                                left: p.x + 40,
                                                position: 'absolute'
                                            });
                                            that.showGeoDiveInfo = true;
                                            that.scope.$apply();
                                        });
                                        that.markers.push(marker);
                                    };
                                    clickHandler();
                                }
                            }
                            var marker = new MarkerClusterer(that.map, that.markers);
                        });
                    }, 100);
                }
            };
            paspController.prototype.getPhotos = function (diveId) {
                this.showGeoDiveInfo = false;
                this.resetPhoto();
                this.selectedDiveId = diveId;
                var that = this;
                this.paspService.GetPhotosIds(this.currentUserEmail, this.selectedDiveId, 0, function (data) {
                    that.photos = data;
                    if (that.photos.length > 0) {
                        that.selectedPhotoIndex = 0;
                        that.changeCurrentPhotoIndex(that.selectedPhotoIndex);
                    }
                });
            };
            paspController.prototype.showNext = function () {
                if (this.selectedPhotoIndex < this.photos.length - 1)
                    this.selectedPhotoIndex++;
                else
                    this.selectedPhotoIndex = 0;
                this.changeCurrentPhotoIndex(this.selectedPhotoIndex);
            };
            paspController.prototype.openPasp = function (id) {
                location.href = 'Pasp/PaspShow?login=' + id;
            };
            paspController.prototype.changeCurrentPhotoIndex = function (index) {
                if (index == -1)
                    this.hidePhoto();
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
            };
            paspController.prototype.resetPhoto = function () {
                this.selectedPhotoIndex = -1;
                this.changeCurrentPhotoIndex(this.selectedPhotoIndex);
            };
            return paspController;
        })();
        Controllers.paspController = paspController;
        var photoDetailes = (function () {
            function photoDetailes() {
            }
            return photoDetailes;
        })();
        Controllers.photoDetailes = photoDetailes;
        var selectedGeoDetailes = (function () {
            function selectedGeoDetailes() {
            }
            return selectedGeoDetailes;
        })();
        Controllers.selectedGeoDetailes = selectedGeoDetailes;
    })(Controllers = Diving.Controllers || (Diving.Controllers = {}));
})(Diving || (Diving = {}));
var MarkerClusterer;
var moment;
