(function () {
    var app = angular.module("clientModule1", []);
    var comController = function ($scope, $http) {
        

        
        var onRequestError = function (reason) {
            hideShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }
        var onSaveComplete = function (response) {
            $scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            //$scope.apply();
            hideShield();
            $scope.$apply();
            
        }
        
        var isValid = function () {
            if (isnullorEmpty($scope.selectedObject.CompanyCode))
                $scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject.BranchCode))
                $scope.listError = "Branch Code is Required";
            else if (isnullorEmpty($scope.selectedObject.BankName))
                $scope.listError = "Bank Name is Required";
            else if (isnullorEmpty($scope.selectedObject.ShortName))
                $scope.listError = "Short Name is Required";
            else if (isnullorEmpty($scope.selectedObject.BankBranch))
                $scope.listError = "BankBranch is Required";
            else if (isnullorEmpty($scope.selectedObject.Address))
                $scope.listError = "Address is Required";
            else if (isnullorEmpty($scope.selectedObject.Addressee))
                $scope.listError = "Addressee is Required";
            else if (isnullorEmpty($scope.selectedObject.AccountTitle))
                $scope.listError = "Account Title is Required";
            else if (isnullorEmpty($scope.selectedObject.AccountType))
                $scope.listError = "Account Type is Required";
            else if (isnullorEmpty($scope.selectedObject.AccountNo))
                $scope.listError = "Account# is Required";
            else if (isnullorEmpty($scope.selectedObject.AccountCode))
                $scope.listError = "Account Code is Required";
            else if (isnullorEmpty($scope.selectedObject.Status))
                $scope.listError = "Status is Required";
            else return true;
            return false;
            //$scope.selectedObject.BranchCode
            
            //
            //
            //
            //
            //
            //
            //
            //
        }
        var clearErros = function () {
            $scope.listError = $scope.saveError = '';
        }
        $scope.save = function () {
            clearErros();
            if (!isValid()) return;
            $scope.saveError = '';
            showShield();
            if ($scope.selectedObject != null && typeof $scope.selectedObject != 'undefined' && $scope.selectedObject.AccountCode != '') {
                $scope.selectedObject.Status = $scope.selectedObject.Status == "1" ? true : false;
                
                $http.post("/UI/saveBank", { Bank: $scope.selectedObject}).then(onSaveComplete, onRequestError);//saif
            }
        }
        var onDelete = function (response) {
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.clear();
            $scope.saveError = 'Record deleted successfully';
        }
        $scope.delete = function () {
            clearErros();
            $scope.saveError = '';
            if (confirm('Are you sure you want to delete this record?'))
                $http.post("/UI/deleteBank", $scope.selectedObject).then(onDelete, onRequestError)
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
            $scope.selectedObject = {};
        }
        
        
        
        $scope.isEditMode = false;
        $scope.selectedObject = {};
        
        
        
                
        hideAllShield();
        //$http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        
    }
    app.controller("BanksController", comController);
}
)();