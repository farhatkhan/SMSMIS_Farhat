(function () {
    var app = angular.module("clientModule1", []);
    var comController = function ($scope, $http) {

        var onGetCompany = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listCompany = true;
            $scope.listCompany = response.data;
            GetCompany = true;
            hideAllShield();
            //isFormLoaded();
        }
        $scope.onCompanyChange = function () {
            showShield();

            $scope.clear();

            $http.post("/UI/getAllItemModelCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onListComplete, onRequestError);
        }
        var onListComplete = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listData = true;
            $scope.listData = response.data;
            ListComplete = true;
            isFormLoaded();
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
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";
            //if (!$scope.isEditMode)
            $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            hideAllShield();
            $scope.$apply();

        }
        var isValid = function () {
            if (isnullorEmpty($scope.selectedObject.CompanyCode))
                return false;//$scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject.ModelName))
                return false;//$scope.listError = "Model Name is Required";
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
            $scope.saveError = '';
            showSaveShield();
            $scope.selectedObject.Status = $scope.selectedObject.Status == "1" ? true : false;
            $http.post("/UI/saveItemModel", { ItemModel: $scope.selectedObject, companyId: $scope.selectedObject.CompanyCode }).then(onSaveComplete, onRequestError);//saif
        }
        var onDelete = function (response) {
            hideAllShield();
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
                $http.post("/UI/deleteItemModel", $scope.selectedObject).then(onDelete, onRequestError)
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
            $scope.isEditMode = false;
            var ccode = $scope.selectedObject.CompanyCode;
            $scope.selectedObject = {  Status: '1', CompanyCode: ccode };
        }
        $scope.listData = [];
        $scope.listCompany = [];
        $scope.isEditMode = false;
        $scope.selectedObject = {  Status: '1' };
        var isFormLoaded = function () {
            if (GetCompany && ListComplete) {
                hideAllShield();
            }
        }

        var GetCompany = false;

        var ListComplete = false;


        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);


    }
    app.controller("ItemModelController", comController);
}
)();