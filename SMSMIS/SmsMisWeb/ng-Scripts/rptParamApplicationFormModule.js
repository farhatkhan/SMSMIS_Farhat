(function () {
    var app = angular.module("adminModule1", []);
    var paramApplicationFormController = function ($scope, $http, $filter) {


        $scope.Session_Change = function (o) {
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

            $scope.selectedObject.empty = "";

            if (o.CourseCode == null || o.SubjectCode == null || o.CourseCode == "" || o.SubjectCode == "") return;

            var opList = $filter('filter')($scope.listAllClassCourseSubject, { CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObject.BranchCode, ClassCode: o.ClassCode, CourseCode: o.CourseCode, SubjectCode: o.SubjectCode });



            if (opList != undefined && opList.length > 1) {
                alert('Same subject can not be defined multiple times,please rectify your change');
                opList[1].SubjectCode = null;
                return;
            }

            var opListEx = $filter('filter')($scope.listAllClassCourseSubject, { CourseCode: '', SubjectCode: '' }, true);
            if (typeof opListEx != 'undefined' && opListEx.length > 0) return;

            $scope.addSubjectCourse();
        }

        $scope.loadSessions = function () {

            $scope.listAllClassCourseSubject.length = 0;
            if ($scope.selectedObject.CompanyCode != undefined && $scope.selectedObject.BranchCode != undefined
                 && $scope.selectedObject.ClassCode != undefined)
                $http.get("/UI/getAllClassCourseSubject").then(onGetAllClassCourseSubject, onRequestError);
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
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
        }
        var onListComplete = function (response) {
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listData = response.data;
            $scope.selectedObject.Status = true;
        }

        $scope.isValid = function () {
            return ((myForm.companyCode.value == '')
                || (myForm.branchCode.value == '')
                );
        }

        var onRequestError = function (reason) {
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }
        $scope.load = function (obj) {
            if (obj != null) {
                $scope.selectedObject = angular.copy(obj);
                if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                $scope.isEditMode = true;
            }
            $scope.saveError = '';
            $scope.addSubjectCourse();
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
        $scope.showReport = function () {

            callReport($scope.selectedObject.CompanyCode, $scope.selectedObject.BranchCode, $scope.selectedObject.SessionCode, $scope.selectedObject.ClassCode,
                $scope.selectedObject.Gender, $scope.selectedObject.fromDate, $scope.selectedObject.fromTo);
            //$scope.saveError = '';
            //$http.post("/UI/saveClassCourseSubject", { ClassCourseSubject: $scope.listAllClassCourseSubject, iCompanyCode: $scope.selectedObject.CompanyCode, iBranchCode: $scope.selectedObject.BranchCode, iClassCode: $scope.selectedObject.ClassCode }).then(onSaveComplete, onRequestError)

        }

        $scope.deleteSubjectCourse = function (object) {
            if (confirm('Are you sure, you want to delete this Subject & Course?'))
                $scope.listAllClassCourseSubject.splice(object, 1);
        }

        var onGetAllBranch = function (response) {
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listBranch = response.data;
        }

        var onGetAllClass = function (response) {
            $scope.listCompanyClass = response.data;
        }

        var onGetAllCourse = function (response) {
            $scope.listCompanyCourse = response.data;
        }

        var onGetAllSubject = function (response) {
            $scope.listCompanySubjects = response.data;
        }

        var onGetAllClassCourseSubject = function (response) {
            $scope.listAllClassCourseSubject = $filter('filter')(response.data, { CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObject.BranchCode, ClassCode: $scope.selectedObject.ClassCode });
            $scope.addSubjectCourse();
        }

        var onGetAllSession = function (response) {
            if (response.data == null || response.data == undefined) listData = true;
            $scope.listSession = response.data;
        }

        $scope.isEditMode = false;
        $scope.clear();
        $scope.saveError = '';
        $scope.listAllClassCourseSubject = {};
        showShield();
        $http.get("/UI/getAllActiveBranches").then(onGetAllBranch, onRequestError);
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        $http.get("/UI/getAllClassesEx").then(onGetAllClass, onRequestError);
        $http.get("/UI/getAllActiveCourses").then(onGetAllCourse, onRequestError);
        $http.get("/UI/getAllActiveSubjects").then(onGetAllSubject, onRequestError);
        $http.get("/UI/getAllActiveSessions").then(onGetAllSession, onRequestError);
        hideShield();
    }

    app.controller("paramApplicationForm", paramApplicationFormController);
}
)();