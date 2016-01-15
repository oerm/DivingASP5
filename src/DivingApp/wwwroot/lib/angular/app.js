var appModule = angular.module("diving-app", []);
appModule.controller("loginController", ['$scope', function ($scope) { return new Diving.Controllers.loginController($scope); }]);
appModule.controller('paspController', ['$scope', "PaspService", Diving.Controllers.paspController]);
appModule.factory("PaspService", ["$http", function ($http) { return new Diving.Services.PaspService($http); }]);
