(function () {
    var app = angular.module("clientModule1", []);
    var comController = function ($scope, $http,$filter) {

        var onGetCompany = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listCompany = true;
            $scope.listCompany = response.data;
            //if ($scope.listCompany != null && $scope.listCompany.length > 0) {
            //    if (typeof $scope.selectedObject[0] == 'undefined' || $scope.selectedObject[0])
            //        setDefault(0, 0);
            //    $scope.selectedObject[0].CompanyCode = $scope.listCompany[0].CompanyCode;

            //}
            //$scope.getCOACompanyWise();
            GetCompany = true;
            hideAllShield();
            //isFormLoaded();
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
            
            //if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            $scope.clear();
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            //$scope.apply();
            $scope.showGrid();
            hideAllShield();
            $scope.$apply();
        }

        var isValid = function () {
            if (isnullorEmpty($scope.selectedObject.CompanyCode))
                return false;//$scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject.BranchCode))
                return false;//$scope.listError = "Branch Code is Required";
            else if (isnullorEmpty($scope.selectedObject.DiscountTypeName))
                return false;//$scope.listError = "Bank is Required";
            else if (isnullorEmpty($scope.selectedObject.ShortName))
                return false;//$scope.listError = "Instrument Type is Required";
            else if (isnullorEmpty($scope.selectedObject.Status))
                return false;//$scope.listError = "Status is Required";
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
            //if (!isAlreadyExits()) return;
            $scope.saveError = '';
            showSaveShield();
            //$scope.selectedObjectCopy = angular.copy($scope.selectedObject);
            
            $scope.selectedObject.Status = $scope.selectedObject.Status == "1" ? true : false;
            var isNew = true;
            if ($scope.isEditMode) isNew = false;
            $http.post("/UI/saveStudentFixedDiscountType", { StudentFixedDiscountType: $scope.selectedObject, 'isNew': isNew }).then(onSaveComplete, onRequestError);

        }
        var onDelete = function (response) {
            hideAllShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            //if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            $scope.clear();
            $scope.showGrid();
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            
            //$scope.saveError = 'Record deleted successfully';
        }
        $scope.delete = function () {
            clearErros();
            $scope.saveError = '';
            if (confirm('Are you sure you want to delete this record?')) {
                showDeleteShield();
                $http.post("/UI/deleteStudentFixedDiscountType", $scope.selectedObject).then(onDelete, onRequestError)
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
        $scope.getCompanyWise = function () {
            GetAllBranch = ListComplete = false;
            showShield();
            $scope.clear();
            $http.post("/UI/getAllActiveBranchesCompanyWise", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllBranch, onRequestError);
            $http.post("/UI/getAllStudentFixedDiscountType", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onListComplete, onRequestError);

        }
        $scope.showGrid = function () {
            $scope.filteredArray = {};
            $scope.clear();
            $scope.isEditMode = false;
            if (!angular.isUndefined($scope.selectedObject.CompanyCode) && !angular.isUndefined($scope.selectedObject.BranchCode) && $scope.selectedObject.BranchCode != null)
                $scope.filteredArray = $filter('filter')($scope.listData, { CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObject.BranchCode }, true);
        }
        $scope.clear = function () {
            clearErros();
            $scope.isEditMode = false;
            var ccode = angular.isUndefined($scope.selectedObject.CompanyCode) ? null : $scope.selectedObject.CompanyCode;
            var bcode = angular.isUndefined($scope.selectedObject.BranchCode) ? null : $scope.selectedObject.BranchCode;
            $scope.selectedObject = { CompanyCode: ccode, BranchCode: bcode, DiscountTypeName: '', ShortName: '', Status: "1" };
        }
        $scope.listBranch = [];
        $scope.listData = [];
        $scope.listCompany = [];
        $scope.isEditMode = false;
        $scope.listCOA = [];
        $scope.selectedObject = { CompanyCode: null, BranchCode: null, DiscountTypeName: '', ShortName: '', Status: "1" };
        
        $scope.listInstrumentType = [];
        var isFormLoaded = function () {
            if (GetAllBranch && GetCompany && ListComplete ) {
                hideAllShield();
                $scope.showGrid();
            }
        }
        var GetAllBranch = false;
        var GetCompany = false;
        var GetInstrumentType = false;
        var ListComplete = false;
        var GetCOA = false;
        $scope.filteredArray = {};
        
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        

    }
    app.controller("StudentFixedDiscountTypeController", comController);
    addDirectives(app);
}
)();