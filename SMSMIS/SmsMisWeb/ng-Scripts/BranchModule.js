(function () {
    var app = angular.module("adminModule1", []);
    var comController = function ($scope, $http, $filter) {

        $scope.$watch('selectedObject.BranchName', function (newValue, oldValue) { if (newValue.length > 75) $scope.selectedObject.CompanyName = oldValue; });
        $scope.$watch('selectedObject.ShortName', function (newValue, oldValue) { if (newValue.length > 10) $scope.selectedObject.ShortName = oldValue; });
        $scope.$watch('selectedObject.Salogan', function (newValue, oldValue) { if (newValue.length > 50) $scope.selectedObject.Salogan = oldValue; });
        $scope.$watch('selectedObject.Address', function (newValue, oldValue) { if (newValue.length > 255) $scope.selectedObject.Address = oldValue; });
        $scope.$watch('selectedObject.Phone1', function (newValue, oldValue) { if (newValue.length > 25) $scope.selectedObject.Phone1 = oldValue; });
        $scope.$watch('selectedObject.Phone2', function (newValue, oldValue) { if (newValue.length > 25) $scope.selectedObject.Phone2 = oldValue; });
        $scope.$watch('selectedObject.Phone3', function (newValue, oldValue) { if (newValue.length > 25) $scope.selectedObject.Phone3 = oldValue; });
        $scope.$watch('selectedObject.Phone4', function (newValue, oldValue) { if (newValue.length > 25) $scope.selectedObject.Phone4 = oldValue; });
        $scope.$watch('selectedObject.Fax1', function (newValue, oldValue) { if (newValue.length > 25) $scope.selectedObject.Fax1 = oldValue; });
        $scope.$watch('selectedObject.Fax2', function (newValue, oldValue) { if (newValue.length > 25) $scope.selectedObject.Fax2 = oldValue; });
        $scope.$watch('selectedObject.URL', function (newValue, oldValue) { if (newValue.length > 50) $scope.selectedObject.URL = oldValue; });
        $scope.$watch('selectedObject.Eamil1', function (newValue, oldValue) { if (newValue.length > 50) $scope.selectedObject.Eamil1 = oldValue; });
        $scope.$watch('selectedObject.Email2', function (newValue, oldValue) { if (newValue.length > 50) $scope.selectedObject.Email2 = oldValue; });
        $scope.$watch('selectedObject.STRNo', function (newValue, oldValue) { if (newValue.length > 25) $scope.selectedObject.STRNo = oldValue; });
        $scope.$watch('selectedObject.NTN', function (newValue, oldValue) { if (newValue.length > 25) $scope.selectedObject.NTN = oldValue; });


        var onGetCompany = function (response) {
            if (response.data == null || response.data == undefined)
                listCompany = true;
            $scope.listCompany = response.data;
            hideShield();
        }

        function createUUID() {
            // http://www.ietf.org/rfc/rfc4122.txt
            var s = [];
            var hexDigits = "0123456789abcdef";
            for (var i = 0; i < 36; i++) {
                s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
            }
            s[14] = "4";  // bits 12-15 of the time_hi_and_version field to 0010
            s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);  // bits 6-7 of the clock_seq_hi_and_reserved to 01
            s[8] = s[13] = s[18] = s[23] = "-";

            var uuid = s.join("");
            return uuid;
        }
        $scope.getImage = function () {
            return $scope.selectedObject.LogoPath + '?' + createUUID();
        }


        var onListComplete = function (response) {
            if (response.data == null || response.data == undefined)
                listData = true;
            $scope.listData = response.data;
            $scope.listDataEx = response.data;
            hideShield();
        }

        $scope.CompanyCode_Change = function () {
            //showShield();
            var vCompanyId = $scope.selectedObject.CompanyCode
            $scope.listData = $filter('filter')($scope.listDataEx, { 'CompanyCode': $scope.selectedObject.CompanyCode });
            $scope.selectedObject = {};
            $scope.selectedObject.Status = true;
            $scope.selectedObject.CompanyCode = vCompanyId;
            $scope.addContact();
            hideShield();
        }

        var onSaveComplete = function (response) {
            hideShield();
            $scope.saveError = 'Save Record successfully';
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.listDataEx = response.data;

            var selectedCompany = $scope.selectedObject.CompanyCode;

            $scope.clear(); //clear on add new mode
            $scope.selectedObject.CompanyCode = selectedCompany;
            $scope.CompanyCode_Change();
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            removeClass();
            $scope.apply();
        }

        var onDelete = function (response) {
            hideShield();
            if (response.data == "null") { delete $scope.listData; $scope.clear(); return; }
            $scope.listData = response.data;
            $scope.listDataEx = response.data;
            var selectedCompany = $scope.selectedObject.CompanyCode;

            $scope.clear(); //clear on add new mode
            $scope.selectedObject.CompanyCode = selectedCompany;
            $scope.CompanyCode_Change();
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
            //$scope.saveError = 'Record deleted successfully';
        }

        $scope.addContact = function () {

            if ($scope.selectedObject.BranchContactPersonList == undefined)
                $scope.selectedObject.BranchContactPersonList = [];

            $scope.selectedObject.BranchContactPersonList.push(new NewContact());
        }

        $scope.deleteContact = function (contact) {
            if (confirm('Are you sure, you want to delete this contact detail?'))
                $scope.selectedObject.BranchContactPersonList.splice($scope.selectedObject.BranchContactPersonList.indexOf(contact), 1);
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
            return ((myForm.BranchName.value == '') || (myForm.ShortName.value == '')
            || (myForm.Salogan.value == '') || (myForm.Address.value == '') || (myForm.Phone1.value == '') || (myForm.email1.value == '') || (myForm.email2.value == '')
            || ($scope.selectedObject.Eamil1 == undefined ? true : false)
            || ($scope.selectedObject.Status == undefined ? true : false)
            || (myForm.companyCode.value == '')

                );
        }

        var onRequestError = function (reason) {
            hideShield();
            alert(reason);
            if (typeof reason.data != 'undefined' && typeof reason.data.error != 'undefined')
                $scope.listError = reason.data.error;
            else
                $scope.listError = reason.status + ': ' + reason.statusText;

            alert('error');

            removeClass();
        }
        $scope.load = function (obj) {
            showShield();
            if (obj != null) {
                $scope.selectedObject = angular.copy(obj);

                if (typeof $scope.selectedObject.BranchContactPersonList != 'undefined' || $scope.selectedObject.BranchContactPersonList.length == 0) {
                    $scope.addContact();
                }

                if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
                $scope.isEditMode = true;
            }
            $scope.saveError = '';
            hideShield();
        }
        $scope.clear = function () {
            $scope.selectedObject = {};
            $scope.dummy = { images: '' };
            $scope.saveError = '';
            $scope.listError = '';
            $scope.isEditMode = false;
            $scope.selectedObject.Status = true;
            $scope.addContact();
            if (window.focusOnFirstAvailableControl) focusOnFirstAvailableControl();
        }
        $scope.save = function () {
            showShield();
            $scope.saveError = '';

            var SrNo = 0;

            if ($scope.selectedObject.BranchContactPersonList != undefined) {
                for (var i = 0; i < $scope.selectedObject.BranchContactPersonList.length; i++) {
                    var obj = $scope.selectedObject.BranchContactPersonList[i];

                    if (!EMAIL_REGEXP.test(obj.Email) && obj.Email != '') {
                        alert('First, please enter the Contact Email address in a good format!');
                        return false;
                    }



                    if ($scope.selectedObject.BranchContactPersonList[i].ContactPerson == '') {
                        $scope.selectedObject.BranchContactPersonList.splice(i, 1);
                        i--;
                    }
                    else {
                        SrNo = SrNo + 1;
                        obj.SrNo = SrNo
                    }
                }
            }

            var img = $scope.dummy.images;

            $http.post("/UI/saveBranch", { branch: $scope.selectedObject, imgFile: img }).then(onSaveComplete, onRequestError)
        }
        $scope.delete = function () {
            $scope.saveError = '';

            var SrNo = 0;

            if ($scope.selectedObject.BranchContactPersonList != undefined) {
                for (var i = 0; i < $scope.selectedObject.BranchContactPersonList.length; i++) {
                    var obj = $scope.selectedObject.BranchContactPersonList[i];
                    if ($scope.selectedObject.BranchContactPersonList[i].ContactPerson == '') {
                        $scope.selectedObject.BranchContactPersonList.splice(i, 1);
                        i--;
                    }
                    else {
                        SrNo = SrNo + 1;
                        obj.SrNo = SrNo
                    }
                }
            }


            if (confirm('Are you sure you want to delete this record?'))
                $http.post("/UI/deleteBranch", $scope.selectedObject).then(onDelete, onRequestError)
        }

        $scope.handleKeyEvent = function (e, contact) {
            if (e.keyCode == 13) {
                if (contact.ContactPerson == '' || contact.LandLine == '' || contact.Cell == '' || contact.Email == '')
                    alert('Enter all the mandatory fields');
                else
                    $scope.addContact();
            }
        }
        var EMAIL_REGEXP = /^[_a-z0-9]+(\.[_a-z0-9]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$/;


        $scope.isEditMode = false;
        $scope.clear();
        $scope.dummy = { images: '' };
        $scope.saveError = '';
        $http.get("/UI/getAllActiveCompanies").then(onGetCompany, onRequestError);
        $http.get("/UI/getAllBranches").then(onListComplete, onRequestError);
        //$scope.addContact();
        hideShield();
    }
    app.controller("branchController", comController);
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