angular.module("MainModule", [])
    .controller("MainCtrl", function ($scope, $http) {
        $scope.title = "Customers";

        $scope.idChange = function () {
            if ($scope.customer.Id !== undefined) {
                $http({
                    method: 'GET',
                    url: '../api/Action/' + $scope.customer.Id
                }).then(function (response) {
                    $scope.customer.Name = response.data.Name;
                    $scope.customer.Age = response.data.Age;
                }, function (error) {
                    $scope.getResponse = error.data.Message;
                });
            }
        }

        $scope.getCustomers = function () {
            $http({
                method: 'GET',
                url: '../api/Action'
            }).then(function (response) {
                $scope.getResponse = response.data;
            }, function (error) {
                $scope.getResponse = error.data.Message;
            });
        }

        $scope.getCustomer = function () {
            $http({
                method: 'GET',
                url: '../api/Action/' + $scope.id
            }).then(function (response) {
                $scope.getResponse = response.data;
            }, function (error) {
                $scope.getResponse = error.data.Message;
            });
        }

        $scope.updateCustomer = function () {
            $http({
                method: 'POST',
                url: '../api/Action',
                data: $scope.customer
            }).then(function (response) {
                $scope.getResponse = response.data;
            }, function (error) {
                $scope.getResponse = error.data.Message;
            });
        }

        $scope.addCustomer = function () {
            $http({
                method: 'PUT',
                url: '../api/Action',
                data: $scope.newCustomer
            }).then(function (response) {
                $scope.getResponse = response.data;
            }, function (error) {
                $scope.getResponse = error.data.Message;
            })
        }        
    })