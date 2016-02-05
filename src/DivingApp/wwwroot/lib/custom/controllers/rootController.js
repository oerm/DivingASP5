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
            return rootController;
        })();
        Controllers.rootController = rootController;
    })(Controllers = Diving.Controllers || (Diving.Controllers = {}));
})(Diving || (Diving = {}));
