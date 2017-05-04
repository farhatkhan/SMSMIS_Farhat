(function () {
    var app = angular.module("clientModule1", []);
    var comController = function ($scope, $http) {


        var onListComplete = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listData = true;
            $scope.listData = response.data;
            hideAllShield();
        }

        var onRequestError = function (reason) {
            hideAllShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }
        var onSaveComplete = function (response) {
            //$scope.saveError = 'Save Record successfully';
            hideAllShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";
            //if (!$scope.isEditMode)
                $scope.clear(); 
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            //$scope.apply();
            
            $scope.$apply();

        }

        var isValid = function () {
            if (isnullorEmpty($scope.selectedObject.CurrencyName))
                return false;//$scope.listError = "Currency Name is Required";
            else if (isnullorEmpty($scope.selectedObject.Symbol))
                return false;//$scope.listError = "Symbol is Required";
            else if (isnullorEmpty($scope.selectedObject.ShortName))
                return false;//$scope.listError = "Short Name is Required";
            else if (isnullorEmpty($scope.selectedObject.Status))
                return false;//$scope.listError = "Status is Required";
            else return true;
            return false;
        }
        var clearErros = function () {
            $scope.listError = $scope.saveError = '';
        }
        $scope.save = function () {
            clearErros();
            if (!isValid()) return;

            showSaveShield();

            $scope.selectedObject.Status = $scope.selectedObject.Status == "1" ? true : false;

            $http.post("/UI/saveCurrency", { Currency: $scope.selectedObject }).then(onSaveComplete, onRequestError);//saif

        }
        var onDelete = function (response) {
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            //if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.clear();
            //$scope.saveError = 'Record deleted successfully';
        }
        $scope.delete = function () {
            clearErros();
            $scope.saveError = '';
            if (confirm('Are you sure you want to delete this record?')) {
                showDeleteShield();
                $http.post("/UI/deleteCurrency", $scope.selectedObject).then(onDelete, onRequestError)
            }
        }
        $scope.load = function (obj) {
            clearErros();
            if (obj != null) {
                $scope.selectedObject = {};
                $scope.selectedObject = angular.copy(obj);
                $scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";
                if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                $scope.isEditMode = true;
            }
            $scope.saveError = '';
        }
        $scope.clear = function () {
            clearErros();
            $scope.isEditMode = false;
            $scope.selectedObject = { 'Status': '1' };
        }

        $scope.listData = [];
        $scope.listCompany = [];

        $scope.isEditMode = false;
        $scope.selectedObject = { 'Status': '1' };
        $http.get("/UI/getAllCurrency").then(onListComplete, onRequestError);
    }
    app.controller("CurrencyController", comController);
}
)();