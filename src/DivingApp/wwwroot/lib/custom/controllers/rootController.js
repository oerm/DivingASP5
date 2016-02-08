var Diving;
(function (Diving) {
    var Controllers;
    (function (Controllers) {
        var rootController = (function () {
            function rootController($scope) {
                this.$scope = $scope;
                this.scope = $scope;
            }
            rootController.prototype.DiveCreateNew = function () {
                this.scope.DiveChild.CreateNewDive();
            };
            rootController.prototype.DiveShowDives = function () {
                this.scope.DiveChild.CancelCreateNewDive();
            };
            rootController.prototype.HasDives = function () {
                this.scope.DiveChild.dives && this.scope.DiveChild.dives.length > 0;
            };
            return rootController;
        })();
        Controllers.rootController = rootController;
    })(Controllers = Diving.Controllers || (Diving.Controllers = {}));
})(Diving || (Diving = {}));
