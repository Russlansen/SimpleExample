angular.module("MainModule", [])
    .controller("MainCtrl", function ($scope, $http) {
        $scope.title = "MainTitle";
        $scope.customers = [{ name: "Ivan", age: "22" },
                            { name: "Oleg", age: "30" },
                            { name: "Andrew", age: "32" },
                            { name: "Roman", age: "23" }];

        $scope.getData = function () {
            $http({
                method: 'GET',
                url: '../api/Action'
            }).then(function (response) {
                $scope.getResponse = response.data;
                $scope.isEnumerable = false;
            }, function (error) {
                $scope.getResponse = error.data.Message;
                $scope.isEnumerable = false;
            });
        }

        $scope.postData = function () {
            $http({
                method: 'POST',
                data: $scope.customers,
                url: '../api/Action'
            }).then(function (response) {
                $scope.getResponse = response.data;
                $scope.isEnumerable = true;
            }, function (error) {
                $scope.getResponse = error.data.Message;
                $scope.isEnumerable = false;
            });
        }

        $scope.postSimpleData = function () {
            $http({
                method: 'POST',
                url: '../api/Action/' + $scope.simpleData
            }).then(function (response) {
                $scope.getResponse = response.data;
                $scope.isEnumerable = false;
            }, function (error) {
                $scope.getResponse = error.data.Message;
                $scope.isEnumerable = false;
            });
        }
    })