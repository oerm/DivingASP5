///<reference path="../../typings/angularjs/angular.d.ts" /> 
var loginController = (function () {
    function loginController($scope) {
        this.$scope = $scope;
        this.showLogin = true;
        this.showSearch = false;
        this.showSearchResults = false;
        this.showSearchNotFound = false;
    }
    loginController.prototype.showTab = function (tabIndex) {
        if (tabIndex == 1) {
            this.showLogin = true;
            this.showSearchResults = this.showSearch = false;
        }
        if (tabIndex == 2) {
            this.showSearch = true;
            this.showLogin = this.showSearchResults = false;
        }
        if (tabIndex == 3) {
            this.showSearchResults = true;
            this.showLogin = this.showSearch = false;
        }
    };
    loginController.prototype.searchDivers = function () {
        this.showSearchNotFound = false;
        $.ajax({
            type: "GET",
            url: "Account/GetUserByFIO/" + this.searchCriteria,
            cache: false,
            context: this,
            success: function (data) {
                if (data.length > 0) {
                    this.showSearch = false;
                    this.showSearchResults = true;
                    this.searchResults = data;
                }
                else {
                    this.showSearchNotFound = true;
                }
                this.$scope.$apply();
            }
        });
    };
    loginController.prototype.openPasp = function (id) {
        location.href = 'Pasp/PaspShow?login=' + id;
    };
    return loginController;
})();
var myApp = angular.module('diving-app', []);
myApp.controller("loginController", loginController);
//# sourceMappingURL=LoginController.js.map