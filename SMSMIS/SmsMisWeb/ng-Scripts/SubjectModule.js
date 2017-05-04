(function () {
    var app = angular.module("adminModule1", []);
    var subController = function ($scope, $http) {

        $scope.$watch('selectedObject.SubjectName', function (newValue, oldValue) { if (newValue.length > 50) $scope.selectedObject.SubjectName = oldValue; });
        $scope.$watch('selectedObject.ShortName', function (newValue, oldValue) { if (newValue.length > 10) $scope.selectedObject.ShortName = oldValue; });


        var onListComplete = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listData = response.data;
            $scope.selectedObject.Status = true;
        }
        var onSaveComplete = function (response) {
            hideShield();
            //$scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.apply();
        }
        var onDelete = function (response) {
            hideShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.clear();
            //$scope.saveError = 'Record deleted successfully';
        }

        $scope.isValid = function () {
            return ((myForm.SubjectName.value == '')
            || (myForm.ShortName.value == '')
            || ($scope.selectedObject.Status == undefined ? true : false));
        }

        var onRequestError = function (reason) {
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }
        $scope.load = function (obj) {
            if (obj != null) {
                $scope.selectedObject = angular.copy(obj);
                if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                $scope.isEditMode = true;
            }
            $scope.saveError = '';
        }
        $scope.clear = function () {
            $scope.selectedObject = {};
            $scope.saveError = '';
            $scope.listError = '';
            $scope.isEditMode = false;
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.selectedObject.Status = true;
        }
        $scope.save = function () {
            showShield();
            $scope.saveError = '';
            $http.post("/UI/saveSubject", $scope.selectedObject).then(onSaveComplete, onRequestError)
        }
        $scope.delete = function () {
            showShield();
            $scope.saveError = '';
            if (confirm('Are you sure you want to delete this record?'))
                $http.post("/UI/deleteSubject", $scope.selectedObject).then(onDelete, onRequestError)
            hideShield();
        }

        $scope.isEditMode = false;
        $scope.clear();
        $scope.saveError = '';
        showShield();
        $http.get("/UI/getAllSubjects").then(onListComplete, onRequestError);
        hideShield();
    }

    app.controller("subjectController", subController);
}
)();