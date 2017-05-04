(function () {
    var app = angular.module("clientModule1", []);
    var comController = function ($scope, $http) {

        
        var onListComplete = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listData = true;
            $scope.listData = response.data;
            ListComplete = true;
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
            hideAllShield();
            //$scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";
            $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            //$scope.apply();
            hideAllShield();
            $scope.$apply();

        }

        var isValid = function () {
            if (isnullorEmpty($scope.selectedObject.InstrumentName))
                return false;//$scope.listError = "Instrument Name is Required";
            else if (isnullorEmpty($scope.selectedObject.ShortName))
                return false;//$scope.listError = "Short Name is Required";
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
            
            showSaveShield();
            
                $scope.selectedObject.Status = $scope.selectedObject.Status == "1" ? true : false;

                $http.post("/UI/saveInstrumentType", { InstrumentType: $scope.selectedObject }).then(onSaveComplete, onRequestError);//saif
            
        }
        var onDelete = function (response) {
            hideAllShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            //$scope.saveError = 'Record deleted successfully';
        }
        $scope.delete = function () {
            clearErros();
            $scope.saveError = '';
            if (confirm('Are you sure you want to delete this record?')) {
                showDeleteShield();
                $http.post("/UI/deleteInstrumentType", $scope.selectedObject).then(onDelete, onRequestError)
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
        $scope.clear = function () {
            clearErros();
            $scope.isEditMode = false;
            $scope.selectedObject = {InstrumentName:"",ShortName:"",ManageSerial:false, Status:"1"};
        }
        
        $scope.listData = [];
        $scope.listCompany = [];
        
        $scope.isEditMode = false;
        $scope.selectedObject = { InstrumentName: "", ShortName: "", ManageSerial: false, Status: "1" };
        
        var isFormLoaded = function () {
            if (ListComplete) {
                hideAllShield();
            }
        }
       
        var ListComplete = false;

        
        
        $http.get("/UI/getAllInstrumentType").then(onListComplete, onRequestError);
    }
    app.controller("InstrumentTypeController", comController);
}
)();