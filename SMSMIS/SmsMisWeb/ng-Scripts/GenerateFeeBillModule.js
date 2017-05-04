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
        /*var arr = new Array();
        function monthlist(obj) {
            var monthname = obj.id;
            monthname = monthname.substring(3);

            var mname = monthname.split(",")[0];
            if (obj.checked) {
                arr[arr.length] = mname
                //alert(arr[arr.length - 1] + '- ' + arr.length);
            } else {
                for (var i = arr.length - 1; i >= 0; i--) {
                    if (arr[i] === mname) {
                        arr.splice(i, 1);
                        // break;       //<-- Uncomment  if only the first term has to be removed
                    }
                }
            }
        }
        var arrclass = new Array();
        function classlist(obj) {
            var monthname = obj.id;
            var mname = monthname.substring(3);

            //var mname = monthname.split(",")[0];
            if (obj.checked) {
                arrclass[arrclass.length] = mname
                //alert(arr[arr.length - 1] + '- ' + arr.length);
            } else {
                for (var i = arrclass.length - 1; i >= 0; i--) {
                    if (arrclass[i] === mname) {
                        arrclass.splice(i, 1);
                        // break;       //<-- Uncomment  if only the first term has to be removed
                    }
                }
            }
        }*/
        $scope.openreport = function () {
            smonth = '';
            
            var arrclass = new Array();
            var arr = new Array();
            for (var x in $scope.listClass)
                if ($scope.listClass[x].isSelected)
                    arrclass[arrclass.length] = $scope.listClass[x].ClassCode;
            for (var x in $scope.monthList)
                if ($scope.monthList[x].isSelected)
                    arr[arr.length] = $scope.monthList[x].onlyname;

            if (arr.length > 1) {
                smonth = arr[0] + ',' + arr[arr.length-1];
                
            } else
                smonth = arr[0];
            
            //alert("/Report/Report?ReportID=103&CompanyCode=" + $scope.selectedObject.CompanyCode + '&BranchCode=' + $scope.selectedObject.BranchCode + '&SessionCode=' + $scope.selectedObject.SessionCode + '&BankCode=' + $scope.selectedObject.BankCode + '&ClassCode=' + arrclass.toString() + '&MonthCode=' + arr.toString());
            window.location = "/Report/Report?CompanyCode=" + $scope.selectedObject.CompanyCode + "&ReportID=103&BranchCode=" + $scope.selectedObject.BranchCode + "&SessionCode=" + $scope.selectedObject.SessionCode + "&BankCode=" + $scope.selectedObject.BankCode + "&ClassCode=" + arrclass.toString() + "&MonthCode=" + smonth;
            //window.location = "/Report/Report?ReportID=103";
        }
        

        var onGetCompany = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
            if ($scope.listCompany != null && $scope.listCompany.length > 0) {
                if (typeof $scope.selectedObject == 'undefined' || $scope.selectedObject == null)
                    setDefault();
                $scope.selectedObject.CompanyCode = $scope.listCompany[0].CompanyCode;

            }

            $scope.getCOACompanyWise();
            GetCompany = true;
            isFormLoaded();
        }
        $scope.getCOACompanyWise = function () {
            GetAllClass = GetAllBank = GetAllSession = GetAllBranch = false;
            showShield();

            $http.post("/UI/getAllBranchofCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllBranch, onRequestError);
            $http.post("/UI/getAllSessionofCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllSession, onRequestError);
            $http.post("/UI/getAllBankofCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllBank, onRequestError);
            $http.post("/UI/getAllActiveClassofCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllClass, onRequestError);
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

        
        var isValid = function () {
            if (typeof $scope.selectedObject == 'undefined' || $scope.selectedObject == null) setDefault();
            if (isnullorEmpty($scope.selectedObject.CompanyCode))
                $scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject.BranchCode))
                $scope.listError = "Branch is Required";
            else if (isnullorEmpty($scope.selectedObject.SessionCode))
                $scope.listError = "Session is Required";
            else if (validateClass())
                $scope.listError = "Class is Required";
            else return true;
            return false;
        }
        var validateClass = function () {
            Novalidate = true;
            for (var x in $scope.listClass)
                if (!angular.isUndefined($scope.listClass[x].isSelected) && $scope.listClass[x].isSelected)
                    Novalidate = false;
            return Novalidate;
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
        var monthDiff = function (d1, d2) {
            var months;
            months = (d2.getFullYear() - d1.getFullYear()) * 12;
            months -= d1.getMonth() + 1;
            months += d2.getMonth() + 1;
            return months <= 0 ? 0 : months;
        }
        var monthNames = ["January", "Feburary", "March", "April", "May", "June", "July", "Augest", "September", "October", "November", "December"];
        $scope.drawTable = function () {
            $scope.monthList = [];

            startMonth = new Date();

            endMonth = new Date(startMonth.getFullYear(), startMonth.getMonth() + 5, 01);
            var sm = new Date(startMonth);
            var em = new Date(endMonth);
            var total = monthDiff(sm, em);
            var a = 1;
            if (total == 0) return;
            sm.setDate(1);
            for (var i = 0; i <= total ; i++) {
                if (i == 0)
                    sm.setMonth(sm.getMonth());
                else
                    sm.setMonth(sm.getMonth() + 1);
                var lastday = new Date(sm.getFullYear(), sm.getMonth() + 1, 0);
                $scope.monthList.push({ "name": monthNames[sm.getMonth()] + ', ' + sm.getFullYear(), "start": sm.getDate() + '-' + sm.getMonth() + '-' + sm.getFullYear(), "end": lastday.getDate() + '-' + sm.getMonth() + '-' + lastday.getFullYear() });
                //alert(lastday.getDate());

            }
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
        $scope.drawTable();
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
    }
    app.controller("FeeParticularRateController", admController);
    addDirectives(app);
}
)();
