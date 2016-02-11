
function FileDestroyController($scope, $http, fileUpload) {
    var file = $scope.file,
        state;

    if ($scope.$parent && $scope.$parent.$parent && $scope.$parent.$parent.$parent.name) {
        $scope.fieldname = $scope.$parent.$parent.$parent.name;
    }

    if (!fileUpload.fieldData[$scope.name]) {
        fileUpload.fieldData[$scope.name] = [];
    }

    $scope.filequeue = fileUpload.fieldData;

    if (file.url) {
        file.$state = function () {
            return state;
        };
        file.$destroy = function () {
            state = 'pending';
            return $http({
                url: file.deleteUrl,
                method: file.deleteType
            }).then(
                function () {
                    state = 'resolved';
                    fileUpload.removeFieldData($scope.fieldname, file.result._id);
                    $scope.clear(file);
                },
                function () {
                    state = 'rejected';
                    fileUpload.removeFieldData($scope.fieldname, file.result._id);
                    $scope.clear(file);
                }
                );


        };
    } else if (!file.$cancel && !file._index) {
        file.$cancel = function () {
            $scope.clear(file);
        };
    }
};
