function fileUploadDirective() {
    return {
        restrict: 'E',
        templateUrl: './templates/fileform.html',
        scope: {
            allowed: '@',
            url: '@',
            autoUpload: '@',
            sizeLimit: '@',
            ngModel: '=',
            name: '@'
        },
        controller: ['$scope', '$element', 'fileUpload', function (
            $scope, $element, fileUpload) {
            $scope.$on('fileuploaddone', function (e, data) {
                fileUpload.addFieldData($scope.name, data._response.result.files[0].result);
            });

            $scope.options = {
                url: $scope.url,
                dropZone: $element,
                maxFileSize: $scope.sizeLimit,
                autoUpload: $scope.autoUpload
            };
            $scope.loadingFiles = false;

            if (!$scope.queue) {
                $scope.queue = [];
            }

            var generateFileObject = function generateFileObjects(objects) {
                angular.forEach(objects, function (value, key) {
                    var fileObject = {
                        name: value.filename,
                        size: value.length,
                        url: value.url,
                        thumbnailUrl: value.url,
                        deleteUrl: value.url,
                        deleteType: 'DELETE',
                        result: value
                    };

                    if (fileObject.url && fileObject.url.charAt(0) !== '/') {
                        fileObject.url = '/' + fileObject.url;
                    }

                    if (fileObject.deleteUrl && fileObject.deleteUrl.charAt(0) !== '/') {
                        fileObject.deleteUrl = '/' + fileObject.deleteUrl;
                    }

                    if (fileObject.thumbnailUrl && fileObject.thumbnailUrl.charAt(0) !== '/') {
                        fileObject.thumbnailUrl = '/' + fileObject.thumbnailUrl;
                    }

                    $scope.queue[key] = fileObject;
                });
            };
            fileUpload.registerField($scope.name);
            $scope.filequeue = fileUpload.fieldData[$scope.name];

            $scope.$watchCollection('filequeue', function (newval) {
                generateFileObject(newval);
            });
        }]
    };
};