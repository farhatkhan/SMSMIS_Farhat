(function () {
    var app = angular.module("adminModule", []);
    var userTypeController = function ($scope, $http, $filter) {

        $scope.setOperationID = function (departmentId, operationId) {
            var objDepOpt = $scope.selectedObject.comUserDepartment['dept_' + departmentId].comUserOperation['opt_' + operationId];
            objDepOpt.OperationID = operationId;
            objDepOpt.DepartmentID = departmentId;
        }
        $scope.GetOperationsByDeptId = function (objDepartment) {
            $scope.departmentOptCopy = $filter('filter')($scope.departmentOpt, { 'DepartmentID': objDepartment.DepartmentID });
            for (dpt in $scope.departmentOptCopy) {
                $scope.departmentOptCopy[dpt].AccessTypes = angular.copy($scope.AllAccessTypes);
            }
        };
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
            $http.get("/UI/getDepartments")
                .success(function (response) {
                    $scope.departments = response;
                    $scope.departmentsCopy = angular.copy($scope.departments);
                })
                .error(function (reason) { });

            $http.get("/UI/getBranches")
               .success(function (response) {
                   $scope.branches = response;
                   $scope.branchesCopy = angular.copy($scope.branches);
               })
               .error(function (reason) { });

            $http.get("/UI/getOperation")
              .success(function (response) {
                  $scope.departmentOpt = response;
                  $scope.departmentOptCopy = angular.copy($scope.departmentOpt);
              })
              .error(function (reason) { });

            $http.get("/UI/getAllAccessTypes")
               .success(function (response) {
                   $scope.AllAccessTypes = response;
               })
               .error(function (reason) { });
            //$http.get("/UI/getUserTypeBranches")
            //   .success(function (response) {
            //       $scope.UserTypeBranches = response;
            //   })
            //   .error(function (reason) { });

        }
        $scope.load = function (obj) {
            $scope.clear();
            if (obj != null) {
                $http.post("/UI/getUserType", { 'UserTypeID': obj.UserTypeID })
                    .success(function (response) {
                        $scope.isModelLoading = true;
                        $scope.selectedObject = response;
                        createIDBasedIndexes();
                        $scope.isEditMode = true;
                        setSelectedValues();
                        if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                        $scope.isModelLoading = false;
                    })
                    .error(function (reason) {
                        alert(reason);
                    });
            }

            //$http.post("/UI/getOperationsEx", { 'DepartmentID': $scope.selectedObject.comUserDepartment.DepartmentID.DepartmentID }).success(function (response) {
            //    $scope.DepartmentOperations = response;
            //}).error(function (reason) { alert(reason); });


            $scope.saveError = '';
        }
        var createIDBasedIndexes = function () {
            if (typeof $scope.selectedObject.comUserDepartment != 'undefined') {
                for (dept in $scope.selectedObject.comUserDepartment) {
                    var objDept = $scope.selectedObject.comUserDepartment[dept];
                    $scope.selectedObject.comUserDepartment['dept_' + objDept.DepartmentID] = objDept;
                    if (typeof objDept.comUserOperation != 'undefined') {
                        for (optId in objDept.comUserOperation) {
                            var objDepOpt = objDept.comUserOperation[optId];
                            objDept.comUserOperation['opt_' + objDepOpt.OperationID] = objDepOpt;
                        }
                    }
                }
            }
        }
        var setSelectedValues = function () {
            
            for (var x in $scope.selectedObject.comUserBranch) {

                var branchId = $scope.selectedObject.comUserBranch[x].BranchID;
                var objList = $filter('filter')($scope.branchesCopy, { BranchID: branchId }, function (actual, expected) { return angular.equals(expected, actual); });
                if (typeof objList != 'undefined' && objList != null && objList.length > 0) objList[0].isSelected = true;

            }

            for (var x in $scope.selectedObject.comUserDepartment) {
                var departmentId = $scope.selectedObject.comUserDepartment[x].DepartmentID;

                var objList = $filter('filter')($scope.departmentsCopy, { 'DepartmentID': departmentId }, function (actual, expected) { return angular.equals(expected, actual); });

                if (typeof objList != 'undefined' && objList != null && objList.length > 0) {
                    objList[0].isSelected = true;
                    $scope.selectedObject.comUserDepartment[x].Name = objList[0].Name;

                }
            }

        }
        $scope.clear = function () {
            $scope.selectedObject = { 'UserTypeID': '00000000-0000-0000-0000-000000000000' };
            $scope.isEditMode = false;
            $scope.selectedId = '';
            var objList = $filter('filter')($scope.branchesCopy, { isSelected: true });
            if (typeof objList != 'undefined' && objList != null && objList.length > 0) {
                for (var x in objList) {
                    objList[x].isSelected = false;
                }
            }
            var values = { isSelected: false };
            var objList = $filter('filter')($scope.departmentsCopy, { isSelected: true });

            if (typeof objList != 'undefined' && objList != null && objList.length > 0) {
                for (var x in objList) {
                    objList[x].isSelected = false;
                }
            }

            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
        }
        $scope.delete = function () {
            $scope.saveError = '';
            if (confirm('Are you sure you want to delete this record?'))
                $http.post("/UI/deleteUserType", $scope.selectedObject).then(onDeleteComplete, onRequestError)
        }

        $scope.addToOperationalDepartmentList = function (object) {

            //var checkbox = $event.target;

            //if (checkbox.checked)
            //    object.isSelected = true;
            //else
            //    object.isSelected = false;

            var filtered = filterSelectedDepartments(object.DepartmentID);
            if ($scope.isModelLoading) return;
            var objList = filterSelectedDepartments(object.DepartmentID);
            
            //$scope.selectedObject.comUserDepartment = filtered;
            if (typeof objList != 'undefined') {
                for (var depIdx in objList) {
                    //$scope.selectedObject.comUserDepartment[$scope.selectedObject.comUserDepartment.length] = filtered[0];
                    var lstAdded = $filter('filter')($scope.selectedObject.comUserDepartment, { 'DepartmentID': object.DepartmentID },
                        function (actual, expected) { return angular.equals(expected, actual); });
                    if (typeof lstAdded == 'undefined' || lstAdded.length == 0) {
                        if (typeof $scope.selectedObject.comUserDepartment == 'undefined') $scope.selectedObject.comUserDepartment = [];
                        var newDept = angular.copy(objList[depIdx]);
                        newDept.comUserOperationList = null;
                        newDept.comUserOperation = [];
                        $scope.selectedObject.comUserDepartment[$scope.selectedObject.comUserDepartment.length] =
                            $scope.selectedObject.comUserDepartment['dept_' + object.DepartmentID] = newDept;
                    }
                }
            }
            objList = filterUnSelectedDepartments(object.DepartmentID);
            if (typeof objList != 'undefined') {
                for (var depIdx in objList) {
                    var objDeptToRemove = $scope.selectedObject.comUserDepartment['dept_' + object.DepartmentID];
                    if (typeof objDeptToRemove != 'undefined' && objDeptToRemove != null) {
                        var indexOfItemToBeRemoved = $scope.selectedObject.comUserDepartment.indexOf(objDeptToRemove);
                        $scope.selectedObject.comUserDepartment.splice(indexOfItemToBeRemoved, 1);
                        delete $scope.selectedObject.comUserDepartment['dept_' + object.DepartmentID];
                        if (typeof $scope.selectedDepartmentObject != 'undefined' && $scope.selectedDepartmentObject.DepartmentID == object.DepartmentID)
                            delete $scope.selectedDepartmentObject;
                    }
                }
            }
            //createIDBasedIndexes();
        };

        $scope.save = function () {
            $scope.saveError = '';

            $scope.selectedObject.comUserDepartment.length = 0;
            var depIdx = 0;
            delete $scope.selectedObject.comUserDepartment['DepartmentID'];
            for (deptId in $scope.selectedObject.comUserDepartment) {

                var objDept = $scope.selectedObject.comUserDepartment[deptId];

                objDept.UserTypeID = $scope.selectedObject.UserTypeID;

                objDept.DepartmentID = deptId.split('_')[1];
                $scope.selectedObject.comUserDepartment[depIdx++] = objDept;
                if (objDept.comUserOperation != 'undefined') {
                    if (objDept.comUserOperation.length > 0) objDept.comUserOperation.length = 0;
                    var optIdx = 0;
                    for (optId in objDept.comUserOperation) {
                        var objOpt = objDept.comUserOperation[optId];
                        objOpt.OperationID = optId.split('_')[1];
                        objDept.comUserOperation[optIdx++] = objOpt;
                        objOpt.DepartmentID = objDept.DepartmentID;
                        objOpt.UserTypeID = $scope.selectedObject.UserTypeID;
                        delete objDept.comUserOperation[optId];
                    }
                    delete $scope.selectedObject.comUserDepartment[deptId];
                }
            }


            var bObj = {
                'UserTypeID': $scope.selectedObject.UserTypeID,
                'userType': $scope.selectedObject.userType,
                'DepartmentID': $scope.selectedObject.comDepartment == undefined ? null : $scope.selectedObject.comDepartment.DepartmentID,
                'BranchID': $scope.selectedObject.comBranches == undefined ? null : $scope.selectedObject.comBranches.BranchID,
                'BPMDash': $scope.selectedObject.BPMDash,
                'DPTDash': $scope.selectedObject.DPTDash,
                'comDepartment': {},
                'comBranches': {},
                'comUserBranch': filterSelectedBranches(), //filter using a separate function
                'comUserDepartment': $scope.selectedObject.comUserDepartment,
                'comUserOperation': $scope.DepartmentOperations,
                'comUserTypeDepartmentsList': {}
            }



            $http.post("/UI/saveUserType", bObj).then(onSaveComplete, onSaveError);
            createIDBasedIndexes();
        }
    

    var filterSelectedBranches = function () {
        return $filter('filter')($scope.branchesCopy, { isSelected: true });
    }

    var filterSelectedDepartments = function (deptID) {
        return $filter('filter')($scope.departmentsCopy, { 'DepartmentID': deptID, 'isSelected': true });
    }
    var filterUnSelectedDepartments = function (deptID) {
        return $filter('filter')($scope.departmentsCopy, { 'DepartmentID': deptID, 'isSelected': false });
    }

    //$scope.selectedItem = $scope.comAccessTypes[0];
    $scope.departmentOpt = {};
    $scope.departmentOptCopy = {};
    $scope.DepartmentOperations = {};
    $scope.comUserTypeDepartmentsList = {};
    $scope.AllAccessTypes = {};
    $scope.departments = {};
    $scope.branches = {};
    $scope.branchesCopy = {};
    $scope.departmentsCopy = {};
    $scope.utBranches = {};
    $scope.UserTypeBranches = {};
    $scope.clear();
    $scope.isListLoaded = false;
    $scope.saveError = '';
    $scope.operationalDepts = {};
    $http.get("/UI/getUserTypes").then(onListComplete, onListError);
    $scope.isModelLoading = false;
    loadDropdowns();
    hideShield();
}
    app.controller("userTypesController", userTypeController);
}
)();