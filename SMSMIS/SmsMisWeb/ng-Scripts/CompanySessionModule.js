(function () {
    var app = angular.module("adminModule1", []);
    var comController = function ($scope, $http, $filter) {

        $scope.$watch('selectedObject.SessionName', function (newValue, oldValue) { if (newValue.length > 50) $scope.selectedObject.SessionName = oldValue; });
        $scope.$watch('selectedObject.ShortName', function (newValue, oldValue) { if (newValue.length > 10) $scope.selectedObject.ShortName = oldValue; });


        var onGetCompany = function (response) {
            showShield();
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
            hideShield();
        }
        var onListComplete = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listData = response.data;
            $scope.listDataEx = response.data;
            $scope.selectedObject.Status = true;
        }
        $scope.CompanyCode_Change = function () {
            $scope.listData = $filter('filter')($scope.listDataEx, { 'CompanyCode': $scope.selectedObject.CompanyCode });
            $scope.selectedObject.SessionName = $scope.selectedObject.ShortName = '';
            $scope.selectedObject.Status = true;
            $scope.isEditMode = false;
            hideShield();
        }

        var onSaveComplete = function (response) {
            hideShield();
            //$scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.listDataEx = response.data;

            var companyCode = $scope.selectedObject.CompanyCode;
            $scope.clear(); //clear on add new mode
            $scope.selectedObject.CompanyCode = companyCode;
            $scope.CompanyCode_Change();
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.apply();

        }

        var onDelete = function (response) {
            hideShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.listDataEx = response.data;
            var companyCode = $scope.selectedObject.CompanyCode;
            $scope.clear(); //clear on add new mode
            $scope.selectedObject.CompanyCode = companyCode;
            $scope.CompanyCode_Change();
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            //$scope.saveError = 'Record deleted successfully';
        }

        $scope.isValid = function () {
            return ((myForm.SessionName.value == '')
            || (myForm.ShortName.value == '')
            || ($scope.selectedObject.Status == undefined ? true : false)
            || (($scope.selectedObject.CompanyCode > 0) ? false : true)
                );
        }

        var onRequestError = function (reason) {
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
            $scope.selectedObject.Status = true;
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
        }
        $scope.save = function () {
            showShield();
            $scope.saveError = '';
            $http.post("/UI/saveCompanySession", $scope.selectedObject).then(onSaveComplete, onRequestError)
        }
        $scope.delete = function () {
            $scope.saveError = '';
            showShield();
            if (confirm('Are you sure, you want to delete this session?'))
                $http.post("/UI/deleteCompanySession", $scope.selectedObject).then(onDelete, onRequestError)
            else
                hideShield();
        }

        $scope.isEditMode = false;
        $scope.clear();
        $scope.saveError = '';
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        $http.get("/UI/getAllSessions").then(onListComplete, onRequestError);
        hideShield();

    }

    app.controller("companysessionController", comController);
}
)();