(function () {
    var app = angular.module("clientModule1", []);
    var admController = function ($scope, $http,$filter) {
        var onGetCompany = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listCompany = true;
            $scope.listCompany = response.data;
            if ($scope.listCompany != null && $scope.listCompany.length > 0) {
                $scope.selectedObject.CompanyCode = $scope.listCompany[0].CompanyCode;
            }
            GetCompany =true;
            $scope.getCompanyWise();
        }
        $scope.getCompanyWise = function () {
            showShield();
            GetAllData  = GetAllSession = GetAllBranch = GetAllStudent = false;
            $http.post("/UI/getAllActiveSessionsCompany", { 'CompanyCode': $scope.selectedObject.CompanyCode }).then(onGetAllSession, onRequestError);
            $http.post("/UI/getAllActiveBranchesCompanyWise", { 'companyCode': $scope.selectedObject.CompanyCode }).then(onGetAllBranch, onRequestError);
            $http.post("/UI/getAllStudentofCompany", { 'CompanyCode': $scope.selectedObject.CompanyCode }).then(onGetAllStudent, onRequestError);
            $http.post("/UI/getAllStudentBillingCycle", { 'CompanyCode': $scope.selectedObject.CompanyCode }).then(onGetAllData, onRequestError);
            $scope.showGrid();
        }
        $scope.showGrid = function () {
            $scope.filteredArray = {};
            if (!angular.isUndefined($scope.selectedObject.CompanyCode) && !angular.isUndefined($scope.selectedObject.BranchCode) && !angular.isUndefined($scope.selectedObject.SessionCode))
                $scope.filteredArray = $filter('filter')($scope.listData, { CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObject.BranchCode, SessionCode: $scope.selectedObject.SessionCode }, true);
        }
        var onGetAllBranch = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listData = true;
            $scope.listBranch = response.data;
                GetAllBranch=true;
                isFormLoaded();
        }

        var onGetAllSession = function (response) {
            if (response.data == null || typeof response.data == 'undefined') listData = true;
            $scope.listSession = response.data;
            GetAllSession =true;
                isFormLoaded();
        }
        var onGetAllStudent = function (response) {
            if (response.data == null || typeof response.data == 'undefined') listData = true;
            $scope.listStudent = response.data;
            GetAllStudent =true;
                isFormLoaded();
        }
        var onGetAllData = function (response) {
            if (response.data == null || typeof response.data == 'undefined') listData = true;
            $scope.listData = response.data;
            GetAllData = true;
                isFormLoaded();
        }
        var onRequestError = function (reason) {
            clearErros();
            hideShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }
        var onSaveComplete = function (response) {
            clearErros();
            //$scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            if (!$scope.isEditMode) { saveState(); $scope.clear(); } //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            hideShield();
            $scope.showGrid();
            $scope.$apply();

        }
        var isValid = function () {
            if (isnullorEmpty($scope.selectedObject.CompanyCode))
                return false;//$scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject.BranchCode))
                return false;//$scope.listError = "Branch is Required";
            else if (isnullorEmpty($scope.selectedObject.SessionCode))
                return false;//$scope.listError = "Session is Required";
            else if (isnullorEmpty($scope.selectedObject.StudentNo))
                return false;//$scope.listError = "Student is Required";
            else if (isnullorEmpty($scope.selectedObject.BiilingCycle))
                return false;//$scope.listError = "Scholarship is Required";
            else return true;
            return false;
        }
        var isAlreadyExits = function () {
            var returnType = true;
            for (var x in $scope.listData)
                if ($scope.listData[x].CompanyCode == $scope.selectedObject.CompanyCode
                    && $scope.listData[x].BranchCode == $scope.selectedObject.BranchCode
            && $scope.listData[x].SessionCode == $scope.selectedObject.SessionCode
            && $scope.listData[x].StudentNo == $scope.selectedObject.StudentNo) {
                    $scope.listError = "Record Alredy Exist";
                    returnType=false;
                }
            return returnType;
        }
        $scope.save = function () {
            clearErros();

            if (!isValid()) return;
            //if (!isAlreadyExits()) return;
            showShield();
            if ($scope.selectedObject != null && typeof $scope.selectedObject != 'undefined' && $scope.selectedObject.StudentNo > 0) {
                var isNew = true;
                isNew = $scope.isEditMode == true ? false : true;
                $http.post("/UI/saveStudentBillingCycle", { StudentBillingCycle: $scope.selectedObject, isNew: isNew }).then(onSaveComplete, onRequestError);//saif
            }
        }
        var onDelete = function (response) {
            hideShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            saveState();
            if (!$scope.isEditMode) $scope.clear();  //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.clear();
            $scope.showGrid();
            //$scope.saveError = 'Record deleted successfully';
        }
        $scope.delete = function () {
            $scope.saveError = '';
                showShield();
            if (confirm('Are you sure you want to delete this record?'))
                $http.post("/UI/deleteStudentBillingCycle", $scope.selectedObject).then(onDelete, onRequestError)
        }
        $scope.load = function (obj) {
            if (obj != null) {
                $scope.selectedObject = {};
                $scope.selectedObject = angular.copy(obj);
                if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                if ($scope.selectedObject.IsNew)
                    $scope.isEditMode = false;
                else $scope.isEditMode = true;
            }
            $scope.saveError = '';
        }
         var clearErros = function () {
            $scope.listError = $scope.saveError = '';
        }
        $scope.clear = function () {
            clearErros();
            $scope.isEditMode = false;
            $scope.selectedObject = {};
            $scope.selectedObject.CompanyCode = ccode;
            $scope.selectedObject.BranchCode = bcode;
            $scope.selectedObject.SessionCode = scode;

            ccode = bcode = scode = '';
            $scope.filteredArray = {};
        }
        function saveState() {
            ccode = $scope.selectedObject.CompanyCode;
            bcode = $scope.selectedObject.BranchCode;
            scode = $scope.selectedObject.SessionCode;
        }
        var ccode = '';
        var bcode = '';
        var scode = '';
        var isFormLoaded = function () {
            if (GetAllData && GetCompany && GetAllSession && GetAllBranch && GetAllStudent) 
                hideShield();
        
        }
        $scope.listStudent = [];
        $scope.listSession = [];
        $scope.listBranch = [];
        $scope.listData = [];
        $scope.listCompany = [];
        $scope.isEditMode = false;
        $scope.selectedObject = {};

        var GetAllData = false;
        var GetAllStudent = false;
        var GetAllBranch = false;
        var GetAllSession = false;
        var GetCompany = false;

        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        

    }
    app.controller("studentBillingCycleController", admController);
}
)();