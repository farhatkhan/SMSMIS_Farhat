function dateParser(val) {
    
    if (val instanceof Date && !isNaN(val.valueOf())) return val;
    if (val.indexOf('/') > -1)
    val = val.replace("/Date(", "").replace(")/", "");
    if (val.indexOf('/') < 0)
    {
        return  new Date(parseInt(val));
    }
    var valArr = val.split('/');
    var dt = new Date(parseInt(valArr[2]), parseInt(valArr[1]) - 1, parseInt(valArr[0]));
    return dt;
}
function toISOFormat(date) {
    date = dateParser(date);
    var strISO = date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
    return strISO;
}

//This method is used to formate date. date param must be a JSON object.
var formatDateFromJSONDate = function ($filter,date) {
    if (date != null && date.substring(0, 6) == "/Date(") {
        date = date.replace("/Date(", "").replace(")/", "");
        date = $filter('date')(date, "dd/MM/yyyy");
    }
    return date;
}
var formatToJSONDate = function ($filter, date) {
    if (date != null && date.substring(0, 6) == "/Date(") {
        date = date.replace("/Date(", "").replace(")/", "");
        date = $filter('date')(date, "yyyy/MM/dd");
    }
    return date;
}
var isnullorEmpty = function (val,field) {
    if (typeof val == 'undefined' || val == null || val == '' && val != 0)
        return true;
    return false;
}
var addDirectives = function (app) {
    app.directive('myDatepicker', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {

                if (typeof attrs.id != 'undefined') element.attr("id", attrs.id); //replacing id

                ngModelCtrl.$formatters.push(function (val) {
                    return val;

                });
                ngModelCtrl.$parsers.push(dateParser);
                $(function () {
                    $(element).datepicker({
                        dateFormat: 'dd/mm/yy',
                        onSelect: function (date) {
                            ngModelCtrl.$setViewValue(date);

                            scope.$apply();
                        }
                    });
                });

            }
        }
    });
    app.directive('numbersOnly', ['$filter', function ($filter) {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, modelCtrl) {
                modelCtrl.$parsers.push(function (inputValue) {
                    // this next if is necessary for when using ng-required on your input. 
                    // In such cases, when a letter is typed first, this parser will be called
                    // again, and the 2nd time, the value will be undefined
                    if (inputValue == undefined) return ''
                    var transformedInput = inputValue.replace(/[^0-9]/g, '');
                    if (transformedInput != inputValue) {
                        modelCtrl.$setViewValue(transformedInput);
                        modelCtrl.$render();
                    }

                    return transformedInput;
                });
            }
        };
    }]);
    app.directive('dicimalNumbers', ['$filter', function ($filter) {
        return {
            require: 'ngModel',
            link: function (elem, $scope, attrs, ngModel) {
                $(elem).on("focus", function () { $(this).select() });
                ngModel.$formatters.push(function (val) {
                    if (val == null) return '';
                    var newVal = $filter('currency')(val, '', 0);
                    if (newVal.substring(newVal.length - 3) == '.00')
                        newVal = newVal.substring(0, newVal.length - 3);
                    return newVal;
                });
                ngModel.$parsers.push(function (val) {
                    return val.replace(/[\$,]/g, '')
                });
            }
        }
    }]);
    
    
}