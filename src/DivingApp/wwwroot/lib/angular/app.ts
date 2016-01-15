var appModule = angular.module("diving-app", []);

appModule.controller("loginController", ['$scope', ($scope) => new Diving.Controllers.loginController($scope)]);
appModule.controller('paspController', ['$scope', "PaspService", Diving.Controllers.paspController]);

appModule.factory("PaspService", ["$http", ($http) => new Diving.Services.PaspService($http)]);
