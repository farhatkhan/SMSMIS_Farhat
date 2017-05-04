(function () {
    var app = angular.module("clientModule1", []);
    var admController = function ($scope, $http) {
        var onGetCompany = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listCompany = true;
            $scope.listCompany = response.data;
            GetCompany = true;
            isFormLoaded();
        }
        var onListComplete = function (response) {
            if (response.data == null || typeof response.data == 'undefined')
                listData = true;
            $scope.listData = response.data;
        }
        var onGetAllData = function (response) {
            if (response.data == null || typeof response.data == 'undefined') listData = true;
            $scope.listData = response.data;
            GetAllData = true;
            isFormLoaded();
        }
        var onRequestError = function (reason) {
            hideShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }
        var onSaveComplete = function (response) {
            $scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            hideShield();
            $scope.$apply();
            

        }
        var clearErros = function () {
            $scope.listError = $scope.saveError = '';
        }
        $scope.save = function () {
            if (!isValid()) return;
            showShield();
            clearErros();
            if ($scope.selectedObject != null && typeof $scope.selectedObject != 'undefined' && $scope.selectedObject.CompanyCode > 0) {
                $scope.selectedObjectCopy = angular.copy($scope.selectedObject);
                $scope.selectedObjectCopy.Status = $scope.selectedObjectCopy.Status == "1" ? true : false;
                if ($scope.selectedObjectCopy.StartDate != null && $scope.selectedObjectCopy.StartDate != "")
                    $scope.selectedObjectCopy.StartDate = dateParser($scope.selectedObjectCopy.StartDate);
                if ($scope.selectedObjectCopy.EndDate != null && $scope.selectedObjectCopy.EndDate != "")
                    $scope.selectedObjectCopy.EndDate = dateParser($scope.selectedObjectCopy.EndDate);
                $http.post("/UI/saveFeeTerm", { FeeTerm: $scope.selectedObjectCopy}).then(onSaveComplete, onRequestError);//saif
            }
        }
        var monthDiff = function (d1, d2) {
            var months;
            months = (d2.getFullYear() - d1.getFullYear()) * 12;
            months -= d1.getMonth() + 1;
            months += d2.getMonth() + 1;
            return months <= 0 ? 0 : months;
        }
        var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        $scope.drawTable = function () {
            $scope.monthList = [];
            if ($scope.selectedObject.StartDate != null && $scope.selectedObject.StartDate != "")
                startMonth = dateParser($scope.selectedObject.StartDate);
            if ($scope.selectedObject.EndDate != null && $scope.selectedObject.EndDate != "")
                endMonth = dateParser($scope.selectedObject.EndDate);
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
        var onDelete = function (response) {
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.clear();
            hideShield();
            $scope.saveError = 'Record deleted successfully';
        }
        $scope.delete = function () {
            clearErros();
            showShield();
            if (confirm('Are you sure you want to delete this record?'))
                $http.post("/UI/deleteFeeTerm", $scope.selectedObject).then(onDelete, onRequestError)
        }
        $scope.load = function (obj) {
            clearErros();
            if (obj != null) {
                $scope.selectedObject = {};
                $scope.selectedObject = angular.copy(obj);
                $scope.selectedObject.StartDate = toISOFormat(dateParser($scope.selectedObject.StartDate)); //formatToJSONDate($scope,$scope.selectedObject.StartDate);
                $scope.selectedObject.EndDate = toISOFormat(dateParser($scope.selectedObject.EndDate));//formatToJSONDate($scope.selectedObject.EndDate);
                $scope.selectedObject.Status = $scope.selectedObject.Status == true ? "1" : "0";
                if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                $scope.isEditMode = true;
                $scope.drawTable();
            }
            $scope.saveError = '';
        }
        var isFormLoaded = function () {
            if (GetAllData && GetCompany) {
                hideShield();
                }
            }
        var isValid = function () {
            if (isnullorEmpty($scope.selectedObject.CompanyCode))
                $scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject.FeeTermName))
                $scope.listError = "Name is Required";
            else if (isnullorEmpty($scope.selectedObject.ShortName))
                $scope.listError = "Short Name is Required";
            else if (isnullorEmpty($scope.selectedObject.StartDate))
                $scope.listError = "From Date is Required";
            else if (isnullorEmpty($scope.selectedObject.EndDate))
                $scope.listError = "To Date is Required";
            else if (dateParser($scope.selectedObject.EndDate) < dateParser($scope.selectedObject.StartDate))
                $scope.listError = "To Date should be Greater";
            else if (isnullorEmpty($scope.selectedObject.Status))
                $scope.listError = "Status is Required";

            else return true;
            return false;
        }
       
        $scope.clear = function () {
            clearErros();
            $scope.isEditMode = false;
            $scope.selectedObject = {};
        }
        $scope.listData = [];
        $scope.listCompany = [];
        $scope.isEditMode = false;
        $scope.selectedObject = {};
        var GetAllData = false;
        var GetCompany = false;

        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        $http.get("/UI/getAllFeeTerm").then(onGetAllData, onRequestError);

    }
    
    app.controller("FeeTermController", admController);
    addDirectives(app);
    
}
)();