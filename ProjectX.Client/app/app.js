(function () {
    'use strict';

    var app = angular.module('app', ["common.services", "ui.router"]);

    app.config(["$stateProvider", "$urlRouterProvider", function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/");

        $stateProvider
            .state("home", {
                url: "/",
                templateUrl: "app/welcomeView.html"
            })
            .state("emailGroupList", {
                url: "/emailgroups",
                templateUrl: "app/emailGroups/emailGroupListView.html",
                controller: "EmailGroupListCtrl as vm"
            })
            .state("emailGroupDetails", {
                url: "/emailgroups/:emailGroupId",
                templateUrl: "app/emailGroups/emailGroupDetailView.html",
                controller: "EmailGroupDetailCtrl as vm"
            })
            .state("emailGroupEdit", {
                url: "/emailgroups/edit/:emailGroupId",
                templateUrl: "app/emailGroups/emailGroupEditView.html",
                controller: "EmailGroupEditCtrl as vm"
            })
            .state("emailGroupMerge", {
                url: "/emailGroupMerge",
                templateUrl: "app/emailGroups/emailGroupMergeView.html",
                controller: "EmailGroupMergeCtrl as vm"
            });
        
    }]);
})();