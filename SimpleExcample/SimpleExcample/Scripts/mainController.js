angular.module("MainModule", [])
    .controller("MainCtrl", function ($scope, $http) {
        $scope.title = "MainTitle";

        $scope.customer = { name : "Ivan", age : "22"};

        $scope.getData = function () {
            $http({
                method: 'GET',
                url: '../api/Action'
            }).then(function (response) {
                $scope.getResponse = response.data;
            }, function (error) {
                $scope.getResponse = error.data.Message;
            });
        }

        $scope.postData = function () {
            $http({
                method: 'POST',
                data: $scope.customer,
                url: '../api/Action'
            }).then(function (response) {
                $scope.getResponse = "Name: " + response.data.Name + ", Age: " + response.data.Age;
            }, function (error) {
                $scope.getResponse = error.data.Message;
            });
        }
    })