(function () {
    var app = angular.module("clientModule1", []);
    var admController = function ($scope, $http, $filter) {
        $scope.GetSelectedAccountTitle = function (data, x,indx) {
            data.AccountCode = x.AccountCode;
            data.AccountTitle = x.AccountTitle;
            $('.QuickSearchResults').hide();
            $scope.validateGrid(indx, true);
        }
        $scope.GetSelectedUEAccountTitle = function (data, x, indx) {
            data.UnEarnedAccountCode = x.AccountCode;
            data.AccountTitle = x.AccountTitle;
            $('.ueQuickSearchResults').hide();
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

        $scope.SetSelectedUEAccountTitle = function (data, indx) {
            data.AccountTitle = data.UnEarnedAccountCode;
            if (data.AccountTitle == '') {
                $('.def' + indx).hide();
                return;
            }

            var filteredArray = $filter('filter')($scope.listCOA, { AccountTitle: data.AccountTitle });
            if (filteredArray.length == 1 && filteredArray[0].AccountTitle.toUpperCase() == data.AccountTitle.toUpperCase()) {
                data.UnEarnedAccountCode = filteredArray[0].AccountCode;
                data.AccountTitle = filteredArray[0].AccountTitle;
                $('.def' + indx).hide();

            }
            else {
                $('.def' + indx).show();
            }
        }

        var removeEmptyRow = function () {
            if ($scope.selectedObject[$scope.selectedObject.length - 1].ParticularCode == '' &&
               $scope.selectedObject[$scope.selectedObject.length - 1].Rate == '' &&
           $scope.selectedObject[$scope.selectedObject.length - 1].AccountCode == ''
                && $scope.selectedObject[$scope.selectedObject.length - 1].UnEarnedAccountCode == '')
                $scope.deleteRecord($scope.selectedObject.length - 1);
        }
        
        var onGetCompany = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
            if ($scope.listCompany != null && $scope.listCompany.length > 0) {
                if (typeof $scope.selectedObject[0] == 'undefined' || $scope.selectedObject[0])
                    setDefault(0, 0);
                $scope.selectedObject[0].CompanyCode = $scope.listCompany[0].CompanyCode;

            }
            $scope.getCOACompanyWise();
            GetCompany = true;
            isFormLoaded();
        }
        $scope.getCOACompanyWise = function () {
            
            if (!angular.isUndefined($scope.selectedObject[0].BranchCode) && $scope.selectedObject[0].BranchCode > 0) {
                showShield();
                $http.post("/UI/getAllCOAbyAccountTypeCompanyBranch", { CompanyCode: $scope.selectedObject[0].CompanyCode, BranchCode: $scope.selectedObject[0].BranchCode, AccountType: "J", levelID: "D" }).then(onGetCOA, onRequestError);
            } else hideAllShield();
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
                listCompany = true;
            $scope.listClass = response.data;
            GetAllClass = true;
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
            hideAllShield();
            
            //isFormLoaded();
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
            //if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
            //    $scope.listError = reason.data.error;
            //else
            //    $scope.listError = reason.status + ': ' + reason.statusText;
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
            hideAllShield();
            //$scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            //$scope.listDataSearch = angular.copy($scope.listData);
            //if (!$scope.isEditMode)
            var ccode = $scope.selectedObject[0].CompanyCode;
            var bcode = $scope.selectedObject[0].BranchCode;
            $scope.clear(); //clear on add new mode
            setDefault(ccode, bcode);
            //$scope.selectedObject[0].CompanyCode = ccode;
            //$scope.selectedObject[0].BranchCode = bcode;
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.apply();

        }
        var onDelete = function (response) {
            hideAllShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            //$scope.listDataSearch = angular.copy($scope.listData);
            var ccode = $scope.selectedObject[0].CompanyCode;
            var bcode = $scope.selectedObject[0].BranchCode;
            $scope.clear(); //clear on add new mode
            setDefault(ccode, bcode);
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            
            //$scope.saveError = 'Record deleted successfully';
        }
        $scope.delete = function () {
            clearErros();
            showDeleteShield();
            
            if (confirm('Are you sure you want to delete this record?'))
                removeEmptyRow();
            $http.post("/UI/deleteFeeParticularRate", $scope.selectedObject).then(onDelete, onRequestError)
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
        var NewRow = function (ParticularCode, ParticularText, ACCode, AcTitle, Rate, ueACCode) {
            //this.SrNo = srNo;
            this.CompanyCode = $scope.selectedObject[0].CompanyCode;
            this.BranchCode = $scope.selectedObject[0].BranchCode;
            this.SessionCode = $scope.selectedObject[0].SessionCode;
            this.ClassCode = $scope.selectedObject[0].ClassCode;
            this.ParticularCode = ParticularCode;
            this.FeeParticularText = ParticularText;
            this.AccountCode = ACCode;
            this.UnEarnedAccountCode = ueACCode;
            this.AccountTitle = AcTitle;
            this.Rate = Rate;
        }
        
        $scope.validateGrid = function (indx, type) {

            var vType = $('#txtRate' + indx);

            var aCode = $('#txtAccountCode' + indx);
            var ueaCode = $('#txtunearnAC' + indx);
            var status = $('#cboStatus' + indx);
            
            var filterArrAcc = $filter('filter')($scope.listCOA, { AccountCode: $scope.selectedObject[indx].AccountCode }, true);
            var filterArrUnAcc = $filter('filter')($scope.listCOA, { AccountCode: $scope.selectedObject[indx].UnEarnedAccountCode }, true);
            var filterParticular = $filter('filter')($scope.selectedObject, { ParticularCode: $scope.selectedObject[indx].ParticularCode }, true);
            //var s = hasDuplicates($scope.selectedObject);
            //var sorted_arr = $scope.selectedObject.sort();
            var validAcc = false;
            var validUnAcc = false;
            var validParticular = false;
            if (!angular.isUndefined(filterArrAcc) && filterArrAcc != null && filterArrAcc.length > 0)
                validAcc = true;
            if (!angular.isUndefined(filterArrUnAcc) && filterArrUnAcc != null && filterArrUnAcc.length > 0)
                validUnAcc = true;
            if (!angular.isUndefined(filterParticular) && filterParticular != null && filterParticular.length < 2)
                validParticular = true;
            var obj = null;
            if (aCode.val() == '' || !validAcc) {
                aCode.addClass("invalidvalue");
                //setTimeout(function () { glcde.focus(); }, 500);
                obj = aCode;
            } else aCode.removeClass("invalidvalue");
            if (ueaCode.val() == '' || !validUnAcc) {
                ueaCode.addClass("invalidvalue");
                //setTimeout(function () { glcde.focus(); }, 500);
                obj = ueaCode;
            } else ueaCode.removeClass("invalidvalue");
            if (vType.val() == '') {
                vType.addClass("invalidvalue");
                //setTimeout(function () { currency.focus(); }, 500);
                obj = vType;
            } else vType.removeClass("invalidvalue");

            if (status.find(":selected").index() == 0 || !validParticular) {
                status.addClass("invalidvalue");
                //setTimeout(function () { currency.focus(); }, 500);
                obj = status;
            } else status.removeClass("invalidvalue");
            
            


            if (obj != null && typeof obj != 'undefined') {
                //setTimeout(function () { obj.focus(); }, 200);
                return false;
            }
            if (type)
                $scope.addGrid('', '', '', '','','', indx, 'new');
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
            if (typeof $scope.selectedObject[0] == 'undefined' || $scope.selectedObject[0] == null) setDefault(0, 0);
            if (isnullorEmpty($scope.selectedObject[0].CompanyCode))
                return false;//$scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject[0].BranchCode))
                return false;//$scope.listError = "Branch is Required";
            else if (isnullorEmpty($scope.selectedObject[0].SessionCode))
                return false;//$scope.listError = "Session is Required";
            else if (isnullorEmpty($scope.selectedObject[0].ClassCode))
                return false;//$scope.listError = "Class is Required";
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
            removeEmptyRow();
            if (!validateAllGrids()) return;

            showSaveShield();
            if (typeof $scope.selectedObject != 'undefined' && $scope.selectedObject.length > 0) {
                var count = 1;
                for (var x in $scope.selectedObject) {
                    $scope.selectedObject[x].SrNo = count;
                    count++;
                }
                $http.post("/UI/saveFeeParticularRate", { FeeParticularRate: $scope.selectedObject, CompanyCode: $scope.selectedObject[0].CompanyCode, BranchCode: $scope.selectedObject[0].BranchCode, SessionCode: $scope.selectedObject[0].SessionCode, ClassCode: $scope.selectedObject[0].ClassCode }).then(onSaveComplete, onRequestError);//saif
            }
        }
        var isFormLoaded = function () {
            if (GetCompany && GetAllSession && GetAllBranch && GetAllClass && ListComplete && GetFeeParticular)
                hideAllShield();
            
        }
        $scope.addGrid = function (ParticularCode, ParticularText, ACCode, AcTitle, Rate,ueACCode, index, mode) {
            if (typeof $scope.selectedObject == 'undefined')
                $scope.selectedObject = [];
            //$scope.selectedObject[index].ParticularCode != '' && $scope.selectedObject[index].AccountCode != '' && $scope.selectedObject[index].Rate != ''
            if (mode == 'edit' ||
                (mode == 'new' && (index == ($scope.selectedObject.length - 1)))) {// && !isnullorEmpty($scope.selectedObject[index].ParticularCode) && !isnullorEmpty($scope.selectedObject[index].AccountCode) && !isnullorEmpty($scope.selectedObject[index].Rate )
                $scope.selectedObject.push(new NewRow(ParticularCode, ParticularText, ACCode, AcTitle, Rate, ueACCode));
                $scope.$apply();
            }
        }
        $scope.handleKeyEvent = function (e, indx) {
            if (e.keyCode == 13) {
               if( $scope.validateGrid(indx, true))
                    $scope.addGrid('', '', '', '', '','', indx, 'new');
            }
        }
        $scope.loadRecord = function (obj) {
            //var count = 0;
            //delete $scope.selectedObject[x];
            //for (var x in $scope.selectedObject) {
            //    //if (count>0)
            //        delete $scope.selectedObject[x];
            //    //count++;
            //}
            if (!angular.isUndefined(obj) && obj == 'b')
                $scope.getCOACompanyWise();
                //$http.post("/UI/getAllCOAbyAccountTypeCompanyBranch", { CompanyCode: $scope.selectedObject[0].CompanyCode, BranchCode: $scope.selectedObject.BranchCode, AccountType: "B", levelID: "D" }).then(onGetCOA, onRequestError);

            clearErros();
            $scope.selectedObject[0].CompanyCode = typeof $scope.selectedObject[0].CompanyCode == 'undefined' ? '' : $scope.selectedObject[0].CompanyCode;
            $scope.selectedObject[0].BranchCode = typeof $scope.selectedObject[0].BranchCode == 'undefined' ? '' : $scope.selectedObject[0].BranchCode;
            $scope.selectedObject[0].SessionCode = typeof $scope.selectedObject[0].SessionCode == 'undefined' ? '' : $scope.selectedObject[0].SessionCode;
            $scope.selectedObject[0].ClassCode= typeof $scope.selectedObject[0].ClassCode == 'undefined' ?'':$scope.selectedObject[0].ClassCode;
            var filteredArray = $filter('filter')($scope.listData, {
                CompanyCode: $scope.selectedObject[0].CompanyCode,
                BranchCode: $scope.selectedObject[0].BranchCode,
                SessionCode: $scope.selectedObject[0].SessionCode,
                ClassCode: $scope.selectedObject[0].ClassCode
            }, true);
            //$scope.selectedObject = [];
            setDefault($scope.selectedObject[0].CompanyCode,$scope.selectedObject[0].BranchCode,$scope.selectedObject[0].SessionCode,$scope.selectedObject[0].ClassCode);
            if (filteredArray.length > 0) {
                $scope.isEditMode = true;
                $scope.selectedObject = angular.copy(filteredArray);
                var count = 0;
                //for (var x in filteredArray) {
                    //$scope.selectedObject[count] = filteredArray[x];
                //count++;
                $scope.addGrid('', '', '', '', '', '', 0, 'edit');
            }
            
                    //$scope.addGrid(filteredArray[x].ParticularCode, '', filteredArray[x].AccountCode, '', filteredArray[x].Rate, 0, 'edit')
        }
        var setDefault = function (company,branch,session,classcode) {
            $scope.selectedObject = [];
            var obj = new Object();
            obj.CompanyCode = company;
            obj.BranchCode = branch;
            obj.SessionCode = session;
            obj.ClassCode = classcode;
            $scope.selectedObject.push(obj);
            //$scope.selectedObject[0].BranchCode = branch;
            //$scope.selectedObject[0].SessionCode = session;
            //$scope.selectedObject[0].ClassCode = classcode;
        }
        //}
        $scope.isEditMode = false;
        $scope.clear();
        $scope.listData = [];
        $scope.selectedObject = [];
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
        
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        $http.get("/UI/getAllBranches").then(onGetAllBranch, onRequestError);
        $http.get("/UI/getAllSessions").then(onGetAllSession, onRequestError);
        $http.get("/UI/getAllClass").then(onGetAllClass, onRequestError);
        $http.get("/UI/getAllFeeParticularRate").then(onListComplete, onRequestError);
        $http.get("/UI/getAllActiveOnlyFeeParticular").then(onGetFeeParticular, onRequestError);
        
    }
    app.controller("FeeParticularRateController", admController);
    addDirectives(app);
}
)();