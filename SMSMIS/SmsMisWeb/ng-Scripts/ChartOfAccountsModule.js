(function () {
    var app = angular.module("clientModule1", []);
    var comController = function ($scope, $http,$filter) {

        var onGetCompany = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listCompany = true;
            $scope.listCompany = response.data;
            //if ($scope.listCompany != null && $scope.listCompany.length > 0) {
            //    if (typeof $scope.selectedObject[0] == 'undefined' || $scope.selectedObject[0])
            //        setDefault(0, 0);
            //    $scope.selectedObject[0].CompanyCode = $scope.listCompany[0].CompanyCode;

            //}
            //$scope.getCOACompanyWise();
            hideAllShield();

            //isFormLoaded();
        }
        
        var onGetAllBranch = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listData = true;
            $scope.listBranch = response.data;
            for (var x in $scope.listBranch)
                $scope.listBranch[x].isSelected = true;
        }
        var onGetCOAAllBranch = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listData = true;
            $scope.listCOABranch = response.data;
            
        }
        var setDefault = function (company, billtype) {
            $scope.selectedObject = [];
            var obj = new Object();
            obj.CompanyCode = company;
            obj.BillType = billtype;
            $scope.selectedObject.push(obj);
        }
        var isContainChild = function () {
            var returnType = false;
            var filteredArray = $filter('filter')($scope.listData, { CompanyCode: $scope.selectedObject.CompanyCode, ParentAccountCode: $scope.selectedObject.AccountCode }, true);
            if (!angular.isUndefined(filteredArray) && filteredArray != null && filteredArray.length > 0) {
                for (var x in filteredArray) {
                    if (filteredArray[x].LevelId == 'D' && $scope.selectedObject.LevelId != 'D') {
                        $scope.listError = "Child Account Exist";
                        returnType = true;
                        break;
                    }
                    else if ((filteredArray[x].LevelId != 'D' && $scope.selectedObject.LevelId != 'D') && (parseInt(filteredArray[x].LevelId) > parseInt($scope.selectedObject.LevelId))) {
                        $scope.listError = "Child Account Exist";
                        returnType = true;
                        break;
                    }
                }
            }
            return returnType;
        }
        var isAlreadyExits = function () {
            var returnType = true;
            var exCode = $scope.selectedObject.AccountCode;
            if (!angular.isUndefined($scope.selectedObject.ParentAccountCode) && $scope.selectedObject.ParentAccountCode != '')
                exCode = $scope.selectedObject.ParentAccountCode +'-'+ $scope.selectedObject.AccountCode;
            var filteredArray = $filter('filter')($scope.listData, { CompanyCode: $scope.selectedObject.CompanyCode, AccountCode: exCode }, true);
            if (filteredArray != null && filteredArray.length > 0) {
            //for (var x in $scope.listData)
        //        if ($scope.listData[x].CompanyCode == $scope.selectedObject.CompanyCode
        //           && $scope.listData[x].BranchCode == $scope.selectedObject.BranchCode
        //&& $scope.listData[x].AccountCode == $scope.selectedObject.AccountCode
        //    && $scope.listData[x].InstrumentTypeCode == $scope.selectedObject.InstrumentTypeCode) {
                    $scope.listError = "Record Already Exist";
                    returnType = false;
                }
            return returnType;
        }
        
        var onGetCOALevel = function (response) {
            if (response.data == null || response.data == undefined)
                listCOALevel = true;
            $scope.listCOALevel = response.data;
            for (var x in $scope.listCOALevel)
                $scope.listCOALevel[x].LevelId = "" + $scope.listCOALevel[x].LevelId;
            setLevelLenght('1');
            GetCOALevel = true;
            //isFormLoaded();
        }
        var onListComplete = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listData = true;
            $scope.listData = response.data;
            setColorCodes();
            ListComplete = true;
            hideAllShield();
            //isFormLoaded();
        }
        var setColorCodes = function () {
            for (var x in $scope.listData)
                $scope.listData[x].LevelColor = "#" + parseInt($scope.listData[x].LevelColor).toString(16).toUpperCase();
        }
        
        var onRequestError = function (reason) {
            hideAllShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }
        $scope.padding = function () {
            if(!angular.isUndefined($scope.selectedObject.AccountCode) && $scope.selectedObject.AccountCode!= "")
                $scope.selectedObject.AccountCode = $scope.selectedObject.AccountCode.padLeft($scope.selectedObject.LevelLength, "0");
        }
        var onSaveComplete = function (response) {
            
            //$scope.saveError = 'Save Record successfully';
            $http.post("/UI/getAllCOABranchesCompanyWise", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetCOAAllBranch, onRequestError);
            hideAllShield();
            document.getElementById('hdnAC').value = '';
            document.getElementById('hdnCompany').value = '';
            if ($scope.selectedObject.AccountType == 'B' && $scope.selectedObject.TransactionType && $scope.isFirstTimeBank) {
                document.getElementById('hdnAC').value = $scope.selectedObject.AccountCode;
                document.getElementById('hdnCompany').value = $scope.selectedObject.CompanyCode;
                $scope.openClient();
            }
            //if ($scope.selectedObject.TransactionType || $scope.selectedObject.LevelId == 'D')
                //$scope.clear();
            //else 
            if (!angular.isUndefined($scope.isNew) && !$scope.isNew)
                $scope.clear();
            else
            {
                clearErros();
                
                //for (var x in $scope.listBranch)
                    //$scope.listBranch[x].isSelected = false;
                //$scope.selectedObject.AccountCodedummy
                $scope.isEditMode = false;
                $scope.isNew = true;
                $scope.selectedObject.AccountCode='';
                $scope.selectedObject.AccountTitle = '';
                if ($scope.selectedObject.LevelId == 'D' && $scope.selectedObject.TransactionType)
                    setLevelLenght("" + $scope.oldLevelId);
                else
                    setLevelLenght("" + $scope.selectedObject.LevelId);

            }
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            setColorCodes();
            //for (var x in $scope.listBranch) {
            //    if (!angular.isUndefined($scope.listBranch[x].isSelected) && $scope.listBranch[x].isSelected)
            //        var a = []
            //    a.BranchCode = $scope.listBranch[x].BranchCode;
            //    a.AccountCode = $scope.selectedObject.AccountCode;
            //    a.CompanyCode = $scope.selectedObject.CompanyCode;
            //    $scope.selectedObject.COABranch.push(a);
                
            //}
            $scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";
            //$scope.selectedObject.IssueDate = formatToJSONDate($scope.selectedObject.IssueDate);
            //if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            //$scope.apply();
            //hideAllShield();
            $scope.$apply();

        }

        var isValid = function () {
            if (isnullorEmpty($scope.selectedObject.CompanyCode))
                $scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject.AccountCode))
                $scope.listError = "Account Code is Required";
            else if ($scope.selectedObject.AccountCode.length != $scope.selectedObject.LevelLength && !dummy)
                $scope.listError = "Account Code Should be " + $scope.selectedObject.LevelLength + " char";
            else if (isnullorEmpty($scope.selectedObject.AccountTitle))
                $scope.listError = "Account Title is Required";
            
            else if (isnullorEmpty($scope.selectedObject.Status))
                $scope.listError = "Status is Required";
            else return true;
            return false;
        }
        
        
        var clearErros = function () {
            $scope.listError = $scope.saveError = '';
        }
        $scope.openClient = function () {
            document.getElementById('hdnAC').value = '';
            document.getElementById('hdnCompany').value = '';
            document.getElementById('hdnAC').value = $scope.selectedObject.AccountCode;
            document.getElementById('hdnCompany').value = $scope.selectedObject.CompanyCode;
            $('#ifrClientInfo').attr("src", "/UI/BankPopup");
            $('#ClientInfoPopup').fadeIn(300);
            //$('#CoverNoteRemarks').fadeOut(300);
            //$('#CoverNoteClauses').fadeOut(300);
            $('#PopupShield').fadeIn(300);
            
        }
        
        $scope.save = function () {
            clearErros();
            if (!isValid()) return;
            if ($scope.isNew && !isAlreadyExits()) return;
            if (!$scope.isNew && !angular.isUndefined($scope.selectedObject.TransactionType) && $scope.selectedObject.TransactionType && isContainChild()) return;
            $scope.saveError = '';
            showSaveShield();

            if (!angular.isUndefined($scope.selectedObject.TransactionType) && $scope.selectedObject.TransactionType) {
                $scope.selectedObject.LevelId = 'D';
            }
            else if($scope.selectedObject.LevelId == 'D' && $scope.oldLevelId)
            {
                var record = $scope.selectedObject.ParentAccountCode.split("-");// effect on level, when create continues then level D and then again untick the transaction type then problem occur.
                $scope.selectedObject.LevelId = "" + (record.length + 1);
            }
            else if ($scope.selectedObject.LevelId == 'D') {
                
                //var record = $scope.selectedObject.AccountCode.split("-");
                $scope.selectedObject.LevelId = "" + $scope.oldLevelId;
            }
            $scope.selectedObject.COABranch = [];
            for (var x in $scope.listBranch) 
                if (!angular.isUndefined($scope.listBranch[x].isSelected) && $scope.listBranch[x].isSelected){
                    var a = [];
                    a[0] = { 'CompanyCode': $scope.selectedObject.CompanyCode, 'BranchCode': $scope.listBranch[x].BranchCode, 'AccountCode': $scope.selectedObject.AccountCode };
                    $scope.selectedObject.COABranch.push(a[0]);
            }

            //$scope.selectedObjectCopy = angular.copy($scope.selectedObject);
            //$scope.selectedObject.IssueDate = dateParser($scope.selectedObject.IssueDate); 
            var record = '';
            if ($scope.selectedObject.AccountCode.indexOf("-") > -1) {
                record = $scope.selectedObject.AccountCode.split("-");
                $scope.selectedObject.AccountCode = record[record.length - 1];
            }
            if ($scope.selectedObject.ParentAccountCode != null && $scope.selectedObject.ParentAccountCode != '')
                $scope.selectedObject.AccountCode = $scope.selectedObject.ParentAccountCode + "-" + $scope.selectedObject.AccountCode;
            $scope.selectedObject.Status = $scope.selectedObject.Status == "1" ? true : false;
            //if (angular.isUndefined($scope.selectedObject.AccountType) || $scope.selectedObject.AccountType == null)
                //$scope.selectedObject.AccountType = '';
            $http.post("/UI/saveChartOfAccounts", { ChartOfAccounts: $scope.selectedObject, isNew: $scope.isNew }).then(onSaveComplete, onRequestError);//saif

        }
        var onDelete = function (response) {
            hideAllShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            setColorCodes();
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.clear();
            //$scope.saveError = 'Record deleted successfully';
        }
        $scope.deleteRow = function (obj) {
            clearErros();
            $scope.saveError = '';
            for (var x in $scope.listData)
                if ($scope.listData[x].AccountCode.indexOf(obj.AccountCode) > -1 && $scope.listData[x].LevelId != obj.LevelId && $scope.listData[x].ParentAccountCode == obj.AccountCode) {
                   $scope.listError = "Child Account Exist, unable to delete";
                    return;
                }
            
            if (confirm('Are you sure you want to delete this record?')) {
                showDeleteShield();
                $http.post("/UI/deleteChartOfAccounts", obj).then(onDelete, onRequestError)
            }
        }
        $scope.loadData = function (obj, type) {
            $scope.selectedRow = obj.AccountCode;
            clearErros();
            $scope.isFirstTimeBank = true;
            //if (!angular.isUndefined(type) && type == true) {

            //}
            if (!angular.isUndefined(obj.AccountType) && obj.AccountType != null && !angular.isUndefined(obj.LevelId))
                if (obj.LevelId == 'D' && obj.AccountType == 'B')
                    $scope.isFirstTimeBank = false;

            $scope.LevelD = false;
            //if (!angular.isUndefined(obj.AccountType) && obj.AccountType != null)
                //$scope.oldAccountType = obj.AccountType;
            //alert($scope.oldAccountType + " =>>> " + obj.AccountType);
            if (obj != null) {
                for (var x in $scope.listBranch)
                    $scope.listBranch[x].isSelected = false;
                //if (!angular.isUndefined(type) && type == true)
                //    for (var x in $scope.listBranch)
                //        $scope.listBranch[x].isSelected = false;

                $scope.selectedObject = {};
                $scope.selectedObject = angular.copy(obj);
                if (!angular.isUndefined($scope.selectedObject.LevelId) && $scope.selectedObject.LevelId != 'D' && (angular.isUndefined(type) || !type ))
                    for (var x in $scope.listBranch)
                        $scope.listBranch[x].isSelected = true;

                if (angular.isUndefined($scope.selectedObject.LevelId) || $scope.selectedObject.LevelId != 'D')
                    $scope.chkType(false);
                else
                    $scope.chkType(true);

                if (!angular.isUndefined(obj.AccountType) && obj.AccountType != null)
                    $scope.oldAccountType = $scope.selectedObject.AccountType = obj.AccountType//$scope.listAccType[0].code;
                else
                    $scope.oldAccountType = $scope.selectedObject.AccountType = $scope.listAccType[0].code;
                
                //$scope.selectedObject.ShortName = '';
                //$scope.selectedObjectCopy = angular.copy(obj);
                $scope.isEditMode = true;
                //$scope.FixedLevel = false;
                $scope.selectedObject.TransactionType = $scope.selectedObject.LevelId == "D";
                
                if ($scope.selectedObject.LevelId != "D" && (angular.isUndefined(type) || type == false)) {
                    $scope.selectedObject.AccountCodedummy = obj.AccountCode;
                    $scope.selectedObject.AccountTitledummy = obj.AccountTitle;
                    $scope.selectedObject.AccountCode = '';
                    $scope.selectedObject.AccountTitle = '';
                    
                    $scope.isNew = true;
                    var filteredDLevel = $filter('filter')($scope.listCOALevel, { LevelId: "D" }, true);
                    var levelID = 4;
                    if (filteredDLevel != null && filteredDLevel.length > 0)
                        levelID = filteredDLevel[0].LevelLength;
                    if ($scope.selectedObject.LevelId != "D") {

                        var filteredArray = $filter('filter')($scope.listCOALevel, { LevelId: "" + (parseInt($scope.selectedObject.LevelId) + 1) }, true);
                        if (filteredArray != null && filteredArray.length > 0) {
                            $scope.selectedObject.LevelId = filteredArray[0].LevelId;
                            $scope.selectedObject.LevelLength = filteredArray[0].LevelLength;
                        }
                        else {
                            $scope.LevelD = true; $scope.selectedObject.TransactionType = true; $scope.selectedObject.LevelId = "D"; $scope.selectedObject.LevelLength = levelID
                        }

                        $scope.selectedObject.ParentAccountCode = $scope.selectedObject.AccountCodedummy;

                    }
                } else {
                    var record = '';
                    
                    var filteredDLevel = $filter('filter')($scope.listCOALevel, { LevelId: "" + ($scope.selectedObject.LevelId) }, true);
                    if ($scope.selectedObject.LevelId != "1") {
                        record = $scope.selectedObject.AccountCode.split("-");
                        if (record.length == $scope.listCOALevel.length)
                        { $scope.LevelD = true; }
                        dummy = true;
                        //$scope.DummyAC = $scope.selectedObject.AccountCode;
                        //$scope.selectedObject.AccountCode = record[record.length - 1];
                        
                    }
                    $scope.selectedObject.LevelLength = filteredDLevel[0].LevelLength;
                    //$scope.isEditMode = true;

                    for (var x in $scope.listBranch) {
                        var filteredBranch = $filter('filter')($scope.listCOABranch, { BranchCode: $scope.listBranch[x].BranchCode,AccountCode: $scope.selectedObject.AccountCode }, true);
                        if (filteredBranch != null && filteredBranch.length > 0)
                            $scope.listBranch[x].isSelected = true;
                         
                    }
                    $scope.isNew = false;
                }
                $scope.oldLevelId = $scope.selectedObject.LevelId;
                $scope.oldLevelLength = $scope.selectedObject.LevelLength;
                $scope.selectedObject.AddDateTime = toISOFormat(dateParser($scope.selectedObject.AddDateTime));
                $scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";
                if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            }
            
            $scope.saveError = '';
        }
        $scope.chkType = function (obj) {
            if (obj) {
                $scope.listAccType = [{ type: 'Journal', code: 'J' }, { type: 'Cash', code: 'C' }, { type: 'Bank', code: 'B' }];
                if (angular.isUndefined($scope.selectedObject.AccountType) || $scope.selectedObject.AccountType == null || $scope.selectedObject.AccountType == '')
                $scope.selectedObject.AccountType = $scope.listAccType[0].code;
            }
            else {
                $scope.listAccType = [{ type: '', code: '' }, { type: 'Journal', code: 'J' }, { type: 'Cash', code: 'C' }, { type: 'Bank', code: 'B' }];
                if (angular.isUndefined($scope.selectedObject.AccountType) || $scope.selectedObject.AccountType == null || $scope.selectedObject.AccountType == '')
                $scope.selectedObject.AccountType = $scope.listAccType[0].code;
            }
        }
        $scope.getCOACompanyWise = function () {
            //if (!$scope.isEditMode) {
            showShield();
            //$scope.selectedObject = {};
            var company = $scope.selectedObject.CompanyCode;
            $scope.selectedObject = {
                'LevelLength': 0, 'LevelId': '1', 'CompanyCode': company, 'AccountCode': '', 'AccountTitle': ''
        , 'ShortName': '', 'Remarks': '', 'AccountType': '', 'ParentAccountCode': '', 'Status': true, 'AddByUserId': ''
            };
            $scope.listData = [];
                $http.post("/UI/getAllCOALevels", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetCOALevel, onRequestError);
                $http.post("/UI/getAllChartOfAccounts", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onListComplete, onRequestError);
                $http.post("/UI/getAllActiveBranchesCompanyWise", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllBranch, onRequestError);
                $http.post("/UI/getAllCOABranchesCompanyWise", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetCOAAllBranch, onRequestError);
                $scope.clear();
            //} else $scope.clear();
        }
        $scope.clear = function () {
            clearErros();
            //document.getElementById('hdnAC').value = '';
            //document.getElementById('hdnCompany').value = '';
            $scope.isFirstTimeBank = true;
            $scope.oldAccountType = '';
            for (var x in $scope.listBranch)
                $scope.listBranch[x].isSelected = true;
            //$scope.selectedObject.AccountCodedummy
            $scope.isEditMode = false;
            $scope.isNew = true;
            var companyId = $scope.selectedObject.CompanyCode;
            $scope.selectedObject = {
                'LevelLength': 0, 'LevelId': '1', 'CompanyCode': 0, 'AccountCode': '', 'AccountTitle': ''
        , 'ShortName': '', 'Remarks': '', 'AccountType': '', 'ParentAccountCode': '', 'Status': true, 'AddByUserId': ''
            };
            $scope.selectedObject.Status = "1";
            $scope.selectedObject.CompanyCode = companyId;
            //$scope.selectedObject.LevelLength = 1;
            setLevelLenght('1');
            $scope.selectedObject.LevelId = "1";
            $scope.LevelD = false;
            dummy = false;
            
            //$scope.listData = [];
            //$scope.addNewAction('', '', '', '', 0, 0, 0, 'first');
        }
        var setLevelLenght = function (lvlID) {
            if ($scope.listCOALevel != null && !angular.isUndefined($scope.listCOALevel) && $scope.listCOALevel.length > 0) {
                var filteredArray = $filter('filter')($scope.listCOALevel, { CompanyCode: $scope.selectedObject.CompanyCode, LevelId: "" + (lvlID) }, true);
                $scope.selectedObject.LevelLength = filteredArray[0].LevelLength;
                
            }
        }
        //$scope.listBranch = [];
        $scope.listData = [];
        $scope.listCompany = [];
        $scope.isEditMode = false;
        $scope.isNew = true;
        $scope.listCOALevel = [];
        $scope.isFirstTimeBank = true;
        $scope.selectedObject = {
            'LevelLength': 0, 'LevelId': '1', 'CompanyCode': 0, 'AccountCode': '', 'AccountTitle': ''
        , 'ShortName': '', 'Remarks': '', 'AccountType': '', 'ParentAccountCode': '', 'Status': true,'AddByUserId':''        
        };
        $scope.selectedObject.Status = "1";
        $scope.listBranch = [];
        
        //$scope.selectedObject.AccountType = $scope.listAccType[1].code;
        var dummy = false;
        //$scope.selectedObjectCopy = {};
        //$scope.listInstrumentType = [];
        //var isFormLoaded = function () {
        //    if (GetCompany && ListComplete && GetCOALevel) {
        //        hideAllShield();
        //    }
        //}
        var GetAllBranch = false;
        var GetCompany = false;
        var GetInstrumentType = false;
        var ListComplete = false;
        var GetCOA = false;
        
        
        
        //$http.get("/UI/getAllBranch").then(onGetAllBranch, onRequestError);
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        
        
    }
    app.controller("ChartOfAccountsController", comController);
    //addDirectives(app);
}
)();