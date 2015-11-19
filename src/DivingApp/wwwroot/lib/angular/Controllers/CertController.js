var myApp = angular.module('diving-app');

function certController($scope, $http) {
  
    $scope.usersCerts = [];

    $scope.showCertDetailes = false;
    $scope.currentCert;
    $scope.showProgress = true;
    $scope.textProgress = '';

    $scope.showCertWithId = function (certId) {
        $http({
            cache: false,
            url: root +  "Cert/GetCert/" + certId,
            method: "GET",
            params: { 'killcache': new Date().getTime() }
        }).success(function (data, status, headers, config) {
            $scope.currentCert = data;
            $scope.showCertDetailes = true;
            $scope.showProgress = false;
        });
    };

    $scope.showAddOrUpdateCert = function (isDeleteing) {
        if (isDeleteing || (!isDeleteing && $scope.currentCert != null && $scope.currentCert.DateArchieve != '' && $scope.currentCert.CertNumber != '')) {
            $scope.textProgress = 'Сохранение изменений...';
            $scope.showProgress = true;
            if ($scope.currentCert != null) {
                $http.post(root + "Cert/MannageCert", $scope.currentCert).then(function (data) {
                    init();
                    $scope.showProgress = false;
                    //   $scope.showCertDetailes = false;
                });
            }         
        }
        else 
        {
            $scope.showProgress = true;
            $scope.textProgress = 'Пустые значения для номера и даты получения';
        }
  
    };

    $scope.deleteCert = function () {
        $scope.currentCert.DateArchieve = '';
        $scope.currentCert.CertNumber = '';
        $scope.currentCert.Issuer = '';
        $scope.showAddOrUpdateCert(true);
        $scope.showProgress = false;
    };



    function init() {
        $http({
            cache: false,
            url: root + "Rest/GetUserCertsIds",
            method: "GET",
            params: { 'killcache': new Date().getTime() }
        }).success(function (data, status, headers, config) {          
            $scope.usersCerts = data;           
        }).error(function (e) {
   
        });
    };

    init();


};

myApp.controller("certController", certController);