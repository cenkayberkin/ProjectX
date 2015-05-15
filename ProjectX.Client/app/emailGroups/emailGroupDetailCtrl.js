(function () {
    'use strict';

    var app = angular.module('app');
    app.controller('EmailGroupDetailCtrl', ["emailGroupResource", "$stateParams", "dataService", emailGroupDetailCtrl]);

    function emailGroupDetailCtrl(emailGroupResource, $stateParams, dataService) {
        /* jshint validthis:true */
        var vm = this;

        var emailGroupId = $stateParams.emailGroupId;

        emailGroupResource.get({ id: emailGroupId }, function (data) {
            vm.emailGroup = data;
            vm.title = "Email group " + vm.emailGroup.name;
        });

        vm.emailDelete = function (selected) {
            //dataService.deleteEmail i cagir.
            vm.message = "Deleting ...";

            dataService.deleteEmailFromGroup(emailGroupId, selected.id)
                    .then(function (data) {
                        vm.emailGroup.emails = _.filter(vm.emailGroup.emails, function (item) {
                        
                            return item.id != selected.id;
                        });
                        vm.message = data.address + " is removed ";
                        
                    })
                    .catch(function () {
                        vm.message = "Problem occured";
                    });
        };
    }
})();
