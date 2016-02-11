var appModule = angular.module("diving-app", ['blueimp.fileupload', 'ngAnimate']);
var FileDestroyController: any;
var fileUploadDirective: any;

appModule.controller('rootController', ['$scope', ($scope) => new Diving.Controllers.rootController($scope)]);
appModule.controller("loginController", ['$scope', ($scope) => new Diving.Controllers.loginController($scope)]);
appModule.controller('paspController', ['$scope', "DataService", Diving.Controllers.paspController]);
appModule.controller('diveController', ['$scope', "DataService", Diving.Controllers.diveController]);
appModule.controller('FileDestroyController', ['$scope', '$http', 'fileUpload', FileDestroyController]);

appModule.directive('ngUploadForm', fileUploadDirective);


appModule.factory("DataService", ["$http", ($http) => new Diving.Services.DataService($http)]);
