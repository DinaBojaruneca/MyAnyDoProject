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
        $http.get("WebService.asmx/GetSubTask")
           .then(function (response) {
               $scope.subtasks = response.data;
           });
        $http.get("WebService.asmx/GetNote")
           .then(function (response) {
               $scope.notes = response.data;
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
        })
            .success(function () {
            loadData();
        });
        $scope.mode = "categ";
        
    };

    //delete category from database
    $scope.DeleteCategory = function () {
        $http({
            method: 'POST',
            url: 'WebService.asmx/DeleteCategory',
            data: { id: $scope.CatId },
            headers: { 'content-type': 'application/json' }
        })
        .success(function () {
            loadData();
        });
        $scope.mode = "categ";        
    };

    //delete Task from database
    $scope.DeleteTask = function () {
        $http({
            method: 'POST',
            url: 'WebService.asmx/DeleteTask',
            data: { id: $scope.TaskId },
            headers: { 'content-type': 'application/json' }
        })
        .success(function () {
            loadData();
        });
       
        $scope.mode = "taskView";
        $scope.viewe = "list";
        
    };

    $scope.CatName;
    $scope.CatId;
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

    $scope.TaskId;
    $scope.TaskName;
    $scope.TimeId;
    $scope.HighPriority;
    $scope.SubTaskName;

    $scope.SetTaskAndMode = function (id, name, modeVal) {
        $scope.TaskId = id;
        $scope.TaskName = name;
        $scope.mode = modeVal;
    }

    $scope.SetTimePriorMode = function (timeId, Hp, modeValue) {
        $scope.TimeId=timeId;
        $scope.HighPriority=Hp;
        $scope.mode = modeValue;
       
    }

    // insert Task to database
    $scope.InsertTask = function () {
        $http({
            method: 'POST',
            url: 'WebService.asmx/InsertTask',
            data: { 'name': $scope.TaskName, 'categoryId': $scope.CatId, 'timeId': $scope.TimeId, 'highPri': $scope.HighPriority },
            headers: { 'content-type': 'application/json' }
        })
            .success(function () {
                loadData();
            });
        $scope.mode = "taskView";
        $scope.TaskName = "";
    }

    //insert Subtask
    $scope.InsertSubTask = function () {
        $http({
            method: 'POST',
            url: 'WebService.asmx/InsertSubTask',
            data: { 'name': $scope.SubTaskName, 'taskId': $scope.TaskId},
            headers: { 'content-type': 'application/json' }
        })
            .success(function () {
                loadData();
            });
        $scope.mode = "subTaskView";
        $scope.SubTaskName = "";
    }

    $scope.Tveiwe = "subTask";
    $scope.SetTveiwe = function (value) {
        $scope.Tveiwe = value;
    }

    //filter subtasks by task Id
    $scope.FilterSubTasks = function (subtask) {
        return subtask.TaskId == $scope.TaskId;
    }

    //filter notes by task Id
    $scope.FilterNotes = function (note) {
        return note.TaskId == $scope.TaskId;
    }
    
});