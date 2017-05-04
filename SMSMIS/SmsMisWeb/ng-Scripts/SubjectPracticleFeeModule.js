(function () {
    var app = angular.module("clientModule1", []);
    var admController = function ($scope, $http, $filter) {

        var removeEmptyRow = function () {
            if ($scope.selectedObject[$scope.selectedObject.length - 1].ParticularCode == '' &&
               $scope.selectedObject[$scope.selectedObject.length - 1].Rate == '' &&
           $scope.selectedObject[$scope.selectedObject.length - 1].AccountCode == '')
                $scope.deleteRecord($scope.selectedObject.length - 1);
        }

        var onGetCompany = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
            if ($scope.listCompany != null && $scope.listCompany.length > 0) {
                if (typeof $scope.selectedObject[0] == 'undefined' || $scope.selectedObject[0])
                    setDefault(0, 0, 0, 0, 0);
                $scope.selectedObject[0].CompanyCode = $scope.listCompany[0].CompanyCode;

            }
            $scope.getCompanyWise();
            GetCompany = true;
            isFormLoaded();
        }
        $scope.getCompanyWise = function () {
            showShield();
            $http.post("/UI/getAllCompanyClassCourseSubjects", { CompanyCode: $scope.selectedObject[0].CompanyCode }).then(onGetClassCourseSubjects, onRequestError);
            $http.post("/UI/getAllBranchofCompany", { CompanyCode: $scope.selectedObject[0].CompanyCode }).then(onGetAllBranch, onRequestError);
            $http.post("/UI/getAllSubjectPracticleFee", { CompanyCode: $scope.selectedObject[0].CompanyCode }).then(onListComplete, onRequestError);
            $http.post("/UI/getAllActiveFeeParticularofCompany", { CompanyCode: $scope.selectedObject[0].CompanyCode }).then(onGetFeeParticular, onRequestError);
            $http.post("/UI/getAllSessionofCompany", { CompanyCode: $scope.selectedObject[0].CompanyCode }).then(onGetAllSession, onRequestError);
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
        //var onGetAllClass = function (response) {
        //    if (response.data == null || response.data == undefined)
        //        listCompany = true;
        //    $scope.listClass = response.data;
        //    GetAllClass = true;
        //    isFormLoaded();
        //}

        var onGetFeeParticular = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listFeeParticular = response.data;
            GetFeeParticular = true;
            isFormLoaded();
        }
        var onGetClassCourseSubjects = function (response) {
            if (response.data == null || response.data == undefined)
                listCOA = true;
            $scope.listClassCourseSubjects = response.data;
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
            $http.post("/UI/SubjectPracticleFee", $scope.selectedObject).then(onDelete, onRequestError)
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
            this.ParticularCode = ParticularCode;
            this.FeeParticularText = ParticularText;
            this.AccountCode = ACCode;
            this.AccountTitle = AcTitle;
            this.Rate = Rate;
        }
        $scope.validateGrid = function (indx, type) {

            var vType = $('#txtRate' + indx);
            var aCode = $('#cboStatus' + indx);
            var status = $('#cboSubject' + indx);

            var obj = null;
            if (vType.val() == '') {
                vType.addClass("invalidvalue");
                //setTimeout(function () { currency.focus(); }, 500);
                obj = vType;
            } else vType.removeClass("invalidvalue");
            if (aCode.find(":selected").index() == 0) {
                aCode.addClass("invalidvalue");
                //setTimeout(function () { glcde.focus(); }, 500);
                obj = aCode;
            } else aCode.removeClass("invalidvalue");

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
            if (typeof $scope.selectedObject[0] == 'undefined' || $scope.selectedObject[0] == null) setDefault(0, 0, 0, 0, 0);
            if (isnullorEmpty($scope.selectedObject[0].CompanyCode))
                $scope.listError = "Company is Required";
            else if (isnullorEmpty($scope.selectedObject[0].BranchCode))
                $scope.listError = "Branch is Required";
            else if (isnullorEmpty($scope.selectedObject[0].SessionCode))
                $scope.listError = "Session is Required";
            else if (isnullorEmpty($scope.selectedObject[0].ClassCode))
                $scope.listError = "Class is Required";
            else if (isnullorEmpty($scope.selectedObject[0].CourseCode))
                $scope.listError = "Course is Required";
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
            removeEmptyRow();
            if (!validateAllGrids()) return;

            showShield();
            if (typeof $scope.selectedObject != 'undefined' && $scope.selectedObject.length > 0) {
                var count = 1;
                for (var x in $scope.selectedObject) {
                    $scope.selectedObject[x].SrNo = count;
                    count++;
                }
                $http.post("/UI/saveSubjectPracticleFee", { SubjectPracticleFee: $scope.selectedObject, CompanyCode: $scope.selectedObject[0].CompanyCode, BranchCode: $scope.selectedObject[0].BranchCode, SessionCode: $scope.selectedObject[0].SessionCode, ClassCode: $scope.selectedObject[0].ClassCode, CourseCode: $scope.selectedObject[0].CourseCode }).then(onSaveComplete, onRequestError);//saif
            }
        }
        var isFormLoaded = function () {
            if (GetCompany && GetAllSession && GetAllBranch && ListComplete && GetFeeParticular)
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
        $scope.loadRecord = function () {
            //var count = 0;
            //delete $scope.selectedObject[x];
            //for (var x in $scope.selectedObject) {
            //    //if (count>0)
            //        delete $scope.selectedObject[x];
            //    //count++;
            //}
            
            clearErros();
            $scope.selectedObject[0].CompanyCode = angular.isUndefined($scope.selectedObject[0].CompanyCode) ? '' : $scope.selectedObject[0].CompanyCode;
            $scope.selectedObject[0].BranchCode = angular.isUndefined($scope.selectedObject[0].BranchCode ) ? '' : $scope.selectedObject[0].BranchCode;
            $scope.selectedObject[0].SessionCode = angular.isUndefined($scope.selectedObject[0].SessionCode ) ? '' : $scope.selectedObject[0].SessionCode;
            $scope.selectedObject[0].ClassCode = angular.isUndefined($scope.selectedObject[0].ClassCode ) ? '' : $scope.selectedObject[0].ClassCode;
            $scope.selectedObject[0].CourseCode = angular.isUndefined($scope.selectedObject[0].CourseCode ) ? '' : $scope.selectedObject[0].CourseCode;
            var filteredArray = $filter('filter')($scope.listData, {
                CompanyCode: $scope.selectedObject[0].CompanyCode,
                BranchCode: $scope.selectedObject[0].BranchCode,
                SessionCode: $scope.selectedObject[0].SessionCode,
                ClassCode: $scope.selectedObject[0].ClassCode,
                CourseCode: $scope.selectedObject[0].CourseCode
            }, true);
            //$scope.selectedObject = [];
            setDefault($scope.selectedObject[0].CompanyCode, $scope.selectedObject[0].BranchCode, $scope.selectedObject[0].SessionCode, $scope.selectedObject[0].ClassCode, $scope.selectedObject[0].CourseCode);
            if (filteredArray.length > 0) {
                $scope.isEditMode = true;
                $scope.selectedObject = angular.copy(filteredArray);
                var count = 0;
                //for (var x in filteredArray) {
                //$scope.selectedObject[count] = filteredArray[x];
                //count++;
                $scope.addGrid('', '', '', '', indx, 'edit');
            }

            ////$scope.addGrid(filteredArray[x].FeeParticularCode, '', filteredArray[x].AccountCode, '', filteredArray[x].Rate, 0, 'edit')
        }
        var setDefault = function (company, branch, session, classcode, CourseCode) {
            $scope.selectedObject = [];
            var obj = new Object();
            obj.CompanyCode = company;
            obj.BranchCode = branch;
            obj.SessionCode = session;
            obj.ClassCode = classcode;
            obj.CourseCode = CourseCode;
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
        $scope.listClassCourseSubjects = [];
        $scope.listClass = [];
        $scope.listSession = [];
        $scope.listBranch = [];
        $scope.listCompany = [];
        

        $scope.saveError = '';
        //$scope.addGrid();
        var GetCompany = false;
        var GetAllBranch = false;
        var GetAllSession = false;
        var GetAllClass = false;
        var ListComplete = false;
        var GetFeeParticular = false;
        var GetClassCourseSubjects = false;
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        
        //$http.post("/UI/getAllCOAbyAccountType", {CompanyCode: $scope.selectedObject[0].CompanyCode, AccountType: "B", levelID: "D" }).then(onGetClassCourseSubjects, onRequestError);//saif
    }
    app.controller("SubjectPracticleFeeController", admController);
    app.filter('unique', function () {

        return function (arr, field) {
            var o = {}, i, l = arr.length, r = [];
            for (i = 0; i < l; i += 1) {
                o[arr[i][field]] = arr[i];
            }
            for (i in o) {
                r.push(o[i]);
            }
            return r;
        };
    });
}
)();