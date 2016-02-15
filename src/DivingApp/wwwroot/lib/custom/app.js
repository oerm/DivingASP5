var appModule = angular.module("diving-app", ['ngAnimate', 'uploadModule', 'blueimp.fileupload']);
appModule.controller('rootController', ['$scope', function ($scope) { return new Diving.Controllers.rootController($scope); }]);
appModule.controller("loginController", ['$scope', function ($scope) { return new Diving.Controllers.loginController($scope); }]);
appModule.controller('paspController', ['$scope', "DataService", Diving.Controllers.paspController]);
appModule.controller('diveController', ['$scope', "DataService", Diving.Controllers.diveController]);
appModule.factory("DataService", ["$http", function ($http) { return new Diving.Services.DataService($http); }]);
