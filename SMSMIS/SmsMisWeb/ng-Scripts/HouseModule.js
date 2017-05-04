(function () {
    var app = angular.module("adminModule1", []);
    var houController = function ($scope, $http, $filter) {

        var onGetAllBranch = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listBranch = response.data;
        }
         
        var onGetCompany = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
        }

        $scope.CompanyCode_Change = function (cmd) {

            if (cmd == 'c')
            {
                $scope.selectedObject.BranchCode = $scope.selectedObject.HouseName = $scope.selectedObject.ShortName = '';
                $scope.selectedObject.Status = true;
            }

            if (cmd == 'b') {
                $scope.selectedObject.HouseName = $scope.selectedObject.ShortName = '';
                $scope.selectedObject.Status = true;
            }
            $scope.listData = null;
            $scope.listDataEx = null;
            if ($scope.selectedObject.CompanyCode > 0 && $scope.selectedObject.BranchCode > 0) {
                //showShield();
                var completedObject = $scope.selectedObject.CompanyCode + '/' + $scope.selectedObject.BranchCode;

                $http.post("/UI/getAllHouse", { 'strValues': completedObject }).then(onListComplete, onRequestError);
            }

        //$scope.listData = $filter('filter')($scope.listDataEx, { 'CompanyCode': $scope.selectedObject.CompanyCode, 'BranchCode': $scope.selectedObject.BranchCode });
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
        $scope.apply();

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
        return ((myForm.HouseName.value.trim() == '')
        || (myForm.ShortName.value == '')
        || ($scope.selectedObject.Status == undefined ? true : false)
        || (($scope.selectedObject.CompanyCode > 0) ? false : true)
        || (($scope.selectedObject.BranchCode > 0) ? false : true)
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
        
        $scope.saveError = '';
        showShield();
        $http.post("/UI/saveHouse", $scope.selectedObject).then(onSaveComplete, onRequestError)
    }
    $scope.delete = function () {
            
        $scope.saveError = '';

        if (confirm('Are you sure, you want to delete this House?')) {
            showShield();
            $http.post("/UI/deleteHouse", $scope.selectedObject).then(onDelete, onRequestError)
        }
        hideShield();
    }
        
    $scope.isEditMode = false;
    $scope.clear();
    $scope.saveError = '';
    showShield();
    $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
    $http.get("/UI/getAllActiveBranches").then(onGetAllBranch, onRequestError);
    hideShield();
}

    app.controller("houseController", houController);
}
)();