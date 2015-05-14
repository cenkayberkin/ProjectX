(function () {
    'use strict';

    var app = angular.module('app');
    app.controller('EmailGroupDetailCtrl', ["emailGroupResource","$stateParams", emailGroupDetailCtrl]);

    function emailGroupDetailCtrl(emailGroupResource, $stateParams) {
        /* jshint validthis:true */
        var vm = this;

        var emailGroupId = $stateParams.emailGroupId;

        emailGroupResource.get({ id: emailGroupId }, function (data) {
            vm.emailGroup = data;
            vm.title = "Email group " + vm.emailGroup.name;
        });
    }
})();
