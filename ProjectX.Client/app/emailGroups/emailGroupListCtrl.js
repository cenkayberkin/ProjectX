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

    }
})();
