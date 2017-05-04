(function () {
    var app = angular.module("clientModule1", []);
    var admController = function ($scope, $http, $filter) {
        $scope.GetSelectedAccountTitle = function (data, x, indx) {
            data.AccountCode = x.AccountCode;
            data.AccountTitle = x.AccountTitle;
            $('.QuickSearchResults').hide();
            $scope.validateGrid(indx, true);
        }

        $scope.SetSelectedAccountTitle = function (data, indx) {
            data.AccountTitle = data.AccountCode;
            if (data.AccountTitle == '') {
                $('.abc' + indx).hide();
                return;
            }

            var filteredArray = $filter('filter')($scope.listCOA, { AccountTitle: data.AccountTitle });
            if (filteredArray.length == 1 && filteredArray[0].AccountTitle.toUpperCase() == data.AccountTitle.toUpperCase()) {
                data.AccountCode = filteredArray[0].AccountCode;
                data.AccountTitle = filteredArray[0].AccountTitle;
                $('.abc' + indx).hide();

            }
            else {
                $('.abc' + indx).show();
            }
        }
        
        $scope.openreport = function () {
            
            if (typeof $scope.selectedObject == 'undefined' || $scope.selectedObject == null) {setDefault();return}
            if (isnullorEmpty($scope.selectedObject.CompanyCode))
                return;
            if (angular.isUndefined($scope.selectedObject.BranchCode))
                $scope.selectedObject.BranchCode = '0';
            if (angular.isUndefined($scope.selectedObject.SessionCode))
                $scope.selectedObject.SessionCode = '0';
            if (angular.isUndefined($scope.selectedObject.ClassCode))
                $scope.selectedObject.ClassCode = '0';
            if (angular.isUndefined($scope.selectedObject.Gender))
                $scope.selectedObject.Gender = '';
            //alert("/Report/Report?ReportID=103&CompanyCode=" + $scope.selectedObject.CompanyCode + '&BranchCode=' + $scope.selectedObject.BranchCode + '&SessionCode=' + $scope.selectedObject.SessionCode + '&BankCode=' + $scope.selectedObject.BankCode + '&ClassCode=' + arrclass.toString() + '&MonthCode=' + arr.toString());
            window.location = "/Report/Report?ReportId=110&CompanyCode=" + $scope.selectedObject.CompanyCode + "&BranchCode=" + $scope.selectedObject.BranchCode + "&SessionCode=" + $scope.selectedObject.SessionCode + "&ClassCode=" + $scope.selectedObject.ClassCode + "&Gender=" + $scope.selectedObject.Gender;
            //window.location = "/Report/Report?ReportID=103";
        }


        var onGetCompany = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
            //if ($scope.listCompany != null && $scope.listCompany.length > 0) {
            //    if (typeof $scope.selectedObject == 'undefined' || $scope.selectedObject == null)
            //        setDefault();
            //    $scope.selectedObject.CompanyCode = $scope.listCompany[0].CompanyCode;

            //}

            //$scope.getCOACompanyWise();
            GetCompany = true;
            hideShield();
            //isFormLoaded();
        }
        $scope.getCOACompanyWise = function () {
            
            showShield();
            GetAllClass = GetAllSession = GetAllBranch = false;
            $http.post("/UI/getAllBranchofCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllBranch, onRequestError);
            $http.post("/UI/getAllSessionofCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllSession, onRequestError);
            //$http.post("/UI/getAllBankofCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllBank, onRequestError);
            $http.post("/UI/getActiveClassesCompanyWise", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllClass, onRequestError);
            //$http.get("/UI/getAllFeeParticularRate").then(onListComplete, onRequestError);
            //$http.get("/UI/getAllActiveFeeParticular").then(onGetFeeParticular, onRequestError);
        }
        $scope.getCOABranch = function () {
            //$http.post("/UI/getAllCOAbyAccountTypeCompanyBranch", { CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObject.BranchCode, AccountType: "B", levelID: "D" }).then(onGetCOA, onRequestError);
        }
        var onGetAllBank = function (response) {
            if (response.data == null || response.data == undefined)
                listBank = true;
            $scope.listBank = response.data;
            GetAllBank = true;
            isFormLoaded();
        }
        var onGetAllBranch = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listBranch = response.data;
            GetAllBranch = true;
            isFormLoaded();
        }
        var onGetAllSession = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listSession = response.data;
            GetAllSession = true;
            isFormLoaded();
        }
        var onGetAllClass = function (response) {
            if (response.data == null || response.data == undefined)
                listClass = true;
            $scope.listClass = response.data;
            GetAllClass = true;
            isFormLoaded();
        }



        var onListComplete = function (response) {
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listData = response.data;
            //$scope.listDataSearch = angular.copy($scope.listData);
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


        $scope.clear = function () {
            $scope.selectedObject = [];
            $scope.isEditMode = false;
        }


        var clearErros = function () {
            $scope.listError = $scope.saveError = '';
        }

        var isFormLoaded = function () {
            if (GetAllClass && GetCompany && GetAllSession && GetAllBranch)
                hideAllShield();
        }

        $scope.loadRecord = function () {

            clearErros();
            $scope.selectedObject[0].CompanyCode = typeof $scope.selectedObject[0].CompanyCode == 'undefined' ? '' : $scope.selectedObject[0].CompanyCode;
            $scope.selectedObject[0].BranchCode = typeof $scope.selectedObject[0].BranchCode == 'undefined' ? '' : $scope.selectedObject[0].BranchCode;
            $scope.selectedObject[0].SessionCode = typeof $scope.selectedObject[0].SessionCode == 'undefined' ? '' : $scope.selectedObject[0].SessionCode;
            $scope.selectedObject[0].ClassCode = typeof $scope.selectedObject[0].ClassCode == 'undefined' ? '' : $scope.selectedObject[0].ClassCode;
            var filteredArray = $filter('filter')($scope.listData, {
                CompanyCode: $scope.selectedObject[0].CompanyCode,
                BranchCode: $scope.selectedObject[0].BranchCode,
                SessionCode: $scope.selectedObject[0].SessionCode,
                ClassCode: $scope.selectedObject[0].ClassCode
            }, true);
            //$scope.selectedObject = [];
            setDefault($scope.selectedObject[0].CompanyCode, $scope.selectedObject[0].BranchCode, $scope.selectedObject[0].SessionCode, $scope.selectedObject[0].ClassCode);
            if (filteredArray.length > 0) {
                $scope.isEditMode = true;
                $scope.selectedObject = angular.copy(filteredArray);
                var count = 0;
                //for (var x in filteredArray) {
                //$scope.selectedObject[count] = filteredArray[x];
                //count++;
                $scope.addGrid('', '', '', '', indx, 'edit');
            }

            //$scope.addGrid(filteredArray[x].FeeParticularCode, '', filteredArray[x].AccountCode, '', filteredArray[x].Rate, 0, 'edit')
        }
        
        var setDefault = function () {

            $scope.selectedObject.CompanyCode = 0;
            $scope.selectedObject.BranchCode = 0;
            $scope.selectedObject.SessionCode = 0;
            $scope.selectedObject.ClassCode = 0;

            //$scope.selectedObject[0].BranchCode = branch;
            //$scope.selectedObject[0].SessionCode = session;
            //$scope.selectedObject[0].ClassCode = classcode;
        }
        //}
        $scope.isEditMode = false;
        $scope.clear();
        $scope.listData = [];
        $scope.selectedObject = {};
        $scope.listFeeParticular = [];
        $scope.listCOA = [];
        $scope.listClass = [];
        $scope.listSession = [];
        $scope.listBranch = [];
        $scope.listCompany = [];
        $scope.listCOA = [];

        $scope.saveError = '';
        //$scope.addGrid();
        var GetCompany = false;
        var GetAllBranch = false;
        var GetAllSession = false;
        var GetAllClass = false;
        var ListComplete = false;
        var GetFeeParticular = false;
        var GetCOA = false;
        var GetAllBank = false;
        //$scope.drawTable();
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
    }
    app.controller("INQListController", admController);
    addDirectives(app);
}
)();
