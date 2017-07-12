angular.module("MainModule", [])
    .controller("MainCtrl", function ($scope, $http) {
        $scope.title = "MainTitle";
        
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
                data: 'Test data from POST method',
                url: '../api/Action'
            }).then(function (response) {
                $scope.getResponse = response.data;
            }, function (error) {
                $scope.getResponse = error.data.Message;
            });
        }
    })