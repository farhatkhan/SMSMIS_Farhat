var appBo = angular.module('appBo', []);

appBo.controller('CountryController', function ($scope) {
    $scope.predicate = 'name';

    $scope.sort = function (predicate) {
        $scope.predicate = predicate;
    }

    $scope.isSorted = function (predicate) {
        return ($scope.predicate == predicate)
    }

    $scope.countries = [
    { name: 'China', population2014: 1355692576 },
    { name: 'India', population2014: 1236344631 },
    { name: 'United States', population2014: 318892103 },
    { name: 'Indonesia', population2014: 253609643 },
    { name: 'Brazil', population2014: 202656788 },
    { name: 'Pakistan', population2014: 196174380 },
    { name: 'Nigeria', population2014: 177155754 },
    { name: 'Bangladesh', population2014: 166280712 },
    { name: 'Russia', population2014: 142470272 },
    { name: 'Japan', population2014: 127103388 }
    ]
});