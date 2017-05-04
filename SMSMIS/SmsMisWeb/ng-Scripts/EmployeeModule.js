(function () {
    var app = angular.module("clientModule", []);
    var admController = function ($scope, $http, $filter) {
        showShield();
        $scope.CompanyCode_Change = function () {
            //$scope.listData = $filter('filter')($scope.listDataEx, { 'CompanyCode': $scope.selectedObject.CompanyCode });
            showShield();
            $scope.isEditMode = false;
            $http.post("/UI/getAllEmployee", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onListComplete, onRequestError);
        }

        $scope.handleKeyEvent_session = function (e, session) {
            if (e.keyCode == 13) {
                if (session.Subject == '' || session.Grade == '' || session.Division == '')
                    alert('Enter all the mandatory fields');
                else
                    $scope.addSubjectGradeDivision();
            }
        }

        $scope.addSubjectGradeDivision = function () {

            if ($scope.selectedObject.EmployeeLastClassSubject == undefined)
                $scope.selectedObject.EmployeeLastClassSubject = [];

            $scope.selectedObject.EmployeeLastClassSubject.push(new NewEmployeeLastClassSubject());

        }

        $scope.addKinship = function () {
            if ($scope.selectedObject.EmployeeKinship == undefined)
                $scope.selectedObject.EmployeeKinship = [];

            $scope.selectedObject.EmployeeKinship.push(new NewEmployeeKinship());
        }

        $scope.addReference = function () {

            if ($scope.selectedObject.EmployeeMarketingReference == undefined)
                $scope.selectedObject.EmployeeMarketingReference = [];

            $scope.selectedObject.EmployeeMarketingReference.push(new NewSession());
        }

        $scope.deleteMarketingReferenceCode = function (obj) {

            $scope.selectedObject.EmployeeMarketingReference.splice(obj, 1);
        }

        var NewSession = function () {
            this.CompanyCode = $scope.selectedObject.CompanyCode;
            this.BranchCode = (($scope.selectedObject.BranchCode == undefined) ? '' : $scope.selectedObject.BranchCode);
            this.SessionCode = (($scope.selectedObject.SessionCode == undefined) ? '' : $scope.selectedObject.SessionCode);
            this.EmployeeNo = 0;
            this.Remarks = '';
        }

        var NewEmployeeLastClassSubject = function () {
            this.CompanyCode = $scope.selectedObject.CompanyCode;
            this.BranchCode = (($scope.selectedObject.BranchCode == undefined) ? '' : $scope.selectedObject.BranchCode);
            this.SessionCode = (($scope.selectedObject.SessionCode == undefined) ? '' : $scope.selectedObject.SessionCode);
            this.EmployeeNo = 0;
            this.SrNo = 0;
            this.Subject = '';
            this.Grade = '';
            this.Division = '';
        }

        var NewEmployeeKinship = function () {
            this.CompanyCode = $scope.selectedObject.CompanyCode;
            this.BranchCode = (($scope.selectedObject.BranchCode == undefined) ? '' : $scope.selectedObject.BranchCode);
            this.SessionCode = (($scope.selectedObject.SessionCode == undefined) ? '' : $scope.selectedObject.SessionCode);
            this.EmployeeNo = 0;
            this.KinshipType = '';
            this.KinshipRelationship = '';
            this.KinshipReferenceNo = 0;
            this.Discount = 0;
        }
        var onGetAllMarketingReference = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listMarketingReference = response.data;
        }


        var onGetCompany = function (response) {
            hideAllShield();
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
        }
        var onListComplete = function (response) {
            hideAllShield();
            if (response.data == null || response.data == undefined)
                listData = true;
            //$scope.listDataEx = response.data;
            $scope.listData = response.data;
        }

        var onGetAllBranch = function (response) {
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listBranch = response.data;
        }

        var onGetAllNationality = function (response) {

            $scope.listAllNationality = response.data;
        }
        var onGetAllReligion = function (response) {

            $scope.listAllReligions = response.data;
        }
        var onGetAllSession = function (response) {
            if (response.data == null || response.data == undefined) listData = true;
            $scope.listSession = response.data;
        }

        var onGetAllClass = function (response) {
            if (response.data == null || response.data == undefined) listData = true;
            $scope.listClass = response.data;
        }
        var onGetAllCourse = function (response) {
            $scope.listCompanyCourse = response.data;
        }

        var onGetAllClassCourseSubjects = function (response) {
            $scope.listAllClassCourseSubjects = response.data;
        }

        var onGetAllSubject = function (response) {
            $scope.listCompanySubjects = response.data;
        }

        var onGetAllKinshipDiscount = function (response) {

            $scope.listAllKinshipDiscount = response.data;
            $scope.listAllKinshipDiscountCopy = angular.copy(response.data);

        }
        var onSaveComplete = function (response) {
            hideAllShield();
            $scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            //$scope.listDataEx = response.data;

            var selectedCompany = $scope.selectedObject.CompanyCode;
            var selectedBranch = $scope.selectedObject.BranchCode;

            $scope.clear(); //clear on add new mode
            $scope.selectedObject.CompanyCode = selectedCompany;
            $scope.selectedObject.BranchCode = selectedBranch;
            //$scope.BranchCode_Change();
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.apply();
        }

        var onDelete = function (response) {
            hideAllShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            $scope.clear();
            $scope.saveError = 'Record deleted successfully';
        }

        $scope.isValid = function () {
            return ((myForm.EmployeeName.value == '')
            || ($scope.selectedObject.Gender.value == '') || (myForm.FatherName.value == '') || ($scope.selectedObject.MaritalStatus.value == '')
                 || (myForm.DateOfBirth.value == '') || (myForm.CNIC.value == '') || (myForm.CNICValidityDate.value == '')
                );
        }

        var onRequestError = function (reason) {
            hideAllShield();
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;
        }

        var onListComplete_getAllEmployee = function (reason) {
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
            if (obj != null) {

                $http.post("/UI/loadEmployee", { 'CompanyCode': obj.CompanyCode, 'EmployeeCode': obj.EmployeeCode })
                    .success(function (response) {

                        if (typeof response == 'undefined' || response.length == 0) return;

                        $scope.selectedObject = angular.copy(response)[0];

                        var mydateofbirth = parseInt($scope.selectedObject.DateOfBirth.replace('/Date(', '').replace(')/', ''));
                        $scope.selectedObject.DateOfBirth = $filter('date')(new Date(mydateofbirth), 'dd/MM/y');

                        if ($scope.selectedObject.PassportValidityDate != null) {
                            var myPassportValidityDate = parseInt($scope.selectedObject.PassportValidityDate.replace('/Date(', '').replace(')/', ''));
                            $scope.selectedObject.PassportValidityDate = $filter('date')(new Date(myPassportValidityDate), 'dd/MM/y');
                        }

                        if ($scope.selectedObject.CNICValidityDate != null) {
                            var myCNICValidityDate = parseInt($scope.selectedObject.CNICValidityDate.replace('/Date(', '').replace(')/', ''));
                            $scope.selectedObject.CNICValidityDate = $filter('date')(new Date(myCNICValidityDate), 'dd/MM/y');
                        }


                        if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                        $scope.isEditMode = true;
                    })
                    .error(function (reason) {
                        alert(reason);
                    });
            }
            $scope.saveError = '';
        }

        $scope.clear = function () {
            $scope.selectedObject = {};
            $scope.dummy = { images: '', Signature: '' };
            $scope.saveError = '';
            $scope.listError = '';
            $scope.isEditMode = false;

            $scope.addSubjectGradeDivision();
            $scope.addKinship();
            $scope.addReference();

            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
        }
        $scope.save = function () {
            $scope.saveError = '';
            var img = $scope.dummy.images;
            var signature = $scope.dummy.Signature;
            if ($scope.selectedObject.EmployeeMarketingReference != undefined) {
                for (var i = 0; i < $scope.selectedObject.EmployeeMarketingReference.length; i++) {
                    var obj = $scope.selectedObject.EmployeeMarketingReference[i];
                    if ($scope.selectedObject.EmployeeMarketingReference[i].MarketingReferenceCode == undefined || $scope.selectedObject.EmployeeMarketingReference[i].MarketingReferenceCode == '') {
                        $scope.selectedObject.EmployeeMarketingReference.splice(i, 1);
                        i--;
                    }
                    //else {
                    //    SrNo = SrNo + 1;
                    //    obj.SrNo = SrNo
                    //}
                }
            }
            var SrNo = 0;
            if ($scope.selectedObject.EmployeeLastClassSubject != undefined) {
                for (var i = 0; i < $scope.selectedObject.EmployeeLastClassSubject.length; i++) {
                    var obj = $scope.selectedObject.EmployeeLastClassSubject[i];
                    if ($scope.selectedObject.EmployeeLastClassSubject[i].Subject == '' || $scope.selectedObject.EmployeeLastClassSubject[i].Grade == ''
                         || $scope.selectedObject.EmployeeLastClassSubject[i].Division == '') {
                        $scope.selectedObject.EmployeeLastClassSubject.splice(i, 1);
                        i--;
                    }
                    else {
                        SrNo = SrNo + 1;
                        obj.SrNo = SrNo
                    }
                }
            }

            if (!angular.isUndefined($scope.selectedObject.DateOfBirth) && $scope.selectedObject.DateOfBirth != null)
                $scope.selectedObject.DateOfBirth = dateParser($scope.selectedObject.DateOfBirth);
            if (!angular.isUndefined($scope.selectedObject.CNICValidityDate) && $scope.selectedObject.CNICValidityDate != null)
                $scope.selectedObject.CNICValidityDate = dateParser($scope.selectedObject.CNICValidityDate);
            if (!angular.isUndefined($scope.selectedObject.PassportValidityDate) && $scope.selectedObject.PassportValidityDate != null && $scope.selectedObject.PassportValidityDate != '')
                $scope.selectedObject.PassportValidityDate = dateParser($scope.selectedObject.PassportValidityDate);

            if (typeof $scope.selectedObject != 'undefined')
                $scope.selectedObject.FullName = $scope.selectedObject.FirstName + ' ' + ($scope.selectedObject.MiddleName == undefined ? '' : $scope.selectedObject.MiddleName) + ' ' + ($scope.selectedObject.LastName == undefined ? '' : $scope.selectedObject.LastName)
            showSaveShield();
            $http.post("/UI/saveEmployee", { Employee: $scope.selectedObject,'isNew':$scope.isEditMode ? false:true, imgEmployeePhoneFile: img, imgSignature: signature }).then(onSaveComplete, onRequestError)
        }
        $scope.delete = function () {
            $scope.saveError = '';
            if (confirm('Are you sure you want to delete this record?')) {
                showDeleteShield();
                $http.post("/UI/deleteEmployee", $scope.selectedObject).then(onDelete, onRequestError)
            }
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

        $scope.isEditMode = false;
        $scope.clear();
        $scope.dummy = { images: '',Signature:'' };
        $scope.selectedObject = { Gender: 'Male', MaritalStatus: 'Unmarried' }
        $scope.saveError = '';
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        $http.post("/UI/getAllNationality", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllNationality, onRequestError);
        $http.post("/UI/getAllReligion", { CompanyCode: $scope.selectedObject.CompanyCode }).then(onGetAllReligion, onRequestError);


        

    }

    //app.directive('dateFix', function () {
    //    return {
    //        restrict: 'A',
    //        require: 'ngModel',
    //        link: function (scope, element, attr, ngModel) {
    //            element.on('change', function () {
    //                scope.$apply(function () {
    //                    ngModel.$setViewValue(element.val());
    //                });
    //            });
    //        }
    //    };
    //});

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

    app.controller("employeeController", admController);
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