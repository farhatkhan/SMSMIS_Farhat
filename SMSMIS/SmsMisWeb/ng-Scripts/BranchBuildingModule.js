﻿(function () {
    var app = angular.module("adminModule1", []);
    var claController = function ($scope, $http, $filter) {

        $scope.CompanyCode_Change = function (cmd) {

            if (cmd == 'c')
            {
                $scope.selectedObject.BranchCode = $scope.selectedObject.BuildingName = $scope.selectedObject.ShortName = '';
            }

            if (cmd == 'b') {
                $scope.selectedObject.BuildingName = $scope.selectedObject.ShortName = '';
            }
            $scope.listData = null;
            $scope.listDataEx = null;

            if ($scope.selectedObject.CompanyCode > 0 && $scope.selectedObject.BranchCode > 0) {
                //showShield();
                var completedObject = $scope.selectedObject.CompanyCode + '/' + $scope.selectedObject.BranchCode;

                $http.post("/UI/getAllBranchBuilding", { 'strValues': completedObject }).then(onListComplete, onRequestError);
            }


            //$scope.listData = $filter('filter')($scope.listDataEx, { 'CompanyCode': $scope.selectedObject.CompanyCode, 'BranchCode': $scope.selectedObject.BranchCode });
        }

        var onGetCompany = function (response) {
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
            var selectedBranch = $scope.selectedObject.BranchCode;

            $scope.clear(); //clear on add new mode
            $scope.selectedObject.CompanyCode = selectedCompany;
            $scope.selectedObject.BranchCode = selectedBranch;
            $scope.CompanyCode_Change();

            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
        }

        var onDelete = function (response) {
            hideShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.listDataEx = response.data;
            var selectedCompany = $scope.selectedObject.CompanyCode;
            var selectedBranch = $scope.selectedObject.BranchCode;

            $scope.clear(); //clear on add new mode
            $scope.selectedObject.CompanyCode = selectedCompany;
            $scope.selectedObject.BranchCode = selectedBranch;
            
            $scope.CompanyCode_Change();
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            //$scope.saveError = 'Record deleted successfully';
        }

        $scope.isValid = function () {
            return ((myForm.BuildingName.value == '')
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
            
            if (obj != null) {
                showShield();
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
            $scope.saveError = '';
            showShield();
            $http.post("/UI/saveBranchBuilding", $scope.selectedObject).then(onSaveComplete, onRequestError)
        }
        $scope.delete = function () {
            $scope.saveError = '';

            if (confirm('Are you sure you want to delete this record?')) {
                showShield();
                $http.post("/UI/deleteBranchBuilding", $scope.selectedObject).then(onDelete, onRequestError)
            }
        }
        var onGetAllBranch = function (response) {
            $scope.listBranch = response.data;
        }

        showShield();

        $scope.isEditMode = false;
        $scope.clear();
        $scope.saveError = '';
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        $http.get("/UI/getAllActiveBranches").then(onGetAllBranch, onRequestError);

        hideShield();
    }

    app.controller("buildingcodeController", claController);
}
)();