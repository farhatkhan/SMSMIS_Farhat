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

        var removeEmptyRow = function () {
            if ($scope.selectedObject[$scope.selectedObject.length - 1].FeeParticularCode == '' &&
               $scope.selectedObject[$scope.selectedObject.length - 1].Rate == '' &&
           $scope.selectedObject[$scope.selectedObject.length - 1].AccountCode == '')
                $scope.deleteRecord($scope.selectedObject.length - 1);
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
            $http.post("/UI/getAllCOAbyAccountType", { CompanyCode: $scope.selectedObject.CompanyCode, AccountType: "B", levelID: "D" }).then(onGetCOA, onRequestError);
            $http.post("/UI/getAllBranchofCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllBranch, onRequestError);
            $http.post("/UI/getAllSessionofCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllSession, onRequestError);
            $http.post("/UI/getAllBankofCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllBank, onRequestError);
            $http.post("/UI/getActiveClassesCompanyWise", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllClass, onRequestError);
            //$http.get("/UI/getAllFeeParticularRate").then(onListComplete, onRequestError);
            //$http.get("/UI/getAllActiveFeeParticular").then(onGetFeeParticular, onRequestError);
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
        var onGetAllBank = function (response) {
            if (response.data == null || response.data == undefined)
                listBank = true;
            $scope.listBank = response.data;
            GetAllBank = true;
            isFormLoaded();
        }

        var onGetFeeParticular = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listFeeParticular = response.data;
            GetFeeParticular = true;
            isFormLoaded();
        }
        var onGetCOA = function (response) {
            if (response.data == null || response.data == undefined)
                listCOA = true;
            $scope.listCOA = response.data;
            GetCOA = true;
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
            hideShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }
        //$scope.load = function (obj) {
        //    if (obj != null) {
        //        $scope.selectedObject = [];
        //        $scope.selectedObject.push(obj);
        //        if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
        //        $scope.isEditMode = true;
        //    }
        //    $scope.saveError = '';
        //}
        var onSaveComplete = function (response) {
            hideShield();
            $scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.selectedObject = {};
            $scope.selectedObject.CompanyCode = $scope.listCompany[0].CompanyCode;
            for (var x in $scope.listClass)
                $scope.listClass[x].isSelected = false;
            for (var x in $scope.monthList)
                $scope.monthList[x].isSelected = false;
            //$scope.listDataSearch = angular.copy($scope.listData);
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.apply();

        }
        var onDelete = function (response) {
            hideShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            //$scope.listDataSearch = angular.copy($scope.listData);
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.clear();
            $scope.saveError = 'Record deleted successfully';
        }
        $scope.delete = function () {
            clearErros();
            showShield();

            if (confirm('Are you sure you want to delete this record?'))
                removeEmptyRow();
            $http.post("/UI/FeeParticularRate", $scope.selectedObject).then(onDelete, onRequestError)
        }
        $scope.deleteGridRow = function (obj, index) {
            $scope.saveError = '';
            if (obj.length > 1) {
                if (confirm("Are you sure to delete this record?")) {
                    obj.splice(index, 1);
                }
            }
        }
        $scope.clear = function () {
            $scope.selectedObject = [];
            $scope.isEditMode = false;
        }
        $scope.addGrid = function () {

            if (typeof $scope.selectedObject == 'undefined')
                $scope.selectedObject = [];

            $scope.selectedObject.push(new NewRow());
        }
        $scope.deleteRecord = function (session) {
            $scope.selectedObject.splice(session, 1);
        }
        var NewRow = function (ParticularCode, ParticularText, ACCode, AcTitle, Rate) {
            //this.SrNo = srNo;
            this.CompanyCode = $scope.selectedObject[0].CompanyCode;
            this.BranchCode = $scope.selectedObject[0].BranchCode;
            this.SessionCode = $scope.selectedObject[0].SessionCode;
            this.ClassCode = $scope.selectedObject[0].ClassCode;
            this.FeeParticularCode = ParticularCode;
            this.FeeParticularText = ParticularText;
            this.AccountCode = ACCode;
            this.AccountTitle = AcTitle;
            this.Rate = Rate;
        }
        $scope.validateGrid = function (indx, type) {

            var vType = $('#txtRate' + indx);
            var aCode = $('#txtAccountCode' + indx);
            var status = $('#cboStatus' + indx);

            var obj = null;
            if (aCode.val() == '') {
                aCode.addClass("invalidvalue");
                //setTimeout(function () { glcde.focus(); }, 500);
                obj = aCode;
            } else aCode.removeClass("invalidvalue");
            if (vType.val() == '') {
                vType.addClass("invalidvalue");
                //setTimeout(function () { currency.focus(); }, 500);
                obj = vType;
            } else vType.removeClass("invalidvalue");

            if (status.find(":selected").index() == 0) {
                status.addClass("invalidvalue");
                //setTimeout(function () { currency.focus(); }, 500);
                obj = status;
            } else status.removeClass("invalidvalue");




            if (obj != null && typeof obj != 'undefined') {
                //setTimeout(function () { obj.focus(); }, 200);
                return false;
            }
            if (type)
                $scope.addGrid('', '', '', '', '', indx, 'new');
            return true;
            //ACCode, AcTitle, status, vCode

        }
        var validateAllGrids = function () {
            var count = 0;
            var err = 0;
            for (x in $scope.selectedObject) {
                if (!$scope.validateGrid(count, false)) err++;
                count++;
            }
            if (err > 0) return false;
            return true;
        }
        var isValid = function () {
            if (typeof $scope.selectedObject == 'undefined' || $scope.selectedObject == null) setDefault();
            //if (isnullorEmpty($scope.selectedObject.CompanyCode))
            //    $scope.listError = "Company is Required";
            //else if (isnullorEmpty($scope.selectedObject.BranchCode))
            //    $scope.listError = "Branch is Required";
            //else if (isnullorEmpty($scope.selectedObject.SessionCode))
            //    $scope.listError = "Session is Required";
            //else if (isnullorEmpty($scope.selectedObject.BankCode))
            //    $scope.listError = "Bank is Required";
            //else if (validateMonth())
            //    $scope.listError = "Month is Required";
            //else if (validateClass())
            //    $scope.listError = "Class is Required";
            //else return true;
            return true;
        }
        var validateClass = function () {
            Novalidate = true;
            for (var x in $scope.listClass)
                if (!angular.isUndefined($scope.listClass[x].isSelected) && $scope.listClass[x].isSelected)
                    Novalidate = false;
            return Novalidate;
        }
        var validateMonth = function () {
            Novalidate = true;
            for (var x in $scope.monthList)
                if (!angular.isUndefined($scope.monthList[x].isSelected) && $scope.monthList[x].isSelected)
                    Novalidate = false;
            return Novalidate;
        }
        var clearErros = function () {
            $scope.listError = $scope.saveError = '';
        }
        $scope.save = function () {

            $scope.saveError = '';
            if (!isValid()) return;
            clearErros();
            //removeEmptyRow();
            //if (!validateAllGrids()) return;

            showShield();
            if (!angular.isUndefined( $scope.selectedObject ) && $scope.selectedObject != null) {
                //var count = 1;
                //for (var x in $scope.selectedObject) {
                //    $scope.selectedObject[x].SrNo = count;
                //    count++;
                //}
                $scope.listClassCopy = $filter('filter')($scope.listClass, { isSelected: true, CompanyCode: $scope.selectedObject.CompanyCode }, true);
                $scope.monthListCopy = $filter('filter')($scope.monthList, { isSelected: true }, true);
                for (var x in $scope.monthListCopy) {
                    
                    $scope.monthListCopy[x].startDate = dateParser($scope.monthListCopy[x].start.replace(/-/g, '/'));
                    $scope.monthListCopy[x].endDate = dateParser($scope.monthListCopy[x].end.replace(/-/g, '/'));
                }
                $http.post("/UI/saveFeeBillMaster", {
                    CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObject.BranchCode, 
                    SessionCode: $scope.selectedObject.SessionCode, BankCode: $scope.selectedObject.BankCode,
                    IssueDate: $scope.selectedObject.IssueDate, DueDate: $scope.selectedObject.DueDate,
                    Class: $scope.listClassCopy, MonthList: $scope.monthListCopy
                }).then(onSaveComplete, onRequestError);//saif
            }
        }
        var isFormLoaded = function () {
            if (GetAllClass && GetAllBank && GetCompany && GetAllSession && GetAllBranch)
                hideShield();

        }
        $scope.addGrid = function (ParticularCode, ParticularText, ACCode, AcTitle, Rate, index, mode) {
            if (typeof $scope.selectedObject == 'undefined')
                $scope.selectedObject = [];
            //$scope.selectedObject[index].FeeParticularCode != '' && $scope.selectedObject[index].AccountCode != '' && $scope.selectedObject[index].Rate != ''
            if (mode == 'edit' ||
                (mode == 'new' && (index == ($scope.selectedObject.length - 1)))) {// && !isnullorEmpty($scope.selectedObject[index].FeeParticularCode) && !isnullorEmpty($scope.selectedObject[index].AccountCode) && !isnullorEmpty($scope.selectedObject[index].Rate )
                $scope.selectedObject.push(new NewRow(ParticularCode, ParticularText, ACCode, AcTitle, Rate));
                $scope.$apply();
            }
        }
        $scope.handleKeyEvent = function (e, indx) {
            if (e.keyCode == 13) {
                if ($scope.validateGrid(indx, true))
                    $scope.addGrid('', '', '', '', '', indx, 'new');
            }
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

        $scope.loadRecord = function () {
            //var count = 0;
            //delete $scope.selectedObject[x];
            //for (var x in $scope.selectedObject) {
            //    //if (count>0)
            //        delete $scope.selectedObject[x];
            //    //count++;
            //}
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
        $scope.drawTable();
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        
        //$http.post("/UI/getAllCOAbyAccountType", {CompanyCode: $scope.selectedObject[0].CompanyCode, AccountType: "B", levelID: "D" }).then(onGetCOA, onRequestError);//saif
    }
    app.controller("FeeParticularRateController", admController);
    addDirectives(app);
}
)();
