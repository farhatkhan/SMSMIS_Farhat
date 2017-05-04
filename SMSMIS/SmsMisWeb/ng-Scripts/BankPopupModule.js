(function () {
    var app = angular.module("clientModule1", []);
    var comController = function ($scope, $http, $filter, $location) {

        
        var onRequestError = function (reason) {
            hideShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }
        var onSaveComplete = function (response) {
            parent.closepopup();
            //$scope.saveError = 'Save Record successfully';
            //if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            //$scope.listData = response.data;
            //$scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";
            //if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            //if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            
            //hideShield();
            //$scope.$apply();

        }

        var isValid = function () {
            if (isnullorEmpty($scope.selectedObject.CompanyCode))
                return false; //$scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject.BankName))
                return false; //$scope.listError = "Bank Name is Required";
            else if (isnullorEmpty($scope.selectedObject.ShortName))
                return false; //$scope.listError = "Short Name is Required";
            else if (isnullorEmpty($scope.selectedObject.BankBranch))
                return false; //$scope.listError = "BankBranch is Required";
            else if (isnullorEmpty($scope.selectedObject.Address))
                return false; //$scope.listError = "Address is Required";
            else if (isnullorEmpty($scope.selectedObject.Addressee))
                return false; //$scope.listError = "Addressee is Required";
            else if (isnullorEmpty($scope.selectedObject.AccountTitle))
                return false; //$scope.listError = "Account Title is Required";
            else if (isnullorEmpty($scope.selectedObject.AccountType))
                return false; //$scope.listError = "Account Type is Required";
            else if (isnullorEmpty($scope.selectedObject.AccountNo))
                return false; //$scope.listError = "Account# is Required";
            else if (isnullorEmpty($scope.selectedObject.AccountCode))
                return false; //$scope.listError = "Account Code is Required";
            
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

                $http.post("/UI/saveBank", { Bank: $scope.selectedObject }).then(onSaveComplete, onRequestError);//saif
            }
        }
        var onDelete = function (response) {
            parent.closepopup();
            //if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            //$scope.listData = response.data;
            //if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            //if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            
            //$scope.clear();
            //$scope.saveError = 'Record deleted successfully';
        }
        $scope.delete = function () {
            clearErros();
            $scope.saveError = '';
            if (confirm('Are you sure you want to delete this record?'))
                $http.post("/UI/deleteBank", $scope.selectedObject).then(onDelete, onRequestError)
        }
        //$scope.load = function (obj) {
        //    clearErros();
        //    if (obj != null) {
        //        $scope.selectedObject = {};
        //        $scope.selectedObject = angular.copy(obj);
        //        $scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";
        //        if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
        //        $scope.isEditMode = true;
        //    }
        //    $scope.saveError = '';
        //}
        $scope.clear = function () {
            clearErros();
            $scope.isEditMode = false;
            $scope.selectedObject = {};
        }
        var onGetList = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listData = true;
            if (response.data != ""){
                $scope.selectedObject = response.data;
                if (angular.isObject($scope.selectedObject))
                    $scope.isEditMode = true;
                    }

        }
        $scope.isEditMode = false;
        $scope.selectedObject = {};
        $scope.selectedObject.CompanyCode = parent.document.getElementById('hdnCompany').value;
        $scope.selectedObject.AccountCode = parent.document.getElementById('hdnAC').value;
        $http.post("/UI/SelectBankByAccountCode", { AccountCode: $scope.selectedObject.AccountCode, CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetList, onRequestError);
        hideAllShield();
        
    }
    app.controller("BanksController", comController);
}
)();