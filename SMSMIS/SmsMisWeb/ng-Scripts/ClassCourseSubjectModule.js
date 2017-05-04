(function () {
    var app = angular.module("adminModule1", []);
    var classcoursesubjectController = function ($scope, $http, $filter) {


        $scope.Session_Change = function (o) {
            hideShield();
            var opList = $filter('filter')($scope.ClassList, {
                ClassCode: o, CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObject.BranchCode,
            });

            if (opList != undefined && opList.length > 1) {
                alert('Same class can not be defined multiple times,please rectify your change');
                opList[1].ClassCode = null;
                return;
            }

            $scope.addSubjectCourse();
        }

        $scope.emptyOrNull = function (item) {
            return !(item.Message === null || item.Message.trim().length === 0)
        }

        $scope.Course_Change = function (o) {
            hideShield();
            $scope.selectedObject.empty = "";

            if (o.SubjectCode == null || o.SubjectCode == "") return;

            var opList = $filter('filter')($scope.listAllClassCourseSubject, { CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObject.BranchCode, ClassCode: $scope.selectedObject.ClassCode, CourseCode: $scope.selectedObject.CourseCode, SubjectCode: o.SubjectCode },true);

            if (opList != undefined && opList.length > 1) {
                alert('Same subject can not be defined multiple times,please rectify your change');
                opList[1].SubjectCode = null;
                return;
            }

            var opListEx = $filter('filter')($scope.listAllClassCourseSubject, { CourseCode: '', SubjectCode: '' }, true);
            if (typeof opListEx != 'undefined' && opListEx.length > 0) return;

            $scope.addSubjectCourse();
        }

        $scope.addClass = function () {

            if ($scope.ClassList == undefined)
                $scope.ClassList = [];

            $scope.ClassList.push(new NewClass());
        }

        var NewClass = function () {
            this.CompanyCode = $scope.selectedObject.CompanyCode;
            this.BranchCode = (($scope.selectedObject.BranchCode == undefined) ? '' : $scope.selectedObject.BranchCode);
            this.ClassCode = '';
        }

        $scope.addSubjectCourse = function () {

            if ($scope.listAllClassCourseSubject == undefined)
                $scope.listAllClassCourseSubject = [];

            $scope.listAllClassCourseSubject.push(new NewSubjectCourse());
        }

        var NewSubjectCourse = function () {
            this.CompanyCode = $scope.selectedObject.CompanyCode;
            this.BranchCode = (($scope.selectedObject.BranchCode == undefined) ? '' : $scope.selectedObject.BranchCode);
            this.ClassCode = $scope.selectedObject.ClassCode;
            this.CourseCode = $scope.selectedObject.CourseCode;
            this.SubjectCode = '';
            this.Mandatory = false;
        }



        $scope.loadSessions = function () {
            showShield();
            $scope.listAllClassCourseSubject.length = 0;
            if ($scope.selectedObject.CompanyCode != undefined && $scope.selectedObject.BranchCode != undefined
                 && $scope.selectedObject.ClassCode != undefined)
                $http.get("/UI/getAllClassCourseSubject").then(onGetAllClassCourseSubject, onRequestError);
            hideShield();
        }

        var onGetAllClassCourse = function (response) {
            delete $scope.ClassList;
            if ($scope.selectedObject.BranchCode != undefined && $scope.selectedObject.BranchCode != '')
                $scope.ClassList = $filter('filter')(response.data, { 'BranchCode': $scope.selectedObject.BranchCode, 'CompanyCode': $scope.selectedObject.CompanyCode });
            else
                delete $scope.ClassList;
            //$scope.ClassList = response.data;
        }

        var onGetCompany = function (response) {
            //hideShield();
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
        }
        var onListComplete = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listData = response.data;
            $scope.selectedObject.Status = true;
        }
        var onSaveComplete = function (response) {
            hideShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            delete $scope.ClassList;
            //$scope.saveError = 'Save Record successfully';
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
            //$scope.saveError = 'Record deleted successfully';
        }

        var NewContact = function () {
            this.CompanyCode = $scope.selectedObject.CompanyCode;
            this.BranchContactPerson = '&nbsp;';
            this.BranchCode = $scope.selectedObject.BranchCode;
            this.ContactPerson = '';
            this.LandLine = '';
            this.Cell = '';
            this.Email = '';
        }

        $scope.isValid = function () {
            return ((myForm.companyCode.value == '')
                || (myForm.branchCode.value == '')
                || (myForm.ClassCode.value == '')
                || (myForm.CourseCode.value == '')
                );
        }

        var onRequestError = function (reason) {
            hideShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }
        $scope.load = function (obj) {
            showShield();
            if (obj != null) {
                $scope.selectedObject = angular.copy(obj);
                if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                $scope.isEditMode = true;
            }
            $scope.saveError = '';
            $scope.addSubjectCourse();
            hideShield();
        }
        $scope.clear = function () {
            $scope.selectedObject = {};
            $scope.saveError = '';
            $scope.listError = '';
            $scope.isEditMode = false;
            $scope.selectedObject.Status = true;
            $scope.listAllClassCourseSubject = {};
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
        }


        $scope.delete = function () {
            if (confirm('Are you sure, you want to delete the selected record?')){
                $scope.listAllClassCourseSubject = null;
                $scope.save();
            }
        }

        $scope.save = function () {
            showShield();
            if ($scope.listAllClassCourseSubject != undefined) {
                for (var i = 0; i < $scope.listAllClassCourseSubject.length; i++) {
                    var obj = $scope.listAllClassCourseSubject[i];
                    obj.ClassCode = $scope.selectedObject.ClassCode;
                    if ($scope.listAllClassCourseSubject[i].CourseCode == '' || $scope.listAllClassCourseSubject[i].SubjectCode == '' || $scope.listAllClassCourseSubject[i].SubjectCode == null) {
                        $scope.listAllClassCourseSubject.splice(i, 1);
                        i--;
                    }
                }
            }

            $scope.saveError = '';
            $http.post("/UI/saveClassCourseSubject", { ClassCourseSubject: $scope.listAllClassCourseSubject, iCompanyCode: $scope.selectedObject.CompanyCode, iBranchCode: $scope.selectedObject.BranchCode, iClassCode: $scope.selectedObject.ClassCode, iCourseCode: $scope.selectedObject.CourseCode }).then(onSaveComplete, onRequestError)
        }

        $scope.deleteSubjectCourse = function (object) {
            if (confirm('Are you sure, you want to delete this Subject & Course?'))
                $scope.listAllClassCourseSubject.splice(object, 1);
            hideShield();
        }

        var onGetAllBranch = function (response) {
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listBranch = response.data;
            hideShield();
        }

        var onGetAllClass = function (response) {
            $scope.listCompanyClass = response.data;
            hideShield();
        }

        var onGetAllCourse = function (response) {
            $scope.listCompanyCourse = response.data;
            hideShield();
        }

        var onGetAllSubject = function (response) {
            $scope.listCompanySubjects = response.data;
            hideShield();
        }

        var onGetAllClassCourseSubject = function (response) {
            $scope.listAllClassCourseSubject = $filter('filter')(response.data, { CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObject.BranchCode, ClassCode: $scope.selectedObject.ClassCode, CourseCode: $scope.selectedObject.CourseCode });
            $scope.addSubjectCourse();
            hideShield();
        }

        $scope.isEditMode = false;
        $scope.clear();
        $scope.saveError = '';
        $scope.listAllClassCourseSubject = {};
        showShield();
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        $http.get("/UI/getAllActiveBranches").then(onGetAllBranch, onRequestError);
        $http.get("/UI/getAllClass").then(onGetAllClass, onRequestError);
        $http.get("/UI/getAllActiveCourses").then(onGetAllCourse, onRequestError);
        $http.get("/UI/getAllActiveSubjects").then(onGetAllSubject, onRequestError);
        hideShield();
    }

    app.controller("classcoursesubjectController", classcoursesubjectController);
}
)();