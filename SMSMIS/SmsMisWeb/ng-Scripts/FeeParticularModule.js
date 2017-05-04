(function () {
    var app = angular.module("adminModule1", []);
    var feeController = function ($scope, $http) {

        var onGetCompany = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
            //$scope.getCOACompanyWise();
            if ($scope.listCompany != null && $scope.listCompany.length > 0) {
                
                $scope.selectedObject.CompanyCode = $scope.listCompany[0].CompanyCode;
                $scope.getCompanyData();
            } else {
                hideAllShield();
                $scope.selectedObject.CompanyCode = '';
            }
        }
        $scope.getCompanyData = function () {
            showShield();
            $http.post("/UI/getAllFeeParticularofCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onListComplete, onRequestError);
            $scope.clear();
        }
        var onListComplete = function (response) {
            hideAllShield();
            
        
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listData = response.data;
        }
        var onGetFeeParticularRecurringType = function (response) {
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listFeeParticularRecurringType = response.data;
        }
        var onSaveComplete = function (response) {
            hideAllShield();
            //$scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";
            $scope.listData = response.data;
            //if (!$scope.isEditMode)
                $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.apply();
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

        $scope.addContact = function () {

            if ($scope.selectedObject.FeeParticularContactPersonList == undefined)
                $scope.selectedObject.FeeParticularContactPersonList = [];

            $scope.selectedObject.FeeParticularContactPersonList.push(new NewContact());
        }

        $scope.deleteContact = function (contact) {
            if (confirm('Are you sure, you want to delete this contact detail?'))
                $scope.selectedObject.FeeParticularContactPersonList.splice(contact, 1);
        }

        var NewContact = function () {
            this.CompanyCode = $scope.selectedObject.CompanyCode;
            this.FeeParticularContactPerson = '&nbsp;';
            this.FeeParticularCode = $scope.selectedObject.FeeParticularCode;
            this.ContactPerson = '';
            this.LandLine = '';
            this.Cell = '';
            this.Email = '';
        }

        var isValid = function () {
            if (isnullorEmpty($scope.selectedObject.CompanyCode))
                $scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject.Recurring))
                $scope.listError = "Recurring is Required";
            else if (isnullorEmpty($scope.selectedObject.Status))
                $scope.listError = "Status is Required";
            else return true;
            return false;
        }

        var onRequestError = function (reason) {
            hideAllShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }
        $scope.load = function (obj) {
            if (obj != null) {
                
                $scope.selectedObject = angular.copy(obj);
                $scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";
                if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                $scope.isEditMode = true;
            }
            $scope.saveError = '';
        }
        $scope.clear = function () {
            var cid = $scope.selectedObject.CompanyCode;
            $scope.selectedObject = {};
            $scope.saveError = '';
            $scope.listError = '';
            $scope.isEditMode = false;
            $scope.selectedObject.Status = "1";
            $scope.selectedObject.CompanyCode = cid;
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();

        }
        $scope.save = function () {
            $scope.saveError = '';
            if (!isValid()) return;
            showSaveShield();
            $scope.selectedObject.Recurring = $scope.selectedObject.Recurring;
            $scope.selectedObject.Status = $scope.selectedObject.Status == "1" ? true : false;

            $http.post("/UI/saveFeeParticular", $scope.selectedObject).then(onSaveComplete, onRequestError)
        }
        $scope.delete = function () {
            $scope.saveError = '';
            showDeleteShield();
            if (confirm('Are you sure you want to delete this record?'))
                $http.post("/UI/deleteFeeParticular", $scope.selectedObject).then(onDelete, onRequestError)
        }
        $scope.isEditMode = false;
        //$scope.clear();
        showShield();
        $scope.saveError = '';
        $scope.selectedObject = {};
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        
        $http.get("/UI/getFeeParticularRecurringType").then(onGetFeeParticularRecurringType, onRequestError);
        

    }

    app.controller("feeparticularController", feeController);
}
)();