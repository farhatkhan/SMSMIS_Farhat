(function () {
    var app = angular.module("adminModule1", []);
    var classListWithSnapModule = function ($scope, $http, $filter) {


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
            this.CourseCode = '';
            this.SubjectCode = '';
            this.Mandatory = false;
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
        
        var onSaveComplete = function (response) {
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            delete $scope.ClassList;
            $scope.saveError = 'Save Record successfully';
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.apply();

        }

        var onDelete = function (response) {
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            if (!$scope.isEditMode) $scope.clear(); //clear on add new mode
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.clear();
            $scope.saveError = 'Record deleted successfully';
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
            return (myForm.companyCode.value == '');
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
        $scope.list = function () {

            var strValues = $scope.selectedObject.CompanyCode + '/' + $scope.selectedObject.BranchCode + '/' + $scope.selectedObject.SessionCode + '/' + $scope.selectedObject.ClassCode + '/' +
                $scope.selectedObject.Gender + '/' + $scope.selectedObject.CourseCode + '/' + $scope.selectedObject.SubjectCode + '/' +
                $scope.selectedObject.SectionCode;

            $http.post("/UI/getClassWithSnap", { 'strValues': strValues }).
                        then(onListComplete, onRequestError);
        }

        var onListComplete = function (response) {
            if (response.data != null || response.data != undefined) {
                Index = 0;
                $scope.listDataEx = response.data;
                $scope.listDataNav = $scope.listDataEx[Index];
            }
        }

        $scope.Forward = function () {

            if (Index >= $scope.listDataEx.length - 1) return;

            Index++;
            $scope.listDataNav = $scope.listDataEx[Index];
            
        }

        $scope.Last = function () {
            $scope.listDataNav = $scope.listDataEx[$scope.listDataEx.length - 1];

        }

        $scope.First = function () {
            $scope.listDataNav = $scope.listDataEx[0];

        }

        $scope.Backward = function () {

            if (Index < $scope.listDataEx.length - 1) return;

            Index--;
            $scope.listDataNav = $scope.listDataEx[Index];
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

        var onGetAllSection = function (response) {
            $scope.listAllSections = response.data;
        }

        $scope.Index = 0;
        $scope.isEditMode = false;
        $scope.clear();
        $scope.saveError = '';
        $scope.listAllClassCourseSubject = {};
        $http.get("/UI/getAllActiveBranches").then(onGetAllBranch, onRequestError);
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        $http.get("/UI/getAllActiveClasses").then(onGetAllClass, onRequestError);
        $http.get("/UI/getAllActiveCourses").then(onGetAllCourse, onRequestError);
        $http.get("/UI/getAllActiveSubjects").then(onGetAllSubject, onRequestError);
        $http.get("/UI/getAllActiveSessions").then(onGetAllSession, onRequestError);
        $http.get("/UI/getAllActiveSections").then(onGetAllSection, onRequestError);
        hideShield();
    }

    app.controller("classListWithSnapModule", classListWithSnapModule);
}
)();