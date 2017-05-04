(function () {
    var app = angular.module("clientModule1", []);
    var admController = function ($scope, $http, $filter) {
        $scope.GetSelectedAccountTitle = function (data, x, indx) {
            data.ChallanNo = x.ChallanNo;
            //data.AccountTitle = x.AccountTitle;
            data.ClassName = x.ClassName;
            data.FullName = x.FullName;
            data.StudentNo = x.StudentNo;
            data.TotalAmount = x.TotalAmount;
            data.DiscountAmount = x.DiscountAmount;
            data.OutstandingAmount = x.OutstandingAmount;
            data.WaivedAmount = 0;
            data.NetAmount = data.ReceivedAmount = x.OutstandingAmount;
            $('.QuickSearchResults').hide();
            $http.post("/UI/getFeeDetailForFeeReciept", { CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObject.BranchCode, SessionCode: $scope.selectedObject.SessionCode, ChallanNo: $scope.selectedObject.ChallanNo }).then(onGetAllFeeDetail, onRequestError);
        }
        
        $scope.calcWaived = function () {
            $scope.selectedObject.ReceivedAmount = $scope.selectedObject.NetAmount = $scope.selectedObject.OutstandingAmount - $scope.selectedObject.WaivedAmount;
        }
        $scope.SetSelectedAccountTitle = function (data) {
            data.ChallanNo = data.ChallanNo;
            if (data.ChallanNo == '') {
                $('.QuickSearchResults').hide();
                return;
            }

            $scope.filteredArray = $filter('filter')($scope.listChallan, { ChallanNo: data.ChallanNo});
            if ($scope.filteredArray.length > 0) {
                //data.ChallanNo = filteredArray[0].ChallanNo;
                //data.AccountTitle = filteredArray[0].AccountTitle;
                $('.QuickSearchResults').show();
            } else $('.QuickSearchResults').hide();
        }
        var removeEmptyRow = function () {
            if ($scope.selectedObject[$scope.selectedObject.length - 1].ParticularCode == '' &&
               $scope.selectedObject[$scope.selectedObject.length - 1].Rate == '' &&
           $scope.selectedObject[$scope.selectedObject.length - 1].AccountCode == ''
                && $scope.selectedObject[$scope.selectedObject.length - 1].UnEarnedAccountCode == '')
                $scope.deleteRecord($scope.selectedObject.length - 1);
        }
        var onGetAllFeeDetail = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
            { }
            $scope.listFeeDetail = response.data;

        }
        var onGetAllFeeReceiptDetail = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
            { }
            $scope.selectedObject = response.data.mainObject[0];
            $scope.listFeeDetail = response.data.subObject;
            $scope.calcWaived();
            
        }
        var onGetInstrumentType = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
            { }
            $scope.listInstrumentType = response.data;
            
        }

        var onGetCompany = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
            if ($scope.listCompany != null && $scope.listCompany.length > 0) {
                //if (typeof $scope.selectedObject == 'undefined' || $scope.selectedObject)
                    //setDefault(0, 0);
                $scope.selectedObject.CompanyCode = $scope.listCompany[0].CompanyCode;

            }
            $scope.getCompanyWise();
            GetCompany = true;
            isFormLoaded();
        }
        $scope.getCompanyWise = function () {
            hideShield();
            $http.post("/UI/getAllBranchofCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllBranch, onRequestError);
            $http.post("/UI/getAllSessionofCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllSession, onRequestError);
            
        }
        var onGetAllBranch = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listBranch = response.data;
            if ($scope.listBranch != null && $scope.listBranch.length > 0) {
                $scope.selectedObject.BranchCode = $scope.listBranch[0].BranchCode;
                $scope.getBranchWise();
            }
            GetAllBranch = true;
            //isFormLoaded();
        }
        var onGetFeeBillMaster = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listChallan = response.data;
        }

        $scope.getBranchWise = function (response) {
            $http.post("/UI/getAllFeeReceiptCompanyBranchWise", { CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObject.BranchCode }).then(onGetFeeReceipt, onRequestError);
            getChallanNo();
        }
        $scope.getSessionWise = function (response) {
            getChallanNo();
        }
        var getChallanNo = function () {
            if (!angular.isUndefined($scope.selectedObject.SessionCode) && !angular.isUndefined($scope.selectedObject.BranchCode) && !angular.isUndefined($scope.selectedObject.CompanyCode))
            $http.post("/UI/getFeeBillMasterWithStudent", { CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObject.BranchCode, SessionCode: $scope.selectedObject.SessionCode }).then(onGetFeeBillMaster, onRequestError);
        }
        var onGetFeeReceipt = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listData = response.data;
            //GetAllSession = true;
        }
        var onGetAllSession = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listSession = response.data;
            GetAllSession = true;
            //isFormLoaded();
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
        $scope.load = function (obj) {
            if (obj != null) {
                $http.post("/UI/getAllFeeReceiptData", { CompanyCode: obj.CompanyCode, BranchCode: obj.BranchCode, SessionCode: obj.SessionCode, ChallanNo: obj.ChallanNo, ReceiptNo: obj.ReceiptNo }).then(onGetAllFeeReceiptDetail, onRequestError);
                if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                $scope.isEditMode = true;
            }
            $scope.saveError = '';
        }
        var onSaveComplete = function (response) {
            hideShield();
            $scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data.mainObject.Data;
            //$scope.listData = response.data;
            $scope.report = response.data.subObject;

            var a = new Date($scope.selectedObject.ReceiptDate.toString());
            var getmonth = a.getMonth() < 9 ? '0' + (a.getMonth() + 1) : a.getMonth() + 1;
            var getdate = a.getDate() < 9 ? '0' + a.getDate() : a.getDate();
            window.location = "/Report/Report?CompanyCode=" + $scope.selectedObject.CompanyCode + "&ReportID=106&BranchCode=" + $scope.selectedObject.BranchCode + "&ReceiptNo=" + $scope.report.ReceiptNo + "&ReceiptDate=" + a.getFullYear() + "-" + getmonth + "-" + getdate;

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
            $scope.selectedObject = {};
            $scope.isEditMode = false;
            //$scope.listChallan = {};
        }
        $scope.addGrid = function () {

            if (typeof $scope.selectedObject == 'undefined')
                $scope.selectedObject = {};

            $scope.selectedObject.push(new NewRow());
        }
        $scope.deleteRecord = function (session) {
            $scope.selectedObject.splice(session, 1);
        }
        var NewRow = function (ParticularCode, ParticularText, ACCode, AcTitle, Rate, ueACCode) {
            //this.SrNo = srNo;
            this.CompanyCode = $scope.selectedObject.CompanyCode;
            this.BranchCode = $scope.selectedObject.BranchCode;
            this.SessionCode = $scope.selectedObject.SessionCode;
            this.ClassCode = $scope.selectedObject.ClassCode;
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

            var obj = null;
            if (aCode.val() == '') {
                aCode.addClass("invalidvalue");
                //setTimeout(function () { glcde.focus(); }, 500);
                obj = aCode;
            } else aCode.removeClass("invalidvalue");
            if (ueaCode.val() == '') {
                ueaCode.addClass("invalidvalue");
                //setTimeout(function () { glcde.focus(); }, 500);
                obj = ueaCode;
            } else ueaCode.removeClass("invalidvalue");
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
                $scope.addGrid('', '', '', '', '', '', indx, 'new');
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
            //if (typeof $scope.selectedObject == 'undefined' || $scope.selectedObject == null) setDefault(0, 0);
            if (isnullorEmpty($scope.selectedObject.CompanyCode))
                $scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject.BranchCode))
                $scope.listError = "Branch is Required";
            
            else if (isnullorEmpty($scope.selectedObject.ReceiptDate))
                $scope.listError = "Receipt Date is Required";

            else if (isnullorEmpty($scope.selectedObject.SessionCode))
                $scope.listError = "Session is Required";
            else if (isnullorEmpty($scope.selectedObject.StudentNo))
                $scope.listError = "Challan # is Required";
            else if (isnullorEmpty($scope.selectedObject.ReceivedAt))
                $scope.listError = "Received at is Required";
            else if (isnullorEmpty($scope.selectedObject.InstrumentCode))
                $scope.listError = "Instrument is Required";
            else return true;
            return false;
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
            //if (typeof $scope.selectedObject != 'undefined' && $scope.selectedObject.length > 0) {
            //    var count = 1;
            //    for (var x in $scope.selectedObject) {
            //        $scope.selectedObject[x].SrNo = count;
            //        count++;
            //    }
                $http.post("/UI/saveFeeReceipt", { FeeReceipt: $scope.selectedObject }).then(onSaveComplete, onRequestError);//saif
            //}
        }
        var isFormLoaded = function () {
            if (GetCOA && GetCompany && GetAllSession && GetAllBranch && GetAllClass && ListComplete && GetFeeParticular)
                hideShield();

        }
        $scope.addGrid = function (ParticularCode, ParticularText, ACCode, AcTitle, Rate, ueACCode, index, mode) {
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
                if ($scope.validateGrid(indx, true))
                    $scope.addGrid('', '', '', '', '', '', indx, 'new');
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
            if(obj == 'C')
            $scope.getCompanyWise();
           
        }
        var setDefault = function (company, branch, session, classcode) {
            $scope.selectedObject = [];
            var obj = new Object();
            obj.CompanyCode = company;
            obj.BranchCode = branch;
            obj.SessionCode = session;
            obj.ClassCode = classcode;
            $scope.selectedObject.push(obj);
            //$scope.selectedObject.BranchCode = branch;
            //$scope.selectedObject.SessionCode = session;
            //$scope.selectedObject.ClassCode = classcode;
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
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        //$http.get("/UI/getAllBranch").then(onGetAllBranch, onRequestError);
        //$http.get("/UI/getAllSession").then(onGetAllSession, onRequestError);
        $http.get("/UI/getAllActiveInstrumentType").then(onGetInstrumentType, onRequestError);
    }
    app.controller("FeeParticularRateController", admController);
    addDirectives(app);
}
)();