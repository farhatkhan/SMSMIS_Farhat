(function () {
    var app = angular.module("clientModule1", []);
    var admController = function ($scope, $http, $filter) {
        $scope.GetSelectedAccountTitle = function (data, x,indx) {
            data.AccountCode = x.AccountCode;
            data.AccountTitle = x.AccountTitle;
            $('.QuickSearchResults').hide();
        }

        $scope.SetSelectedAccountTitle = function (data,index) {
           data.AccountTitle = data.AccountCode;
            if (data.AccountTitle == '') {
                $('.abc' + index).hide();
                return;
            }

            var filteredArray = $filter('filter')($scope.listCOA, { AccountTitle: data.AccountTitle });
            if (filteredArray.length == 1 && filteredArray[0].AccountTitle.toUpperCase() == data.AccountTitle.toUpperCase()) {
                data.AccountCode = filteredArray[0].AccountCode;
                data.AccountTitle = filteredArray[0].AccountTitle;
                $('.abc' + index).hide();
            }
            else {
                $('.abc' + index).show();
            }
        }

        var onGetCompany = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
            //if ($scope.listCompany != null && $scope.listCompany.length > 0) {
            //    if (typeof $scope.selectedObject[0] == 'undefined' || $scope.selectedObject[0])
            //        setDefault(0,0);
            //    $scope.selectedObject[0].CompanyCode = $scope.listCompany[0].CompanyCode;
                
            //}
            //$scope.getCOACompanyWise();
            GetCompany = true;
            hideAllShield();
            //isFormLoaded();
        }
        
        var onGetAllBranch = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listBranch = response.data;
            GetAllBranch = true;
            isFormLoaded();
        }
        
        var onGetCOA = function (response) {
            if (response.data == null || response.data == undefined)
                listCOA = true;
            $scope.listCOA = response.data;
            GetCOA = true;
            isFormLoaded();
        }
        var onGetVoucherType = function (response) {
            if (response.data == null || response.data == undefined)
                listVType = true;
            $scope.listVType = response.data;
            GetVoucherType = true;
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
        $scope.load = function (obj) {
            if (obj != null) {
                $scope.selectedObject = [];
                $scope.selectedObject.push(obj);
                if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                $scope.isEditMode = true;
            }
            $scope.saveError = '';
        }
        var onSaveComplete = function (response) {
            hideAllShield();
            //$scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.clear();
            //$scope.listDataSearch = angular.copy($scope.listData);
            //if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.apply();

        }
        var onDelete = function (response) {
            hideAllShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            //$scope.listDataSearch = angular.copy($scope.listData);
            //if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.clear();
            //$scope.saveError = 'Record deleted successfully';
        }
        $scope.delete = function () {
            showDeleteShield();
            $scope.saveError = '';
            if (confirm('Are you sure you want to delete this record?'))
                removeEmptyRow();
            $http.post("/UI/deleteVoucherTypeBranch", $scope.selectedObject).then(onDelete, onRequestError)
        }
        $scope.deleteGridRow = function (obj, index) {
            $scope.saveError = '';
            if (obj.length > 1) {
                if (confirm("Are you sure to delete this record?")) {
                    if ($scope.selectedObject.length == (index + 1))
                        $scope.addGrid('', '', '', index + 1, 'new');
                    obj.splice(index, 1);
                    //$scope.reSort(obj);
                }
            }
            
        }
        $scope.clear = function () {
            var CCode = 0;
            var BCode = 0;
            if (!angular.isUndefined($scope.selectedObject) && $scope.selectedObject != null) {
                CCode = typeof $scope.selectedObject[0].CompanyCode == 'undefined' ? '' : $scope.selectedObject[0].CompanyCode;
                BCode = typeof $scope.selectedObject[0].BranchCode == 'undefined' ? '' : $scope.selectedObject[0].BranchCode;
            }
            $scope.selectedObject = [];
            setDefault(CCode, BCode, 0);
            
            $scope.isEditMode = false;
        }
        $scope.deleteRecord = function (session) {
            $scope.selectedObject.splice(session, 1);
        }
        var NewRow = function (ACCode, AcTitle, status,vCode) {
            this.CompanyCode = $scope.selectedObject[0].CompanyCode;
            this.BranchCode = $scope.selectedObject[0].BranchCode;
            this.VoucherCode = $scope.selectedObject[0].VoucherCode;
            this.AccountCode = ACCode;
            this.AccountTitle = AcTitle;
            this.Status = status;
            
        }
        var validateAllGrids = function () {
            var count = 0;
            var err = 0;
            for (x in $scope.selectedObject) {
               if(!$scope.validateGrid(count,false)) err++;
                count++;
            }
            if (err > 0) return false;
            return true;
        }
        var isValid = function () {
            if (typeof $scope.selectedObject[0] == 'undefined' || $scope.selectedObject[0] == null) setDefault(0, 0,0);
            if (isnullorEmpty($scope.selectedObject[0].CompanyCode))
                $scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject[0].BranchCode))
                $scope.listError = "Branch Code is Required";
            else return true;
            return false;
        }
        var clearErros = function () {
            $scope.listError = $scope.saveError = '';
        }
        var removeEmptyRow = function () {
            if ($scope.selectedObject[$scope.selectedObject.length - 1].Status == '' &&
               
           $scope.selectedObject[$scope.selectedObject.length - 1].AccountCode == '')
                $scope.deleteRecord($scope.selectedObject.length - 1);
        }
        $scope.save = function () {

            $scope.saveError = '';
            if (!isValid()) return;
            clearErros();
            removeEmptyRow();
            if (!validateAllGrids()) return;
            
            showSaveShield();
            if (typeof $scope.selectedObject != 'undefined' && $scope.selectedObject.length > 0) {
                $scope.selectedObjectCopy = angular.copy($scope.selectedObject);
                for (var x in $scope.selectedObjectCopy) {
                    $scope.selectedObjectCopy[x].Status = $scope.selectedObjectCopy[x].Status == "1" ? true : false;
                }
                $http.post("/UI/saveVoucherTypeBranch", { VoucherTypeBranch: $scope.selectedObjectCopy, CompanyCode: $scope.selectedObjectCopy[0].CompanyCode, BranchCode: $scope.selectedObjectCopy[0].BranchCode, SessionCode: $scope.selectedObjectCopy[0].SessionCode, ClassCode: $scope.selectedObjectCopy[0].ClassCode }).then(onSaveComplete, onRequestError);//saif
            }
        }
        var isFormLoaded = function () {
            if (GetAllBranch && ListComplete && GetVoucherType && GetCOA)
                hideAllShield();
        }
        $scope.addGrid = function (ACCode, AcTitle, status, index, mode) {
            if (typeof $scope.selectedObject == 'undefined')
                $scope.selectedObject = [];
            //$scope.selectedObject[index].FeeParticularCode != '' && $scope.selectedObject[index].AccountCode != '' && $scope.selectedObject[index].Rate != ''
            if (mode == 'edit' || (mode == 'new' && (index == ($scope.selectedObject.length - 1) //&& !isnullorEmpty($scope.selectedObject[index].FeeParticularCode) && !isnullorEmpty($scope.selectedObject[index].AccountCode) && !isnullorEmpty($scope.selectedObject[index].Rate)
                ))) 
            {
                $scope.selectedObject.push(new NewRow(ACCode, AcTitle, status));
                $scope.$apply();
            }
        }
        
        $scope.loadRecord = function (isCompany) {
            clearErros();
            if (isCompany)
                getCOACompanyWise();
            $scope.selectedObject[0].CompanyCode = typeof $scope.selectedObject[0].CompanyCode == 'undefined' ? '' : $scope.selectedObject[0].CompanyCode;
            $scope.selectedObject[0].BranchCode = typeof $scope.selectedObject[0].BranchCode == 'undefined' ? '' : $scope.selectedObject[0].BranchCode;
            $scope.selectedObject[0].VoucherCode = typeof $scope.selectedObject[0].VoucherCode == 'undefined' ? '' : $scope.selectedObject[0].VoucherCode;
            var filteredArray = $filter('filter')($scope.listData, {
                CompanyCode: $scope.selectedObject[0].CompanyCode,
                BranchCode: $scope.selectedObject[0].BranchCode,
                VoucherCode: $scope.selectedObject[0].VoucherCode
            }, true);
            //$scope.selectedObject = [];
            setDefault($scope.selectedObject[0].CompanyCode, $scope.selectedObject[0].BranchCode, $scope.selectedObject[0].VoucherCode);
            if (filteredArray.length > 0) {
                $scope.isEditMode = true;
                $scope.selectedObject = angular.copy(filteredArray);
                for (x in $scope.selectedObject)
                    $scope.selectedObject[x].Status = $scope.selectedObject[x].Status == true ? "1" : "0";
                //for (var x in filteredArray) {
                //$scope.selectedObject[count] = filteredArray[x];
                //count++;
                $scope.addGrid('', '', '', filteredArray.length, 'edit');
            }
            
            //$scope.addGrid(filteredArray[x].FeeParticularCode, '', filteredArray[x].AccountCode, '', filteredArray[x].Rate, 0, 'edit')
        }
        var setDefault = function (company, branch,vcode) {
            $scope.selectedObject = [];
            var obj = new Object();
            obj.CompanyCode = company;
            obj.BranchCode = branch;
            obj.VoucherCode = vcode;
            $scope.selectedObject.push(obj);
        }
        var getCOACompanyWise = function () { 
            showShield();
            GetAllBranch = ListComplete = GetVoucherType = GetCOA = false;
            $http.post("/UI/getAllActiveBranchesCompanyWise", { CompanyCode: $scope.selectedObject[0].CompanyCode }).then(onGetAllBranch, onRequestError);
            $http.post("/UI/getAllVoucherTypeBranch", { CompanyCode: $scope.selectedObject[0].CompanyCode }).then(onListComplete, onRequestError);
            $http.post("/UI/getApprovedVoucherTypeFilterJournal", { CompanyCode: $scope.selectedObject[0].CompanyCode }).then(onGetVoucherType, onRequestError);
            $http.post("/UI/getAllCOAbyAccountType", { CompanyCode: $scope.selectedObject[0].CompanyCode, AccountType: "B", levelID: "D" }).then(onGetCOA, onRequestError);
        }
        //var handleKeyEvent = function (event,indx) {
        //    if (event.keyCode == 13)
        //        validateGrid(indx);
        //}
        $scope.handleKeyEvent = function (e, indx) {
            if (e.keyCode == 13) {
                if($scope.validateGrid(indx, true))
                $scope.addGrid('', '', '', indx, 'new');
            }
        }
        $scope.validateGrid = function (indx,type) {
   
            //var vType = $('#cboVoucherType' + indx);
            var aCode = $('#txtAccountCode' + indx);
            var status = $('#cboStatus' + indx);
            var filterArrAcc = $filter('filter')($scope.listCOA, { AccountCode: $scope.selectedObject[indx].AccountCode }, true);
            var filterArrAcc1 = $filter('filter')($scope.selectedObject, { AccountCode: $scope.selectedObject[indx].AccountCode }, true);
            var validAcc = true;
            if (!angular.isUndefined(filterArrAcc1) && filterArrAcc1 != null && filterArrAcc1.length > 1)
                validAcc = false;
            if (!angular.isUndefined(filterArrAcc) && filterArrAcc != null && filterArrAcc.length == 0)
                validAcc = false;
            
            var obj = null;
            if (status.find(":selected").index() == 0) {
                status.addClass("invalidvalue");
                //setTimeout(function () { currency.focus(); }, 500);
                obj = status;
            } else status.removeClass("invalidvalue");
            if (aCode.val() == '' || !validAcc) {
                aCode.addClass("invalidvalue");
                //setTimeout(function () { glcde.focus(); }, 500);
                obj = aCode;
            } else aCode.removeClass("invalidvalue");
            //if (vType.find(":selected").index() == 0) {
            //    vType.addClass("invalidvalue");
            //    //setTimeout(function () { currency.focus(); }, 500);
            //    obj = vType;
            //} else vType.removeClass("invalidvalue");
            
            
            
            if (obj != null && typeof obj != 'undefined') {
                setTimeout(function () { obj.focus(); }, 200);
                return false;
            }
            if(type)
            $scope.addGrid('', '', '', indx, 'new');
            return true;
            //ACCode, AcTitle, status, vCode
            
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
        $scope.listVType = [];

        $scope.saveError = '';
        //$scope.addGrid();
        var GetCompany = false;
        var GetAllBranch = false;
        var GetAllSession = false;
        var GetAllClass = false;
        var ListComplete = false;
        var GetFeeParticular = false;
        var GetCOA = false;
        var GetVoucherType = false;
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        
    }

    app.controller("BranchVoucherTypeController", admController);
}
)();