﻿/// <reference path="C:\Users\dinab\Desktop\MyAnyDo\MyAnyDo\scripts/angular.min.js" />

var myAnyDoApp = angular.module("myAnyDoApp", []);
myAnyDoApp.controller("myAppCtrl", function ($scope, $http, $filter) {
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
        $scope.viewe = "time";
        $scope.CategoryName = "";
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
    $scope.Note;
    $scope.CreationDate;
    //dateAsString = $filter('date')($scope.CreationDate, "yyyy-MM-dd");

    $scope.SetTaskAndMode = function (id, name, modeVal, highPr, crDate) {
        $scope.TaskId = id;
        $scope.TaskName = name;
        $scope.mode = modeVal;
        $scope.HighPriority = highPr;
        $scope.CreationDate = crDate;
    }

    $scope.CheckHighPrior = function () {
        return $scope.HighPriority == 'True';
    }

    $scope.ChangePriority = function () {
        if ($scope.HighPriority == 'True') {
            $http({
                method: 'POST',
                url: 'WebService.asmx/SetAsHighPriority',
                data: { 'id': $scope.TaskId, 'value':'0' },
                headers: { 'content-type': 'application/json' }
            })
            .success(function () {
                loadData();
            });
        }
        else {
            $http({
                method: 'POST',
                url: 'WebService.asmx/SetAsHighPriority',
                data: { 'id': $scope.TaskId, 'value': '1' },
                headers: { 'content-type': 'application/json' }
            })
            .success(function () {
                loadData();
            });
        }
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

    //insert Note
    $scope.InsertNote = function () {
        $http({
            method: 'POST',
            url: 'WebService.asmx/InsertNote',
            data: { 'name': $scope.Note, 'taskId': $scope.TaskId },
            headers: { 'content-type': 'application/json' }
        })
            .success(function () {
                loadData();
            });
        $scope.mode = "subTaskView";
        $scope.Note = "";
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
    
    //delete Subtask
    $scope.DeleteSubtask = function (Id) {
        $http({
            method: 'POST',
            url: 'WebService.asmx/DeleteSubTask',
            data: { id: Id },
            headers: { 'content-type': 'application/json' }
        })
        .success(function () {
            loadData();
        });
        $scope.mode = "subTaskView";        
    }

    //delete Note
    $scope.DeleteNote = function (Id) {
        $http({
            method: 'POST',
            url: 'WebService.asmx/DeleteNote',
            data: { id: Id },
            headers: { 'content-type': 'application/json' }
        })
        .success(function () {
            loadData();
        });
        $scope.mode = "subTaskView";
    }


});