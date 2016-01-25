var appModule = angular.module("diving-app", []);

appModule.controller("loginController", ['$scope', ($scope) => new Diving.Controllers.loginController($scope)]);
appModule.controller('paspController', ['$scope', "DataService", Diving.Controllers.paspController]);
appModule.controller('diveController', ['$scope', "DataService", Diving.Controllers.diveController]);

appModule.factory("DataService", ["$http", ($http) => new Diving.Services.DataService($http)]);
