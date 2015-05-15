(function () {
    'use strict';

    var app = angular.module('app');
    app.controller('EmailGroupEditCtrl', ["emailGroupResource", "$stateParams","$state", emailGroupEditCtrl]);

    function emailGroupEditCtrl(emailGroupResource, $stateParams,$state) {
        /* jshint validthis:true */
        var vm = this;
        vm.message = "";
        var emailGroupId = $stateParams.emailGroupId;

        if (emailGroupId == 0) {
            vm.title = "New Email Group";
        } else {
            emailGroupResource.get({ id: emailGroupId }, function (data) {
                vm.emailGroup = data;
                vm.title = "Email group " + vm.emailGroup.name;
            });
        }

        vm.submit = function () {
            console.log(vm.emailGroup);
            emailGroupResource.save(vm.emailGroup, function () {
                vm.message = "Saved";
            });
        };

        vm.cancel = function (editForm) {
            editForm.$setPristine();
            vm.emailGroup = {};
            vm.message = "";
            $state.go('emailGroupList');
        };
    }
})();
