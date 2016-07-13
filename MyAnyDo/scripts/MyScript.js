/// <reference path="C:\Users\dinab\Desktop\MyAnyDo\MyAnyDo\scripts/angular.min.js" />

var myAnyDoApp = angular.module("myAnyDoApp", []);
myAnyDoApp.controller("myAppCtrl", function ($scope, $http) {
    loadData();

    //read data from database
    function loadData() {
        $http.get("TaskService.asmx/GetCategory")
       .then(function (response) {
           $scope.categories = response.data;
       });
        $http.get("TaskService.asmx/GetTask")
        .then(function (response) {
            $skope.tasks = response.data;
        });
    };

    

});