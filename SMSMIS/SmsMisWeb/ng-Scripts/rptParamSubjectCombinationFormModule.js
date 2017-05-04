(function () {
    var app = angular.module("adminModule1", []);
    var paramSubjectCombinationFormController = function ($scope, $http, $filter) {

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
        $scope.showReport = function () {
            callReport($scope.selectedObject.CompanyCode, $scope.selectedObject.BranchCode, $scope.selectedObject.SessionCode, $scope.selectedObject.ClassCode,
                $scope.selectedObject.Gender, $scope.selectedObject.CourseCode, $scope.selectedObject.SubjectCode,
                $scope.selectedObject.SectionCode
                );
        }

        var onGetAllBranch = function (response) {
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listBranch = response.data;
        }

        var onGetAllClass = function (response) {
            $scope.listCompanyClass = response.data;
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


        $scope.isEditMode = false;
        $scope.clear();
        $scope.saveError = '';
        $scope.listAllClassCourseSubject = {};
        showShield();
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        $http.get("/UI/getAllActiveBranches").then(onGetAllBranch, onRequestError);
        $http.get("/UI/getAllActiveSessions").then(onGetAllSession, onRequestError);
        $http.get("/UI/getAllClassesEx").then(onGetAllClass, onRequestError);
        $http.get("/UI/getAllActiveSections").then(onGetAllSection, onRequestError);
        hideShield();
    }

    app.controller("paramSubjectCombinationFormController", paramSubjectCombinationFormController);
}
)();