angular.module("MainModule", [])
    .controller("MainCtrl", function ($scope, $http) {
        $scope.title = "Customers";
        $scope.showMessage = false;

        $scope.idChange = function () {
            if ($scope.customer.Id !== undefined) {
                $http({
                    method: 'GET',
                    url: '../api/Action/' + $scope.customer.Id
                }).then(function (response) {
                    $scope.showMessage = false;
                    $scope.customer.Name = response.data[0].Name;
                    $scope.customer.Age = response.data[0].Age;
                }, function (error) {
                    $scope.showMessage = true;
                    $scope.message = error.data.Message;
                });
            }
        }

        $scope.getCustomers = function () {
            $http({
                method: 'GET',
                url: '../api/Action'
            }).then(function (response) {
                $scope.showMessage = false;
                $scope.getResponse = response.data;
            }, function (error) {
                $scope.showMessage = true;
                $scope.message = error.data.Message;
            });
        }

        $scope.getCustomer = function () {
            $http({
                method: 'GET',
                url: '../api/Action/' + $scope.id
            }).then(function (response) {
                $scope.showMessage = false;
                $scope.getResponse = response.data;
            }, function (error) {
                $scope.showMessage = true;
                $scope.message = error.data.Message;
            });
        }

        $scope.updateCustomer = function () {
            $http({
                method: 'POST',
                url: '../api/Action',
                data: $scope.customer
            }).then(function (response) {
                $scope.getCustomers();
                $scope.message = "User updated";
            }, function (error) {
                $scope.message = error.data.Message;
            });
            $scope.showMessage = true;
        }

        $scope.addCustomer = function () {
            $http({
                method: 'PUT',
                url: '../api/Action',
                data: $scope.newCustomer
            }).then(function (response) {
                $scope.getCustomers();
                $scope.message = "User created";
            }, function (error) {
                $scope.getResponse = error.data.Message;
            });
            $scope.showMessage = true;
        } 

        $scope.deleteCustomer = function () {
            $http({
                method: 'DELETE',
                url: '../api/Action/' + $scope.idDelete,
            }).then(function (response) {
                $scope.getCustomers();
                $scope.message = "User deleted";    
            }, function (error) {
                $scope.message = error.data.Message;
            });
            $scope.showMessage = true;
        }
        $scope.getCustomers();
    })