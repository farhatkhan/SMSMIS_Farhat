(function () {
    var app = angular.module("adminModule1", []);
    var branchController = function ($scope, $http, $filter) {


        $scope.Session_Change = function (o) {
            var opList = $filter('filter')($scope.BranchSessionList, { SessionCode: o, CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObject.BranchCode });
            var opListEx = $filter('filter')($scope.BranchSessionList, { SessionCode: '' });
            if (opList != undefined && opList.length > 1) {
                alert('Same session can not be defined multiple times,please rectify your change');
                opList[1].SessionCode = null;
                return;
            }

            if (o != null && opListEx.length <=2)
                $scope.addSession();
            hideShield();
        }

        $scope.addSession = function () {

            if ($scope.BranchSessionList == undefined)
                $scope.BranchSessionList = [];

            $scope.BranchSessionList.push(new NewSession());
        }

        var NewSession = function () {
            this.CompanyCode = $scope.selectedObject.CompanyCode;
            this.BranchCode = (($scope.selectedObject.BranchCode == undefined) ? '' : $scope.selectedObject.BranchCode);
            this.SessionCode = '';
        }


        $scope.loadSessions = function () {
            //
            $http.get("/UI/getAllBranchSession").then(onGetAllBranchSession, onRequestError);
        }

        var onGetAllBranchSession = function (response) {
             
            delete $scope.BranchSessionList;
            if ($scope.selectedObject.BranchCode != undefined && $scope.selectedObject.BranchCode != '') {
                $scope.BranchSessionList = $filter('filter')(response.data, { 'BranchCode': $scope.selectedObject.BranchCode, 'CompanyCode': $scope.selectedObject.CompanyCode });
                $scope.addSession();
            }
            else
                delete $scope.BranchSessionList;

            if ($scope.selectedObject.BranchCode == undefined) {
                showShield();
                $scope.selectedObject.BranchCode = $scope.listBranch[0].BranchCode;
                $scope.loadSessions();
            }
            hideShield();
            //$scope.BranchSessionList = response.data;
        }

        var onGetCompany = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
            hideShield();
        }
        var onListComplete = function (response) {
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listData = response.data;
            $scope.selectedObject.Status = true;
            hideShield();
        }
        var onSaveComplete = function (response) {
            hideShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            delete $scope.BranchSessionList;
            //$scope.saveError = 'Save Record successfully';
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.apply();

        }

        var onDelete = function (response) {
            hideShield();
            $scope.BranchSessionList = {};
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.clear();
            //$scope.saveError = 'Record deleted successfully';
        }

        $scope.deleteBranchSession = function (session) {
            showShield();
            if (confirm('Are you sure, you want to delete this Branch Session?'))
                $scope.BranchSessionList.splice($scope.BranchSessionList.indexOf(session), 1);
            hideShield();
        }

        $scope.isValid = function () {
            return ((myForm.companyCode.value == '')
                || (myForm.branchCode.value == '')
                );
        }

        var onRequestError = function (reason) {
            hideShield();
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
            showShield();
            if ($scope.BranchSessionList != undefined) {
                for (var i = 0; i < $scope.BranchSessionList.length; i++) {
                    var obj = $scope.BranchSessionList[i];
                    if ($scope.BranchSessionList[i].SessionCode == null || $scope.BranchSessionList[i].SessionCode == '') {
                        $scope.BranchSessionList.splice(i, 1);
                        i--;
                    }
                    //else {
                    //    SrNo = SrNo + 1;
                    //    obj.SrNo = SrNo
                    //}
                }
            }

            $scope.saveError = '';
            if ($scope.BranchSessionList.length > 0)
            $http.post("/UI/saveBranchSession", { branchSession: $scope.BranchSessionList, iCompanyCode: $scope.selectedObject.CompanyCode, iBranchCode: $scope.selectedObject.BranchCode }).then(onSaveComplete, onRequestError)
        }
        $scope.delete = function () {
            $scope.saveError = '';
            showShield();
            if (confirm('Are you sure, you want to delete all session(s) against the selected Branch?'))
                $http.post("/UI/deleteBranchSession", { branchSession: $scope.BranchSessionList, iCompanyCode: $scope.selectedObject.CompanyCode, iBranchCode: $scope.selectedObject.BranchCode }).then(onDelete, onRequestError)
            hideShield();
        }

        var onGetAllBranch = function (response) {
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listBranch = response.data;
        }

        var onGetAllSession = function (response) {
            $scope.listCompanySession = response.data;
        }

        $scope.isEditMode = false;
        $scope.clear();
        $scope.saveError = '';
        showShield();
        $http.get("/UI/getAllActiveBranches").then(onGetAllBranch, onRequestError);
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        $http.get("/UI/getAllActiveSessions").then(onGetAllSession, onRequestError);
        hideShield();
        
    }

    app.controller("branchsessionController", branchController);
}
)();