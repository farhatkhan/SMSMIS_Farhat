(function () {
    var app = angular.module("adminModule", []);
    var regController = function ($scope, $http) {
        var onListComplete = function (response) {
            $scope.listData = response.data;
        }
        var onSaveComplete = function (response) {
            hideShield();
            $scope.listData = response.data;
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.apply();
        }
        var onRequestError = function (reason) {
            hideShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.saveError = reason.data.error;
            else
                $scope.saveError = reason.status + ': ' + reason.statusText;
        }
        var onDeleteComplete = function (response) {
            hideShield();
            $scope.listData = response.data;
            $scope.clear();
            $scope.apply();
        }
        $scope.load = function (obj) {
            showShield();
            if (obj != null) {
                $scope.selectedObject = angular.copy(obj);
                if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                $scope.isEditMode = true;
            }
            $scope.saveError = '';
            hideShield();
        }
        $scope.clear = function () {
            $scope.selectedObject = {};
            $scope.isEditMode = false;
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
        }
        $scope.delete = function () {
            showShield();
            $scope.saveError = '';
            if(confirm('Are you sure you want to delete this record?'))
                $http.post("/UI/deleteRegion", $scope.selectedObject).then(onDeleteComplete, onRequestError)
            hideShield();
        }
        $scope.save = function () {
            showShield();
            $scope.saveError = '';
            $http.post("/UI/saveRegion", $scope.selectedObject).then(onSaveComplete, onRequestError)
        }
        $scope.isEditMode = false;
        $scope.clear();
        $scope.saveError = '';
        $http.get("/UI/getRegions").then(onListComplete, onRequestError);
        hideShield();
    }
    app.controller("regionsController", regController);
}
)();