(function () {
    var app = angular.module("adminModule", []);
    var depController = function ($scope, $http, $filter) {
        var onListComplete = function (response) {
            $scope.listData = response.data;
        }
        var onSaveComplete = function (response) {
            $scope.listData = response.data;
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.apply();
        }
        var onRequestError = function (reason) {
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.saveError = reason.data.error;
            else
                $scope.saveError = reason.status + ': ' + reason.statusText;
        }
        var onDeleteComplete = function (response) {
            $scope.listData = response.data;
            $scope.clear();
            $scope.apply();
        } 
        var loadOperations = function (DeptID) {
            $http.get("/UI/getAllOperations")
                .success(function (response) {
                    $scope.Operations = response;
                })
                .error(function (reason) { });
            
 

            //$http.get("/UI/getUserTypeBranches")
            //   .success(function (response) {
            //       $scope.UserTypeBranches = response;
            //   })
            //   .error(function (reason) { });

        }
        $scope.load = function (DepartmentID) {
            $scope.clear();
            $http.post("/UI/getDepartment", { 'DepartmentID': DepartmentID } )
            .success(function (response) {
                $scope.selectedObject = response;
                setSelectedValues();
            })
            .error(function (reason) {

            });
            $scope.saveError = '';
        }
        var setSelectedValues = function () {
            for (var x in $scope.selectedObject.comDepartmentOperationList) {
                var departmentid = $scope.selectedObject.DepartmentID;
                var operationId = $scope.selectedObject.comDepartmentOperationList[x].OperationID;
                var opList = $filter('filter')($scope.Operations, { OperationID: operationId }, function (actual, expected) { return angular.equals(expected, actual); });
                if (typeof opList != 'undefined' && opList != null && opList.length > 0) {
                    opList[0].isSelected = true;
                    opList[0].DepartmentID = departmentid;
                }
            }
        }
        $scope.clear = function () {
            $scope.selectedObject = {};
            $scope.isEditMode = false;
            var opList = $filter('filter')($scope.Operations, { isSelected: true });
            for (var x in opList) {
                opList[x].isSelected = false;
            }
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
        }
        $scope.delete = function () {
            $scope.saveError = '';
            if (confirm('Are you sure you want to delete this record?'))
                $http.post("/UI/deleteDepartment", $scope.selectedObject).then(onDeleteComplete, onRequestError)
        }
        $scope.save = function () {
            $scope.saveError = '';
            $scope.selectedObject.comDepartmentOperationList = $filter('filter')($scope.Operations, { isSelected: true });
            $http.post("/UI/saveDepartment", $scope.selectedObject).then(onSaveComplete, onRequestError)
        }
        $scope.Operations = {};
        $scope.isEditMode = false;
        $scope.clear();
        $scope.saveError = '';
        $http.get("/UI/getDepartments").then(onListComplete, onRequestError);
        loadOperations();
        hideShield();
        
    }
    app.controller("departmentController", depController);
}
)();