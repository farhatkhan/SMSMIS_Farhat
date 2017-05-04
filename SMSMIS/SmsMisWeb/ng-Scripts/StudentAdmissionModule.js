(function () {
    var app = angular.module("clientModule", []);
    var admController = function ($scope, $http, $filter) {

        $scope.deleteSubjectCourse = function (object) {
            if (confirm('Are you sure, you want to delete this Subject?'))
                $scope.listAllClassCourseSubject.splice(object, 1);
        }

        $scope.close = function(){
            HideDiv();
        }

        $scope.LoadStudents = function () {
            showShield();
            if ($scope.selectedObject != undefined &&
                $scope.selectedObject.CompanyCode > 0 && $scope.selectedObject.BranchCode > 0 && $scope.selectedObject.SessionCode > 0 && $scope.selectedObject.ClassCode > 0 && $scope.selectedObject.Gender != undefined) {
                var completedObject = $scope.selectedObject.CompanyCode + '/' + $scope.selectedObject.BranchCode + '/' + $scope.selectedObject.SessionCode + '/' + $scope.selectedObject.ClassCode + '/' + $scope.selectedObject.Gender;

                $http.post("/UI/getAllStudents", {
                    'strValues': completedObject
                }).
                        then(onListComplete, onRequestError);
            }
            hideShield();
        }

        var onLoadStudent = function (response) {

            if (typeof response.data != 'undefined' && response.data.length > 0) {
                $scope.selectedObjectPop = response.data[0];
                $scope.ClassCourse_Change();
                ShowDiv();
            }
            //$scope.selectedObjectPop.BranchCode = $scope.selectedObject.BranchCode;
            //$scope.selectedObjectPop.SessionCode = $scope.selectedObject.SessionCode;
            //$scope.selectedObjectPop.ClassCode = $scope.selectedObject.ClassCode;

        }

        $scope.AdmissionStatus_Change = function (o) {

            if (o != undefined && o != null) {

                if (o.AdmissionStatus == "Admission") {

                    $scope.selectedObjectPop = [];
                    $http.post("/UI/load", {
                        'CompanyCode': $scope.selectedObject.CompanyCode, 'BranchCode': $scope.selectedObject.BranchCode,
                        'SessionCode': $scope.selectedObject.SessionCode, 'StudentNo': o.StudentNo
                    }).
                                then(onLoadStudent, onRequestError);
                }
            }
        }


        var onRequestError = function (reason) {
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }

        $scope.isValid = function () {
            return (
                ($scope.selectedObjectPop == undefined ? true : false) ||
                ($scope.selectedObjectPop.BranchCode == undefined ? true : false) ||
                ($scope.selectedObjectPop.BuildingCode == undefined ? true : false) ||
                ($scope.selectedObjectPop.SessionCode == undefined ? true : false) ||
                ($scope.selectedObjectPop.ClassCode == undefined ? true : false) ||
                ($scope.selectedObjectPop.CourseCode == undefined ? true : false) ||
                ($scope.selectedObjectPop.SectionCode == undefined ? true : false) ||
                ($scope.selectedObjectPop.StudentRollNo == null ? true : false)
                )
                ;
            //|| (myForm.Address.value == '') || (myForm.Amount.value == '')) || (myForm.DateOfBirth.value == '');
        }

        $scope.load = function (obj) {
        }

        $scope.clear = function () {
        }

        var onSave = function (response) {

            var object = $filter('filter')($scope.listStudentData, { StudentNo: $scope.selectedObjectPop.StudentNo, CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObjectPop.BranchCode, ClassCode: $scope.selectedObjectPop.ClassCode, CourseCode: $scope.selectedObjectPop.CourseCode });

            if (typeof object != 'undefined')
                $scope.listStudentData.splice(object, 1);

            HideDiv();
        }

        $scope.save = function () {
            $http.post('/UI/saveStudentAdmission/', { 'strStudentNo': $scope.selectedObjectPop.StudentNo, 'stdAdmission': $scope.selectedObjectPop, 'lstAdmissionSubjects': $scope.listAllClassCourseSubject }).then(onSave, onRequestError);
        }
        $scope.delete = function () {
        }

        var onGetCompany = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
        }
        var onGetAllBranch = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listBranch = response.data;
            $scope.listBranchEx = response.data;
        }
        var onGetAllSession = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined) listData = true;
            $scope.listSession = response.data;
            $scope.listSessionEx = response.data;
        }
        var onGetAllClasses = function (response) {
            hideShield();
            $scope.listAllClass = response.data;
        }
        var onGetAllBranchBuilding = function (response) {
            hideShield();

            $scope.listBranchBuilding = response.data;

        }
        
        var onGetAllClassCourses = function (response) {
            hideShield();
            $scope.listAllClassCourse = response.data;
        }

        var onGetAllSection = function (response) {
            hideShield();
            $scope.listAllSections = response.data;
        }

        var onListComplete = function (response) {
            hideShield();
            $scope.listStudentData = response.data;
        }

        var onGetAllClassCourseSubject = function (response) {
            $scope.listAllClassCourseSubject = $filter('filter')(response.data, { CompanyCode: $scope.selectedObject.CompanyCode, BranchCode: $scope.selectedObjectPop.BranchCode, ClassCode: $scope.selectedObjectPop.ClassCode, CourseCode: $scope.selectedObjectPop.CourseCode });
            //$scope.addSubjectCourse();
        }

        $scope.ClassCourse_Change = function () {

            if ($scope.selectedObject.CompanyCode != undefined &&  $scope.selectedObjectPop.BranchCode != undefined
                && $scope.selectedObjectPop.ClassCode != undefined
                && $scope.selectedObjectPop.CourseCode != undefined)
            {
                $http.get("/UI/getAllClassCourseSubject").then(onGetAllClassCourseSubject, onRequestError);
            }
        }
        var onGetAllSubject = function (response) {
            $scope.listCompanySubjects = response.data;
        }
        $scope.isEditMode = false;
        $scope.clear();
        $scope.saveError = '';
        showShield();
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        $http.get("/UI/getAllActiveBranches").then(onGetAllBranch, onRequestError);
        $http.get("/UI/getAllActiveSessions").then(onGetAllSession, onRequestError);
        $http.get("/UI/getAllClassesEx").then(onGetAllClasses, onRequestError);

        $http.get("/UI/getAllBranchBuilding").then(onGetAllBranchBuilding, onRequestError);
        $http.get("/UI/getAllClassCourses").then(onGetAllClassCourses, onRequestError);
        $http.get("/UI/getAllActiveSections").then(onGetAllSection, onRequestError);
        $http.get("/UI/getAllActiveSubjects").then(onGetAllSubject, onRequestError);
        hideShield();
    }
    app.controller("studentAdmissionController", admController);
}
)();