(function () {
    var app = angular.module("clientModule1", []);
    var comController = function ($scope, $http, $filter) {

        var onGetCompany = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listCompany = true;
            $scope.listCompany = response.data;
            hideShield();
        }
        var onGetAllBranch = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listData = true;
            $scope.listBranch = response.data;
            hideShield();
        }

        var setDefault = function (company, billtype) {
            $scope.selectedObject = [];
            var obj = new Object();
            obj.CompanyCode = company;
            obj.BillType = billtype;
            $scope.selectedObject.push(obj);
        }
        //var isAlreadyExits = function () {
        //    var returnType = true;
        //    for (var x in $scope.listData)

        //        if ($scope.listData[x].CompanyCode == $scope.selectedObject.CompanyCode
        //           && $scope.listData[x].BranchCode == $scope.selectedObject.BranchCode
        //&& $scope.listData[x].AccountCode == $scope.selectedObject.AccountCode
        //    && $scope.listData[x].InstrumentTypeCode == $scope.selectedObject.InstrumentTypeCode) {
        //            $scope.listError = "Record Already Exist";
        //            returnType = false;
        //        }
        //    return returnType;
        //}

        
        var onRequestError = function (reason) {
            hideShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }
        
        //var isValid = function () {
        //    if (isnullorEmpty($scope.selectedObject.CompanyCode))
        //        $scope.listError = "Company is Required";
        //    else if (isnullorEmpty($scope.selectedObject.AccountCode))
        //        $scope.listError = "Account Code is Required";
        //    else if ($scope.selectedObject.AccountCode.length != $scope.selectedObject.LevelLength)
        //        $scope.listError = "Account Code Should be " + $scope.selectedObject.LevelLength + " char";
        //    else if (isnullorEmpty($scope.selectedObject.AccountTitle))
        //        $scope.listError = "Account Title is Required";

        //    else if (isnullorEmpty($scope.selectedObject.Status))
        //        $scope.listError = "Status is Required";
        //    else return true;
        //    return false;
        //}


        var clearErros = function () {
            $scope.listError = $scope.saveError = '';
        }
        
        $scope.getCOACompanyWise = function () {
            showShield();
            $http.post("/UI/getAllBranchofCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllBranch, onRequestError);
            $scope.clear();
        }
        $scope.generateReport = function () {
            var branchID = !angular.isUndefined($scope.selectedObject.BranchCode) ? $scope.selectedObject.BranchCode : 0;
            window.location = "/Report/Report?CompanyCode=" + $scope.selectedObject.CompanyCode + "&BranchCode=" + branchID + "&ReportID=107";
        }
        //$scope.listBranch = [];
        
        $scope.listCompany = [];
        $scope.listBranch = [];

        var GetAllBranch = false;
        var GetCompany = false;

        //$http.get("/UI/getAllActiveBranches").then(onGetAllBranch, onRequestError);
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);


    }
    app.controller("ChartOfAccountsController", comController);
    //addDirectives(app);
}
)();