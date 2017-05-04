(function() {
        var app = angular.module("clientModule1", []);
        var comController = function($scope, $http, $filter, $window) {
            $scope.GetSelectedAccountTitle = function(data, x, indx) {
                data.AccountCode = x.AccountCode;
                data.AccountTitle = x.AccountTitle;
                $('.QuickSearchResults').hide();
            }
            var _InstrumentIndex = -1;
            $scope.GetSelectedInstrumentName = function(x) {
                $scope.selectedObject.InstrumentName = x.InstrumentName;
                $scope.selectedObject.InstrumentTypeCode = x.InstrumentTypeCode;
                _InstrumentIndex = x.InstrumentTypeRowNumber;
                $('.QuickSearchResults').hide();
            }

            $scope.SetSelectedInstrumentType = function(data) {
                data.InstrumentName = data.InstrumentName;
                if (data.InstrumentName == '') {
                    $('.insType').hide();
                    return;
                }
                $('.insType').show();
                var filteredArray = $filter('filter')($scope.InstrumentType, { InstrumentName: data.InstrumentName });
            }
            $scope.SetSelectedAccountTitle = function(data, indx) {
                data.AccountTitle = data.AccountCode;
                if (data.AccountTitle == '') {
                    $('.abc' + indx).hide();
                    return;
                }

                var filteredArray = $filter('filter')($scope.listCOA, { AccountTitle: data.AccountTitle });
                if (filteredArray.length == 1 &&
                    filteredArray[0].AccountTitle.toUpperCase() == data.AccountTitle.toUpperCase()) {
                    data.AccountCode = filteredArray[0].AccountCode;
                    data.AccountTitle = filteredArray[0].AccountTitle;
                    $('.abc' + indx).hide();
                } else {
                    $('.abc' + indx).show();
                }
            }
            
            $scope.IsUpdate = false;
            $scope.IsDelete = false;
            $scope.IsSave = true;
            var _selectedVoucherType = null;
            var _index = null;
            var _selectedObject = null;
            var transactionType = null;
            var category = null;
            $scope.isVDateDisabled = true;
            $scope.dsablAcctCdeAgnstJournalVT = false;
            $scope.dsablAcctTitAgnstJournalVT = false;
            $scope.dsablInstrmntAgnstJournalVT = false;
            $scope.dsablInstrmntNbrAgnstJournalVT = false;
            $scope.dsablInstrmntDteAgnstJournalVT = false;
            $scope.dsablCurrncyAgnstJournalVT = false;
           // $scope.IsLCDebitDisable = false; IsLocalCurrency //fc debit
            //$scope.IsLCCreditDisable = false; IsFCCreditDisable// fc credit
            //$scope.IsLocalCurrency = false;

            $scope.readOnlyAcctCode = false;
            $scope.readOnlyAcctTitle = false;
            $scope.readOnlyAcctCurrency = false;



            $scope.IsLocalCurrency = false;            
            $scope.IsFCCreditDisable = false;
            $scope.IsLCCreditDisable = false;
            $scope.IsLCDebitDisable = false;
            $scope.CurrencySymbol = null;
            $scope.ExchangeRate = null;

            var _VoucherTypeIndex = -1;
            //Farhat Ullah
            var onValidateInstrumentNumber = function (response) {
                if (response.data == false || typeof response.data == 'undefined')
                    $scope.listError = "Instrument is not valid";
            }
            //Farhat Ullah
            $scope.ValidateInstrumentNo = function (value) {

                $http.post("/UI/ValidateInstrumentNumber",
                    {
                        companyCode: $scope.selectedObject.CompanyCode,
                        branchCode: $scope.selectedObject.BranchCode,
                        instrumentRowNumber: _InstrumentIndex,
                        voucherTypeRowNumber: _VoucherTypeIndex,
                        instrumentNumber: value,
                    })
                    .then(onValidateInstrumentNumber, onRequestError);

            }
            $scope.SetSelectedVoucherType = function (data) {
                if (data.VoucherName == '') {
                    $('.tblVoucherTypeSearch').hide();
                    $('.tblVoucherTypeSearch').hide();
                    $scope.isVDateDisabled = true;
                    $scope.dsablAcctCdeAgnstJournalVT = false;
                    $scope.dsablAcctTitAgnstJournalVT = false;
                    $scope.dsablInstrmntAgnstJournalVT = false;
                    $scope.dsablInstrmntNbrAgnstJournalVT = false;
                    $scope.dsablInstrmntDteAgnstJournalVT = false;
                    $scope.dsablCurrncyAgnstJournalVT = false;
                    // $scope.IsLCDebitDisable = false; IsLocalCurrency //fc debit
                    //$scope.IsLCCreditDisable = false; IsFCCreditDisable// fc credit
                    //$scope.IsLocalCurrency = false;
                    $scope.readOnlyAcctCode = false;
                    $scope.readOnlyAcctTitle = false;
                    $scope.readOnlyAcctCurrency = false;
                    $scope.IsLocalCurrency = false;
                    $scope.IsFCCreditDisable = false;
                    $scope.IsLCCreditDisable = false;
                    $scope.IsLCDebitDisable = false;
                    return;
                }

                var filteredArray = $filter('filter')($scope.listVoucherType, { VoucherName: data.VoucherName });
                if (filteredArray.length == 1 &&
                    filteredArray[0].VoucherName.toUpperCase() == data.VoucherName.toUpperCase()) {
                    data.VoucherName = filteredArray[0].VoucherName;
                    data.VoucherCode = filteredArray[0].VoucherCode;
                    $('.tblVoucherTypeSearch').hide();
                    $('#tblVocuherTypes').removeClass('tblVoucherTypeSearchBody');
                } else {
                    $('.tblVoucherTypeSearch').show();
                }

            }
            $scope.GetSelectedVoucherType = function(selectedObject, selectedVoucherType, index, isFromDate) {
                if (!isFromDate) {
                    _VoucherTypeIndex = index;
                    _selectedVoucherType = selectedVoucherType;
                    _selectedObject = selectedObject;
                    if (index != undefined) {
                        _index = index;
                    }
                    selectedObject.VoucherCode = selectedVoucherType.VoucherCode;
                    selectedObject.VoucherName = selectedVoucherType.VoucherName;
                    $('.tblVoucherTypeSearch').hide();
                    $scope.isVDateDisabled = false;

                    if (selectedObject.VoucherDate != null) {
                        selectedObject.VoucherNo = '';
                        selectedObject.AccountCode = '';
                        selectedObject.AccountTitle = '';

                        if (!$scope.IsUpdate) {
                            $http.post("/UI/generateVoucherNumber",
                                {
                                    companyCode: selectedObject.CompanyCode,
                                    branchCode: selectedObject.BranchCode,
                                    voucherCode: $scope.listVoucherType[_index - 1].VoucherCode,
                                    VoucherDate: selectedObject.VoucherDate,
                                })
                                .then(onGetVoucherNumber, onRequestError);
                        }
                    }
                } else {
                    _selectedObject.VoucherCode = _selectedVoucherType.VoucherCode;
                    _selectedObject.VoucherName = _selectedVoucherType.VoucherName;
                    $('.tblVoucherTypeSearch').hide();

                    $http.post("/UI/generateVoucherNumber",
                        {
                            companyCode: _selectedObject.CompanyCode,
                            branchCode: _selectedObject.BranchCode,
                            voucherCode: $scope.listVoucherType[_index - 1].VoucherCode,
                            VoucherDate: _selectedObject.VoucherDate,
                        })
                        .then(onGetVoucherNumber, onRequestError);
                    _selectedVoucherType = null;
                    //_index = null;
                    _selectedObject = null;
                    $scope.isVDateDisabled = true;
                }
                //$scope.validateGrid(index, true);
                // As voucher type is changed, get a voucher number from server
                var categoryType = null;
                var foreignCurrecny=null;
                var localCurrecny = null;                
                //By Farhat
                if ($scope.listVoucherType != undefined || $scope.listVoucherType == null) {
                    //transactionTypes = $scope.listVoucherType[index].TransactionType;
                    //categoryType = $scope.listVoucherType[index].Category;
                    foreignCurrecny = $scope.listVoucherType[_index - 1].CurrencyCode;
                    localCurrecny = $scope.listVoucherType[_index - 1].LocalCurrencyCode;
                    transactionType = $scope.listVoucherType[_index - 1].TransactionType;
                    category = $scope.listVoucherType[_index - 1].Category;
                }

                if (transactionType.toUpperCase() === 'JOURNAL') {
                    $scope.dsablAcctCdeAgnstJournalVT = true;
                    $scope.dsablAcctTitAgnstJournalVT = true;
                    $scope.dsablInstrmntAgnstJournalVT = true;
                    $scope.dsablInstrmntNbrAgnstJournalVT = true;
                    $scope.dsablInstrmntDteAgnstJournalVT = true;
                    $scope.dsablCurrncyAgnstJournalVT = true;
                    $scope.IsLocalCurrency = true;
                    $scope.IsFCCreditDisable = true;
                } else if (transactionType.toUpperCase() === 'CASH' && (category.toUpperCase === 'PAYMENT' || category.toUpperCase ==='RECEIPT')) {
                    $scope.dsablInstrmntAgnstJournalVT = true;
                    $scope.dsablInstrmntNbrAgnstJournalVT = true;
                    $scope.dsablInstrmntDteAgnstJournalVT = true;
                    $scope.readOnlyAcctCode = true;
                    $scope.readOnlyAcctTitle = true;
                    $scope.readOnlyAcctCurrency = true;                   
                } else if (transactionType.toUpperCase() === 'BANK' && category.toUpperCase === 'RECEIPT') {
                    
                    $scope.readOnlyAcctCode = true;
                    $scope.readOnlyAcctTitle = true;
                    $scope.readOnlyAcctCurrency = true;                   
                    $scope.dsablCurrncyAgnstJournalVT = true;                    
                } else if (transactionType.toUpperCase() === 'BANK' && category.toUpperCase === 'PAYMENT') {
                    
                    $scope.readOnlyAcctCode = true;
                    $scope.readOnlyAcctTitle = true;
                    $scope.readOnlyAcctCurrency = true;                    
                } else if (transactionType.toUpperCase() === 'BANK' || category.toUpperCase === 'PAYMENT') {
                    
                    $scope.dsablAcctCdeAgnstJournalVT = true;
                    $scope.dsablAcctTitAgnstJournalVT = true;
                    
                }
                //else if (transactionTypes != 'Journal' && transactionTypes != 'Cash') {
                //    $scope.dsablAcctCdeAgnstJournalVT = false;
                //    $scope.dsablAcctTitAgnstJournalVT = false;
                //    $scope.dsablInstrmntAgnstJournalVT = false;
                //    $scope.dsablInstrmntNbrAgnstJournalVT = false;
                //    $scope.dsablInstrmntDteAgnstJournalVT = false;
                //    $scope.dsablCurrncyAgnstJournalVT = false;
                //    $scope.IsLocalCurrency = false;
                //    $scope.IsLCDebitDisable = false;
                //    $scope.IsLCCreditDisable = false;
                //}
                //End By Farhat
                if ((transactionType.toUpperCase() === 'BANK' || transactionType.toUpperCase() === 'CASH') &&
                    category.toUpperCase() === 'PAYMENT') {
                    $scope.IsFCCreditDisable = true;
                    $scope.IsLCCreditDisable = true;
                }

                if ((transactionType.toUpperCase() === 'BANK' || transactionType.toUpperCase() === 'CASH') &&
                    category.toUpperCase() === 'RECEIPT') {
                    $scope.IsLCDebitDisable = true;
                    //$scope.IsLocalCurrency = true;
                } 
                if (foreignCurrecny === localCurrecny) {
                    $scope.IsLocalCurrency = true;
                    $scope.IsFCCreditDisable = true;
                    $scope.CurrencySymbol = "";
                } else {
                    $scope.ExchangeRate = "1";
                    $scope.IsLocalCurrency = true;
                    $scope.CurrencySymbol = "(" + $scope.listVoucherType[_index - 1].CurrencySymbol + ")";
                }                
            }
                var onGetCompany = function(response) {
                    if (response.data == null || typeof response.data == 'undefined')
                        listCompany = true;
                    $scope.listCompany = response.data;
                    $scope.getCOACompanyWise();
                    GetCompany = true;
                    isFormLoaded();
                }
                var onListPreferences = function(response) {
                    if (response.data == null || typeof response.data == 'undefined')
                        listCompany = true;
                    $scope.listPreference = response.data;
                    //GetCompany = true;
                    //isFormLoaded();
                }
                var onGetCOA = function(response) {
                    if (response.data == null || response.data == undefined)
                        listCOA = true;
                    $scope.listCOA = response.data;
                    GetCOA = true;
                    //hideShield();
                    if ($scope.isEditMode)
                        loadbyobj();
                    //isFormLoaded();
                }
                var onGetVoucherType = function(response) {
                    if (response.data == null || response.data == undefined)
                        listCOA = true;
                    $scope.listVoucherType = response.data;
                    GetVoucherType = true;
                    angular.forEach($scope.listVoucherType,
                        function(value, index) {
                            if (value.VoucherCode == _selectedVocuherCode) {
                                $scope.selectedObject.VoucherName = value.VoucherName;
                            }
                        });

                    angular.forEach($scope.InstrumentType,
                        function(value, index) {
                            if (value.InstrumentTypeCode == _selectedInstrumentCode) {
                                $scope.selectedObject.InstrumentName = value.InstrumentName;
                            }

                        });

                    angular.forEach($scope.listParty,
                        function(value, index) {
                            if (value.PartyCode == _selectedPartyCode) {
                                $scope.selectedObject.PartyCode = value.PartyCode;
                            }

                        });
                    if (_IsBranchClicked) {
                        $scope.selectedObject.VoucherName = '';
                        $scope.selectedObject.InstrumentName = '';
                        $scope.selectedObject.PartyCode = '';
                    }
                    //if ($scope.selectedObject.PartyCode == _selectedPartyCode) {
                    //    
                    //}
                    //if ($scope.selectedObject.InstrumentCode == _selectedInstrumentCode) {
                    //    $scope.selectedObject.InstrumentName = _selectedInstrumentCode;
                    //}
                    //hideShield();
                }
                $scope.getCOACompanyWise = function() {

                    GetCOA = GetCompany = ListComplete = GetAllBranch = false;
                    if ($scope.selectedObject != null || $scope.selectedObject == undefined) //added by Farhat Ullah
                    {
                        if (!angular.isUndefined($scope.selectedObject.CompanyCode) ||
                            $scope.selectedObject.CompanyCode != null) {
                            showShield();
                            $http.post("/UI/getAllCOAbyAccountType",
                                { CompanyCode: $scope.selectedObject.CompanyCode, /*AccountType: "B",*/ levelID: "D" })
                                .then(onGetCOA, onRequestError);
                            $http.post("/UI/getAllBranchofCompany", { CompanyCode: $scope.selectedObject.CompanyCode })
                                .then(onGetAllBranch, onRequestError);
                            $http.post("/UI/getCompanyVoucherMaster",
                                { CompanyCode: $scope.selectedObject.CompanyCode })
                                .then(onListComplete, onRequestError);
                            $http.post("/UI/getAllPreferencesofCompany",
                                { CompanyCode: $scope.selectedObject.CompanyCode })
                                .then(onListPreferences, onRequestError);
                            $http.post("/UI/getAllEmployee", { CompanyCode: $scope.selectedObject.CompanyCode })
                                .then(onListEmployee, onRequestError);

                        }
                    }
                }
                // Generate Voucher number when voucher type changed
                var _IsBranchClicked = null;
                $scope.getVoucherNumber = function() {
                    // 1 - Get Frequency
                    $http.post("/UI/generateVoucherNumber",
                        {
                            companyCode: $scope.selectedObject.CompanyCode,
                            branchCode: $scope.selectedObject.BranchCode,
                            RowNumber: $scope.selectedObject.RowNumber,
                            VoucherDate: $scope.selectedObject.VoucherDate,
                        })
                        .then(onGetVoucherNumber, onRequestError);
                }
                var onGetVoucherNumber = function (response) {
                    if (response.data == null || response.data == undefined) {

                    } else {
                        $scope.selectedObject.VoucherNo = response.data.VoucherNumber;
                        if (response.data.AccountCode != null) {
                            $scope.selectedObject.AccountCode = response.data.AccountCode;
                            $scope.selectedObject.AccountTitle = response.data.AccountTitle;
                        } else {
                            $scope.dsablAcctCdeAgnstJournalVT = true;
                            $scope.dsablAcctTitAgnstJournalVT = true;
                        }
                    }
                }
                $scope.onBranchChanged = function(IsBranchClicked) {
                    // 1 - Get Frequency
                    _IsBranchClicked = IsBranchClicked;
                    $scope.selectedObject.VoucherNo = '';
                    $scope.selectedObject.AccountCode = '';
                    $http.post("/UI/getApprovedVoucherType",
                        {
                            CompanyCode: $scope.selectedObject.CompanyCode,
                            BranchCode: $scope.selectedObject.BranchCode
                        })
                        .then(onGetVoucherType, onRequestError);

                    $http.post("/UI/getallCostCenterBranch",
                        {
                            CompanyCode: $scope.selectedObject.CompanyCode,
                            BranchCode: $scope.selectedObject
                                .BranchCode
                        })
                        .then(onListCostCenter, onRequestError);
                    $http.post("/UI/getAllAnalysisTypeBranch",
                        {
                            CompanyCode: $scope.selectedObject.CompanyCode,
                            BranchCode: $scope.selectedObject
                                .BranchCode
                        })
                        .then(onListAnalysisType, onRequestError);
                    $http.post("/UI/getallProjectBranch",
                        {
                            CompanyCode: $scope.selectedObject.CompanyCode,
                            BranchCode: $scope.selectedObject
                                .BranchCode
                        })
                        .then(onListProject, onRequestError);
                    $http.post("/UI/getApprovedInstrumentType",
                        {
                            CompanyCode: $scope.selectedObject.CompanyCode,
                            BranchCode: $scope.selectedObject
                                .BranchCode
                        })
                        .then(onListInstrumentType, onRequestError);
                    //Add By Farhat ullah above line
                    $http.post("/UI/getAllParty",
                        {
                            companyCode: $scope.selectedObject.CompanyCode,
                            branchCode: $scope.selectedObject.BranchCode
                        })
                        .then(onGetPartyType, onRequestError);
                }
                var onGetPartyType = function(response) {
                    if (response.data == null || response.data == undefined) {

                    } else {
                        $scope.listParty = response.data;
                    }
                }
                var onListEmployee = function(response) {
                    if (response.data == null || typeof response.data == 'undefined')
                        listCompany = true;
                    $scope.listEmployee = response.data;
                    //GetCompany = true;
                    //isFormLoaded();
                }
                var onListCostCenter = function(response) {
                    if (response.data == null || typeof response.data == 'undefined')
                        listCompany = true;
                    $scope.listCostCenter = response.data;
                    //GetCompany = true;
                    //isFormLoaded();
                }
                var onListProject = function(response) {
                    if (response.data == null || typeof response.data == 'undefined')
                        listCompany = true;
                    $scope.listProjects = response.data;
                    //GetCompany = true;
                    //isFormLoaded();
                }
                var onListAnalysisType = function(response) {
                    if (response.data == null || typeof response.data == 'undefined')
                        listCompany = true;
                    $scope.listAnalysis = response.data;
                    //GetCompany = true;
                    //isFormLoaded();
                }
                var onGetAllBranch = function(response) {
                    if (response.data == null || typeof response.data == 'undefined')
                        listData = true;
                    $scope.listBranch = response.data;
                    GetAllBranch = true;
                    isFormLoaded();
                }
                var onListComplete = function(response) {
                    if (response.data == null || typeof response.data == 'undefined')
                        listData = true;
                    $scope.listData = response.data;
                    ListComplete = true;
                    isFormLoaded();
                }
                var onRequestError = function(reason) {
                    hideShield();
                    if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                        $scope.listError = reason.data.error;
                    else
                        $scope.listError = reason.status + ': ' + reason.statusText;
                }
                var onListInstrumentType = function(reason) {
                    hideShield();
                    $scope.InstrumentType = reason.data;
                    //if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                }
                var onListCurrency = function(reason) {
                    //hideShield();
                    $scope.listCurrency = reason.data;
                }
                var onSaveComplete = function(response) {
                    $scope.saveError = 'Save Record successfully';
                    if (response.data == "null") {
                        delete $scope.listData;
                        $scope.clear();
                        return;
                    }

                    $scope.listData = response.data;
                    //$scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";
                    if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
                    if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                    hideShield();
                }
                var onLoadComplete = function(response) {
                    //$scope.saveError = 'Save Record successfully';
                    if (response.data == "null") {
                        delete $scope.listData;
                        $scope.clear();
                        return;
                    }
                    $scope.selectedObject = response.data[0];
                    $scope.isEditMode = true;

                    $scope.getCOACompanyWise();
                }
                var loadbyobj = function() {
                    $scope.actionList = [];
                    //if (angular.isUndefined($scope.selectedObject.VoucherDetail) || $scope.selectedObject.VoucherDetail.length == 0)
                    for (var y in $scope.selectedObject.VoucherDetail) {
                        var obj = $scope.selectedObject.VoucherDetail[y];
                        var filteredArray = $filter('filter')($scope.listCOA, { AccountCode: obj.AccountCode });
                        $scope.addNewAction(obj.VoucherNo,
                            obj.AccountCode,
                            filteredArray[0].AccountTitle,
                            obj.Narration,
                            obj.LC_Debit,
                            obj.LC_Credit,
                            obj.FC_Debit,
                            obj.FC_Credit,
                            0,
                            'edit');
                    }
                    $scope.addNewAction('', '', '', '', 0, 0, 0, 0, 0, 'first');
                    $scope.selectedObject.VoucherDate = toISOFormat(dateParser($scope.selectedObject.VoucherDate));
                    if (!angular.isUndefined($scope.selectedObject.InstrumentDate))
                        $scope.selectedObject
                            .InstrumentDate = toISOFormat(dateParser($scope.selectedObject.InstrumentDate));

                    $scope.isEditMode = true;
                    //if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
                    if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                    hideShield();
                    //Temp Comment
                    //$scope.$apply();
                }

                var isValid = function() {

                    if (isnullorEmpty($scope.selectedObject.CompanyCode))
                        $scope.listError = "Company is Required";
                    else if (isnullorEmpty($scope.selectedObject.BranchCode))
                        $scope.listError = "Branch is Required";
                    else if (isnullorEmpty($scope.selectedObject.VoucherCode))
                        $scope.listError = "Voucher Type is Required";
                    else if (isnullorEmpty($scope.selectedObject.VoucherDate))
                        $scope.listError = "Voucher Date is Required";
                    else return true;
                    return false;

                }
                $scope.addNewAction = function(VID,
                    accNo,
                    accTitle,
                    Narration,
                    Debit,
                    Credit,
                    FDebit,
                    FCredit,
                    index,
                    mode) {
                    var a = $scope.actionList.length;
                    if (mode == 'edit' ||
                    (
                        //mode == 'new' && (index == ($scope.actionList.length - 1)) && $scope.actionList[index].AccountCode != '' && ($scope.actionList[index].LC_Debit > 0 || $scope.actionList[index].LC_Credit > 0)) ||
                        mode == 'new' &&
                            (index == ($scope.actionList.length - 1)) &&
                            $scope.actionList[index].AccountCode != '') ||
                    (mode == 'first' && index == 0)) {
                        $scope.actionList
                            .push(new actionModel(VID, accNo, accTitle, Narration, Debit, Credit, FDebit, FCredit));
                        $scope.reSort();
                        //Temp Comment
                        //$scope.$apply();
                    }
                }
                $scope.handleKeyEvent = function(e, indx) {
                    if (e.keyCode == 13) {
                        //if (validateDetailGrid(indx))
                        $scope.addNewAction('', '', '', '', 0, 0, 0, 0, indx, 'new');
                        //$scope.reSort();
                    }
                }
                var validateDetailGrid = function(indx) {
                    var glcde = $('#txtAccountCode' + indx);
                    var debit = $('#txtDebit' + indx);
                    var credit = $('#txtCredit' + indx);
                    var obj = null;

                    if ((debit.val() == '0' || debit.val() == '') && (credit.val() == '0' || credit.val() == '') ||
                    (parseInt(debit.val()) > 0 && parseInt(credit.val()) > 0)) {
                        debit.addClass("invalidvalue");
                        credit.addClass("invalidvalue");
                        obj = debit;
                        //setTimeout(function () { debit.focus(); }, 500);
                    } else {
                        debit.removeClass("invalidvalue");
                        credit.removeClass("invalidvalue");
                    }

                    if (glcde.val() == '') {
                        glcde.addClass("invalidvalue");
                        //setTimeout(function () { glcde.focus(); }, 500);
                        obj = glcde;
                    } else glcde.removeClass("invalidvalue");
                    if (obj != null && typeof obj != 'undefined') {
                        setTimeout(function() { obj.focus(); }, 500);
                        return false;
                    }
                    return true;
                }
                var clearErros = function() {
                    $scope.listError = $scope.saveError = '';
                }
                $scope.save = function() {

                    if ((transactionType === "Bank" || transactionType === "Cash") && $scope.actionList.length < 1) {
                        $scope.listError = 'At least 1 Transaction should exist in grid.';
                        return;
                    } else if (transactionType === "Journal" && $scope.actionList.length < 2) {
                        $scope.listError = 'At least 2 Transaction should exist in grid.';
                        return;
                    }

                    clearErros();
                    if (!isValid()) {
                        //return;
                    }
                    if ($scope.actionList.length == 0) {
                        $scope.listError = 'Debit and Credit is Required';
                        //return;
                    } else if ($scope.TotalLCCredit != $scope.TotalLCDebit) {
                        $scope.listError = 'Debit and Credit Should be Equal';
                    }
                    if (!$scope.ValidateDebitCreidtValues())
                        return;
                    if (isnullorEmpty($scope.actionList[$scope.actionList.length - 1].AccountCode) &&
                        isnullorEmpty($scope.actionList[$scope.actionList.length - 1].LC_Debit) &&
                        isnullorEmpty($scope.actionList[$scope.actionList.length - 1].LC_Credit) &&
                        isnullorEmpty($scope.actionList[$scope.actionList.length - 1].Narration))
                        $scope.deleteActions($scope.actionList.length - 1, true);
                    for (var x in $scope.actionList) {
                        var indx = $scope.actionList[x].SerialNo - 1;

                        if (!validateDetailGrid(indx)) {
                            //return
                        };
                    }
                    showShield();
                    $scope.selectedObjectCopy = angular.copy($scope.selectedObject);
                    $scope.selectedObjectCopy.VoucherDate = dateParser($scope.selectedObjectCopy.VoucherDate);
                    if (!angular.isUndefined($scope.selectedObjectCopy.InstrumentDate))
                        $scope.selectedObjectCopy.InstrumentDate = dateParser($scope.selectedObjectCopy.InstrumentDate);
                    $scope.selectedObjectCopy.VoucherDetail = angular.copy($scope.actionList);
                    $http.post("/UI/saveVoucherMaster", { VoucherMaster: $scope.selectedObjectCopy })
                        .then(onSaveComplete, onRequestError);
                }

                $scope.Update = function() {

                    if ((transactionType === "Bank" || transactionType === "Cash") && $scope.actionList.length < 1) {
                        $scope.listError = 'At least 1 Transaction should exist in grid.';
                        return;
                    } else if (transactionType === "Journal" && $scope.actionList.length < 2) {
                        $scope.listError = 'At least 2 Transaction should exist in grid.';
                        return;
                    }

                    //if (!$scope.isValidValues) {
                    //    alert('Debit and Credit values are not equal');
                    //    return;
                    //}
                    clearErros();
                    if (!isValid()) {
                        //return;
                    }
                    //else {
                    //    _IsBranchClicked = false;
                    //    clearErros();
                    //    if (!isValid()) {
                    //        return;
                    //    }
                    if ($scope.actionList.length == 0) {
                        $scope.listError = 'Debit and Credit is Required';
                        return;
                    } else if ($scope.TotalLCCredit != $scope.TotalLCDebit) {
                        $scope.listError = 'Debit and Credit Should be Equal';
                    }

                    if (!$scope.ValidateDebitCreidtValues())
                        return;
                    if (isnullorEmpty($scope.actionList[$scope.actionList.length - 1].AccountCode) &&
                        isnullorEmpty($scope.actionList[$scope.actionList.length - 1].LC_Debit) &&
                        isnullorEmpty($scope.actionList[$scope.actionList.length - 1].LC_Credit) &&
                        isnullorEmpty($scope.actionList[$scope.actionList.length - 1].Narration))
                        $scope.deleteActions($scope.actionList.length - 1, true);
                    for (var x in $scope.actionList) {
                        var indx = $scope.actionList[x].SerialNo - 1;

                        if (!validateDetailGrid(indx)) {
                            //return
                        };
                    }
                    showShield();
                    $scope.selectedObjectCopy = angular.copy($scope.selectedObject);

                    $scope.selectedObjectCopy.VoucherDate = dateParser($scope.selectedObjectCopy.VoucherDate);
                    if (!angular.isUndefined($scope.selectedObjectCopy.InstrumentDate))
                        $scope.selectedObjectCopy.InstrumentDate = dateParser($scope.selectedObjectCopy.InstrumentDate);

                    $scope.selectedObjectCopy.VoucherDetail = angular.copy($scope.actionList);
                    $http.post("/UI/saveVoucherMaster", { VoucherMaster: $scope.selectedObjectCopy })
                        .then(onSaveComplete, onRequestError);
                }

                var onDelete = function(response) {
                    if (response.data == "null") {
                        delete $scope.listData;
                        $scope.clear();
                        return;
                    }
                    $scope.listData = response.data;
                    if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
                    if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                    $scope.clear();
                    $scope.saveError = 'Record deleted successfully';
                }
                $scope.delete = function() {
                    clearErros();
                    //UnCommented the $scope.saveError after G.A rollback on 4/22/2017
                    $scope.saveError = '';
                    //End. G.A rollback on 4/22/2017
                    $scope.selectedObject.VoucherDate = dateParser($scope.selectedObject.VoucherDate);
                    if (!angular.isUndefined($scope.selectedObject.InstrumentDate))
                        $scope.selectedObject.InstrumentDate = dateParser($scope.selectedObject.InstrumentDate);
                    if (confirm('Are you sure you want to delete this record?'))
                        $http.post("/UI/deleteVoucherMaster", $scope.selectedObject).then(onDelete, onRequestError)
                }

                function dateconvert(value) {
                    //if (!IsEmpty(value))
                    if (Object.keys(value).length > 0) {
                        var dateObj = new Date(parseInt(value.replace(/(^.*\()|([+-].*$)/g, '')));
                        var date = dateObj.getDate();
                        if (date < 10) {
                            date = "0" + date;
                        }
                        var month = dateObj.getMonth() + 1;
                        if (month < 10) {
                            month = "0" + month;
                        }
                        var startDate = date + "/" + month + "/" + dateObj.getFullYear();
                        return startDate;
                    } else {
                        return null;
                    }
                }

                var _selectedVocuherCode = null;
                var _selectedInstrumentCode = null;
                var _selectedPartyCode = null;
                var _selectedCostCenter = null;
                var _selectedEmployee = null;
                var _selectedProject = null;
                var _selectedAnalysis = null;
                $scope._VoucherDate = null;

                $scope.load = function(obj) {
                    $scope.listError = '';
                    _IsBranchClicked = false;
                    clearErros();
                    $scope.TotalLCDebit = $scope.TotalLCCredit = $scope.TotalFCDebit = $scope.TotalFCCredit = 0;
                    if (obj != null) {

                        var vDate = null;
                        $scope._VoucherDate = obj.VoucherDate;
                        if ($scope._VoucherDate.indexOf("Date") != -1) {
                            vDate = dateconvert(obj.VoucherDate);

                            if (obj.InstrumentDate != null) {
                                if (dateconvert(obj.InstrumentDate) != 'NaN/NaN/NaN') {
                                    obj.InstrumentDate = dateconvert(obj.InstrumentDate);
                                }
                            } else {
                                obj.InstrumentDate = 'N/A';
                            }
                            obj.AddDateTime = dateconvert(obj.AddDateTime);
                        } else {
                            vDate = obj.VoucherDate.substr(8, 2) +
                                "/" +
                                obj.VoucherDate.substr(5, 2) +
                                "/" +
                                obj.VoucherDate.substr(0, 4);
                            if (obj.InstrumentDate != null) {
                                if (dateconvert(obj.InstrumentDate) != 'NaN/NaN/NaN') {
                                    obj.InstrumentDate = obj.InstrumentDate.substr(8, 2) +
                                        "/" +
                                        obj.InstrumentDate.substr(5, 2) +
                                        "/" +
                                        obj.InstrumentDate.substr(0, 4);
                                }
                            } else {
                                obj.InstrumentDate = 'N/A';
                            }
                            obj.AddDateTime = obj.AddDateTime; //Conversios
                        }

                        if (!angular.isUndefined(obj.InstrumentDate) && obj.InstrumentDate != null)
                            var iDate = dateParser(obj.InstrumentDate);
                        obj.VoucherDate = vDate;
                        //(dateconvert(obj.InstrumentDate) != 'NaN/NaN/NaN')                
                        $scope.selectedObject = obj;
                        $scope.isEditMode = false;
                        $scope.IsUpdate = true;
                        $scope.IsDelete = true;
                        $scope.IsSave = false;
                        $scope.getCOACompanyWise();
                        //$http.post("/UI/getVoucherByID", { companycode: obj.CompanyCode, branchcode: obj.BranchCode, voucherCode: obj.VoucherCode, voucherNo: obj.VoucherNo, voucherDate: vDate }).then(onLoadComplete, onRequestError)
                        //$scope.selectedObject = {};
                        //$scope.selectedObject = angular.copy(obj);
                        //$scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";
                        //Test-
                        _selectedVocuherCode = obj.VoucherCode;
                        _selectedInstrumentCode = obj.InstrumentTypeCode;
                        _selectedPartyCode = obj.PartyCode;
                        $scope.actionList = obj.VoucherDetail;
                        var countOfVoucherDetail = $scope.actionList.length;
                        var keepGoing = true;
                        for (var i = 0; i < countOfVoucherDetail; i++) {
                            keepGoing = true;
                            var accountCode = $scope.actionList[i].AccountCode;
                            //$http.post("/UI/getAccountTitle", { AccountCode: accountCode }).then(onGetVoucherType, onRequestError);
                            for (var j = 0; j < $scope.listCOA.length && keepGoing; j++) {
                                if (accountCode == $scope.listCOA[j].AccountCode) {
                                    $scope.actionList[i].AccountTitle = $scope.listCOA[j].AccountTitle;
                                    keepGoing = false;
                                }
                            }
                        }

                        $http.post("/UI/getApprovedVoucherType",
                            {
                                CompanyCode: $scope.selectedObject.CompanyCode,
                                BranchCode: $scope.selectedObject
                                    .BranchCode
                            })
                            .then(onGetVoucherType, onRequestError);
                        $http.post("/UI/getAllParty",
                            {
                                CompanyCode: $scope.selectedObject.CompanyCode,
                                BranchCode: $scope.selectedObject
                                    .BranchCode
                            })
                            .then(onGetPartyType, onRequestError);
                        $http.post("/UI/getallCostCenterBranch",
                            {
                                CompanyCode: $scope.selectedObject.CompanyCode,
                                BranchCode: $scope.selectedObject
                                    .BranchCode
                            })
                            .then(onListCostCenter, onRequestError);
                        $http.post("/UI/getAllAnalysisTypeBranch",
                            {
                                CompanyCode: $scope.selectedObject.CompanyCode,
                                BranchCode: $scope.selectedObject
                                    .BranchCode
                            })
                            .then(onListAnalysisType, onRequestError);
                        $http.post("/UI/getallProjectBranch",
                            {
                                CompanyCode: $scope.selectedObject.CompanyCode,
                                BranchCode: $scope.selectedObject
                                    .BranchCode
                            })
                            .then(onListProject, onRequestError);

                        //$http.post("/UI/getAllParty",
                        //    {
                        //        companyCode: $scope.selectedObject.CompanyCode,
                        //        branchCode: $scope.selectedObject.BranchCode
                        //    }).then(onGetPartyType, onRequestError);
                    }

                }
                var actionModel = function(VID, accNo, accTitle, Narration, LCDebit, LCCredit, FCDebit, FCCredit) {

                    //if (VID == '') VID = $scope.selectedObject.VoucherID;
                    //this.VDetailID= VDetailID;
                    this.voucherNo = VID;
                    //this.SerialNo = $scope.actionList.length + 1;
                    //this.GLCode = glCode;
                    this.AccountCode = accNo;
                    //this.InstrumentNo = InsNo;
                    //if (InsDate != null && InsDate.substring(0, 6) == "/Date(") {
                    //    InsDate = InsDate.replace("/Date(", "").replace(")/", "");
                    //    InsDate = $filter('date')(InsDate, "dd/MM/yyyy");
                    //}
                    //this.InstrumentDate = InsDate;
                    this.Narration = Narration;
                    this.AccountTitle = accTitle;
                    //this.CurrencyID = Curr;
                    this.LC_Debit = LCDebit;
                    this.LC_Credit = LCCredit;

                    this.FC_Debit = FCDebit;
                    this.FC_Credit = FCCredit;
                    //this.delete = '';

                }
                $scope.reSort = function() {
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
                $scope.deleteActions = function(index, type) {
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
                $scope.currencyChanged = function(obj) {
                    if (obj != 'PKR') {
                        document.getElementById('tdLCDebit').innerHTML = "Debit " + obj;
                        document.getElementById('tdLCCredit').innerHTML = "Credit " + obj;
                    }
                }
                $scope.clear = function() {
                    $scope.listError = '';
                    $scope.TotalLCCredit = $scope.TotalLCDebit = $scope.TotalFCCredit = $scope.TotalFCDebit = 0;
                    $scope.isEditMode = false;
                    $scope.selectedObject = {};
                    $scope.actionList = [];
                    $scope.addNewAction('', '', '', '', 0, 0, 0, 0, 0, 'first');
                    $scope.IsUpdate = false;
                    $scope.IsDelete = false;
                    $scope.IsSave = true;
                    //$scope.saveError = null;

                }
                $scope.clearOnAdd = function () {
                    $scope.listError = '';
                    $scope.TotalLCCredit = $scope.TotalLCDebit = $scope.TotalFCCredit = $scope.TotalFCDebit = 0;
                    $scope.isEditMode = false;
                    $scope.selectedObject = {};
                    $scope.actionList = [];
                    $scope.addNewAction('', '', '', '', 0, 0, 0, 0, 0, 'first');
                    $scope.IsUpdate = false;
                    $scope.IsDelete = false;
                    $scope.saveError = '';
                    $scope.IsSave = true;
                    //$scope.saveError = null;

                }
                $scope.actionList = [];
                $scope.addNewAction('', '', '', '', 0, 0, 0, 0, 0, 'first');
                $scope.listData = [];
                $scope.listBranch = [];
                $scope.listCompany = [];
                $scope.isEditMode = false;
                $scope.listCOA = [];
                $scope.selectedObject = {};
                $scope.listVoucherType = [];
                $scope.TotalLCDebit = 0;
                $scope.TotalLCCredit = 0;

                $scope.TotalFCDebit = 0;
                $scope.TotalFCCredit = 0;
                $scope.InstrumentType = [];
                $scope.listCurrency = [];
                $scope.subTotal = 0;
                var isFormLoaded = function() {
                    if (ListComplete && GetAllBranch && GetCOA) {
                        hideShield();
                    }
                }
                var GetAllBranch = false;
                var GetCompany = false;
                var GetCOA = false;
                var GetVoucherType = false;
                var ListComplete = false;
                $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
                $http.get("/UI/getAllInstrumentType").then(onListInstrumentType, onRequestError);                
                $http.get("/UI/getAllCurrency").then(onListCurrency, onRequestError);

                $scope.ValidateDebitCreidtValues = function() {
                    var isValidValue = true;
                    for (var x in $scope.actionList) {
                        var indx = $scope.actionList[x].SerialNo - 1;
                        if ($('#txtDebit' + indx).val() != $('#txtCredit' + indx).val()) {
                            $('#txtDebit' + indx).addClass("invalidvalue");
                            $('#txtCredit' + indx).addClass("invalidvalue");
                            $scope.listError = 'Debit and Credit Should be Equal';
                            isValidValue = false;
                        } else {
                            $('#txtDebit' + indx).removeClass("invalidvalue");
                            $('#txtCredit' + indx).removeClass("invalidvalue");
                        }
                        if ($('#txtFDebit' + indx).val() != $('#txtFCredit' + indx).val()) {
                            $('#txtFDebit' + indx).addClass("invalidvalue");
                            $('#txtFCredit' + indx).addClass("invalidvalue");
                            $scope.listError = 'Debit and Credit Should be Equal';
                            isValidValue = false;
                        } else {
                            $('#txtFDebit' + indx).removeClass("invalidvalue");
                            $('#txtFCredit' + indx).removeClass("invalidvalue");
                        }
                    }
                    return isValidValue;
                };

                $scope.VoucherTypeClick = function(data) {
                    $('.tblVoucherTypeSearch').show();
                }

                $scope.sort = function(predicate) {
                    $scope.predicate = predicate;
                }

                $scope.isSorted = function(predicate) {
                    return ($scope.predicate == predicate)
                }

                $scope.SettingValueForLCDebit = function(indexOf) {

                    if ((transactionType === "Bank" || transactionType === "Cash") && category === "Receipt") {
                        $scope.actionList[indexOf].LC_Debit = $scope.actionList[indexOf].LC_Credit;
                        //$scope.TotalLCDebit += parseInt($scope.actionList[indexOf].LC_Debit);
                    }
                }
        }
        app.controller("VoucherController", comController);
        addDirectives(app);
    }
)();