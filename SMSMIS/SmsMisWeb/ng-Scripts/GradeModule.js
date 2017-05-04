(function () {
    var app = angular.module("adminModule1", []);
    var couController = function ($scope, $http,$filter) {

        var onGetCompany = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
        }
        $scope.CompanyCode_Change = function () {
            //if (!$scope.isEditMode) {
            showShield();
            $scope.selectedObject.ShortName = $scope.selectedObject.GradeName = '';
            $scope.isEditMode = false;
            $http.post("/UI/getAllGrade", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onListComplete, onRequestError);
            $http.post("/UI/getAllActiveBranchesCompanyWise", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllBranch, onRequestError);

        }
        
        var onGetAllBranch = function (response) {
            //hideShield();
            $scope.listBranch = response.data;
            //$scope.listBranchMaster = angular.copy($scope.listBranch);
            fillBranch();
        }

        var onListComplete = function (response) {
            hideAllShield();
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listData = response.data;
            $scope.listDataEx = response.data;
            $scope.selectedObject.Status = true;
        }

        

        var GetSelectedBranches = function () {
            return $filter('filter')($scope.listBranch, { 'isSelected': true });
        }

        var onSaveComplete = function (response) {
            hideAllShield();
            //$scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.listDataEx = response.data;

            var selectedCompany = $scope.selectedObject.CompanyCode;

            $scope.clear(); //clear on add new mode
            $scope.selectedObject.CompanyCode = selectedCompany;
            //$scope.CompanyCode_Change();
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.apply();
        }

        var onDelete = function (response) {
            hideAllShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.listDataEx = response.data;
            var selectedCompany = $scope.selectedObject.CompanyCode;

            $scope.clear(); //clear on add new mode
            $scope.selectedObject.CompanyCode = selectedCompany;
            //$scope.CompanyCode_Change();
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            //$scope.saveError = 'Record deleted successfully';
        }

        $scope.isValid = function () {
            return ((myForm.GradeName.value == '')
            || (myForm.ShortName.value == '')
            || ($scope.selectedObject.Status == undefined ? true : false)
            || (myForm.companyCode.value == ''));
        }

        var onRequestError = function (reason) {
            hideAllShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }
        $scope.load = function (obj) {
            showShield();
            $scope.clear();
            if (obj != null) {
                $scope.selectedObject = angular.copy(obj);

                for (var x in $scope.selectedObject.GradeBranch) {

                    var vBranchCode = $scope.selectedObject.GradeBranch[x].BranchCode;
                    var objList = $filter('filter')($scope.listBranch, { BranchCode: vBranchCode, CompanyCode: $scope.selectedObject.CompanyCode }, function (actual, expected) { return angular.equals(expected, actual); });
                    if (typeof objList != 'undefined' && objList != null && objList.length > 0) objList[0].isSelected = true;

                }


                if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                $scope.isEditMode = true;
            }
            $scope.saveError = '';
            hideAllShield();
        }

        


        $scope.clear = function () {
            $scope.selectedObject = {};
            $scope.saveError = '';
            $scope.listError = '';
            $scope.isEditMode = false;
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.selectedObject.Status = true;
            
            fillBranch();


        }
        var fillBranch = function () {
            for (var x in $scope.listBranch)
                $scope.listBranch[x].isSelected = true;
        }
        $scope.save = function () {
            $scope.selectedObject.GradeBranch = GetSelectedBranches();
            if ($scope.selectedObject.GradeBranch == null || $scope.selectedObject.GradeBranch.length < 1)
                return;
            showSaveShield();
            $scope.saveError = '';

            //$scope.selectedObject.GradeBranch = GetSelectedBranches();
            //$scope.isEditMode ? $scope.selectedObject.GradeCode:
            $http.post("/UI/saveGrade", { 'Grade': $scope.selectedObject, 'isNew': $scope.isEditMode ? false : true }).then(onSaveComplete, onRequestError);
        }
        $scope.delete = function () {
            showDeleteShield();
            $scope.saveError = '';

            if (confirm('Are you sure you want to delete this record?'))
                $http.post("/UI/deleteGrade", $scope.selectedObject).then(onDelete, onRequestError)
            hideAllShield();
        }
        $scope.isEditMode = false;
        $scope.clear();
        $scope.saveError = '';
        showShield();
        
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        
        //hideShield();
    }

    app.controller("gradeController", couController);
}
)();