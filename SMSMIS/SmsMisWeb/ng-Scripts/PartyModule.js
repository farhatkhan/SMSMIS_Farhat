(function () {
    var app = angular.module("clientModule1", []);
    var comController = function ($scope, $http) {

        var onGetCompany = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listCompany = true;
            $scope.listCompany = response.data;
            if ($scope.listCompany != null && $scope.listCompany.length > 0) {
                $scope.selectedObject.CompanyCode = $scope.listCompany[0].CompanyCode;

            }
            
            GetCompany = true;
            isFormLoaded();
        }
        
        var onListComplete = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listData = true;
            $scope.listData = response.data;
            ListComplete = true;
            isFormLoaded();
        }

        var onGetAllBranch = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listData = true;
            $scope.listBranch = response.data;
            GetAllBranch = true;
            isFormLoaded();
        }
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
            else if (isnullorEmpty($scope.selectedObject.PartyName))
                $scope.listError = "Party Name is Required";
            else if (isnullorEmpty($scope.selectedObject.ShortName))
                $scope.listError = "Short Name is Required";
            else if (isnullorEmpty($scope.selectedObject.Status))
                $scope.listError = "Status is Required";
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
            showShield();
            if ($scope.selectedObject != null && typeof $scope.selectedObject != 'undefined' && $scope.selectedObject.AccountCode != '') {
                $scope.selectedObject.Status = $scope.selectedObject.Status == "1" ? true : false;

                $http.post("/UI/saveParty", { Party: $scope.selectedObject }).then(onSaveComplete, onRequestError);//saif
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
                $http.post("/UI/deleteParty", $scope.selectedObject).then(onDelete, onRequestError)
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
        $scope.listBranch = [];
        $scope.listData = [];
        $scope.listCompany = [];
        $scope.listFeeTerm = [];
        $scope.isEditMode = false;
        $scope.selectedObject = {};
        
        var isFormLoaded = function () {
            if (GetAllBranch && GetCompany && ListComplete) {
                hideShield();
            }
        }
        var GetAllBranch = false;
        var GetCompany = false;
        
        var ListComplete = false;

        

        $http.get("/UI/getAllActiveBranches").then(onGetAllBranch, onRequestError);
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);

        $http.get("/UI/getAllParty").then(onListComplete, onRequestError);
    }
    app.controller("PartysController", comController);
}
)();