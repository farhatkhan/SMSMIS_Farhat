(function () {
    var app = angular.module("clientModule1", []);
    var comController = function ($scope, $http,$filter) {

        var onGetCompany = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listCompany = true;
            $scope.listCompany = response.data;
            //if ($scope.listCompany != null && $scope.listCompany.length > 0) {
            //    //if (typeof $scope.selectedObject[0] == 'undefined' || $scope.selectedObject[0])
            //        //setDefault(0, 0);
            //    $scope.selectedObject.CompanyCode = $scope.listCompany[0].CompanyCode;

            //}
            //$scope.getCOACompanyWise();
            GetCompany = true;
            hideAllShield();
            //isFormLoaded();
        }
        //var setDefault = function (company, billtype) {
        //    $scope.selectedObject =[];
        //    var obj = new Object();
        //    obj.CompanyCode = company;
        //    obj.BillType = billtype;
        //    $scope.selectedObject.push(obj);
        //}
         var isAlreadyExits = function () {
            var returnType = true;
            for (var x in $scope.listData)

                if ($scope.listData[x].CompanyCode == $scope.selectedObject.CompanyCode
                   && $scope.listData[x].BranchCode == $scope.selectedObject.BranchCode
        && $scope.listData[x].AccountCode == $scope.selectedObject.AccountCode
            && $scope.listData[x].InstrumentTypeCode == $scope.selectedObject.InstrumentTypeCode) {
                 $scope.listError = "Record Already Exist";
                 returnType = false;
                 }
         return returnType;
        }
        var onGetInstrumentType = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listCompany = true;
            $scope.listInstrumentType = response.data;
            GetInstrumentType = true;
            isFormLoaded();
        }
        var onGetCOA = function (response) {
            if (response.data == null || response.data == undefined)
                listCOA = true;
            $scope.listCOA = response.data;
            hideAllShield();
            //GetCOA = true;
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
            hideAllShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            filteredList();
            $scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";
            $scope.selectedObject.IssueDate = formatToJSONDate($scope.selectedObject.IssueDate);
            //if (!$scope.isEditMode)
                $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            //$scope.apply();
            //hideAllShield();
            $scope.$apply();

        }

        var isValid = function () {
            if (isnullorEmpty($scope.selectedObject.CompanyCode))
                return false;//$scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject.BranchCode))
                return false;//$scope.listError = "Branch Code is Required";
            else if (isnullorEmpty($scope.selectedObject.AccountCode))
                return false;//$scope.listError = "Bank is Required";
            else if (isnullorEmpty($scope.selectedObject.InstrumentTypeCode))
                return false;//$scope.listError = "Instrument Type is Required";
            else if (isnullorEmpty($scope.selectedObject.StartingInstrumentNo))
                return false;//$scope.listError = "Starting # is Required";
            else if (isnullorEmpty($scope.selectedObject.EndingInstrumentNo))
                return false;//$scope.listError = "Ending # is Required";
            else if (isnullorEmpty($scope.selectedObject.IssueDate))
                return false;//$scope.listError = "Issue Date is Required";
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
            if(!$scope.isEditMode) if (!isAlreadyExits()) return;
            $scope.saveError = '';
            showSaveShield();
            //$scope.selectedObjectCopy = angular.copy($scope.selectedObject);
            $scope.selectedObject.IssueDate = dateParser($scope.selectedObject.IssueDate);
            $scope.selectedObject.Status = $scope.selectedObject.Status == "1" ? true : false;
            var isNew = $scope.isEditMode ? false : true;
            //$scope.selectedObject.InstrumentSerialDetail = null;
            $http.post("/UI/saveInstrumentSerialMaster", { InstrumentSerialMaster: $scope.selectedObject, 'isNew': isNew }).then(onSaveComplete, onRequestError);//saif

        }
        var onDelete = function (response) {
            hideAllShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            filteredList();
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.clear();
            //$scope.saveError = 'Record deleted successfully';
        }
        $scope.delete = function () {
            clearErros();
            $scope.saveError = '';
            if (confirm('Are you sure you want to delete this record?'))
                $http.post("/UI/deleteInstrumentSerialMaster", $scope.selectedObject).then(onDelete, onRequestError)
        }
        $scope.load = function (obj) {
            clearErros();
            if (obj != null) {
                $scope.selectedObject = {};
                $scope.selectedObject = angular.copy(obj);
                $scope.selectedObject.IssueDate = toISOFormat(dateParser($scope.selectedObject.IssueDate));
                $scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";
                if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                $scope.isEditMode = true;
            }
            $scope.saveError = '';
        }
        $scope.getCOACompanyWise = function () {
            if (!angular.isUndefined($scope.selectedObject.CompanyCode) && $scope.selectedObject.CompanyCode != null) {
                showShield();
                GetAllBranch = ListComplete = false;

                $http.post("/UI/getAllActiveBranchesCompanyWise", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllBranch, onRequestError);
                $http.post("/UI/getAllInstrumentSerialMaster", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onListComplete, onRequestError);
                
                    showCOA();
            }
        }
        var filteredList = function () {
            $scope.filterList = [];
            $scope.filterList = $filter('filter')($scope.listData, { CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObject.BranchCode }, true);
        }
        $scope.showCOA = function () {
            $scope.isEditMode = false;
            if (!angular.isUndefined($scope.selectedObject.BranchCode) && $scope.selectedObject.BranchCode != null) {
                //GetCOA = false;
                filteredList();
                $http.post("/UI/getAllCOAbyAccountTypeCompanyBranch", { CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObject.BranchCode, AccountType: "B", levelID: "D" }).then(onGetCOA, onRequestError);
            }
        }

        $scope.clear = function () {
            clearErros();
            $scope.isEditMode = false;
            var ccode = angular.isUndefined($scope.selectedObject.CompanyCode) ? 0 : $scope.selectedObject.CompanyCode;
            var bcode = angular.isUndefined($scope.selectedObject.BranchCode) ? 0 : $scope.selectedObject.BranchCode;
            $scope.selectedObject = { CompanyCode: ccode, BranchCode: bcode, Status: "1", InstrumentTypeCode: '', StartingInstrumentNo: '', EndingInstrumentNo: '',IssueDate:'' };
            $scope.selectedObject.Status = "1";
        }
        $scope.listBranch = [];
        $scope.listData = [];
        $scope.listCompany = [];
        $scope.isEditMode = false;
        $scope.listCOA = [];
        $scope.selectedObject = {};
        $scope.selectedObject.Status = "1";
        $scope.selectedObjectCopy = { };
        $scope.listInstrumentType = [];
        $scope.filterList = [];
        var isFormLoaded = function () {
            if (GetCompany && GetAllBranch && GetInstrumentType && ListComplete ) {
                hideAllShield();
            }
        }
        var GetAllBranch = false;
        var GetCompany = false;
        var GetInstrumentType = false;
        var ListComplete = false;
        
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        $http.get("/UI/getAllActiveInstrumentTypeManageSerial").then(onGetInstrumentType, onRequestError);
        
    }
    app.controller("InstrumentSerialController", comController);
    addDirectives(app);
}
)();