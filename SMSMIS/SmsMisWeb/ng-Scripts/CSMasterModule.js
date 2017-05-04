(function () {
    var app = angular.module("clientModule1", []);
    var comController = function ($scope, $http, $filter) {
        $scope.GetSelectedAccountTitle = function (data, x, indx) {
            data.AccountCode = x.AccountCode;
            data.AccountTitle = x.AccountTitle;
            $('.QuickSearchResults').hide();
            $scope.validateGrid(indx, true);
        }
        $scope.GetSelectedInstrumentName = function (x) {
            $scope.selectedObject.InstrumentName = x.InstrumentName;
            $scope.selectedObject.InstrumentCode = x.InstrumentCode;
            $('.QuickSearchResults').hide();
        }
        $scope.SetSelectedInstrumentType = function (data) {
            data.InstrumentName = data.InstrumentName;
            if (data.InstrumentName == '') {
                $('.insType').hide();
                return;
            }
            $('.insType').show();
            var filteredArray = $filter('filter')($scope.InstrumentType, { InstrumentName: data.InstrumentName });
            //if (filteredArray.length == 1 && filteredArray[0].InstrumentName.toUpperCase() == data.InstrumentName.toUpperCase()) {
            //    data.AccountCode = filteredArray[0].AccountCode;
            //    data.InstrumentName = filteredArray[0].InstrumentName;
            //    $('.abc' + indx).hide();
            //}
            //else {

            //}
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
        var onGetCompany = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listCompany = true;
            $scope.listCompany = response.data;
            //$scope.getCompanyWise();
            GetCompany = true;
            //isFormLoaded();
        }
        
        
        
        $scope.getCompanyWise = function () {
            
            if (!angular.isUndefined($scope.selectedObject.CompanyCode) || $scope.selectedObject.CompanyCode != null) {
                showShield();
                $scope.clear();
                GetCOAGroup = ListComplete = false;
                //$http.post("/UI/getAllCOAbyAccountType", { CompanyCode: $scope.selectedObject.CompanyCode, AccountType: "B", levelID: "D" }).then(onGetCOA, onRequestError);
                //$http.post("/UI/getApprovedCSType", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetCSType, onRequestError);
                //$http.post("/UI/getAllBranchofCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllBranch, onRequestError);
                $http.post("/UI/getCSMasterByCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onListComplete, onRequestError);
                $http.post("/UI/getAllCOAGroup", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onListCOAGroup, onRequestError);
                
                //$http.post("/UI/getAllPreferencesofCompany", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onListPreferences, onRequestError);
            }
        }
        var onListCSFontStyle = function (response) {
            if (response.data == null || response.data == undefined)
                listCOA = true;
            $scope.listCSFontStyle = response.data;
            GetCSFontStyle = true;
            isFormLoaded();
        }
        var onListCSDataAs = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listCompany = true;
            $scope.listCSDataAs = response.data;
            GetCSDataAs = true;
            isFormLoaded();
        }
        var onListCSObjectBorder = function (response) {
            if (response.data == null || response.data == undefined)
                listCOA = true;
            $scope.listCSObjectBorder = response.data;
            GetCSObjectBorder = true;
            isFormLoaded();
        }
        
        var onListCOAGroup = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listData = true;
            $scope.listCOAGroup = response.data;
            GetCOAGroup = true;
            isCompanyDataLoaded();
            //
            //isFormLoaded();
        }
        var onListCSRowAction = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listData = true;
            $scope.listCSRowAction = response.data;
            GetCSRowAction = true;
            isFormLoaded();
        }
        var onListComplete = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listData = true;
            $scope.listData = response.data;
            //filteredData
            //$scope.filteredData = $filter('filter')($scope.listData, { CompanyCode: $scope.selectedObject.CompanyCode },true);
            ListComplete = true;
            isCompanyDataLoaded();
            //isFormLoaded();
        }
        var onRequestError = function (reason) {
            hideAllShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }
        var onListInstrumentType = function (reason) {
            hideAllShield();
            $scope.InstrumentType = reason.data;
            //if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
        }
        var onListCurrency = function (reason) {
            //hideAllShield();
            $scope.listCurrency = reason.data;
        }
        var onSaveComplete = function (response) {
            //$scope.saveError = 'Save Record successfully';
            hideAllShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }

            $scope.listData = response.data;
            //$scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";


            //if (!$scope.isEditMode)
                $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            
            $scope.$apply();

        }
        var onLoadComplete = function (response) {
            //$scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.selectedObject = response.data[0];
            $scope.isEditMode = true;

            $scope.getCompanyWise();
        }
        var loadbyobj = function () {
            $scope.actionList = [];
            //$scope.addNewAction(obj.CSNo, obj.AccountCode, obj.Narration, obj.LC_Debit, obj.LC_Credit, 0, 'edit');
            //if (angular.isUndefined($scope.selectedObject.CSDetail) || $scope.selectedObject.CSDetail.length == 0)


            for (var y in $scope.selectedObject.CSDetail) {
                var obj = $scope.selectedObject.CSDetail[y];
                var filteredArray = $filter('filter')($scope.listCOA, { AccountCode: obj.AccountCode });
                $scope.addNewAction(obj.CSNo, obj.AccountCode, filteredArray[0].AccountTitle, obj.Narration, obj.LC_Debit, obj.LC_Credit, obj.FC_Debit, obj.FC_Credit, 0, 'edit');
            }
            $scope.addNewAction('', '', '', '1', '', '', '1', '', '', '', 'Regular', '1', 'None', 'None', '', 0, 'first');
            //$scope.selectedObject.CSDate = toISOFormat(dateParser($scope.selectedObject.CSDate));
            //if (!angular.isUndefined($scope.selectedObject.InstrumentDate))
            //    $scope.selectedObject.InstrumentDate = toISOFormat(dateParser($scope.selectedObject.InstrumentDate));

            $scope.isEditMode = true;
            //if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            hideAllShield();
            $scope.$apply();
        }

        var isValid = function () {
            if (isnullorEmpty($scope.selectedObject.CompanyCode))
                return false;//$scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject.ReportName))
                return false;//$scope.listError = "Statement Name is Required";
            else if (isnullorEmpty($scope.selectedObject.ReportTitle))
                return false;//$scope.listError = "CS Type is Required";
            else if (isnullorEmpty($scope.selectedObject.Remarks))
                return false;//$scope.listError = "CS Date is Required";
            else return true;
            return false;

        }
        $scope.addNewAction = function (gID, desc, subdesc, main,coagroup, dateas, invsign, action, formula,fontsize,style,underline,tborder,bborder,remarks, index, mode) {
            var a = $scope.actionList.length;
            if (mode == 'edit' || (
                mode == 'new' && (index == ($scope.actionList.length - 1)) ) ||
                (mode == 'first' && index == 0)) {
                $scope.actionList.push(new actionModel(gID, desc, subdesc, main,coagroup, dateas, invsign, action, formula, fontsize, style, underline, tborder, bborder, remarks));
                $scope.reSort();
                $scope.$apply();
            }
        }
        $scope.handleKeyEvent = function (e, indx) {
            if (e.keyCode == 13) {
                if (validateDetailGrid(indx))
                    $scope.addNewAction('', '', '', '1', '', '', '1', '', '', '', 'Regular', '1', 'None', 'None', '', indx, 'new');
            }
        }
        var validateDetailGrid = function (indx) {
            var glcde = $('#txtGroupID' + indx);
            var desc = $('#txtDescription' + indx);
            var notecode = $('#cboNoteCode' + indx);
            var fontstyle = $('#cboFontStyle' + indx);
            var tborder = $('#cboTopBorder' + indx);
            var bborder = $('#cboBottomBorder' + indx);
            //var credit = $('#cboMain' + indx);

            

            var obj = null;


            if (notecode.find(":selected").index() == 0) {
                notecode.addClass("invalidvalue");
                //setTimeout(function () { currency.focus(); }, 500);
                obj = notecode;
            } else notecode.removeClass("invalidvalue");

            if (fontstyle.find(":selected").index() == 0) {
                fontstyle.addClass("invalidvalue");
                //setTimeout(function () { currency.focus(); }, 500);
                obj = fontstyle;
            } else fontstyle.removeClass("invalidvalue");

            if (tborder.find(":selected").index() == 0) {
                tborder.addClass("invalidvalue");
                //setTimeout(function () { currency.focus(); }, 500);
                obj = tborder;
            } else tborder.removeClass("invalidvalue");

            if (bborder.find(":selected").index() == 0) {
                bborder.addClass("invalidvalue");
                //setTimeout(function () { currency.focus(); }, 500);
                obj = bborder;
            } else bborder.removeClass("invalidvalue");


            if ((desc.val() == '' )) {
                desc.addClass("invalidvalue"); 
                obj = desc;
                //setTimeout(function () { debit.focus(); }, 500);
            } else { desc.removeClass("invalidvalue");}


            if (glcde.val() == '') {
                glcde.addClass("invalidvalue");
                //setTimeout(function () { glcde.focus(); }, 500);
                obj = glcde;
            } else glcde.removeClass("invalidvalue");
            if (obj != null && typeof obj != 'undefined') {
                setTimeout(function () { obj.focus(); }, 500);
                return false;
            }
            return true;
        }
        var clearErros = function () {
            $scope.listError = $scope.saveError = '';
        }
        $scope.save = function () {
            clearErros();
            if (!isValid()) return;
            //if ($scope.actionList.length == 0) {
            //    $scope.listError = 'Debit and Credit is Required';
            //    return;
            //} else if ($scope.TotalLCCredit != $scope.TotalLCDebit) {
            //    $scope.listError = 'Total Debit and Credit Should be Equal';
            //    return;
            //}
            //if (isnullorEmpty($scope.actionList[$scope.actionList.length - 1].AccountCode) && isnullorEmpty($scope.actionList[$scope.actionList.length - 1].LC_Debit) && isnullorEmpty($scope.actionList[$scope.actionList.length - 1].LC_Credit) && isnullorEmpty($scope.actionList[$scope.actionList.length - 1].Narration))
            //    $scope.deleteActions($scope.actionList.length - 1, true);
            for (var x in $scope.actionList) {
                var indx = $scope.actionList[x].SerialNo - 1;

                if (!validateDetailGrid(indx)) return;
            }

            
            //$scope.selectedObjectCopy = angular.copy($scope.selectedObject);

            //$scope.selectedObjectCopy.CSDate = dateParser($scope.selectedObjectCopy.CSDate);
            //if (!angular.isUndefined($scope.selectedObjectCopy.InstrumentDate))
            //    $scope.selectedObjectCopy.InstrumentDate = dateParser($scope.selectedObjectCopy.InstrumentDate);
            $scope.selectedObject.CSDetail = [];
            $scope.selectedObject.CSDetail = angular.copy($scope.actionList);
            //$scope.selectedObject.Status = $scope.selectedObject.Status == "1" ? true : false;
            showSaveShield();
            $http.post("/UI/saveCSMaster", { CSMaster: $scope.selectedObject }).then(onSaveComplete, onRequestError);//saif
        }
        var onDelete = function (response) {
            hideAllShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            //if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.clear();
            //$scope.saveError = 'Record deleted successfully';
        }
        $scope.delete = function () {
            clearErros();
            $scope.saveError = '';
            //$scope.selectedObject.CSDate = dateParser($scope.selectedObject.CSDate);
            //if (!angular.isUndefined($scope.selectedObject.InstrumentDate))
            //    $scope.selectedObject.InstrumentDate = dateParser($scope.selectedObject.InstrumentDate);
            if (confirm('Are you sure you want to delete this record?')) {
                showDeleteShield();
                $http.post("/UI/deleteCSMaster", $scope.selectedObject).then(onDelete, onRequestError)
            }
        }
        $scope.load = function (obj) {
            clearErros();
            //$scope.TotalLCDebit = $scope.TotalLCCredit = $scope.TotalFCDebit = $scope.TotalFCCredit = 0;
            if (obj != null) {
                //var vDate = dateParser(obj.CSDate);
                //if (!angular.isUndefined(obj.InstrumentDate) && obj.InstrumentDate != null)
                //    var iDate = dateParser(obj.InstrumentDate);

                $scope.selectedObject = obj;
                $scope.actionList = $scope.selectedObject.CSDetail;
                $scope.isEditMode = true;
                $scope.addNewAction('', '', '', '1', '', '', '1', '', '', '', 'Regular', '1', 'None', 'None', '', $scope.selectedObject.CSDetail.length - 1, 'new');
            }
            $scope.saveError = '';
        }
        $scope.deleteActions = function (index, type) {
            $scope.saveError = '';
            if ($scope.actionList.length > 1) {
                if (type) {
                    $scope.actionList.splice(index, 1);
                    $scope.reSort();
                } else if (confirm("Are you sure to delete this record?")) {
                    $scope.actionList.splice(index, 1);
                    $scope.reSort();
                }
            }
        }
        var actionModel = function (gID, desc, subdesc, main,coagroup, dateas, invsign, action, formula, fontsize, style, underline, tborder, bborder, remarks) {
            this.GroupId = gID,
            this.Description = desc,
            this.SubDescription = subdesc,
            this.Main = main,
            this.NoteCode = coagroup,
            this.NoteDataAs = dateas,
            this.InverseSign = invsign,
            this.RowAction = action,
            this.RowFormula = formula,
            this.FontSize = fontsize,
            this.FontStyle = style,
            this.FontUnderline = underline,
            this.TopBorder = tborder,
            this.BottomBorder = bborder,
            this.Remarks = remarks
        }
        $scope.reSort = function () {
            var indx = 1;

            $scope.TotalFCCredit = $scope.TotalFCDebit = $scope.TotalLCCredit = $scope.TotalLCDebit = 0;
            for (dept in $scope.actionList) {

                $scope.actionList[dept].SerialNo = indx;
                $scope.TotalLCDebit += parseInt($scope.actionList[dept].LC_Debit);
                $scope.TotalLCCredit += parseInt($scope.actionList[dept].LC_Credit);

                $scope.TotalFCDebit += parseInt($scope.actionList[dept].FC_Debit);
                $scope.TotalFCCredit += parseInt($scope.actionList[dept].FC_Credit);

                indx++;
            }
            $scope.subTotal = $scope.TotalLCCredit - $scope.TotalLCDebit;
        }
        $scope.deleteActions = function (index, type) {
            $scope.saveError = '';
            if ($scope.actionList.length > 1) {
                if (type) {
                    $scope.actionList.splice(index, 1);
                    $scope.reSort();
                } else if (confirm("Are you sure to delete this record?")) {
                    $scope.actionList.splice(index, 1);
                    $scope.reSort();
                }
            }
        }
        
        $scope.clear = function () {
            //$scope.TotalLCCredit = $scope.TotalLCDebit = $scope.TotalFCCredit = $scope.TotalFCDebit = 0;
            $scope.isEditMode = false;
            var ccode = $scope.selectedObject.CompanyCode;
            $scope.selectedObject = { CompanyCode: ccode };
            
            $scope.actionList = [];
            $scope.addNewAction('', '', '', '1', '', '', '1', '', '', '', 'Regular', '1', 'None', 'None', '', 0, 'first');
        }
        $scope.actionList = [];
        $scope.addNewAction('', '', '', '1', '', '', '1', '', '', '', 'Regular', '1', 'None', 'None', '', 0, 'first');


        $scope.listData = [];
        
        $scope.listCompany = [];
        $scope.isEditMode = false;
        
        $scope.selectedObject = {};
        
        $scope.listCOAGroup = [];
        $scope.listCSRowAction = [];
        $scope.listCSObjectBorder = [];
        $scope.listCSDataAs = [];
        $scope.listCSFontStyle = [];
        
        var isFormLoaded = function () {
            if (GetCompany && GetCSRowAction && GetCSObjectBorder && GetCSDataAs && GetCSFontStyle) 
                hideAllShield();
            
        }
        var isCompanyDataLoaded = function () {
            if (GetCOAGroup && ListComplete)
                hideAllShield();
        }

        var GetCSRowAction = false; var GetCSObjectBorder = false; var GetCSDataAs = false; var GetCSFontStyle = false;
        var GetCompany = false; var GetCOAGroup = false;
        
        var ListComplete = false;
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        $http.get("/UI/getCSDataAs").then(onListCSDataAs, onRequestError);
        $http.get("/UI/getCSFontStyle").then(onListCSFontStyle, onRequestError);
        $http.get("/UI/getCSObjectBorder").then(onListCSObjectBorder, onRequestError);
        $http.get("/UI/getCSRowAction").then(onListCSRowAction, onRequestError);
        
    }
    app.controller("CSController", comController);
    addDirectives(app);
}
)();