(function () {
    'use strict';

    var app = angular.module('app');
    app.controller('EmailGroupListCtrl', ["emailGroupResource", emailGroupListCtrl]);

    function emailGroupListCtrl(emailGroupResource) {
        /* jshint validthis:true */
        var vm = this;

        emailGroupResource.query(function (data) {
            vm.emailGroups = data;
        });

        vm.title = 'EmailGroupListCtrl';
      
        vm.groupDelete = function (id) {
            vm.message = "Deleting ...";
            emailGroupResource.delete({ id: id }, function (data) {
                vm.message = "Email group " + data.name + " deleted";
                vm.emailGroups = _.filter(vm.emailGroups, function (item) {

                    return item.id != id;
                });

            }, function (error) {
                vm.message = "Could not delete";
            });

        };
    }
})();
