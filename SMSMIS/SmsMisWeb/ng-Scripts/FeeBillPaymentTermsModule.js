(function () {
    var app = angular.module("clientModule1", []);
    var admController = function ($scope, $http, $filter) {
        var onGetCompany = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
            GetCompany = true;
            isFormLoaded();
        }
        var onListComplete = function (response) {
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listData = response.data;
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
                $scope.selectedObject = [];
                $scope.selectedObject.push(obj);
                if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                $scope.isEditMode = true;
            }
            $scope.saveError = '';
        }
        var onSaveComplete = function (response) {
            hideShield();
            $scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.apply();

        }
        var onDelete = function (response) {
            hideShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.clear();
            $scope.saveError = 'Record deleted successfully';
        }
        $scope.delete = function () {
           showShield();
            $scope.saveError = '';
            if (confirm('Are you sure you want to delete this record?'))
                $http.post("/UI/FeeBillPaymentTerms", $scope.selectedObject).then(onDelete, onRequestError)
        }

        $scope.clear = function(){
            $scope.selectedObject = [];
            $scope.isEditMode = false;
        }
        $scope.addGrid = function (paymentTerm, indx, mode) {

            if (typeof $scope.selectedObject == 'undefined')
                $scope.selectedObject = [];
            if (mode == 'edit' || (mode == 'new' && (indx == ($scope.selectedObject.length - 1)))) {
                $scope.selectedObject.push(new NewRow(paymentTerm));
                $scope.$apply();
            }
        }
        $scope.deleteRecord = function (session) {
            $scope.selectedObject.splice(session, 1);
        }
        var NewRow = function (paymentTerm) {
            //this.SrNo = srNo;
            this.CompanyCode = $scope.selectedObject[0].CompanyCode;
            this.BillType = $scope.selectedObject[0].BillType;
            //this.SrNo = indx ;
            this.PaymentTerm = paymentTerm;
            //this.delete = '';
        }
        var clearErros = function () {
            $scope.listError = $scope.saveError = '';
        }
        $scope.loadRecord = function () {
            clearErros();
            $scope.selectedObject[0].CompanyCode = typeof $scope.selectedObject[0].CompanyCode == 'undefined' ? '' : $scope.selectedObject[0].CompanyCode;
            $scope.selectedObject[0].BillType = typeof $scope.selectedObject[0].BillType == 'undefined' ? '' : $scope.selectedObject[0].BillType;
            var filteredArray = $filter('filter')($scope.listData, {
                CompanyCode: $scope.selectedObject[0].CompanyCode,
                BillType: $scope.selectedObject[0].BillType
            }, true);
            //$scope.selectedObject = [];
            setDefault($scope.selectedObject[0].CompanyCode, $scope.selectedObject[0].BillType);
            if (filteredArray.length > 0) {
                $scope.isEditMode = true;
                $scope.selectedObject = angular.copy(filteredArray);
                $scope.addGrid('', filteredArray.length, 'edit');
            }

            //$scope.addGrid(filteredArray[x].FeeParticularCode, '', filteredArray[x].AccountCode, '', filteredArray[x].Rate, 0, 'edit')
        }
        var setDefault = function (company, billtype) {
            $scope.selectedObject = [];
            var obj = new Object();
            obj.CompanyCode = company;
            obj.BillType = billtype;
            $scope.selectedObject.push(obj);
        }
        var isValid = function () {
            if (typeof $scope.selectedObject[0] == 'undefined' || $scope.selectedObject[0] == null) setDefault(0, 0);
            if (isnullorEmpty($scope.selectedObject[0].CompanyCode))
                $scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject[0].BillType))
                $scope.listError = "BillType is Required";
            else return true;
            return false;
        }
        var removeEmptyRow = function () {
            if ($scope.selectedObject[$scope.selectedObject.length - 1].PaymentTerm == '')
                $scope.deleteRecord($scope.selectedObject.length - 1);
        }
        $scope.save = function () {
            
            if (!isValid()) return;
            clearErros();
            showShield();
            removeEmptyRow();
            if (typeof $scope.selectedObject != 'undefined' && $scope.selectedObject.length > 0) {
                var count = 1;
                for (var x in $scope.selectedObject)
                {
                    $scope.selectedObject[x].SrNo = count;
                    count++;
                }
                $http.post("/UI/saveFeeBillPaymentTerms", { FeeBillPaymentTerms: $scope.selectedObject, CompanyCode: $scope.selectedObject[0].CompanyCode, BillType: $scope.selectedObject[0].BillType }).then(onSaveComplete, onRequestError);//saif
            }
        }
        $scope.deleteGridRow = function (obj, index) {
            $scope.saveError = '';
            if (obj.length > 1) {
                if (confirm("Are you sure to delete this record?")) {
                    obj.splice(index, 1);
                    //$scope.reSort(obj);
                }
            }
        }
        $scope.handleKeyEvent = function (e, indx, type) {
            if (e.keyCode == 13) {
                    $scope.addGrid('', indx, 'new');
            }
        }
        var isFormLoaded = function () {
            if (ListComplete && GetCompany)
                hideShield();

        }
        //$scope.reSort = function (obj) {
        //    var indx = 1;
        //    for (srno in obj) {
        //        obj[srno].SerialNo = indx;
        //        indx++;
        //    }
        //}
        $scope.isEditMode = false;
        $scope.clear();
        $scope.listData = [];
        $scope.selectedObject = [];
        $scope.saveError = '';
        //$scope.addGrid();
        var GetCompany = false;
        var ListComplete = false;
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        $http.get("/UI/getAllFeeBillPaymentTerms").then(onListComplete, onRequestError);
        
    }

    app.controller("FeeBillPaymentTermController", admController);
}
)();