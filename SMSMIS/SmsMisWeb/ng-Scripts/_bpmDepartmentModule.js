(function () {
    var app = angular.module("adminModule", []);
    var branchController = function ($scope, $http) {
        var onListComplete = function (response) {
            $scope.listData = response.data;
            isListLoaded = true;
        }
        var onListError = function (reason) {
            $scope.listError = reason.status + ': ' + reason.statusText;
        }
        var onSaveComplete = function (response) {
            $scope.listData = response.data;
            isListLoaded = true;
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.apply();
        }
        var onSaveError = function (reason) {
            $scope.saveError = reason.status + ': ' + reason.statusText;
        }
        var onDeleteComplete = function (response) {
            $scope.listData = response.data;
            isListLoaded = true;
            $scope.clear();
            $scope.apply();
        }
        var onDeleteError = function (reason) {
            $scope.saveError = reason.status + ': ' + reason.statusText;
        }
        
          
        
        var loadDropdowns = function () {
           $http.get("/Admin/getBranches")
                .success(function (response) {
                    $scope.regions = response;
                })
                .error(function (reason) { });   
            $http.get("/Admin/getDepartments")
                .success(function (response) {
                    $scope.departments = response;
                })
                .error(function (reason) { });

            //$http.get("/Admin/getOperations", { 'DepartmentID': 3 })
            //   .success(function (response) {
            //       $scope.operations = response;
            //   })
            //   .error(function (reason) { });
            $http.post("/Admin/getOperations", { 'DepartmentID': 3 })
                    .success(function (response) {
                        $scope.operations = angular.copy(response);
                        //$scope.isEditMode = true;
                        if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                    })
                    .error(function (reason) {
                        alert(reason);
                    });
        }

        
        $scope.load = function (obj) {
            if (obj != null) {
                $http.post("/Admin/getBranch", { 'BranchID': obj.BranchID })
                    .success(function (response) {
                        $scope.selectedObject = angular.copy(response);
                        $scope.isEditMode = true;
                        if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                    })
                    .error(function (reason) {
                        alert(reason);
                    });
            }
            $scope.saveError = '';
        }
        $scope.clear = function () {
            $scope.selectedObject = { 'BranchID': '00000000-0000-0000-0000-000000000000' };
            $scope.selectedId = '';
            $scope.isEditMode = false;
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
        }
        $scope.delete = function () {
            $scope.saveError = '';
            if (confirm('Are you sure you want to delete this record?'))
                $http.post("/Admin/deleteBranch", $scope.selectedObject).then(onDeleteComplete, onDeleteError)
        }
        $scope.save = function () {
            $scope.saveError = '';
            //Do we have to make a custom object? Let's find out next
            var bObj = {
                'BranchID': $scope.selectedObject.BranchID,
                'Name': $scope.selectedObject.Name,
                'Description': $scope.selectedObject.Description,
                'RegionID': $scope.selectedObject.comRegions.RegionID
            }
            $http.post("/Admin/saveBranch", bObj).then(onSaveComplete, onSaveError)
        }
        $scope.regions = {};
        $scope.departments = {};
        $scope.operations = {};
        $scope.clear();
        $scope.isListLoaded = false;
        $scope.saveError = '';
        $http.get("/Admin/getBranches").then(onListComplete, onListError);
        loadDropdowns();
    }
    app.controller("branchesController", branchController);
}
)();