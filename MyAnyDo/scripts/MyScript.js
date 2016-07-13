/// <reference path="C:\Users\dinab\Desktop\MyAnyDo\MyAnyDo\scripts/angular.min.js" />

var myAnyDoApp = angular.module("myAnyDoApp", []);
myAnyDoApp.controller("myAppCtrl", function ($scope, $http) {
    loadData();

    //read data from database
    function loadData() {
        $http.get("WebService.asmx/GetCategory")
       .then(function (response) {
           $scope.categories = response.data;
       });
        $http.get("WebService.asmx/GetTask")
        .then(function (response) {
            $scope.tasks = response.data;
        });
    };

    $scope.mode = "categ";

    $scope.SetModeValue = function (value) {
        $scope.mode = value;        
    }
    
    //insert category to database
    $scope.insertCategory = function () {
        $http({
            method: 'POST',
            url: 'WebService.asmx/InsertCategory',
            data: { name: $scope.CategoryName },
            headers: { 'content-type': 'application/json' }
        });
        loadData();
        $scope.mode = "categ";
    };

});