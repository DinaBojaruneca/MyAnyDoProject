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
        $http.get("WebService.asmx/GetTaskByTime")
           .then(function (response) {
               $scope.times = response.data;
           });
    };

    $scope.mode = "categ";

    $scope.SetModeValue = function (value, vieweValue) {
        $scope.mode = value;
        $scope.viewe = vieweValue;
    }
    
    //insert category to database
    $scope.InsertCategory = function () {
        $http({
            method: 'POST',
            url: 'WebService.asmx/InsertCategory',
            data: { name: $scope.CategoryName },
            headers: { 'content-type': 'application/json' }
        });        
        $scope.mode = "categ";
        loadData();
    };

    //delete category from database
    $scope.DeleteCategory = function () {
        $http({
            method: 'POST',
            url: 'WebService.asmx/DeleteCategory',
            data: { id: $scope.CatId },
            headers: { 'content-type': 'application/json' }
        });       
        $scope.mode = "categ";
        loadData();
    };

    $scope.CatName = 0;
    $scope.CatId = 0;

    $scope.SetCatAndMode = function (id, name, modeValue) {
        $scope.CatId = id;
        $scope.CatName = name;
        $scope.mode = modeValue;
    }

    $scope.viewe = "time";
    $scope.SetVieweValue = function (value) {
        $scope.viewe = value;
    }

    //filter by category Id
    $scope.FilterTasks = function (task) {
        return task.CategoryId == $scope.CatId;
    }


});