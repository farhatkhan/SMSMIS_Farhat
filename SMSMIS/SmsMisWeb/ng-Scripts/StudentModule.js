(function () {
    var app = angular.module("clientModule", []);
    var admController = function ($scope, $http, $filter) {

        $scope.ClassCourse_Change = function () {

            $http.get("/UI/getAllStudentClassCourseSubjects").then(onGetAllClassCourseSubjects, onRequestError_GetAllClassCourseSubjects);

            //$scope.listAllClassCourseSubjects = $filter('filter')($scope.listAllClassCourseSubjects, { 'CompanyCode': $scope.selectedObject.CompanyCode, 'BranchCode': $scope.selectedObject.BranchCode, 'ClassCode': $scope.selectedObject.ClassCode, 'CourseCode': $scope.selectedObject.CourseCode }, true);

        }

        $scope.deleteStudentLastClassSubject = function (index) {
            if (confirm('Are you sure you want to delete selected record?'))
                $scope.selectedObject.StudentLastClassSubject.splice(index, 1);
        }

        $scope.deleteSubjectCourse = function (object) {
            //if (confirm('Are you sure, you want to delete this Subject?'))
            //$scope.listAllClassCourseSubjects.splice(object, 1);

            for (var v = 0; $scope.listAllClassCourseSubjects.length > 0; v++) {

                if ($scope.listAllClassCourseSubjects[v].CompanyCode == $scope.selectedObject.CompanyCode &&
                    $scope.listAllClassCourseSubjects[v].BranchCode == $scope.selectedObject.BranchCode &&
                    $scope.listAllClassCourseSubjects[v].ClassCode == $scope.selectedObject.ClassCode &&
                    $scope.listAllClassCourseSubjects[v].CourseCode == $scope.selectedObject.CourseCode &&
                    $scope.listAllClassCourseSubjects[v].SubjectCode == object) {

                    $scope.listAllClassCourseSubjects.splice(v, 1);
                }

            }

        }


        $scope.BranchCode_Change = function (cmd) {

            if (cmd == 'c')
                $scope.selectedObject.BranchCode = $scope.selectedObject.SessionCode = undefined;
            if (cmd == 'b')
                $scope.selectedObject.SessionCode = undefined;



            if ($scope.selectedObject.CompanyCode != undefined && $scope.selectedObject.BranchCode != undefined && $scope.selectedObject.SessionCode != undefined
                && $scope.selectedObject.CompanyCode != null && $scope.selectedObject.BranchCode != null && $scope.selectedObject.SessionCode != null) {


                if ($scope.selectedObject != undefined && $scope.selectedObject.CompanyCode > 0 && $scope.selectedObject.BranchCode > 0 && $scope.selectedObject.SessionCode > 0) {
                    var completedObject = $scope.selectedObject.CompanyCode + '/' + $scope.selectedObject.BranchCode + '/' + $scope.selectedObject.SessionCode;
                    showShield();
                    $http.post("/UI/getAllStudentsByCompanyBranchSession", { 'strValues': completedObject }).then(onListComplete, onListComplete_getAllStudent);
                    $http.post("/UI/getAllEmployee", { 'CompanyCode': $scope.selectedObject.CompanyCode }).then(onGetAllEmployee, onRequestError);
                    $scope.partialClear();
                }
            } else {
                $scope.partialClear();
                $scope.listDataEx = null;
                $scope.listData = null;
                $scope.selectedObject.Gender = 'Male';
            }
        }

        $scope.PreviewReport = function () {
            callReport($scope.selectedObject.StudentNo, $scope.selectedObject.CompanyCode, $scope.selectedObject.BranchCode, $scope.selectedObject.SessionCode);
        }

        $scope.handleKeyEvent_session = function (e, session) {
            if (e.keyCode == 13) {
                if (session.Subject == '' || session.Grade == '')
                    alert('Enter all the mandatory fields');
                else
                    $scope.addSubjectGradeDivision();
            }
        }

        $scope.addSubjectGradeDivision = function () {

            if ($scope.selectedObject.StudentLastClassSubject == undefined)
                $scope.selectedObject.StudentLastClassSubject = [];

            $scope.selectedObject.StudentLastClassSubject.push(new NewStudentLastClassSubject());

        }

        $scope.addKinship = function () {
            if ($scope.selectedObject.StudentKinship == undefined)
                $scope.selectedObject.StudentKinship = [];

            $scope.selectedObject.StudentKinship.push(new NewStudentKinship());
        }

        $scope.callStudentSubject = function (obj, index) {
            if (obj != null && obj.SubjectCode != null) {

                var selectedClassCourseSubjects = $filter('filter')($scope.listAllClassCourseSubjects, { 'CompanyCode': $scope.selectedObject.CompanyCode, 'BranchCode': $scope.selectedObject.BranchCode, 'ClassCode': $scope.selectedObject.ClassCode, 'CourseCode': $scope.selectedObject.CourseCode, 'SubjectCode': obj.SubjectCode }, true);

                if (selectedClassCourseSubjects.length > 1) {
                    obj.SubjectCode = null;
                    alert('Duplicate subject can not be selected, please some other subject!');
                }
                else {
                    var selectedClassCourseSubjects = $filter('filter')($scope.listAllClassCourseSubjects, { 'CompanyCode': $scope.selectedObject.CompanyCode, 'BranchCode': $scope.selectedObject.BranchCode, 'ClassCode': $scope.selectedObject.ClassCode, 'CourseCode': $scope.selectedObject.CourseCode, 'SubjectCode': 0 }, true);
                    if (selectedClassCourseSubjects.length == 0)
                        $scope.addStudentSubject();
                }
            }
            else
                $scope.listAllClassCourseSubjects.splice(index, 1);
        }

        $scope.addStudentSubject = function () {


            if ($scope.listAllClassCourseSubjects == undefined)
                $scope.listAllClassCourseSubjects = [];

            $scope.listAllClassCourseSubjects.push(new NewStudentSubject());
        }

        $scope.addReference = function () {

            if ($scope.selectedObject.StudentMarketingReference == undefined)
                $scope.selectedObject.StudentMarketingReference = [];

            $scope.selectedObject.StudentMarketingReference.push(new NewSession());
        }

        $scope.deleteMarketingReferenceCode = function (obj) {

            $scope.selectedObject.StudentMarketingReference.splice(obj, 1);
        }

        var NewSession = function () {
            this.CompanyCode = $scope.selectedObject.CompanyCode;
            this.BranchCode = (($scope.selectedObject.BranchCode == undefined) ? '' : $scope.selectedObject.BranchCode);
            this.SessionCode = (($scope.selectedObject.SessionCode == undefined) ? '' : $scope.selectedObject.SessionCode);
            this.StudentNo = 0;
            this.Remarks = '';
        }

        var NewStudentLastClassSubject = function () {
            this.CompanyCode = $scope.selectedObject.CompanyCode;
            this.BranchCode = (($scope.selectedObject.BranchCode == undefined) ? '' : $scope.selectedObject.BranchCode);
            this.SessionCode = (($scope.selectedObject.SessionCode == undefined) ? '' : $scope.selectedObject.SessionCode);
            this.StudentNo = 0;
            this.SrNo = 0;
            this.Subject = '';
            this.Grade = '';
            this.Division = '';
        }

        var NewStudentApplicationCheckList = function (DocCode) {
            this.CompanyCode = $scope.selectedObject.CompanyCode;
            this.BranchCode = (($scope.selectedObject.BranchCode == undefined) ? '' : $scope.selectedObject.BranchCode);
            this.SessionCode = (($scope.selectedObject.SessionCode == undefined) ? '' : $scope.selectedObject.SessionCode);
            this.StudentNo = 0;
            this.DocNo = DocCode;
        }

        var NewStudentKinship = function () {
            this.CompanyCode = $scope.selectedObject.CompanyCode;
            this.BranchCode = (($scope.selectedObject.BranchCode == undefined) ? '' : $scope.selectedObject.BranchCode);
            this.SessionCode = (($scope.selectedObject.SessionCode == undefined) ? '' : $scope.selectedObject.SessionCode);
            this.StudentNo = 0;
            this.KinshipType = '';
            this.KinshipRelationship = '';
            this.KinshipReferenceNo = '';
            this.Discount = '';
        }

        var NewStudentSubject = function () {
            this.CompanyCode = $scope.selectedObject.CompanyCode;
            this.BranchCode = (($scope.selectedObject.BranchCode == undefined) ? '' : $scope.selectedObject.BranchCode);
            this.SessionCode = (($scope.selectedObject.SessionCode == undefined) ? '' : $scope.selectedObject.SessionCode);
            this.StudentNo = $scope.selectedObject.StudentNo;
            this.ClassCode = $scope.selectedObject.ClassCode;
            this.CourseCode = $scope.selectedObject.CourseCode;
            this.SubjectCode = '';
            this.Mandatory = false;
        }


        var onGetAllMarketingReference = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listMarketingReference = response.data;
        }


        var onGetCompany = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;

        }
        var onListComplete = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listDataEx = response.data;
            $scope.listData = response.data;
            $scope.selectedObject.Gender = 'Male';
        }

        var onGetAllBranch = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listBranch = response.data;
        }

        var onGetAllSession = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined) listData = true;
            $scope.listSession = response.data;
        }

        var onGetAllClass = function (response) {
            hideShield();
            if (response.data == null || response.data == undefined) listData = true;
            $scope.listClass = response.data;
        }

        var onGetAllCourse = function (response) {
            hideShield();
            $scope.listCompanyCourse = response.data;
        }

        var onGetAllClasses = function (response) {
            hideShield();
            $scope.listAllClass = response.data;
        }

        var onGetAllClassCourses = function (response) {
            hideShield();
            $scope.listAllClassCourse = response.data;
        }

        var onGetAllClassCourseSubjects = function (response) {
            hideShield();

            $scope.listAllClassCourseSubjects = $filter('filter')(response.data, { 'Mandatory': true });
            $scope.addStudentSubject();
            //$scope.listAllClassCourseSubjects = response.data;
        }

        var onGetAllSubject = function (response) {
            hideShield();
            $scope.listCompanySubjects = response.data;
        }

        var onGetAllKinshipDiscount = function (response) {
            hideShield();
            $scope.listAllKinshipDiscount = response.data;
            $scope.listAllKinshipDiscountCopy = angular.copy(response.data);
        }

        var onSaveComplete = function (response) {
            hideShield();
            //$scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data.dt;
            $scope.listDataEx = response.data.dt;

            var selectedCompany = $scope.selectedObject.CompanyCode;
            var selectedBranch = $scope.selectedObject.BranchCode;
            var selectedSession = $scope.selectedObject.SessionCode;

            try { callReport(response.data.ID, $scope.selectedObject.CompanyCode, $scope.selectedObject.BranchCode, $scope.selectedObject.SessionCode); }
            catch (ex) { alert('Report in error'); }

            $scope.clear(); //clear on add new mode
            $scope.selectedObject.CompanyCode = selectedCompany;
            $scope.selectedObject.BranchCode = selectedBranch;
            $scope.selectedObject.SessionCode = selectedSession;
            $scope.BranchCode_Change();
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            //$scope.apply();
        }

        $scope.partialClear = function () {

            var selectedCompany = $scope.selectedObject.CompanyCode;
            var selectedBranch = $scope.selectedObject.BranchCode;
            var selectedSession = $scope.selectedObject.SessionCode;

            $scope.clear(); //clear on add new mode
            $scope.selectedObject.CompanyCode = selectedCompany;
            $scope.selectedObject.BranchCode = selectedBranch;
            $scope.selectedObject.SessionCode = selectedSession;
            //if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
        }

        var onDelete = function (response) {
            hideShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.partialClear();
            //$scope.saveError = 'Record deleted successfully';
        }

        $scope.isValid = function () {
            return ((myForm.FirstName.value == '')
            || (myForm.Address.value == '') || (myForm.Amount.value == '')) || (myForm.DateOfBirth.value == '') || (myForm.ReligionCode.value == '');
        }

        var onRequestError = function (reason) {
            alert(reason.statusText);
            hideShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }

        var onListComplete_getAllStudent = function (reason) {
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }

        var onRequestError_GetAllClassCourseSubjects = function (reason) {
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }


        $scope.load = function (obj) {
            //$http.get("/UI/getAllStudentClassCourseSubjects").then(onGetAllClassCourseSubjects, onRequestError_GetAllClassCourseSubjects);
            showShield();
            if (obj != null) {

                $http.post("/UI/load", { 'CompanyCode': obj.CompanyCode, 'BranchCode': obj.BranchCode, 'SessionCode': obj.SessionCode, 'StudentNo': obj.StudentNo })
                    .success(function (response) {

                        if (typeof response == 'undefined' || response.length == 0) return;



                        $scope.selectedObject = angular.copy(response)[0];

                        //\\$scope.listAllClassCourseSubjects = $scope.selectedObject.StudentSubjectList;

                        var selectedClassCourseSubjects=$scope.listAllClassCourseSubjects = $scope.selectedObject.StudentSubjectList;


                        if (selectedClassCourseSubjects != undefined && selectedClassCourseSubjects != null) {

                            $scope.listAllClassCourseSubjects = [];

                            for (var v = 0; v < selectedClassCourseSubjects.length; v++) {

                                if (selectedClassCourseSubjects[v].SubjectCode > 0)
                                    $scope.listAllClassCourseSubjects.push(new studentSelectedSubjects(selectedClassCourseSubjects[v].SubjectCode, selectedClassCourseSubjects[v].Mandatory));
                            }
                        }



                        //$scope.searchText = '100';

                        //if ($scope.selectedObject.StudentKinship.length == 0) 
                        $scope.addKinship();

                        //if ($scope.selectedObject.StudentLastClassSubject.length == 0)
                        $scope.addSubjectGradeDivision

                        //if ($scope.selectedObject.StudentMarketingReference.length== 0)
                        $scope.addReference();

                        $scope.addSubjectGradeDivision();

                        $scope.addStudentSubject();

                        if (typeof $scope.selectedObject.StudentKinship != 'undefined' && $scope.selectedObject.StudentKinship.length > 0) {
                            for (var v in $scope.selectedObject.StudentKinship) {

                                if ($scope.selectedObject.StudentKinship[v].KinshipType == 'Staff') {
                                    var obj = $filter('filter')($scope.listAllEmployees, { 'EmployeeCode': $scope.selectedObject.StudentKinship[v].KinshipReferenceNo, 'CompanyCode': $scope.selectedObject.CompanyCode });

                                    if (typeof obj != 'undefined' && obj.length>0)
                                        $scope.selectedObject.StudentKinship[v].Name = obj[0].EmployeeName;
                                }
                                if ($scope.selectedObject.StudentKinship[v].KinshipType == 'Student') {
                                    var obj = $filter('filter')($scope.listData, { 'StudentNo': $scope.selectedObject.StudentKinship[v].KinshipReferenceNo, 'CompanyCode': $scope.selectedObject.CompanyCode });
                                    if (typeof obj != 'undefined' && obj.length>0)
                                        $scope.selectedObject.StudentKinship[v].Name = obj[0].FullName;
                                }

                            }
                        }

                        if (typeof $scope.listAllDocType != 'undefined' && $scope.listAllDocType.length > 0) {
                            for (var v in $scope.listAllDocType) {
                                $scope.listAllDocType[v].isSelected = false;
                            }
                        }

                        if (typeof $scope.selectedObject.StudentApplicationCheckList != 'undefined' && $scope.selectedObject.StudentApplicationCheckList.length > 0) {
                            for (var v in $scope.selectedObject.StudentApplicationCheckList) {

                                $filter('filter')($scope.listAllDocType, { 'DocCode': $scope.selectedObject.StudentApplicationCheckList[v].DocNo })[0].isSelected = true;
                            }
                        }

                        var mydate = parseInt($scope.selectedObject.DateOfBirth.replace('/Date(', '').replace(')/', ''));
                        $scope.selectedObject.DateOfBirth = $filter('date')(new Date(mydate), 'dd/MM/y');
                        if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                        $scope.isEditMode = true;
                    })
                    .error(function (reason) {
                        alert(reason);
                    });
            }
            $scope.saveError = '';

            hideShield();
        }

        $scope.clear = function () {

            $scope.selectedObject = {};
            $scope.selectedObject.Gender = 'Male';
            $scope.dummy = { images: '' };
            $scope.saveError = '';
            $scope.listError = '';
            $scope.isEditMode = false;

            $scope.addSubjectGradeDivision();
            $scope.addKinship();
            $scope.addReference();

            if (typeof $scope.listAllDocType != 'undefined' && $scope.listAllDocType.length > 0) {
                for (var v in $scope.listAllDocType) {
                    $scope.listAllDocType[v].isSelected = false;
                }
            }
            //if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
        }


        var studentSelectedSubjects = function (subjectcode, mandatory) {
            this.CompanyCode = $scope.selectedObject.CompanyCode;
            this.BranchCode = (($scope.selectedObject.BranchCode == undefined) ? '' : $scope.selectedObject.BranchCode);
            this.SessionCode = (($scope.selectedObject.SessionCode == undefined) ? '' : $scope.selectedObject.SessionCode);
            this.StudentNo = (($scope.selectedObject.StudentNo == undefined) ? 0 : $scope.selectedObject.StudentNo);
            this.ClassCode = $scope.selectedObject.ClassCode;
            this.CourseCode = $scope.selectedObject.CourseCode;
            this.SubjectCode = subjectcode;//selectedClassCourseSubjects[v].SubjectCode;
            this.Mandatory = mandatory;//selectedClassCourseSubjects[v].SubjectCode;

            var subjectname = $filter('filter')($scope.listCompanySubjects,{CompanyCode: $scope.selectedObject.CompanyCode,BranchCode: $scope.selectedObject.BranchCode,ClassCode: $scope.selectedObject.ClassCode,CourseCode: $scope.selectedObject.CourseCode,SubjectCode:subjectcode},true)[0].SubjectName;
            //Shah
            this.SubjectName = subjectname;

        }


        $scope.save = function () {


            if ($scope.selectedObject.StudentNo > 0)
                var selectedClassCourseSubjects = $scope.listAllClassCourseSubjects;//$filter('filter')($scope.listAllClassCourseSubjects, { 'CompanyCode': $scope.selectedObject.CompanyCode, 'BranchCode': $scope.selectedObject.BranchCode, 'ClassCode': $scope.selectedObject.ClassCode, 'CourseCode': $scope.selectedObject.CourseCode, 'StudentNo': $scope.selectedObject.StudentNo }, true);
            else
                var selectedClassCourseSubjects = $filter('filter')($scope.listAllClassCourseSubjects, { 'CompanyCode': $scope.selectedObject.CompanyCode, 'BranchCode': $scope.selectedObject.BranchCode, 'ClassCode': $scope.selectedObject.ClassCode, 'CourseCode': $scope.selectedObject.CourseCode }, true);


            if (selectedClassCourseSubjects != undefined && selectedClassCourseSubjects != null) {

                $scope.selectedObject.StudentSubjectList = [];

                for (var v = 0; v < selectedClassCourseSubjects.length; v++) {

                    if (selectedClassCourseSubjects[v].SubjectCode > 0)
                        $scope.selectedObject.StudentSubjectList.push(new studentSelectedSubjects(selectedClassCourseSubjects[v].SubjectCode, selectedClassCourseSubjects[v].Mandatory));
                }
            }

            showShield();
            $scope.saveError = '';
            var img = $scope.dummy.images;
            if ($scope.selectedObject.StudentMarketingReference != undefined) {
                for (var i = 0; i < $scope.selectedObject.StudentMarketingReference.length; i++) {
                    var obj = $scope.selectedObject.StudentMarketingReference[i];
                    if ($scope.selectedObject.StudentMarketingReference[i].MarketingReferenceCode == undefined || $scope.selectedObject.StudentMarketingReference[i].MarketingReferenceCode == ''
                         || $scope.selectedObject.StudentMarketingReference[i].MarketingReferenceCode == '0') {
                        $scope.selectedObject.StudentMarketingReference.splice(i, 1);
                        i--;
                    }
                }
            }
            var SrNo = 0;
            if ($scope.selectedObject.StudentLastClassSubject != undefined) {
                for (var i = 0; i < $scope.selectedObject.StudentLastClassSubject.length; i++) {
                    var obj = $scope.selectedObject.StudentLastClassSubject[i];
                    if ($scope.selectedObject.StudentLastClassSubject[i].Subject == '' || $scope.selectedObject.StudentLastClassSubject[i].Grade == '') {
                        $scope.selectedObject.StudentLastClassSubject.splice(i, 1);
                        i--;
                    }
                    else {
                        SrNo = SrNo + 1;
                        obj.SrNo = SrNo
                    }
                }
            }

            if ($scope.selectedObject.StudentKinship != undefined) {
                for (var i = 0; i < $scope.selectedObject.StudentKinship.length; i++) {
                    var obj = $scope.selectedObject.StudentKinship[i];

                    //if (obj.KinshipType == 'Staff')
                    //    obj.KinshipReferenceNo = $filter('filter')($scope.listAllEmployees, { 'EmployeeName': obj.KinshipReferenceNo, 'CompanyCode': $scope.selectedObject.CompanyCode })[0].EmployeeCode;

                    if ($scope.selectedObject.StudentKinship[i].KinshipType == undefined || $scope.selectedObject.StudentKinship[i].KinshipType == '' ||
                        $scope.selectedObject.StudentKinship[i].KinshipRelationship == undefined || $scope.selectedObject.StudentKinship[i].KinshipRelationship == '' ||
                        $scope.selectedObject.StudentKinship[i].KinshipReferenceNo == undefined || $scope.selectedObject.StudentKinship[i].KinshipReferenceNo == '' ||
                        $scope.selectedObject.StudentKinship[i].Discount == undefined || $scope.selectedObject.StudentKinship[i].Discount == '') {
                        $scope.selectedObject.StudentKinship.splice(i, 1);
                        i--;
                    }
                }
            }

            if ($scope.listAllDocType != undefined && $scope.listAllDocType.length > 0) {
                $scope.selectedObject.StudentApplicationCheckList = [];
                for (var v in $scope.listAllDocType) {
                    if ($scope.listAllDocType[v].isSelected) {
                        $scope.selectedObject.StudentApplicationCheckList.push(new NewStudentApplicationCheckList($scope.listAllDocType[v].DocCode));
                    }
                }
            }


            if (typeof $scope.selectedObject != 'undefined')
                $scope.selectedObject.FullName = $scope.selectedObject.FirstName + ' ' + ($scope.selectedObject.MiddleName == undefined ? '' : $scope.selectedObject.MiddleName) + ' ' + ($scope.selectedObject.LastName == undefined ? '' : $scope.selectedObject.LastName)
            if (!$scope.isEditMode) $scope.selectedObject.Status = true;
            if (angular.isUndefined($scope.selectedObject.FormReceived) || $scope.selectedObject.FormReceived == null)
                $scope.selectedObject.FormReceived = false;
            
            $http.post("/UI/saveStudent", { Student: $scope.selectedObject, imgFile: img }).then(onSaveComplete, onRequestError)
        }
        $scope.delete = function () {
            showShield();
            $scope.saveError = '';
            if (confirm('Are you sure you want to delete this record?')) {

                if ($scope.selectedObject.StudentNo > 0)
                    var selectedClassCourseSubjects = $scope.listAllClassCourseSubjects;//$filter('filter')($scope.listAllClassCourseSubjects, { 'CompanyCode': $scope.selectedObject.CompanyCode, 'BranchCode': $scope.selectedObject.BranchCode, 'ClassCode': $scope.selectedObject.ClassCode, 'CourseCode': $scope.selectedObject.CourseCode, 'StudentNo': $scope.selectedObject.StudentNo }, true);
                else
                    var selectedClassCourseSubjects = $filter('filter')($scope.listAllClassCourseSubjects, { 'CompanyCode': $scope.selectedObject.CompanyCode, 'BranchCode': $scope.selectedObject.BranchCode, 'ClassCode': $scope.selectedObject.ClassCode, 'CourseCode': $scope.selectedObject.CourseCode }, true);


                if (selectedClassCourseSubjects != undefined && selectedClassCourseSubjects != null) {

                    $scope.selectedObject.StudentSubjectList = [];

                    for (var v = 0; v < selectedClassCourseSubjects.length; v++) {

                        if (selectedClassCourseSubjects[v].SubjectCode > 0)
                            $scope.selectedObject.StudentSubjectList.push(new studentSelectedSubjects(selectedClassCourseSubjects[v].SubjectCode, selectedClassCourseSubjects[v].Mandatory));
                    }
                }

                showShield();
                $scope.saveError = '';
                var img = $scope.dummy.images;
                if ($scope.selectedObject.StudentMarketingReference != undefined) {
                    for (var i = 0; i < $scope.selectedObject.StudentMarketingReference.length; i++) {
                        var obj = $scope.selectedObject.StudentMarketingReference[i];
                        if ($scope.selectedObject.StudentMarketingReference[i].MarketingReferenceCode == undefined || $scope.selectedObject.StudentMarketingReference[i].MarketingReferenceCode == ''
                             || $scope.selectedObject.StudentMarketingReference[i].MarketingReferenceCode == '0') {
                            $scope.selectedObject.StudentMarketingReference.splice(i, 1);
                            i--;
                        }
                    }
                }
                var SrNo = 0;
                if ($scope.selectedObject.StudentLastClassSubject != undefined) {
                    for (var i = 0; i < $scope.selectedObject.StudentLastClassSubject.length; i++) {
                        var obj = $scope.selectedObject.StudentLastClassSubject[i];
                        if ($scope.selectedObject.StudentLastClassSubject[i].Subject == '' || $scope.selectedObject.StudentLastClassSubject[i].Grade == '') {
                            $scope.selectedObject.StudentLastClassSubject.splice(i, 1);
                            i--;
                        }
                        else {
                            SrNo = SrNo + 1;
                            obj.SrNo = SrNo
                        }
                    }
                }

                if ($scope.selectedObject.StudentKinship != undefined) {
                    for (var i = 0; i < $scope.selectedObject.StudentKinship.length; i++) {
                        var obj = $scope.selectedObject.StudentKinship[i];

                        //if (obj.KinshipType == 'Staff')
                        //    obj.KinshipReferenceNo = $filter('filter')($scope.listAllEmployees, { 'EmployeeName': obj.KinshipReferenceNo, 'CompanyCode': $scope.selectedObject.CompanyCode })[0].EmployeeCode;

                        if ($scope.selectedObject.StudentKinship[i].KinshipType == undefined || $scope.selectedObject.StudentKinship[i].KinshipType == '' ||
                            $scope.selectedObject.StudentKinship[i].KinshipRelationship == undefined || $scope.selectedObject.StudentKinship[i].KinshipRelationship == '' ||
                            $scope.selectedObject.StudentKinship[i].KinshipReferenceNo == undefined || $scope.selectedObject.StudentKinship[i].KinshipReferenceNo == '' ||
                            $scope.selectedObject.StudentKinship[i].Discount == undefined || $scope.selectedObject.StudentKinship[i].Discount == '') {
                            $scope.selectedObject.StudentKinship.splice(i, 1);
                            i--;
                        }
                    }
                }

                if ($scope.listAllDocType != undefined && $scope.listAllDocType.length > 0) {
                    $scope.selectedObject.StudentApplicationCheckList = [];
                    for (var v in $scope.listAllDocType) {
                        if ($scope.listAllDocType[v].isSelected) {
                            $scope.selectedObject.StudentApplicationCheckList.push(new NewStudentApplicationCheckList($scope.listAllDocType[v].DocCode));
                        }
                    }
                }

                $http.post("/UI/deleteStudent", $scope.selectedObject).then(onDelete, onRequestError)
            }
            hideShield();
        }

        $scope.filterKinShip = function (val) {
            $scope.listAllKinshipDiscount = [];
            //$scope.listAllKinshipDiscount = $filter('filter')($scope.listAllKinshipDiscount, { 'KinshipType': Kinship.KinshipType, 'CompanyCode': selectedObject.CompanyCode, 'BranchCode': selectedObject.BranchCode });
            $scope.listAllKinshipDiscount = $filter('filter')($scope.listAllKinshipDiscountCopy, { 'KinshipType': val, 'BranchCode': $scope.selectedObject.BranchCode, 'CompanyCode': $scope.selectedObject.CompanyCode });
        }

        $scope.ShowDiscountByKinShipRelation = function (kinship) {

            var obj = $filter('filter')($scope.listAllKinshipDiscountCopy, { 'KinshipRelation': kinship.KinshipRelationship, 'KinshipType': kinship.KinshipType, 'BranchCode': $scope.selectedObject.BranchCode, 'CompanyCode': $scope.selectedObject.CompanyCode });

            if (typeof obj != 'undefined' && obj.length > 0)
                kinship.Discount = obj[0].Discount;
            else
                kinship.Discount = '';

        }

        var onGetAllEmployee = function (response) {
            hideShield();
            $scope.listAllEmployees = response.data;
        }

        var onGetAllDocType = function (response) {
            hideShield();
            $scope.listAllDocType = response.data;
        }

        $scope.handleKeyEvent_MarketingRef = function (e, session) {
            if (e.keyCode == 13) {
                if (typeof session.MarketingReferenceCode == 'undefined' || session.MarketingReferenceCode == '0')
                    alert('Enter all the mandatory fields');
                else
                    $scope.addReference();
            }
        }


        $scope.SesionCode_CheckDuplicate = function (mreference) {

            $scope.list = $filter('filter')($scope.selectedObject.StudentMarketingReference, { 'MarketingReferenceCode': mreference.MarketingReferenceCode });

            if ($scope.list.length > 1) {
                alert('Reference already selected, please select some other reference!');
                mreference.MarketingReferenceCode = '0';
                return false;
            }

        }

        var onGetAllReligion = function (response) {

            $scope.listAllReligions = response.data;
        }

        $scope.Search_Click = function (obj, oKinship, index) {

            oKinship.CompanyCode = $scope.selectedObject.CompanyCode;
            oKinship.BranchCode = $scope.selectedObject.BranchCode;
            oKinship.SessionCode = $scope.selectedObject.SessionCode;
            oKinship.StudentNo = $scope.selectedObject.StudentNo;


            if (oKinship.KinshipType == 'Staff') {
                var duplicateCount = $filter('filter')($scope.selectedObject.StudentKinship, { 'KinshipType': "Staff", 'KinshipReferenceNo': obj.EmployeeCode });
                if (duplicateCount.length > 0) {
                    oKinship.KinshipReferenceNo = undefined;
                    $('.txtEmployeeName' + index).val('');
                    alert('Duplicate found');
                }
                else {
                    oKinship.KinshipReferenceNo = obj.EmployeeCode;
                    $('.txtEmployeeName' + index).val(obj.EmployeeName);
                }
                $('.autosearchStaff' + index).hide();
            }
            else {

                var duplicateCount = $filter('filter')($scope.selectedObject.StudentKinship, { 'KinshipType': "Student", 'KinshipReferenceNo': obj.StudentNo },true);
                if (duplicateCount.length > 0){
                    oKinship.KinshipReferenceNo = undefined;
                    $('.txtEmployeeName' + index).val('');
                    alert('Duplicate found');
                }
                else {
                    oKinship.KinshipReferenceNo = obj.StudentNo;
                    $('.txtEmployeeName' + index).val(obj.FullName);
                }
                $('.autosearchStudent' + index).hide();
            }


        }

        $scope.handleKeyEvent_Discount = function (e, kinship) {
            if (e.keyCode == 13) {
                if (kinship.KinshipType == '' || kinship.KinshipRelationship == '' || kinship.Discount == '' || kinship.Discount == 0 || kinship.Discount == 0 || kinship.KinshipReferenceNo == 0 || kinship.KinshipReferenceNo == ''
                     || kinship.KinshipReferenceNo == undefined)
                    alert('Enter all the mandatory fields');
                else
                    $scope.addKinship();
            }
        }

        $scope.showRelevantDiv = function (searchText, KinshipType, index) {
            if (searchText.length == 0) {
                $('.autosearchStaff' + index).hide();
                $('.autosearchStudent' + index).hide();
            }
            else if (KinshipType == 'Staff') {
                $('.autosearchStudent' + index).hide();
                $('.autosearchStaff' + index).show();
            }
            else if (KinshipType == 'Student') {
                $('.autosearchStudent' + index).show();
                $('.autosearchStaff' + index).hide();
            }
        }

        $scope.deleteKinship = function (object) {
            $scope.selectedObject.StudentKinship.splice(object, 1);
        }

        $scope.Reset_Kinship = function (cmd, Kinship, index) {
            $('.txtEmployeeName' + index).val('');
            if (cmd == 't')
            {
                Kinship.KinshipRelationship = '';
                Kinship.KinshipReferenceNo = '';
                Kinship.Discount = '';
            }
            if (cmd == 'r')
            {
                Kinship.KinshipReferenceNo = '';
                Kinship.Discount = '';
            }
        }

        //var filteredData;

        //$scope.SetSelectedAccountTitle = function (data, indx) {
        //    data.AccountTitle = data.KinshipReferenceNo;
        //    if (data.KinshipReferenceNo == '') {
        //        $('.abc' + indx).hide();
        //        return;
        //    }

        //    var filteredArray = $filter('filter')($scope.listAllEmployees, { EmployeeCode: data.KinshipReferenceNo });
        //    if (filteredArray.length == 1 && filteredArray[0].AccountTitle.toUpperCase() == data.AccountTitle.toUpperCase()) {
        //        data.EmployeeCode = filteredArray[0].EmployeeCode;
        //        data.EmployeeName = filteredArray[0].EmployeeName;
        //        $('.abc' + indx).hide();
        //    }
        //    else {
        //        $('.abc' + indx).show();
        //    }
        //}

        //$scope.GetSelectedAccountTitle = function (data, x, indx) {
        //    data.AccountCode = x.AccountCode;
        //    data.AccountTitle = x.AccountTitle;
        //    $('.QuickSearchResults').hide();
        //    $scope.validateGrid(indx, true);
        //}


        showShield();

        //$scope.selectedObject.Gender = 'Male';
        $scope.isEditMode = false;
        $scope.clear();
        $scope.dummy = { images: '' };
        $scope.saveError = '';
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        $http.get("/UI/getAllActiveSessions").then(onGetAllSession, onRequestError);
        $http.get("/UI/getAllActiveBranches").then(onGetAllBranch, onRequestError);
        //$http.get("/UI/getAllClasses").then(onGetAllClass, onRequestError);
        $http.get("/UI/getAllClass").then(onGetAllClass, onRequestError);
        $http.get("/UI/getAllActiveCourses").then(onGetAllCourse, onRequestError);

        $http.get("/UI/getAllClassesEx").then(onGetAllClasses, onRequestError_GetAllClassCourseSubjects);
        //$http.get("/UI/getAllClasses").then(onGetAllClasses, onRequestError_GetAllClassCourseSubjects);

        $http.get("/UI/getAllClassCourses").then(onGetAllClassCourses, onRequestError_GetAllClassCourseSubjects);



        //$http.get("/UI/getAllStudentClassCourseSubjects").then(onGetAllClassCourseSubjects, onRequestError_GetAllClassCourseSubjects);


        $http.get("/UI/getAllMarketingReference").then(onGetAllMarketingReference, onRequestError);



        $http.get("/UI/getAllActiveStudentSubjects").then(onGetAllSubject, onRequestError);


        $http.get("/UI/getAllReligion").then(onGetAllReligion, onRequestError);
        $http.get("/UI/getAllKinshipDiscount").then(onGetAllKinshipDiscount, onRequestError);
        $http.get("/UI/getAllDocType").then(onGetAllDocType, onRequestError);
        $http.get("/UI/getAllDocType").then(onGetAllDocType, onRequestError);



        hideShield();

    }
    app.filter('unique', function () {
        return function (input, key) {
            var unique = {};
            var uniqueList = [];
            for (var i = 0; i < input.length; i++) {
                if (typeof unique[input[i][key]] == "undefined") {
                    unique[input[i][key]] = "";
                    uniqueList.push(input[i]);
                }
            }
            return uniqueList;
        };
    });
    app.controller("studentController", admController);

    app.directive('validateEmail', function () {
        var EMAIL_REGEXP = /^[_a-z0-9]+(\.[_a-z0-9]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$/;
        return {
            link: function (scope, elm) {
                elm.on("keyup", function () {

                    var isMatchRegex = EMAIL_REGEXP.test(elm.val());
                    if (elm.val().length == 0)
                        elm.removeClass('warning');
                    else if (isMatchRegex && elm.hasClass('warning')) {
                        elm.removeClass('warning');
                    } else if (isMatchRegex == false && !elm.hasClass('warning')) {
                        elm.addClass('warning');
                    }
                });
            }
        }
    });


    app.directive('fileModel', function ($q) {
        /*
        made by elmerbulthuis@gmail.com WTFPL licensed
        */
        var slice = Array.prototype.slice;

        return {
            restrict: 'A',
            require: '?ngModel',
            link: function (scope, element, attrs, ngModel) {
                if (!ngModel) return;

                ngModel.$render = function () { }

                element.bind('change', function (e) {
                    var element = e.target;
                    if (!element.value) return;

                    element.disabled = true;
                    $q.all(slice.call(element.files, 0).map(readFile))
                      .then(function (values) {
                          if (element.multiple) ngModel.$setViewValue(values);
                          else ngModel.$setViewValue(values.length ? values[0] : null);
                          element.value = null;
                          element.disabled = false;
                      });

                    function readFile(file) {
                        var deferred = $q.defer();

                        var reader = new FileReader()
                        reader.onload = function (e) {
                            deferred.resolve(e.target.result);
                        }
                        reader.onerror = function (e) {
                            deferred.reject(e);
                        }
                        reader.readAsDataURL(file);

                        return deferred.promise;
                    }

                }); //change

            } //link

        }; //return

    }) //appFilereader
    addDirectives(app);
}
)();