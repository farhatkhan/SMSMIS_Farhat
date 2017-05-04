(function () {
    var app = angular.module("clientModule1", []);
    var admController = function ($scope, $http, $filter) {

        var onGetCompany = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
            if ($scope.listCompany != null && $scope.listCompany.length > 0) {
                if (typeof $scope.selectedObject == 'undefined' || $scope.selectedObject)
                    setDefault(0, 0);
                $scope.selectedObject.CompanyCode = $scope.listCompany[0].CompanyCode;

            }
            //$scope.getCOACompanyWise();
            GetCompany = true;
            hideAllShield();
            //isFormLoaded();
        }
        $scope.getCOACompanyWise = function () {
                showShield();
                ListComplete = GetCOA = false;
                $http.post("/UI/getAllCOAGroup", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onListComplete, onRequestError);
                $http.post("/UI/getAllActiveChartOfAccounts", { CompanyCode: $scope.selectedObject.CompanyCode}).then(onGetCOA, onRequestError);
        }
        var onGetCOA = function (response) {
            if (response.data == null || response.data == undefined)
                listCOA = true;
            $scope.listCOA = response.data;
            //hideAllShield();
            GetCOA = true;
            isFormLoaded();
        }
        var onListComplete = function (response) {
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listData = response.data;
            if ($scope.listData.length > 0)
                setDefault($scope.listData[0].CompanyCode, $scope.listData[0].Code, $scope.listData[0].Description, $scope.listData[0].ShortName);
            else
                setDefault($scop.selectedObject.CompanyCode, '', '', '');
            ListComplete = true;
            isFormLoaded();
        }
        var onRequestError = function (reason) {
            hideAllShield();
            //if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
            //    $scope.listError = reason.data.error;
            //else
            //    $scope.listError = reason.status + ': ' + reason.statusText;
        }
        
        var onSaveComplete = function (response) {
            hideAllShield();
            //$scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            //$scope.listDataSearch = angular.copy($scope.listData);
            //if (!$scope.isEditMode)
            //var ccode = $scope.selectedObject.CompanyCode;
            //var bcode = $scope.selectedObject.Code;
            $scope.clear(); //clear on add new mode
            //setDefault(ccode, bcode);
            //$scope.selectedObject.CompanyCode = ccode;
            //$scope.selectedObject.Code = bcode;
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.apply();

        }
        var onDelete = function (response) {
            hideAllShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            //$scope.listDataSearch = angular.copy($scope.listData);
            //var ccode = $scope.selectedObject.CompanyCode;
            //var bcode = $scope.selectedObject.Code;
            $scope.clear(); //clear on add new mode
            //setDefault(ccode, bcode);
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();

            //$scope.saveError = 'Record deleted successfully';
        }
        $scope.delete = function () {
            clearErros();
            showDeleteShield();

            if (confirm('Are you sure you want to delete this record?'))
                removeEmptyRow();
            $http.post("/UI/deleteCOAGroup", $scope.selectedObject).then(onDelete, onRequestError)
        }
        
        $scope.clear = function () {
            $scope.selectedObject = {};
            $scope.listCOA = [];
            $scope.listData = [];
            $scope.isEditMode = false;
        }
        
        

        
        
        var isValid = function () {
            if (typeof $scope.selectedObject == 'undefined' || $scope.selectedObject == null) setDefault(0, 0);
            if (isnullorEmpty($scope.selectedObject.CompanyCode))
                return false;//$scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject.Code))
                return false;//$scope.listError = "Branch is Required";
            else if (isnullorEmpty($scope.selectedObject.Description))
                return false;//$scope.listError = "Description is Required";
            else if (isnullorEmpty($scope.selectedObject.ShortName))
                return false;//$scope.listError = "Class is Required";
            else if (angular.isUndefined($scope.listData) || $scope.listData == null || $scope.listData.length == 0)
                return false;
            else return true;
            //return false;
        }
        var clearErros = function () {
            $scope.listError = $scope.saveError = '';
        }
        $scope.save = function () {
            $scope.saveError = '';
            if (!isValid()) return;
            clearErros();

            showSaveShield();
            //for (var x in $scope.selectedObject) {
            //    $scope.selectedObject[x].SrNo = count;
            //    count++;
            //}
            //$scope.selectedObject, CompanyCode: $scope.selectedObject.CompanyCode, Code: $scope.selectedObject.Code, Description: $scope.selectedObject.Description, ShortName: $scope.selectedObject.ShortName
            $scope.listData[0].Description = $scope.selectedObject.Description;
            $scope.listData[0].Code = $scope.selectedObject.Code;
            $scope.listData[0].ShortName = $scope.selectedObject.ShortName;
            $http.post("/UI/saveCOAGroup", { COAGroup:$scope.listData  }).then(onSaveComplete, onRequestError);//saif
        }
        var isFormLoaded = function () {
            if (GetCompany && ListComplete)
                loaded();
                hideAllShield();
            

        }
        
        
        
        $scope.setRightSelected = function (obj) {
            $scope.selectedLeftRow = null;
            $scope.selectedRow = $scope.selectedRightRow = obj.AccountCode;
        }
        $scope.setLeftSelected = function (obj) {
            $scope.selectedRightRow = null;
            $scope.selectedRow = $scope.selectedLeftRow = obj.AccountCode;
        }
        $scope.moveLeft = function () {
            if ($scope.selectedRightRow != null) {
                
                var filteredArray = $filter('filter')($scope.listData, { AccountCode: $scope.selectedRightRow }, true);
                $scope.listCOA.push(filteredArray[0]);
                removeArray($scope.listData, $scope.selectedRightRow);
                $scope.selectedLeftRow = $scope.selectedRightRow;
                $scope.selectedRightRow = null;
            }
        }
        $scope.moveRight = function () {
            if ($scope.selectedLeftRow != null) {
                
                var filteredArray = $filter('filter')($scope.listCOA, { AccountCode: $scope.selectedLeftRow }, true);
                filteredArray[0].Code = $scope.selectedObject.Code;
                filteredArray[0].Description = $scope.selectedObject.Description;
                filteredArray[0].ShortName = $scope.selectedObject.ShortName;
                $scope.listData.push(filteredArray[0]);
                removeArray($scope.listCOA, $scope.selectedLeftRow)
                $scope.selectedRightRow = $scope.selectedLeftRow;
                $scope.selectedLeftRow = null;
            }
            
        }
        var removeArray = function (myArr, searchTerm) {
            var index = findIndex(myArr, searchTerm);
            myArr.splice(index, 1);
        }
        var loaded = function () {
            if (!angular.isUndefined($scope.listData) && !angular.isUndefined($scope.listCOA) && $scope.listData.length > 0 && $scope.listCOA.length > 0)
            {
                for (var x in $scope.listData)
                    removeArray($scope.listCOA, $scope.listData[x].AccountCode);
            }
        }
        var findIndex = function (myArray, searchTerm)
        {
            for (var i = 0, len = myArray.length; i < len; i++) {
                if (myArray[i].AccountCode === searchTerm) {
                    return i;
                    break;
                }
            }
            return -1;
        }
        var setDefault = function (CompanyCode, Code, Description, ShortName) {
            $scope.selectedObject = {
                CompanyCode: CompanyCode, Code: Code, Description: Description
        , ShortName: ShortName
            }
        }
        //}
        $scope.isEditMode = false;
        $scope.clear();
        $scope.listData = [];
        $scope.selectedObject = {};
        
        $scope.listCompany = [];
        $scope.listCOA = [];
        $scope.selectedLeftRow = $scope.selectedRow = $scope.selectedRightRow = null;
        
        $scope.saveError = '';
        //$scope.addGrid();
        var GetCompany = false;
        var GetCOA = false;
        var ListComplete = false;
        

        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        
        

    }
    app.controller("COAGroupController", admController);
    addDirectives(app);
}
)();