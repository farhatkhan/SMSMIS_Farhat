(function () {
    var app = angular.module("clientModule1", []);
    var comController = function ($scope, $http, $filter) {
        var onGetCompany = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listCompany = true;
            $scope.listCompany = response.data;
            if ($scope.listCompany != null && $scope.listCompany.length > 0) {
                $scope.selectedObject.CompanyCode = $scope.listCompany[0].CompanyCode;

            }
            GetCompany = true;
            $scope.getCOACompanyWise();

            isFormLoaded();
        }
        var onGetCOA = function (response) {
            //hideShield();
            if (response.data == null || typeof response.data == 'undefined')
                listCompany = true;
            $scope.listCOA = response.data;
            GetCOA = true;
            isFormLoaded();
        }
        var onGetVoucher = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listVoucherType = true;
            $scope.listVoucherType = response.data;
            GetVoucher = true;
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
            hideShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }

        var clearErros = function () {
            $scope.listError = $scope.saveError = '';
        }
        $scope.openreport = function () {

            var toDate = '';
            var fromDate = '';
            var arrbranch = new Array();
            var arrCOA = new Array();
            var arrVType = new Array();
            for (var x in $scope.listCOA)
                if ($scope.listCOA[x].isSelected)
                    arrCOA[arrCOA.length] = $scope.listCOA[x].AccountCode;
            for (var x in $scope.listBranch)
                if ($scope.listBranch[x].isSelected)
                    arrbranch[arrbranch.length] = $scope.listBranch[x].BranchCode;

            for (var x in $scope.listVoucherType)
                if ($scope.listVoucherType[x].isSelected)
                    arrVType[arrVType.length] = $scope.listVoucherType[x].VoucherCode;
            if (!angular.isUndefined($scope.selectedObject.FromDate))//&& !angular.isDate($scope.selectedObject.ToDate)
                fromDate = toSQLFormat($scope.selectedObject.FromDate);
            if (!angular.isUndefined($scope.selectedObject.ToDate))
                toDate = toSQLFormat($scope.selectedObject.ToDate);
            fromDate = angular.isUndefined(fromDate) ? '' : fromDate;
            toDate = angular.isUndefined(toDate) ? '' : toDate;
            var tovoucher = angular.isUndefined($scope.selectedObject.ToVoucher) ? '' : $scope.selectedObject.ToVoucher;
            var frmvoucher = angular.isUndefined($scope.selectedObject.FromVoucher) ? '' : $scope.selectedObject.FromVoucher;
            //alert("/Report/Report?ReportID=103&CompanyCode=" + $scope.selectedObject.CompanyCode + '&BranchCode=' + $scope.selectedObject.BranchCode + '&SessionCode=' + $scope.selectedObject.SessionCode + '&BankCode=' + $scope.selectedObject.BankCode + '&ClassCode=' + arrbranch.toString() + '&MonthCode=' + arr.toString());
            window.location = "/Report/Report?CompanyCode=" + $scope.selectedObject.CompanyCode + "&ReportID=109&BranchCodes=" + arrbranch.toString() + "&COA=" + arrCOA.toString() + "&VoucherType=" + arrVType.toString() + "&FromDate=" + fromDate + "&ToDate=" + toDate + "&FromVoucher=" + frmvoucher + "&ToVoucher=" + tovoucher;
            //window.location = "/Report/Report?ReportID=103";
        }
        function toSQLFormat(date) {
            date = dateParser(date);
            var strISO = date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();
            return strISO;
        }
        $scope.clear = function () {
            clearErros();
            $scope.isEditMode = false;
            $scope.selectedObject = {};
        }
        $scope.listBranch = [];
        $scope.listCompany = [];
        $scope.listFeeTerm = [];
        $scope.listVoucherType = [];
        $scope.selectedObject = {};
        $scope.listCOA = [];
        var isFormLoaded = function () {
            if (GetAllBranch && GetCompany && GetCOA && GetVoucher) {
                hideShield();
            }
        }
        var GetAllBranch = false;
        var GetCompany = false;
        var GetCOA = false;
        var GetVoucher = false;

        $scope.getCOACompanyWise = function () {
            showShield();
            $http.post("/UI/getAllCOAbyAccountType", { CompanyCode: $scope.selectedObject.CompanyCode, AccountType: "B", levelID: "D" }).then(onGetCOA, onRequestError);
            $http.post("/UI/getApprovedVoucherType", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetVoucher, onRequestError);
        }

        $http.get("/UI/getAllActiveBranches").then(onGetAllBranch, onRequestError);
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);

    }
    app.controller("PJController", comController);
    addDirectives(app);
}
)();