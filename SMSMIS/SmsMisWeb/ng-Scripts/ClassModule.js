(function () {
    var app = angular.module("adminModule1", []);
    var claController = function ($scope, $http,$filter) {
        $scope.$watch('selectedObject.ClassName', function (newValue, oldValue) { if (newValue.length > 50) $scope.selectedObject.ClassName = oldValue; });
        $scope.$watch('selectedObject.ShortName', function (newValue, oldValue) { if (newValue.length > 10) $scope.selectedObject.ShortName = oldValue; });

        $scope.CompanyCode_Change = function () {
            var vCompanyId = $scope.selectedObject.CompanyCode
            $scope.listData = $filter('filter')($scope.listDataEx, { 'CompanyCode': $scope.selectedObject.CompanyCode });
            $scope.selectedObject = {};
            $scope.selectedObject.Status = true;
            $scope.selectedObject.CompanyCode = vCompanyId;
        }

        var onGetCompany = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
        }

        var onListComplete = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listData = response.data;
            $scope.listDataEx = response.data;
            $scope.selectedObject.Status = true;
        }
        var onSaveComplete = function (response) {
            hideShield();
            //$scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.listDataEx = response.data;
            var selectedCompany = $scope.selectedObject.CompanyCode;

            $scope.clear(); //clear on add new mode
            $scope.selectedObject.CompanyCode = selectedCompany;
            $scope.CompanyCode_Change();

            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.apply();
        }

        var onDelete = function (response) {
            hideShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.listDataEx = response.data;
            var selectedCompany = $scope.selectedObject.CompanyCode;

            $scope.clear(); //clear on add new mode
            $scope.selectedObject.CompanyCode = selectedCompany;
            $scope.CompanyCode_Change();
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            //$scope.saveError = 'Record deleted successfully';
        }

        $scope.isValid = function () {
            return ((myForm.ClassName.value == '')
            || (myForm.ShortName.value == '')
            || ($scope.selectedObject.Status == undefined ? true : false)
            || (myForm.companyCode.value == ''));
        }

        var onRequestError = function (reason) {
            hideShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
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
            $scope.saveError = '';
            $scope.listError = '';
            $scope.isEditMode = false;
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.selectedObject.Status = true;
        }
        $scope.save = function () {
            showShield();
            $scope.saveError = '';
            $http.post("/UI/saveClass", $scope.selectedObject).then(onSaveComplete, onRequestError)
        }
        $scope.delete = function () {
            showShield();
            $scope.saveError = '';

            if (confirm('Are you sure you want to delete this record?'))
                $http.post("/UI/deleteClass", $scope.selectedObject).then(onDelete, onRequestError)
            hideShield();
        }
        $scope.isEditMode = false;
        $scope.clear();
        $scope.saveError = '';
        showShield();
        $http.get("/UI/getAllClasses").then(onListComplete, onRequestError);
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        hideShield();
    }

    app.controller("classController", claController);
}
)();