///<reference path="../../typings/angularjs/angular.d.ts" /> 

class loginController {

    showLogin: boolean;
    showSearch: boolean;
    showSearchResults: boolean;
    showSearchNotFound: boolean;
    searchResults;

    searchCriteria: string;

    constructor(private $scope: ng.IScope) {
        this.showLogin = true;
        this.showSearch = false;
        this.showSearchResults = false;
        this.showSearchNotFound = false;
    }

    showTab(tabIndex: number) {
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
    }

    searchDivers() {     
        this.showSearchNotFound = false;
        $.ajax({
            type: "GET",
            url: "api/users/getusersbyname/" + this.searchCriteria,
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
    }

    openPasp(id: number) {       
        location.href = 'Pasp/PaspShow?login='+id ;
    }
}

var myApp = angular.module('diving-app', []);
myApp.controller("loginController", loginController);